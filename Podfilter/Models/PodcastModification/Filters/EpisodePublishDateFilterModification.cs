using Podfilter.Models.ContentFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.PodcastModification.Filters
{
    public class EpisodePublishDateFilterModification : BasePodcastElementModification
    {
        public EpisodePublishDateFilterModification(long date, DateFilter.DateFilterMethods method)
            : base(
                  "//item/pubdate",
                  new XElementFilterModification(new Podfilter.Models.ContentFilters.DateFilter(method, date))
                  )
        {
            //
        }
    }
}
