using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification
{
    public class EpisodeDescriptionFilterModification : BaseModification
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