using PodfilterCore.Models.ContentFilters;
using PodfilterCore.Models.ContentFilters;

namespace PodfilterCore.Models.PodcastModification.Filters
{
    public class EpisodeTitleFilterModification : BasePodcastElementModification
    {
        public string Argument => this.Argument;
        public StringFilter.StringFilterMethod Method => this.Method;

        public EpisodeTitleFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant = true) 
            : base(
                "//item/title",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            //
        }
    }
}