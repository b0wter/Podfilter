using Podfilter.Models.ContentFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.PodcastFilters
{
    public abstract class PodcastStringPropertyFilter : XPathPodcastFilter<string>
    {
        protected static XPathPodcastFilter WithStringFilters(IEnumerable<string> arguments, StringFilter.StringFilterMethod method)
        {
            var filters = new List<IContentFilter>(arguments.Count());
            foreach (var argument in arguments)
            {
                filters.Add(new StringFilter(argument, method, false));
            }
            return WithFilters(filters);
        }

        public static XPathPodcastFilter WithContainsFilter(IEnumerable<string> mustContain)
        {
            return WithStringFilters(mustContain, StringFilter.StringFilterMethod.Contains);
        }

        public static XPathPodcastFilter WithContainsFilter(string mustContain)
        {
            return WithContainsFilter(new string[] { mustContain });
        }

        public static XPathPodcastFilter WithDoesNotContainFilter(IEnumerable<string> doesNotContain)
        {
            return WithStringFilters(doesNotContain, StringFilter.StringFilterMethod.DoesNotContain);
        }

        public static XPathPodcastFilter WithDoesNotContainFilter(string doesNotContain)
        {
            return WithDoesNotContainFilter(new string[] { doesNotContain });
        }
    }
}
