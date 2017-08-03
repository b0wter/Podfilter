using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Podfilter.Helpers;
using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastFilters
{
    /// <summary>
    /// Filters podcasts by using <see cref="IContentFilter"/>s and XPaths. Non-generic base class for easiert storage in a list.
    /// </summary>
    public abstract class XPathPodcastFilter : PodcastFilter
    {       
        private readonly XpathPodcastElementProvider _xpathPodcastElementProvider = new XpathPodcastElementProvider(string.Empty);
        
        public abstract string XPath { get; }

        protected static XPathPodcastFilter WithFilters<T>(IEnumerable<IContentFilter> filters) where T : XPathPodcastFilter, new()
        {
            var filter = new T
            {
                Filters = filters.ToList()
            };
            return filter;
        }

        protected static XPathPodcastFilter WithFilter<T>(IContentFilter filter) where T : XPathPodcastFilter, new()
        {
            return WithFilters<T>(new IContentFilter[] {filter});
        }
        
        public override XDocument Filter(XDocument podcast)
        {
            return FilterWithCustomFilters(podcast, Filters);
        }

        public override XDocument FilterWithCustomFilters(XDocument podcast, IEnumerable<IContentFilter> filters)
        {
            ValidateIFilterTypeMatchesContent(filters);
            var itemsToRemove = GetItemsToRemove(podcast, filters);
            foreach (var item in itemsToRemove)
                item.Parent.Remove();
            return podcast;
        }

        public XDocument FilterWithCustomFilter(XDocument podcast, IContentFilter filter)
        {
            return FilterWithCustomFilters(podcast, new List<IContentFilter>() {filter});
        }
       
        private IEnumerable<XElement> GetItemsToRemove(XDocument podcast, IEnumerable<IContentFilter> filters)
        {
            var matchingElements = _xpathPodcastElementProvider.GetElements(podcast, XPath);
            var itemsToRemove = new List<XElement>();

            foreach(var element in matchingElements)
            {
                if (ElementPassesFilters(element.Value, filters) == false)
                    itemsToRemove.Add(element);
            }

            return itemsToRemove;
        }

        private bool ElementPassesFilters(string element, IEnumerable<IContentFilter> filters)
        {
            foreach (var filter in filters)
                if (filter.PassesFilter(element) == false)
                    return false;
            return true;
        }
    }
}
