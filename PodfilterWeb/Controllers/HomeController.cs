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
        private List<BasePodcastModification> _currentModifications = new List<BasePodcastModification>();

        /// <summary>
        /// Index view displays the main ui for creating new podcast urls.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/addFilter")]
        public IActionResult AddFilter([FromForm] string filterType)
        {
            // in case the select box hint was submitted
            if(string.IsNullOrWhiteSpace(filterType))
                return View();



            return View();
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