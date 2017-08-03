using System;

namespace Podfilter.Models.ContentFilters
{
    public interface IContentFilter
    {
        // IFilter is a non-generic interface because instances with different types need to be stored in a single List<IFilter>.
        
        /// <summary>
        /// Tests if a given object passes this filter.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool PassesFilter(object obj);
        
        /// <summary>
        /// Type of content this filter works on. E.g. String, DateTime,...
        /// </summary>
        Type TargetType { get; }
    }
}