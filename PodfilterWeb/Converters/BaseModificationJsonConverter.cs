using System;
using Newtonsoft.Json;
using PodfilterCore.Models.PodcastModification;
using Newtonsoft.Json.Linq;
using PodfilterCore.Models.PodcastModification.Filters;

namespace PodfilterWeb.Converters
{
    public class BaseModificationJsonConverter : JsonConverter
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
                throw new ArgumentException($"Cannot deserialized objects which don't have a 'TypeName'-property.");

            BasePodcastModification mod = null;

            /*
            if (type == typeof(EpisodeDescriptionFilterModification).FullName)
                mod = EpisodeDescriptionFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(EpisodeTitleFilterModification).FullName)
                mod = EpisodeTitleFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(EpisodeDurationFilterModification).FullName)
                mod = EpisodeDurationFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(EpisodePublishDateFilterModification).FullName)
                mod = EpisodePublishDateFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(RemoveDuplicateEpisodesModification).FullName)
                mod = RemoveDuplicateEpisodesModification.CreateEmptyInstanceForDeserialization();
            else
                throw new ArgumentException($"Cannot deserialize objects with the TypeName '{type}'.");

            */

            serializer.Populate(obj.CreateReader(), mod);
            return mod;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}