using Podfilter.Models.ContentFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.PodcastFilters
{
    public abstract class PodcastStringPropertyFilter : XPathPodcastFilter<string>
    {
        protected static XPathPodcastFilter WithStringFilters<T>(IEnumerable<string> arguments, StringFilter.StringFilterMethod method) where T : PodcastStringPropertyFilter, new()
        {
            var filters = new List<IContentFilter>(arguments.Count());
            foreach (var argument in arguments)
            {
                filters.Add(new StringFilter(argument, method, false));
            }
            return WithFilters<T>(filters);
        }

        public static XPathPodcastFilter WithContainsFilter<T>(IEnumerable<string> mustContain) where T : PodcastStringPropertyFilter, new()
        {
            return WithStringFilters<T>(mustContain, StringFilter.StringFilterMethod.Contains);
        }

        public static XPathPodcastFilter WithContainsFilter<T>(string mustContain) where T : PodcastStringPropertyFilter, new()
        {
            return WithContainsFilter<T>(new string[] { mustContain });
        }

        public static XPathPodcastFilter WithDoesNotContainFilter<T>(IEnumerable<string> doesNotContain) where T : PodcastStringPropertyFilter, new()
        { 
            return WithStringFilters<T>(doesNotContain, StringFilter.StringFilterMethod.DoesNotContain);
        }

        public static XPathPodcastFilter WithDoesNotContainFilter<T>(string doesNotContain) where T : PodcastStringPropertyFilter, new()
        {
            return WithDoesNotContainFilter<T>(new string[] { doesNotContain });
        }
    }
}
