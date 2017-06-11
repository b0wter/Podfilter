using System.Collections.Generic;

namespace Podfilter.Models
{
    public class PodcastTitleFilter : XPathPodcastFilter
    {
        public override string XPath => "//item/title";

        public PodcastTitleFilter() : base()
        {
            // Selects the title of all items in the rss feed.
        }
        
        public PodcastTitleFilter(string xpath, Dictionary<string, string> namespaces) : base()
        {
            //
        }
    }
}