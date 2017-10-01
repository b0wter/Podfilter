using PodfilterCore.Models.ContentFilters;
using System;

namespace PodfilterCore.Models.PodcastModification
{
    public class EpisodeTitleFilterModification : BasePodcastElementModification
    {
        public readonly string Argument;
        public readonly StringFilter.StringFilterMethod Method;
        public readonly bool CaseInvariant;

        public EpisodeTitleFilterModification(string argument, StringFilter.StringFilterMethod method, bool caseInvariant = true) 
            : base(
                "//item/title",
                new XElementFilterModification(new StringFilter(argument, method, caseInvariant)))
        {
            Argument = argument;
            Method = method;
            CaseInvariant = caseInvariant;
        }

        public EpisodeTitleFilterModification(string argument, string methodToParse, bool caseInvariant = true)
            : base(
                  "//item/title",
                  new XElementFilterModification(new StringFilter(argument, (StringFilter.StringFilterMethod)Enum.Parse(typeof(StringFilter.StringFilterMethod), methodToParse), caseInvariant)))
        {
            Argument = argument;
            Method = (StringFilter.StringFilterMethod)Enum.Parse(typeof(StringFilter.StringFilterMethod), methodToParse);
            CaseInvariant = caseInvariant;
        }
    }
}