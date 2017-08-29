using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using PodfilterCore.Models;
using PodfilterCore.Data;
using PodfilterWeb.Helpers;

namespace PodfilterWeb.Controllers
{
	/// <summary>
	/// Does the actual filtering of the remote podcasts.
	/// </summary>
	[Route("api/[controller]")]
	public class FilterController : ApiBaseController
	{
        private IHttpContentProvider<string> _podcastProvider;
        private IContentDeserializer<string> _podcastDeserializer;

        public FilterController(IHttpContentProvider<string> podcastProvider, IContentDeserializer<string> podcastDeserializer)
        {
            _podcastProvider = podcastProvider;
            _podcastDeserializer = podcastDeserializer;
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
		[HttpGet]
		public async Task<ActionResult> HttpGet_FilterPodcast(
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
            var podcastCore = new Core(_podcastProvider, _podcastDeserializer);
            var filteredPodcast = await podcastCore.Modify(url, fromEpoch, toEpoch, removeDuplicateEpisodes, titleMustNotContain, titleMustContain, minDuration, maxDuration, removeDuplicates);
            var serializedFilteredPodcast = filteredPodcast.ToStringWithDeclaration();

            var mediaType = MediaTypeHeaderValue.Parse("application/xml");
            var content = Content(serializedFilteredPodcast, mediaType);

            return content;
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