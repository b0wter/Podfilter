using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastFilters
{
    public class PodcastDurationFilter : XPathPodcastFilter<TimeSpan>
    {
        public override string XPath => "//item/itunes:duration";

        public override string Description => "Filters podcast items based on their duration.";

        public static XPathPodcastFilter WithDurationFilter(TimeSpan duration, DurationFilter.DurationFilterMethods method)
        {
            var filter = new DurationFilter(method, duration);
            return WithFilter<PodcastDurationFilter>(filter);
        }

        public static XPathPodcastFilter WithMinMaxDurationFilter(long minSeconds, long maxSeconds)
        {
            var filters = new List<IContentFilter>(2);
            if(minSeconds != int.MinValue)
                filters.Add(new DurationFilter(DurationFilter.DurationFilterMethods.GreaterEquals, minSeconds));
            if(maxSeconds != int.MaxValue)
                filters.Add(new DurationFilter(DurationFilter.DurationFilterMethods.SmallerEquals, maxSeconds));

            return WithFilters<PodcastDurationFilter>(filters);
        }
        
        public static XPathPodcastFilter WithMinDurationFilter(TimeSpan duration)
        {
            var filter = new DurationFilter(DurationFilter.DurationFilterMethods.GreaterEquals, duration);
            return WithFilter<PodcastDurationFilter>(filter);
        }

        public static XPathPodcastFilter WithMinDurationFilter(int durationInSeconds)
        {
            var timespan = TimeSpan.FromSeconds(durationInSeconds);
            return WithMinDurationFilter(timespan);
        }

        public static XPathPodcastFilter WithMaxDurationFilter(TimeSpan duration)
        {
            var filter = new DurationFilter(DurationFilter.DurationFilterMethods.SmallerEquals, duration);
            return WithFilter<PodcastDurationFilter>(filter);
        }

        public static XPathPodcastFilter WithMaxDurationFilter(int durationInSeconds)
        {
            var timespan = TimeSpan.FromSeconds(durationInSeconds);
            return WithMaxDurationFilter(timespan);
        }

        public static XPathPodcastFilter WithDurationFilters(TimeSpan min, TimeSpan max)
        {
            var minFilter = new DurationFilter(DurationFilter.DurationFilterMethods.GreaterEquals, min);
            var maxFilter = new DurationFilter(DurationFilter.DurationFilterMethods.SmallerEquals, max);

            return WithFilters<PodcastDurationFilter>(new IContentFilter[] {minFilter, maxFilter});
        }
    }
}
