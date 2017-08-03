using System;
using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification
{
    public class MinEpisodeDurationFilterModification : BaseModification
    {
        public MinEpisodeDurationFilterModification(DurationFilter.DurationFilterMethods method, long duration) 
            : base(
                "//item/itunes:duration", 
                new XElementFilterModification(new DurationFilter(method, duration))
                )
        {
            //
        }
        
        public MinEpisodeDurationFilterModification(DurationFilter.DurationFilterMethods method, TimeSpan duration) 
            : base(
                "//item/itunes:duration", 
                new XElementFilterModification(new DurationFilter(method, duration))
                )
        {
            //
        }
    }
}