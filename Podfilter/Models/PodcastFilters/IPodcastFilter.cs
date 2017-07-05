using Podfilter.Models.ContentFilter;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastFilters
{
    public interface IPodcastFilter
    {
        /// <summary>
        /// Uses only <paramref name="filters"/> to filter the <paramref name="podcast"/>. Does not include default filters.
        /// </summary>
        /// <param name="podcast"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        XDocument FilterWithCustomFilters(XDocument podcast, IEnumerable<IContentFilter> filters);

        /// <summary>
        /// Uses the default set of filters to remove items from <paramref name="podcast"/>.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        XDocument Filter(XDocument podcast);

        /// <summary>
        /// Determines if the target types of the filters are valid for this kind of podcast filter.
        /// </summary>
        /// <param name="filters"></param>
        void ValidateIFilterTypeMatchesContent(IEnumerable<IContentFilter> filters);

        /// <summary>
        /// Human readable description of the filter.
        /// </summary>
        string Description { get; }
    }
}