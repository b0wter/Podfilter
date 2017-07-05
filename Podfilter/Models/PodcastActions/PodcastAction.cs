using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Podfilter.Models.ContentActions;

namespace Podfilter.Models.PodcastActions
{
    public abstract class PodcastAction : IPodcastAction
    {
        private List<IContentAction> Actions { get; set; } = new List<IPodcastAction>();
        
        public XDocument PerformAction(XDocument podcast)
        {
            
        }

        public abstract IEnumerable<XElement> GetMatchingElements();
    }
}