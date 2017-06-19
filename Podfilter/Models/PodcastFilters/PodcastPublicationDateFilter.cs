using System;
using System.Collections.Generic;

namespace Podfilter.Models
{
    public class PodcastPublicationDateFilter : XPathPodcastFilter<DateTime>
    {
        public override string XPath => "//item/pubDate";

        public override string Description => "Filters items based on their publication date.";

        public static XPathPodcastFilter WithLaterThanFilter(long fromEpoch)
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.GreaterEquals, fromEpoch);
            return WithFilter(filter);
        }

        public static XPathPodcastFilter WithEarlierThanFilter(long toEpoch)
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.SmallerEquals, toEpoch);
            return WithFilter(filter);
        }

        public static XPathPodcastFilter WithEarlierAndLaterFilter(long fromEpoch, long toEpoch)
        {
            var filters = new List<IFilter>(2);
            
            if(fromEpoch != long.MinValue)
                filters.Add(new DateFilter(DateFilter.DateFilterMethods.GreaterEquals, fromEpoch));
            
            if(toEpoch != long.MaxValue)
                filters.Add(new DateFilter(DateFilter.DateFilterMethods.SmallerEquals, toEpoch));

            return WithFilters(filters);
        }
    }
}