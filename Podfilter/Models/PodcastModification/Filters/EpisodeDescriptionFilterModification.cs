using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification.Filters
{
    public class EpisodeDescriptionFilterModification : BasePodcastModification
    {
        public EpisodeDescriptionFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant = true) 
            : base(
                "//item/description",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            //
        }
    }
}