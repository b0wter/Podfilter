using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodfilterWeb.Models
{
    public abstract class DisplayableBasePodcastModification
    {
        public string Argument { get; protected set; }
        public string Method { get; protected set; }
        // needs to be public for deserialization
        public BasePodcastElementModification Modification { get; set; }

        public DisplayableBasePodcastModification()
        {
            // Constructor for deserialization purposes.
        }
    }
}
