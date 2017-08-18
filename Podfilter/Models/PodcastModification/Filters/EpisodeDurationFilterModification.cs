using System;
using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification.Filters
{
    public class EpisodeDurationFilterModification : BasePodcastModification
    {
        public EpisodeDurationFilterModification(DurationFilter.DurationFilterMethods method, long duration) 
            : base(
                "//item/itunes:duration", 
                new XElementFilterModification(new DurationFilter(method, duration))
                )
        {
            //
        }
        
        public EpisodeDurationFilterModification(DurationFilter.DurationFilterMethods method, TimeSpan duration) 
            : base(
                "//item/itunes:duration", 
                new XElementFilterModification(new DurationFilter(method, duration))
                )
        {
            //
        }
    }
}