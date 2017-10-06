using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using PodfilterWeb.Helpers;

namespace PodfilterWeb.Models
{
    public abstract class DisplayableBasePodcastModification
    {
        public string Method { get; set; }
        
        [JsonIgnore]
        public abstract string Name {get;}

        [JsonIgnore]
        public abstract string Description {get;}        

        [JsonIgnore]
        public BasePodcastModification Modification { get; protected set; }

        [JsonProperty(PropertyName="Type")]
        public string TypeName => this.GetType().FullName;

        protected DisplayableBasePodcastModification()
        {
            // Constructor for deserialization purposes.
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            DeserializeModification();
        }

        protected abstract void DeserializeModification();

        public abstract string DescribeWithTranslator(BaseModificationMethodTranslator translator);

        public abstract DisplayableBasePodcastModificationSource ToSource();
    }

    public abstract class DisplayableBasePodcastModification<T> : DisplayableBasePodcastModification
    {
        public T Argument { get; set; }

        public override DisplayableBasePodcastModificationSource ToSource()
        {
            return new DisplayableBasePodcastModificationSource
            {
                Type = this.GetType().Name,
                Method = Method,
                Argument = Argument.ToString()
            };
        }
    }
}
