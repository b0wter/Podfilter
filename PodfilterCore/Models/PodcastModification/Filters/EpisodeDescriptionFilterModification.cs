using PodfilterCore.Models.ContentFilters;

namespace PodfilterCore.Models.PodcastModification.Filters
{
    public class EpisodeDescriptionFilterModification : BasePodcastElementModification
    {
        public EpisodeDescriptionFilterModification()
        {
            // constructor for deserialization
        }

        public EpisodeDescriptionFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant = true) 
            : base(
                "//item/description",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            //
        }
    }
}