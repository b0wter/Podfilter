using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using PodfilterCore.Models;
using PodfilterCore.Data;
using PodfilterWeb.Helpers;
using System.Xml.Linq;
using System.Collections.Generic;
using PodfilterCore.Models.PodcastModification;
using Newtonsoft.Json;
using PodfilterWeb.Converters;
using PodfilterWeb.Models;
using System.Linq;

namespace PodfilterWeb.Controllers
{
	/// <summary>
	/// Does the actual filtering of the remote podcasts.
	/// </summary>
	[Route("api/[controller]")]
	public class FilterController : ApiBaseController
	{
		private BaseCore _core;

        public FilterController(BaseCore core)
        {
			_core = core;
        }

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
		[HttpGet("/simple")]
		public async Task<ActionResult> HttpGet_SimpleFilterPodcast(
			[RequiredFromQuery] string url,
			[FromQuery] long fromEpoch = long.MinValue,
			[FromQuery] long toEpoch = long.MaxValue,
			[FromQuery] bool removeDuplicateEpisodes = false,
			[FromQuery] string titleMustNotContain = null,
			[FromQuery] string titleMustContain = null,
			[FromQuery] int minDuration = int.MinValue,
			[FromQuery] int maxDuration = int.MaxValue,
            [FromQuery] string removeDuplicates = "")
		{
            var filteredPodcast = await _core.Modify(url, fromEpoch, toEpoch, removeDuplicateEpisodes, titleMustNotContain, titleMustContain, minDuration, maxDuration, removeDuplicates);
            var serializedFilteredPodcast = filteredPodcast.ToStringWithDeclaration();
			var content = CreateContentResultForSerializedPodcast(serializedFilteredPodcast);
            return content;
		}

		/// <summary>
		/// Prefered way of using the controller to filter a podcast.
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> HttpGet_FilterPodcast([RequiredFromQuery] string url, [RequiredFromQuery] string filters)
		{
			var displayableModifications = JsonConvert.DeserializeObject<List<DisplayableBasePodcastModification>>(
										filters, 
										new JsonSerializerSettings{ 
											Converters = new List<JsonConverter>{
												new DisplayableBaseModificationJsonConverter()
											}
										});
			
			var modifications = displayableModifications.Select(x => x.Modification);
			var serializedFilteredPodcast = await ModifyWithDefaultCore(url, modifications);
			var content = CreateContentResultForSerializedPodcast(serializedFilteredPodcast);
            return content;
		}

		/// <summary>
		/// Creates a ContentResult that contains the serialized podcast and sets the media type to xml.
		/// </summary>
		private ContentResult CreateContentResultForSerializedPodcast(string podcast)
		{
            var mediaType = MediaTypeHeaderValue.Parse("application/xml");
            var content = Content(podcast, mediaType);
            return content;
		}

		private async Task<string> ModifyWithDefaultCore(string url, IEnumerable<BasePodcastModification> modifications)
		{
			var filteredPodcast = await _core.Modify(url, modifications);
			var serializedFilteredPodcast = filteredPodcast.ToStringWithDeclaration();
			return serializedFilteredPodcast;
		}

        /// <summary>
        /// Retrieves a stored recipe from the database and filters
        /// </summary>
        /// <param name="podcastId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> HttpGet_SavedPodcast([RequiredFromQuery]string podcastId)
        {
            return null;
        }

		protected override ActionResult MakeResult(object obj)
		{
			//TODO: replace this with something nice
			throw new NotImplementedException();
		}
	}
}