using PodfilterCore.Data;
using PodfilterCore.Helpers;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Models.PodcastModification.Actions;
using PodfilterCore.Models.PodcastModification.Filters;
using PodfilterCore.Models.PodcastModification.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodfilterCore.Models
{
    public class Core : BaseCore
    {
        private IHttpContentProvider<string> _podcastProvider;
        private IContentDeserializer<string> _podcastDeserializer;

        public Core(IHttpContentProvider<string> httpContentProvider, IContentDeserializer<string> contentDeserializer)
        {
            _podcastProvider = httpContentProvider;
            _podcastDeserializer = contentDeserializer;
        }

        public async override Task<XDocument> Modify(string url, IEnumerable<BasePodcastModification> modifications)
        {
            var podcast = await GetPodcastFromUrl(url);
            ApplyModifications(podcast, modifications);
            AddFilteredHintToPodcastTitle(podcast);
            return podcast;
        }

        /// <summary>
        /// Applies every given modification to the podcast. The modifications are performed in place.
        /// </summary>
        /// <param name="podcast"></param>
        /// <param name="modifications"></param>
        private void ApplyModifications(XDocument podcast, IEnumerable<BasePodcastModification> modifications)
        {
            foreach (var mod in modifications)
                mod.Modify(podcast);
        }

        /// <summary>
        /// Adds a default hint to the podcast title.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        private XDocument AddFilteredHintToPodcastTitle(XDocument podcast)
        {
            var mod = new AddStringToTitleModification(null, " (filtered)");
            mod.Modify(podcast);
            return podcast;
        }

        /// <summary>
        /// Retrieves the contents of a podcast from a remote url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<XDocument> GetPodcastFromUrl(string url)
        {
            var content = await GetStringFromUrl(url);
            var document = ReadStringAsXDocument(content);
            return document;
        }

        /// <summary>
        /// Retrieves the string content from a remote url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> GetStringFromUrl(string url)
        {
            var result = await _podcastProvider.LoadStringFromUrl(url, _podcastDeserializer);
            result.ResponseMessage.EnsureSuccessStatusCode();

            return result.Content;
        }

        /// <summary>
        /// Interpretes a string as an <see cref="XDocument"/>.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private XDocument ReadStringAsXDocument(string content)
        {
            return XDocument.Parse(content);
        }
    }
}
