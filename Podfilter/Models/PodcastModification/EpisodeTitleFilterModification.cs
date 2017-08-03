using Podfilter.Models.ContentFilters;

namespace Podfilter.Models.PodcastModification
{
    public class EpisodeTitleFilterModification : BaseModification
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