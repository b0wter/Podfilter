using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Podfilter.Models
{
    public class XPathPodcastFilter : IPodcastFilter
    {
        public string XPath { get; set; }

        private XmlNamespaceManager _namespaceManager;

        public XPathPodcastFilter(string xpath, Dictionary<string, string> namespaces)
        {
            this.XPath = xpath;
            CreateNamespaceManager(namespaces);
        }

        private void CreateNamespaceManager(Dictionary<string, string> namespaces)
        {
            _namespaceManager = new XmlNamespaceManager(new NameTable());
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
