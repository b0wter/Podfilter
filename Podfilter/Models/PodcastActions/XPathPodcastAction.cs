using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastActions
{
    public abstract class XPathPodcastAction : PodcastAction
    {
        /// <summary>
        /// Reads <see cref="XElement"/>s from <see cref="XDocument"/>s using an XPath expression.
        /// </summary>
        private readonly XpathPodcastElementProvider _xpathPodcastElementProvider = new XpathPodcastElementProvider(string.Empty);

        /// <summary>
        /// XPath selector for the elements.
        /// </summary>
        public abstract string XPath { get; }

        protected override IEnumerable<XElement> GetMatchingElements(XDocument podcast)
        {
            var matchingItems =_xpathPodcastElementProvider.GetElements(podcast, XPath);
            return matchingItems;
        }
    }
}
