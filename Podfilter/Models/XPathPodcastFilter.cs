using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Podfilter.Helpers;

namespace Podfilter.Models
{
    /// <summary>
    /// Filters podcasts by using <see cref="IFilter"/>s and XPaths.
    /// </summary>
    public abstract class XPathPodcastFilter : PodcastFilter
    {
        //public string XPath { get; set; }
        public abstract string XPath { get; }

        private XmlNamespaceManager _namespaceManager;

        public XPathPodcastFilter()
        {
            CreateNamespaceManager(null);
        }

        public XPathPodcastFilter(Dictionary<string, string> namespaces) : this()
        {
            CreateNamespaceManager(namespaces);
        }

        private void CreateNamespaceManager(Dictionary<string, string> namespaces)
        {
            _namespaceManager = new XmlNamespaceManager(new NameTable());
            _namespaceManager.AddNamespace("itunes", _itunesNamespace);
            _namespaceManager.AddNamespace("atom", _atomNamespace);
            
            if(namespaces != null)
                foreach (var pair in namespaces)
                    _namespaceManager.AddNamespace(pair.Key, pair.Value);
        }

        public XDocument FilterPodcast(XDocument podcast, IEnumerable<IFilter> filters)
        {
            var itemsToRemove = GetItemsToRemove(podcast, filters);
            foreach (var item in itemsToRemove)
                item.Remove();
            return podcast;
        }

        public XDocument FilterPodcast(XDocument podcast, IFilter filter)
        {
            return FilterPodcast(podcast, new List<IFilter>() {filter});
        }

        private IEnumerable<XElement> GetItemsToRemove(XDocument podcast, IEnumerable<IFilter> filters)
        {
            var matchingElements = podcast.XPathSelectElements("/", _namespaceManager);
            var itemsToRemove = new List<XElement>();

            foreach(var element in matchingElements)
            {
                if (ElementPassesFilters(element.Value, filters) == false)
                    itemsToRemove.Add(element);
            }

            return itemsToRemove;
        }

        private bool ElementPassesFilters(string element, IEnumerable<IFilter> filters)
        {
            foreach (var filter in filters)
                if (filter.PassesFilter(element) == false)
                    return false;
            return true;
        }
    }
}
