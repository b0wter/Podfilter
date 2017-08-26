using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult AddFilter([FromForm] string filterType, [FromForm] string argument, [FromForm] string method)
        {
            if(!string.IsNullOrWhiteSpace(filterType))
            {
                var modification = CreateModificationFromArguments(filterType, argument, method);
                HttpContext.Session.Set<BasePodcastModification>()

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
    }
}