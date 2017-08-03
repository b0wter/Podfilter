using System;
using System.Collections.Generic;
using System.Linq;
using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastFilters
{
    public class PodcastPublicationDateFilter : XPathPodcastFilter<DateTime>
    {
        public override string XPath => "//item/pubDate";

        public override string Description => "Filters items based on their publication date.";

        public static XPathPodcastFilter WithLaterThanFilter(long fromEpoch)
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.GreaterEquals, fromEpoch);
            return WithFilter<PodcastPublicationDateFilter>(filter);
        }

        public static XPathPodcastFilter WithEarlierThanFilter(long toEpoch)
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.SmallerEquals, toEpoch);
            return WithFilter<PodcastPublicationDateFilter>(filter);
        }

        public static XPathPodcastFilter WithEarlierAndLaterFilter(long fromEpoch, long toEpoch)
        {
            var filters = new List<IContentFilter>(2);
            
            if(fromEpoch != long.MinValue)
                filters.Add(new DateFilter(DateFilter.DateFilterMethods.GreaterEquals, fromEpoch));
            
            if(toEpoch != long.MaxValue)
                filters.Add(new DateFilter(DateFilter.DateFilterMethods.SmallerEquals, toEpoch));

            return WithFilters<PodcastPublicationDateFilter>(filters);
        }

        public override void ValidateIFilterTypeMatchesContent(IEnumerable<IContentFilter> filters)
        {
            var firstNonMatchingFilter = filters.FirstOrDefault(f => f.TargetType != typeof(long) && f.TargetType != typeof(DateTime));
            if(firstNonMatchingFilter != null)
                throw new ArgumentException($"Tried to use a filter that targets {firstNonMatchingFilter.TargetType} for long / DateTime.");
        }
    }
}