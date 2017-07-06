using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Podfilter.Models.ContentActions;
using System;

namespace Podfilter.Models.PodcastActions
{
    public abstract class PodcastAction : IPodcastAction
    {
        private List<IPodcastAction> Actions { get; set; } = new List<IPodcastAction>();
        
        public XDocument PerformAction(XDocument podcast)
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable<XElement> GetMatchingElements();
    }
}