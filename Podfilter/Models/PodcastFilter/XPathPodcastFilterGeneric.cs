using System;
using System.Collections.Generic;
using System.Linq;

namespace Podfilter.Models
{
    /// <summary>
    /// Generic version of <see cref="XPathPodcastFilter"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class XPathPodcastFilter<T> : XPathPodcastFilter
    {
        public override void ValidateIFilterTypeMatchesContent(IEnumerable<IFilter> filters)
        {
            var firstNonMatchingFilter = filters.FirstOrDefault(f => f.TargetType != typeof(T));
            if (firstNonMatchingFilter != null)
                throw new ArgumentException(
                    $"Tried to use a filter that targets {firstNonMatchingFilter.TargetType} for {typeof(T)}.");
        }
    }
}