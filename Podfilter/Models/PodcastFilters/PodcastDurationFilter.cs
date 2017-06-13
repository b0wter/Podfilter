using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podfilter.Models
{
    public class PodcastDurationFilter : XPathPodcastFilter<TimeSpan>
    {
        public override string XPath => "//item/itunes:duration";

        public override string Description => "Filters podcast items based on their duration.";

        public static XPathPodcastFilter WithDurationFilter()
    }
}
