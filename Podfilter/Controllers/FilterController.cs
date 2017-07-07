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
using Podfilter.Models.PodcastFilters;
using Podfilter.Models.PodcastActions;

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
		/// <param name="removeDuplicateTitles">removes any items with a title that has been used previously (keeping the oldest item)</param>
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
			[FromQuery] bool removeDuplicateTitles = false,
			[FromQuery] string titleMustNotContain = null,
			[FromQuery] string titleMustContain = null,
			[FromQuery] int minDuration = int.MinValue,
			[FromQuery] int maxDuration = int.MaxValue)
		{
			var filters = CreateFiltersFromArguments(fromEpoch, toEpoch, removeDuplicateTitles, titleMustNotContain, titleMustContain, minDuration, maxDuration);
			var podcast = await GetPodcastFromUrl(url);
			var filteredPodcast = filters.FilterPodcast(podcast);
            filteredPodcast = AddFilteredHintToPodcastTitle(filteredPodcast);
			var serializedFilteredPodcast = filteredPodcast.ToStringWithDeclaration();

			var mediaType = MediaTypeHeaderValue.Parse("application/xml");
			var content = Content(serializedFilteredPodcast, mediaType);

			return content;
		}

		private PodcastFilterCollection CreateFiltersFromArguments(long fromEpoch, long toEpoch, bool removeDuplicateTitles, string titleMustNotContain, string titleMustContain, int minDuration, int maxDuration)
		{
			var filters = new List<IPodcastFilter>(8);

			if (removeDuplicateTitles)
				filters.Add(new PodcastDuplicateEntriesFilter());

			//TODO: Change filters in a way that makes the extra type unnecessary.
			if (titleMustContain != null)
				filters.Add(PodcastStringPropertyFilter.WithContainsFilter<PodcastTitleFilter>(titleMustContain));

			if (titleMustNotContain != null)
				filters.Add(PodcastStringPropertyFilter.WithDoesNotContainFilter<PodcastTitleFilter>(titleMustNotContain));

			if (minDuration != int.MinValue || maxDuration != int.MaxValue)
				filters.Add(PodcastDurationFilter.WithMinMaxDurationFilter(minDuration, maxDuration));

            if (fromEpoch != long.MinValue || toEpoch != long.MaxValue)
                filters.Add(PodcastPublicationDateFilter.WithEarlierAndLaterFilter(fromEpoch, toEpoch));

			return new PodcastFilterCollection(filters);
		}

        private XDocument AddFilteredHintToPodcastTitle(XDocument podcast)
        {
            var action = AddStringToTitlePodcastAction.WithSuffixAction(" (filtered)");
            podcast = action.PerformAction(podcast);
            return podcast;
        }

		private async Task<XDocument> GetPodcastFromUrl(string url)
		{
			var content = await GetStringFromUrl(url);
			var document = ReadStringAsXDocument(content);
			return document;
		}

        private async Task<string> GetStringFromUrl(string url)
        {
	        var httpProvider = new HttpContentProvider<string>();
	        var result = await httpProvider.LoadStringFromUrl(url, new StringContentDeserializer());

	        result.ResponseMessage.EnsureSuccessStatusCode();

	        return result.Content;
        }

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