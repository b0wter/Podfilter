using Podfilter.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models
{
    /// <summary>
    /// Performs the filtering of podcasts.
    /// </summary>
    public abstract class PodcastFilter : IPodcastFilter
    {
        protected static string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        protected static string AtomNamespace = "http://www.w3.org/2005/Atom";

        /// <summary>
        /// Filters that are used with the <see cref="Filter(XDocument)"/> method.
        /// </summary>
        public List<IFilter> Filters { get; set; } = new List<IFilter>();

        /// <summary>
        /// Uses only <paramref name="filters"/> to filter the <paramref name="podcast"/>. Does not include default filters.
        /// </summary>
        /// <param name="podcast"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public abstract XDocument FilterWithCustomFilters(XDocument podcast, IEnumerable<IFilter> filters);

        /// <summary>
        /// Uses the default set of filters to remove items from <paramref name="podcast"/>.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        public abstract XDocument Filter(XDocument podcast);

        /// <summary>
        /// Determines if the target types of the filters are valid for this kind of podcast filter.
        /// </summary>
        /// <param name="filters"></param>
        public abstract void ValidateIFilterTypeMatchesContent(IEnumerable<IFilter> filters);
    }
}