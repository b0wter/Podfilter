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
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.WebUtilities;
using PodfilterCore.Models;
using PodfilterCore.Data;

namespace PodfilterWeb.Controllers
{
    public class HomeController : HtmlBaseController
    {
        private const string PodcastUrlKey = "podcastUrl";
        /// <summary>
        /// Property encapsulating the contents of the "podcastUrl"-value in the current session.
        /// </summary>
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

        // Handles the session management of the modifications. Cannot be implemented as a property because of the constant (de)serialization which breaks the handlers.
        private const string PodcastModificationsKey = "podcastModifications";
        private void SetPodcastModifications(List<DisplayableBasePodcastModification> mods)
        {
            if (HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.Set<List<DisplayableBasePodcastModification>>(PodcastModificationsKey, mods);
            }
        }
        private List<DisplayableBasePodcastModification> GetPodcastModifications()
        {
            if(HttpContext.Session.IsAvailable && HttpContext.Session.Keys.Contains(PodcastModificationsKey))
            {
                // Since the list is deserialized it never returns null, only an empty list.
                var cachedModifications = HttpContext.Session.Get<List<DisplayableBasePodcastModification>>(PodcastModificationsKey);
                return cachedModifications;
            }
            else
            {
                return new List<DisplayableBasePodcastModification>();
            }
        }
        
        private const string SelectedFilterKey = "selectedFilter";
        /// <summary>
        /// Encapsulates the contents of the "selectedFilter"-value in the current session.
        /// </summary>
        private int SelectedFilterIndex
        {
            get
            {
                if (HttpContext.Session.IsAvailable)
                {
                    var selection = HttpContext.Session.Get<int>(SelectedFilterKey);
                    return selection;
                }

                return -1;
            }
            set
            {
                if (HttpContext.Session.IsAvailable)
                {
                    HttpContext.Session.Set<int>(SelectedFilterKey, value);
                }
            }
        }

        private BaseModificationMethodTranslator _methodTranslator;
        private BaseStringCompressor _stringCompressor;
        private ISavedPodcastRepository _savedPodcastProvider;

        public HomeController(BaseModificationMethodTranslator methodTranslator, BaseStringCompressor stringCompressor, ISavedPodcastRepository savedPodcastProvider)
        {
            _methodTranslator = methodTranslator;
            _stringCompressor = stringCompressor;
            _savedPodcastProvider = savedPodcastProvider;
        }

        /// <summary>
        /// Index view displays the main ui for creating new podcast urls.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // store filter information
            var filters = GetPodcastModifications();
            ViewData["currentFilters"] = filters;
            ViewData["currentFilterCount"] = filters.Count;
            ViewData["methodTranslator"] = _methodTranslator;
            ViewData["selectedFilter"] = SelectedFilterIndex;

            ViewData["podcastUrl"] = PodcastUrl;
            return View();
        }

        /// <summary>
        /// Is called from the website if the user wants to add a new filter.
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="newFilterArgument"></param>
        /// <param name="newFilterMethod"></param>
        /// <param name="urlInputField"></param>
        /// <returns></returns>
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
                    var existingModifications = GetPodcastModifications();
                    existingModifications.Add(modification);
                    SetPodcastModifications(existingModifications);
                }
                else
                {
                    TempData["warningMessage"] = "To add a filter please select a filter from the selection box.";
                }
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = $"Could not add filter because: {ex.Message}";
            }

            return Redirect("/");
        }

        /// <summary>
        /// Is called from the website if the user wants to remove a filter.
        /// </summary>
        /// <param name="urlInputField"></param>
        /// <returns></returns>
        [HttpPost("/removeFilter")]
        public IActionResult RemoveFilter([FromForm] string urlInputField)
        {
            PodcastUrl = urlInputField;
            if(HttpContext.Session.IsAvailable)
            {
                int filterIndex = SelectedFilterIndex;

                var modifications = GetPodcastModifications();
                if(filterIndex >= 0 && filterIndex < modifications.Count)
                {
                    modifications.RemoveAt(filterIndex);
                    SetPodcastModifications(modifications);
                    SelectedFilterIndex--;
                }
                else
                {
                    TempData["warningMessage"] = "There is no filter to remove.";
                }
            }

            return Redirect("/");
        }

        /// <summary>
        /// Called from the website if the user selected another filter.
        /// </summary>
        /// <param name="filterIndex"></param>
        /// <returns></returns>
        [HttpGet("/selectFilter/{filterIndex}")]
        public IActionResult SelectFilter(int filterIndex)
        {
            if(HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.Set<int>(SelectedFilterKey, filterIndex);
            }
            return Redirect("/");
        }

        /// <summary>
        /// Called from the website if the user wants to create a new podcast url.
        /// </summary>
        /// <param name="urlInputField"></param>
        /// <returns></returns>
        [HttpPost("/create")]
        public IActionResult CreatePodcastUrl([FromForm] string urlInputField)
        {
            PodcastUrl = urlInputField;
            if (string.IsNullOrWhiteSpace(urlInputField))
            {
                TempData["errorMessage"] = "You need to enter a podcast url.";
                return Redirect("/");
            }

            var modifications = GetPodcastModifications();
            if (modifications.Count <= 0)
                TempData["warningMessage"] = "You have not added any filters.";
            else
            {
                CreateAndAddPermanentPodfilterUrl(urlInputField);
                CreateAndAddDbPodfilterUrl(urlInputField);
            }

            return Redirect("/");
        }

        /// <summary>
        /// Creates an url that is permanently valid because it contains serialized filter information.!--
        /// These urls are pretty long (easily 500+ chars) which limits their compatibility.
        /// Also adds the new url to TempData.
        /// </summary>
        private void CreateAndAddPermanentPodfilterUrl(string podcastUrl)
        {
            var baseUrl = GetBaseUrl();
            var serializedArgument = GetUrlEncodedSerializedArgument(podcastUrl);
            var filteredPodcastUrl = $"{baseUrl}/api/filter?argument={serializedArgument}";
            TempData["filteredPodcastUrl"] = filteredPodcastUrl;
            // trigger the display of a hint if the ui if the resulting url is too long
            // although the rfcs allow longer urls they are not properly supported by the browsers
            TempData["filteredPodcastUrlTooLong"] = filteredPodcastUrl.Length > 2000;
        }

        private string GetUrlEncodedSerializedArgument(string url)
        {
            var modifications = GetPodcastModifications();
            var argument = new PodfilterUrlArgument(url, modifications);
            var serializedArgument = JsonConvert.SerializeObject(argument);
            var encodedSerializedArgument = _stringCompressor.CompressAndBase64UrlEncodeUnicode(serializedArgument);
            return encodedSerializedArgument;
        }

        private void CreateAndAddDbPodfilterUrl(string podcastUrl)
        {
            var savedPodcast = new SavedPodcast
            {
                LastUpdated = DateTime.Now,
                LastUsed = DateTime.Now,
                Modifications = GetPodcastModifications().Select(x => x.Modification).ToList(),
                Url = podcastUrl
            };

            try{
                var persistedPodcast = _savedPodcastProvider.Persist(savedPodcast);

                var baseUrl = GetBaseUrl();
                TempData["savedPodcastId"] = persistedPodcast.Id;
                TempData["savedPodcastUrl"] = $"{baseUrl}/api/filter/{persistedPodcast.Id}";
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = $"There was a problem ({ex}). Please try again. If the problem persists contat the administrator.";
            }
        }

        /// <summary>
        /// Called from the website if the user wnats to validate a remote url returns a valid xml document.
        /// </summary>
        /// <param name="urlInputField"></param>
        /// <returns></returns>
        [HttpPost("/validate")]
        public async Task<IActionResult> ValidatePodcastUrl([FromForm] string urlInputField)
        {
            try
            {
                await ValidateRemoteUrlIsXmlDocument(urlInputField);
                TempData["infoMessage"] = "Remote url is a valid XML document.";
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException) 
            {
                TempData["warningMessage"] = "You need to supply a valid url for the test.";
            }
            catch(HttpRequestException ex)
            {
                TempData["warningMessage"] = $"The given podcast url does not seem to be valid ({ex.GetType().Name}).";
            }
            catch(XmlException)
            {
                TempData["warningMessage"] = $"The remote url does not point to a valid XML document and is most likely not a podcast url!";
            }
            catch(Exception)
            {
                TempData["warningManager"] = "An unknown error has occured. Please check your input and send a feedback email!";
            }

            PodcastUrl = urlInputField;
            return Redirect("/");
        }

        /// <summary>
        /// Retrieves the remote content of an url and checks it it's a valid xml document.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task ValidateRemoteUrlIsXmlDocument(string url)
        {
            //TODO: Replace HttpClient with interface!
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            var document = XDocument.Parse(content);
        }

        /// <summary>
        /// Renders the about page.
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Renders the contact page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Renders a generic error page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Dynamically creates an instance of a subclass of <see cref="DisplayableBasePodcastModification"/> using the given typename and arguments.
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="argument"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private DisplayableBasePodcastModification CreateModificationFromArguments(string filterType, string argument, string method)
        {
            var typeAsString = $"PodfilterWeb.Models.Displayable{filterType}, PodfilterWeb";
            var modification = (DisplayableBasePodcastModification)Activator.CreateInstance(Type.GetType(typeAsString), new object[] { argument, method });
            return modification;
        }

        /// <summary>
        /// Returns the first non-null string from <paramref name="arguments"/>.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string GetArgumentFromArgumentsArray(string[] arguments)
        {
            if(arguments.Count(s => !string.IsNullOrWhiteSpace(s)) != 1)
                throw new ArgumentException("Arguments array needs to contain exactly one value.");

            return arguments.First(argument => string.IsNullOrWhiteSpace(argument) == false);
        }

        /// <summary>
        /// Returns the element of <paramref name="methods"/> whose index matches that of the first non-null string in <paramref name="arguments"/>.
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="methods"></param>
        /// <returns></returns>
        private string GetFilterMethodFromArgumentsArray(string[] arguments, string[] methods)
        {
            int i = 0;
            var method = methods[arguments.Select(a => new Tuple<int, string>(i++, a)).First(b => string.IsNullOrWhiteSpace(b.Item2) == false).Item1];
            return method;
        }

        /// <summary>
        /// Retrieves the base url of this web application.
        /// </summary>
        /// <returns></returns>
        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
