using PodfilterCore.Models.PodcastModification.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterCore.Models.ContentFilters;

namespace PodfilterWeb.Models
{
    public class DisplayableEpisodeDescriptionFilterModification : DisplayableBasePodcastModification
    {
        public DisplayableEpisodeDescriptionFilterModification()
        {
            // Constructor for deserialization.
        }

        public DisplayableEpisodeDescriptionFilterModification(string argument, string methodToParse, bool caseInvariant = true)
        {
            this.Argument = argument;
            this.Method = methodToParse;

            var method = (StringFilter.StringFilterMethod)Enum.Parse(typeof(StringFilter.StringFilterMethod), methodToParse);
            this.Modification = new EpisodeDescriptionFilterModification(argument, method, caseInvariant);
        }
    }
}
