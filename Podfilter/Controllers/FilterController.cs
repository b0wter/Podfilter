using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Podfilter.Helpers;
using Podfilter.Models;
using System.Collections.Generic;

namespace Podfilter.Controllers
{
    /// <summary>
    /// Does the actual filtering of the remote podcasts.
    /// </summary>
    [Route("api/[controller]")]
    public class FilterController : JsonApiBaseController
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
       public ActionResult HttpGet_FilterPodcast(
          [RequiredFromQuery]string url, 
          [FromQuery]int fromEpoch = int.MinValue, 
          [FromQuery]int toEpoch = int.MaxValue, 
          [FromQuery]bool removeDuplicateTitles = false,
          [FromQuery]string titleMustNotContain = null,
          [FromQuery]string titleMustContain = null,
          [FromQuery]int minDuration = int.MinValue,
          [FromQuery]int maxDuration = int.MaxValue)
       {
            var filters = new List<IPodcastFilter>(8);

            if (removeDuplicateTitles)
                filters.Add(new PodcastDuplicateEntriesFilter());

            if (titleMustContain != null)
                filters.Add(PodcastTitleFilter.WithContainsFilter(titleMustContain));

            if (titleMustNotContain != null)
                filters.Add(PodcastTitleFilter.WithDoesNotContainFilter(titleMustNotContain));


            return null;
       }
    }
}