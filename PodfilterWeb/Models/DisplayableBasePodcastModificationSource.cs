using System;
using Newtonsoft.Json;

namespace PodfilterWeb.Models
{
    public class DisplayableBasePodcastModificationSource
    {
        [JsonProperty(PropertyName="T")]
        public string Type {get;set;}
        [JsonProperty(PropertyName="A")]
        public string Argument {get;set;}
        [JsonProperty(PropertyName="M")]
        public string Method {get;set;}

        /// <summary>
        /// Dynamically creates an instance of a subclass of <see cref="DisplayableBasePodcastModification"/> using the given typename and arguments.
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="argument"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public DisplayableBasePodcastModification ToModification()
        {
            var typeAsString = $"PodfilterWeb.Models.{Type}, PodfilterWeb";
            var modification = (DisplayableBasePodcastModification)Activator.CreateInstance(System.Type.GetType(typeAsString), new object[] { Argument, Method });
            return modification;
        }
    }
}