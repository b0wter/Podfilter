using PodfilterCore.Models.ContentFilters;
using System;

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

        public EpisodeTitleFilterModification(string argument, string methodToParse, bool caseInvariant = true)
            : base(
                  "//item/title",
                  new XElementFilterModification(new StringFilter(argument, (StringFilter.StringFilterMethod)Enum.Parse(typeof(StringFilter.StringFilterMethod), methodToParse), caseInvariant)))
        {
            //
        }
    }
}