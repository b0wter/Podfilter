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
        public ActionResult HttpGet_SavedPodcast([RequiredFromQuery]string podcastId)
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