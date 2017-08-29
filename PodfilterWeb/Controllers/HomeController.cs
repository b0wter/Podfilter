using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterWeb.Helpers;
using Microsoft.AspNetCore.Mvc;
using PodfilterCore.Models.PodcastModification;
using PodfilterWeb.Models;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml;

namespace PodfilterWeb.Controllers
{
    public class HomeController : HtmlBaseController
    {
        private const string PodcastUrlKey = "podcastUrl";
        private string PodcastUrl
        {
            get
            {
                if(HttpContext.Session.IsAvailable)
                {
                    var url = HttpContext.Session.Get<string>(PodcastUrlKey);
                    return url;
                }

                return string.Empty;
            }
            set
            {
                if(HttpContext.Session.IsAvailable)
                {
                    HttpContext.Session.Set<string>(PodcastUrlKey, value);
                }
            }
        }

        private const string PodcastModificationsKey = "podcastModifications";
        private const string SelectedFilterKey = "selectedFilter";
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
            ViewData["selectedFilter"] = GetSelectedFilterFromCache();
            ViewData["podcastUrl"] = PodcastUrl;
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

        private int GetSelectedFilterFromCache()
        {
            if(HttpContext.Session.IsAvailable)
            {
                var selection = HttpContext.Session.Get<int>(SelectedFilterKey);
                return selection;
            }

            return -1;
        }

        [HttpPost("/addFilter")]
        public IActionResult AddFilter([FromForm] string filterType, [FromForm] string[] newFilterArgument, [FromForm] string[] newFilterMethod, [FromForm] string urlInputField)
        {
            PodcastUrl = urlInputField;
            try
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
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = $"Could not add filter because: {ex.Message}";
            }

            return Redirect("/");
        }

        [HttpPost("/removeFilter")]
        public IActionResult RemoveFilter([FromForm] string urlInputField)
        {
            PodcastUrl = urlInputField;
            if(HttpContext.Session.IsAvailable)
            {
                int filterIndex = HttpContext.Session.Get<int>(SelectedFilterKey);
                var modifications = HttpContext.Session.Get<List<DisplayableBasePodcastModification>>(PodcastModificationsKey);
                if(filterIndex >= 0 && filterIndex < modifications.Count)
                {
                    modifications.RemoveAt(filterIndex);
                    HttpContext.Session.Set<List<DisplayableBasePodcastModification>>(PodcastModificationsKey, modifications);
                }
            }

            return Redirect("/");
        }

        [HttpGet("/selectFilter/{filterIndex}")]
        public IActionResult SelectFilter(int filterIndex)
        {
            if(HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.Set<int>(SelectedFilterKey, filterIndex);
            }
            return Redirect("/");
        }

        [HttpPost("/create")]
        public IActionResult CreatePodcastUrl([FromForm] string urlInputField)
        {
            PodcastUrl = urlInputField;
            if(string.IsNullOrWhiteSpace(urlInputField))
                TempData["errorMessage"] = "You need to enter a podcast url.";

            var modifications = GetSessionModificationsFromCache();
            if (modifications.Count <= 0)
                TempData["warningMessage"] = "You have not added any filters.";
            else
            {
                var baseUrl = GetBaseUrl();
                var queryParameters = $"{baseUrl}/api/filter?{modifications.Select(mod => mod.ToQueryString()).Aggregate((a, b) => $"{a}&{b}")}";
                var encodedUrl = System.Net.WebUtility.UrlEncode(urlInputField);
                queryParameters += $"&url={encodedUrl}";

                TempData["filteredPodcastUrl"] = queryParameters;
            }

            return Redirect("/");
        }

        [HttpPost("/validate")]
        public async Task<IActionResult> ValidatePodcastUrl([FromForm] string urlInputField)
        {
            try
            {
                await ValidateRemoteUrlIsXmlDocument(urlInputField);
                TempData["infoMessage"] = "Remote url is a valid XML document.";
            }
            catch(InvalidOperationException ex)
            {
                TempData["warningMessage"] = "You need to supply a valid url for the test.";
            }
            catch(HttpRequestException ex)
            {
                TempData["warningMessage"] = $"The given podcast url does not seem to be valid ({ex.GetType().Name}).";
            }
            catch(XmlException ex)
            {
                TempData["warningMessage"] = $"The remote url does not point to a valid XML document and is most likely not a podcast url!";
            }

            PodcastUrl = urlInputField;
            return Redirect("/");
        }

        private async Task ValidateRemoteUrlIsXmlDocument(string url)
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            var document = XDocument.Parse(content);
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

        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}