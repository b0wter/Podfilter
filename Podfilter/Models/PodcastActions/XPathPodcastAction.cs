using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.PodcastActions
{
    public abstract class XPathPodcastAction
    {
        private readonly XpathPodcastElementProvider _xpathPodcastElementProvider = new XpathPodcastElementProvider();

        public abstract string XPath { get; }
    }
}
