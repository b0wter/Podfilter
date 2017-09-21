using System;
using PodfilterCore.Models.ContentFilters;

namespace PodfilterCore.Models.PodcastModification
{
    public class EpisodeDurationFilterModification : BasePodcastElementModification
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