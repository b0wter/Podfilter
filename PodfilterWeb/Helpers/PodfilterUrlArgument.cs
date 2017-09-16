using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PodfilterWeb.Models;

namespace PodfilterWeb.Helpers
{
    public class PodfilterUrlArgument
    {
        [JsonProperty(PropertyName="U")]
        public string Url {get;set;}
        [JsonProperty(PropertyName="M")]
        public List<DisplayableBasePodcastModificationSource> Modifications {get;set;}

        public PodfilterUrlArgument()
        {
            // serialization
        }

        public PodfilterUrlArgument(string url, List<DisplayableBasePodcastModificationSource> modifications)
        {
            this.Url = url;
            this.Modifications = modifications;
        }

        public PodfilterUrlArgument(string url, List<DisplayableBasePodcastModification> modifications)
            : this(url, modifications.Select(x => x.ToSource()).ToList())
        {
            //
        }
    }
}