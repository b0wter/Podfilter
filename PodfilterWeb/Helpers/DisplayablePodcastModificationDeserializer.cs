using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PodfilterCore.Helpers;
using PodfilterCore.Models.PodcastModification;
using PodfilterWeb.Converters;
using PodfilterWeb.Models;

namespace PodfilterWeb.Helpers
{
    public class DisplayablePodcastModificationDeserializer : BaseDisplayablePodcastModificationDeserializer
    {
        public override IEnumerable<DisplayableBasePodcastModification> Deserialize(string content)
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new DisplayableBaseModificationJsonConverter()
                }
            };
            var result = JsonConvert.DeserializeObject<List<DisplayableBasePodcastModification>>(content, settings);
            return result;
        }
    }
}