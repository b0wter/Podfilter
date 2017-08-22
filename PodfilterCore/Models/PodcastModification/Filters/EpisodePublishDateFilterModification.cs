using PodfilterCore.Models.ContentFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodfilterCore.Models.PodcastModification.Filters
{
    public class EpisodePublishDateFilterModification : BasePodcastElementModification
    {
        public EpisodePublishDateFilterModification(long date, DateFilter.DateFilterMethods method)
            : base(
                  "//item/pubdate",
                  new XElementFilterModification(new PodfilterCore.Models.ContentFilters.DateFilter(method, date))
                  )
        {
            //
        }
    }
}
