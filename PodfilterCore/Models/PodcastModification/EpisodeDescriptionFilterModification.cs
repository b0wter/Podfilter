using PodfilterCore.Models.ContentFilters;

namespace PodfilterCore.Models.PodcastModification
{
    public class EpisodeDescriptionFilterModification : BasePodcastElementModification
    {
        public readonly string Argument;
        public readonly StringFilter.StringFilterMethod Method;
        public readonly bool CaseInvariant;

        public EpisodeDescriptionFilterModification(string argument, StringFilter.StringFilterMethod method)
            : this(argument, method, true)
        {
            //
        }

        public EpisodeDescriptionFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant) 
            : base(
                "//item/description",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            Argument = argument;
            Method = method;
            CaseInvariant = caseInvariant;
        }
    }
}