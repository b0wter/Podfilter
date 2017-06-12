using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Parsing;

namespace Podfilter.Models
{
    public class PodcastTitleFilter : XPathPodcastFilter<string>
    {
        public override string XPath => "//item/title";
        public override string Description => "Filters podcast items based on their title.";

        protected static PodcastTitleFilter WithFilters(IEnumerable<IFilter> filters)
        {
            var filter = new PodcastTitleFilter
            {
                Filters = filters.ToList()
            };
            return filter;
        }

        protected static PodcastTitleFilter WithStringFilters(IEnumerable<string> arguments, StringFilter.StringFilterMethod method)
        {
            var filters = new List<StringFilter>(arguments.Count());
            foreach(var argument in arguments)
            {
                filters.Add(new StringFilter(argument, method, false));
            }
            return WithFilters(filters);
        }

        public static PodcastTitleFilter WithContainsFilter(IEnumerable<string> mustContain)
        {
            return WithStringFilters(mustContain, StringFilter.StringFilterMethod.Contains);
        }

        public static PodcastTitleFilter WithContainsFilter(string mustContain)
        {
            return WithContainsFilter(new string[] { mustContain });
        }

        public static PodcastTitleFilter WithDoesNotContainFilter(IEnumerable<string> doesNotContain)
        {
            return WithStringFilters(doesNotContain, StringFilter.StringFilterMethod.DoesNotContain);
        }
        
        public static PodcastTitleFilter WithDoesNotContainFilter(string doesNotContain)
        {
            return WithDoesNotContainFilter(new string[] { doesNotContain });
        }
    }
}