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
    /// Filters podcasts by using <see cref="IFilter"/>s and XPaths. Non-generic base class for easiert storage in a list.
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

        protected static XPathPodcastFilter WithFilters(IEnumerable<IFilter> filters)
        {
            var filter = new PodcastTitleFilter
            {
                Filters = filters.ToList()
            };
            return filter;
        }

        protected static XPathPodcastFilter WithFilter(IFilter filter)
        {
            return WithFilters(new IFilter[] {filter});
        }
        
        private void CreateNamespaceManager(Dictionary<string, string> namespaces)
        {
            _namespaceManager = new XmlNamespaceManager(new NameTable());
            _namespaceManager.AddNamespace("itunes", ItunesNamespace);
            _namespaceManager.AddNamespace("atom", AtomNamespace);
            
            if(namespaces != null)
                foreach (var pair in namespaces)
                    _namespaceManager.AddNamespace(pair.Key, pair.Value);
        }

        public override XDocument Filter(XDocument podcast)
        {
            return FilterWithCustomFilters(podcast, Filters);
        }

        public override XDocument FilterWithCustomFilters(XDocument podcast, IEnumerable<IFilter> filters)
        {
            ValidateIFilterTypeMatchesContent(filters);
            var itemsToRemove = GetItemsToRemove(podcast, filters);
            foreach (var item in itemsToRemove)
                item.Parent.Remove();
            return podcast;
        }

        public XDocument FilterWithCustomFilter(XDocument podcast, IFilter filter)
        {
            return FilterWithCustomFilters(podcast, new List<IFilter>() {filter});
        }
       
        private IEnumerable<XElement> GetItemsToRemove(XDocument podcast, IEnumerable<IFilter> filters)
        {
            var match = podcast.XPathEvaluate("//item/title");
            var matchingElements = podcast.XPathSelectElements("//item/title", _namespaceManager);
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
