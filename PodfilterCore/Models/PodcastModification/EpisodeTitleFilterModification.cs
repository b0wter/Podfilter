using PodfilterCore.Models.ContentFilters;
using System;

namespace PodfilterCore.Models.PodcastModification
{
    public class EpisodeTitleFilterModification : BasePodcastElementModification
    {
        public EpisodeTitleFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant = true) 
            : base(
                "//item/title",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            //
        }

        public EpisodeTitleFilterModification(string argument, string methodToParse, bool caseInvariant = true)
            : base(
                  "//item/title",
                  new XElementFilterModification(new StringFilter(argument, (StringFilter.StringFilterMethod)Enum.Parse(typeof(StringFilter.StringFilterMethod), methodToParse), caseInvariant)))
        {
            //
        }
    }
}