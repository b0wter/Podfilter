using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Podfilter.Helpers;
using Podfilter.Models;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Net.Http.Headers;
using Podfilter.Models.PodcastModification;
using Podfilter.Models.PodcastModification.Filters;
using Podfilter.Models.PodcastModification.Actions;
using Podfilter.Models.PodcastModification.Others;

namespace Podfilter.Controllers
{
	/// <summary>
	/// Does the actual filtering of the remote podcasts.
	/// </summary>
	[Route("api/[controller]")]
	public class FilterController : ApiBaseController
	{
		/// <summary>
		/// Returns a short summary on how to use the api.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult HttpGet_ReturnHelp()
		{
			dynamic message = new ExpandoObject();
			message.Message = $"The api can only work if parameters are supplied.";
			return MakeResult(message);
		}

		/// <summary>
		/// Filters a podcast based on the given parameters.
		/// Since this is a GET options for filtering are limited.
		/// </summary>
		/// <remarks>
		/// POSTs would allow for a better defined filtering but rss feed requests
		/// are always GET requests.
		/// </remarks>
		/// <param name="url">url-encoded url of the podcast</param>
		/// <param name="fromEpoch">discard all items older then the epoch (unix timestamp)</param>
		/// <param name="toEpoch">discards all items newer then the epoch (unix timestamp)</param>
		/// <param name="removeDuplicateEpisodes">removes any items with a title that has been used previously (keeping the oldest item)</param>
		/// <param name="titleMustNotContain">removes any item if it contains at least one of the elements of this string (semicolon separated</param>
		/// <param name="titleMustContain">removes each item that does not contain at least one of the elements of this string (semicolon separated</param>
		/// <param name="minDuration">minimum duration of the postcast in seconds</param>
		/// <param name="maxDuration">maximum duration of the podcast in seconds</param>
		/// <returns>filtered podcast</returns>
		[HttpGet]
		public async Task<ActionResult> HttpGet_FilterPodcast(
			[RequiredFromQuery] string url,
			[FromQuery] long fromEpoch = long.MinValue,
			[FromQuery] long toEpoch = long.MaxValue,
			[FromQuery] bool removeDuplicateEpisodes = false,
			[FromQuery] string titleMustNotContain = null,
			[FromQuery] string titleMustContain = null,
			[FromQuery] int minDuration = int.MinValue,
			[FromQuery] int maxDuration = int.MaxValue)
		{
            var modifications = CreateModifications(fromEpoch, toEpoch, removeDuplicateEpisodes, titleMustNotContain, titleMustContain, minDuration, maxDuration);
            var podcast = await GetPodcastFromUrl(url);
            ApplyModifications(podcast, modifications);
            AddFilteredHintToPodcastTitle(podcast);

            var serializedFilteredPodcast = podcast.ToStringWithDeclaration();
            var mediaType = MediaTypeHeaderValue.Parse("application/xml");
            var content = Content(serializedFilteredPodcast, mediaType);

            return content;
		}

        private IEnumerable<BasePodcastModification> CreateModifications(long fromEpoch, long toEpoch, bool removeDuplicateEpisodes, string titleMustNotContain, string titleMustContain, int minDuration, int maxDuration)
        {
            var mods = new List<BasePodcastModification>();

            if (fromEpoch != long.MinValue)
                mods.Add(new EpisodePublishDateFilterModification(fromEpoch, Models.ContentFilters.DateFilter.DateFilterMethods.GreaterEquals));

            if (toEpoch != long.MaxValue)
                mods.Add(new EpisodePublishDateFilterModification(toEpoch, Models.ContentFilters.DateFilter.DateFilterMethods.SmallerEquals));

            if (removeDuplicateEpisodes)
                mods.Add(new RemoveDuplicateEpisodesModification());

            if (!string.IsNullOrEmpty(titleMustContain))
                mods.Add(new EpisodeTitleFilterModification(titleMustContain, Models.ContentFilters.StringFilter.StringFilterMethod.Contains));

            if (!string.IsNullOrEmpty(titleMustNotContain))
                mods.Add(new EpisodeTitleFilterModification(titleMustNotContain, Models.ContentFilters.StringFilter.StringFilterMethod.DoesNotContain));

            if (minDuration != int.MinValue)
                mods.Add(new EpisodeDurationFilterModification(Models.ContentFilters.DurationFilter.DurationFilterMethods.GreaterEquals, minDuration));

            if (maxDuration != int.MaxValue)
                mods.Add(new EpisodeDurationFilterModification(Models.ContentFilters.DurationFilter.DurationFilterMethods.SmallerEquals, maxDuration));

            return mods;
        }

        /// <summary>
        /// Applies every given modification to the podcast. The modifications are performed in place.
        /// </summary>
        /// <param name="podcast"></param>
        /// <param name="modifications"></param>
        private void ApplyModifications(XDocument podcast, IEnumerable<BasePodcastModification> modifications)
        {
            foreach (var mod in modifications)
                mod.Modify(podcast);
        }

        /// <summary>
        /// Adds a default hint to the podcast title.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        private XDocument AddFilteredHintToPodcastTitle(XDocument podcast)
        {
            var mod = new AddStringToTitleModification(null, " (filtered)");
            mod.Modify(podcast);
            return podcast;
        }

        /// <summary>
        /// Retrieves the contents of a podcast from a remote url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<XDocument> GetPodcastFromUrl(string url)
		{
			var content = await GetStringFromUrl(url);
			var document = ReadStringAsXDocument(content);
			return document;
		}

        /// <summary>
        /// Retrieves the string content from a remote url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> GetStringFromUrl(string url)
        {
	        var httpProvider = new HttpContentProvider<string>();
	        var result = await httpProvider.LoadStringFromUrl(url, new StringContentDeserializer());

	        result.ResponseMessage.EnsureSuccessStatusCode();

	        return result.Content;
        }

        /// <summary>
        /// Interpretes a string as an <see cref="XDocument"/>.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
		private XDocument ReadStringAsXDocument(string content)
		{
			return XDocument.Parse(content);
		}

		protected override ActionResult MakeResult(object obj)
		{
			//TODO: replace this with something nice
			throw new NotImplementedException();
		}
	}
}