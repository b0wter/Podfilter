using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using PodfilterCore.Models.PodcastModification;
using PodfilterWeb.Models;

namespace PodfilterWeb.Controllers
{
    public class HomeController : HtmlBaseController
    {
        private const string PodcastUrlKey = "podcastUrl";
        private const string PodcastModificationsKey = "podcastModifications";
        private BaseModificationMethodTranslator _methodTranslator;

        public HomeController(BaseModificationMethodTranslator methodTranslator)
        {
            _methodTranslator = methodTranslator;
        }

        /// <summary>
        /// Index view displays the main ui for creating new podcast urls.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["currentFilters"] = GetSessionModificationsFromCache();
            return View();
        }

        private List<DisplayableBasePodcastModification> GetSessionModificationsFromCache()
        {
            if (HttpContext.Session.IsAvailable)
            {
                var cachedModifications = HttpContext.Session.Get<List<DisplayableBasePodcastModification>>(PodcastModificationsKey);
                if (cachedModifications == null || cachedModifications.Count == 0)
                    return new List<DisplayableBasePodcastModification>();
                else
                    return cachedModifications;
            }
            else
            {
                return new List<DisplayableBasePodcastModification>();
            }
        }

        [HttpPost("/addFilter")]
        public IActionResult AddFilter([FromForm] string filterType, [FromForm] string[] newFilterArgument, [FromForm] string[] newFilterMethod)
        {
            if (!string.IsNullOrWhiteSpace(filterType))
            {
                var modificationArgument = GetArgumentFromArgumentsArray(newFilterArgument);
                var filterMethod = GetFilterMethodFromArgumentsArray(newFilterArgument, newFilterMethod);
                var modification = CreateModificationFromArguments(filterType, modificationArgument, filterMethod);
                var existingFilters = HttpContext.Session.Get<List<DisplayableBasePodcastModification>>(PodcastModificationsKey) ?? new List<DisplayableBasePodcastModification>();
                existingFilters.Add(modification);
                HttpContext.Session.Set<List<DisplayableBasePodcastModification>>(PodcastModificationsKey, existingFilters);
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

        private DisplayableBasePodcastModification CreateModificationFromArguments(string filterType, string argument, string method)
        {
            var typeAsString = $"PodfilterWeb.Models.Displayable{filterType}, PodfilterWeb";
            var modification = (DisplayableBasePodcastModification)Activator.CreateInstance(Type.GetType(typeAsString), new object[] { argument, method });
            return modification;
        }

        private string GetArgumentFromArgumentsArray(string[] arguments)
        {
            if(arguments.Count(s => !string.IsNullOrWhiteSpace(s)) != 1)
                throw new ArgumentException("Arguments array needs to contain exactly one value.");

            return arguments.First(argument => string.IsNullOrWhiteSpace(argument) == false);
        }

        private string GetFilterMethodFromArgumentsArray(string[] arguments, string[] methods)
        {
            int i = 0;
            var method = methods[arguments.Select(a => new Tuple<int, string>(i++, a)).First(b => string.IsNullOrWhiteSpace(b.Item2) == false).Item1];
            return method;
        }
    }
}