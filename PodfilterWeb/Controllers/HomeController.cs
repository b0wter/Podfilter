using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterWeb.Controllers
{
    public class HomeController : HtmlBaseController
    {
        private const string PodcastUrlKey = "podcastUrl";
        private const string PodcastModificationsKey = "podcastModifications";

        /// <summary>
        /// Index view displays the main ui for creating new podcast urls.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/addFilter")]
        public IActionResult AddFilter([FromForm] string filterType, [FromForm] string[] newFilterArgument, [FromForm] string newFilterMethod)
        {
            if(!string.IsNullOrWhiteSpace(filterType))
            {
                var modification = CreateModificationFromArguments(filterType, newFilterArgument, newFilterMethod);
                var existingFilters = HttpContext.Session.Get<List<BasePodcastModification>>(PodcastModificationsKey);
                existingFilters.Add(modification);
                HttpContext.Session.Set<List<BasePodcastModification>>(PodcastModificationsKey, existingFilters);
            }

            return Redirect("/");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private BasePodcastModification CreateModificationFromArguments(string filterType, string[] arguments, string method)
        {
            var argument = arguments.First();
            var modification = (BasePodcastModification)Activator.CreateInstance(Type.GetType($"{filterType}, PodfilterCore"), new object[] { method, argument });
            return modification;
        }
    }
}