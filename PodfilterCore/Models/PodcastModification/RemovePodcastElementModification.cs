using System;
using System.Linq;
using System.Xml.Linq;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Helpers;

namespace PodfilterCore.Models.PodcastModification
{
    public class RemovePodcastElementModification : BasePodcastModification
    {
        private string XPath {get;set;}

        public RemovePodcastElementModification(string xPath)
        {
            this.XPath = xPath;
        }

        public override void Modify(XDocument podcast)
        {
            if (podcast == null)
                throw new ArgumentNullException("podcast");

            var selector = new XPathPodcastElementProvider(XPath);
            var elements = selector.GetElements(podcast);
            foreach(var element in elements)
                element.Remove();
        }
    }
}