using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastActions
{
    public abstract class XPathPodcastAction : IPodcastAction
    {
        private readonly XpathPodcastElementProvider _xpathPodcastElementProvider = new XpathPodcastElementProvider();

        public abstract string XPath { get; }

        public XDocument PerformAction(XDocument podcast)
        {
            var matchingItems =_xpathPodcastElementProvider.GetElements(podcast, XPath);

            //foreach(var item in matchingItems)
            //    item.Value

            return podcast;
        }

    }
}
