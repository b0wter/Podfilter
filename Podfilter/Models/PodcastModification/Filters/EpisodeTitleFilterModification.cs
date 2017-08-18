using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification.Filters
{
    public class EpisodeTitleFilterModification : BasePodcastModification
    {
        public EpisodeTitleFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant = true) 
            : base(
                "//item/title",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            //
        }
    }
}