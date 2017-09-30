using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodfilterRepository.Helpers
{
    public class BasePodcastModificationConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (typeof(BasePodcastModification).IsAssignableFrom(objectType));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var type = obj["Type"].Value<string>();

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException($"Cannot deserialized objects which don't have a 'Type'-property.");

            BasePodcastModification mod = null;

            if (type == typeof(EpisodeDescriptionFilterModification).FullName)
                mod = obj.ToObject<EpisodeDescriptionFilterModification>();
            else if (type == typeof(EpisodeTitleFilterModification).FullName)
                mod = obj.ToObject<EpisodeTitleFilterModification>();
            else if (type == typeof(EpisodeDurationFilterModification).FullName)
                mod = obj.ToObject<EpisodeDurationFilterModification>();
            else if (type == typeof(EpisodePublishDateFilterModification).FullName)
                mod = obj.ToObject<EpisodePublishDateFilterModification>();
            else if (type == typeof(RemoveDuplicateEpisodesModification).FullName)
                mod = obj.ToObject<RemoveDuplicateEpisodesModification>();
            else
                throw new ArgumentException($"Cannot deserialize objects with the Type '{type}'.");

            return mod;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

