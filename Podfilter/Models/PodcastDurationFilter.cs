using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podfilter.Models
{
    public class PodcastDurationFilter : XPathPodcastFilter
    {
        public override string XPath => "//item/itunes:duration";

        public PodcastDurationFilter() : base()
        {
            // Selects the title of all items in the rss feed.
        }

        public PodcastDurationFilter(string xpath, Dictionary<string, string> namespaces) : base()
        {
            //
        }
    }
}
