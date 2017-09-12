using System.Collections.Generic;
using PodfilterWeb.Models;

namespace PodfilterWeb.Helpers
{
    public class PodfilterUrlArgument
    {
        public string Url {get;set;}
        public List<DisplayableBasePodcastModification> Modifications {get;set;}

        public PodfilterUrlArgument()
        {
            // serialization
        }

        public PodfilterUrlArgument(string url, List<DisplayableBasePodcastModification> modifications)
        {
            this.Url = url;
            this.Modifications = modifications;
        }
    }
}