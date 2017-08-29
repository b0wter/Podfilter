using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Models.PodcastModification.Filters;
using PodfilterWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodfilterWeb.Converters
{
    public class DisplayableBaseModificationJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (typeof(DisplayableBasePodcastModification).IsAssignableFrom(objectType));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var type = obj["Type"].Value<string>();

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException($"Cannot deserialized objects which don't have a 'TypeName'-property.");

            DisplayableBasePodcastModification mod = null;

            if (type == typeof(DisplayableEpisodeDescriptionFilterModification).FullName)
                mod = DisplayableEpisodeDescriptionFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(DisplayableEpisodeTitleFilterModification).FullName)
                mod = DisplayableEpisodeTitleFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(DisplayableEpisodeDurationFilterModification).FullName)
                mod = DisplayableEpisodeDurationFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(DisplayableEpisodePublishDateFilterModification).FullName)
                mod = DisplayableEpisodePublishDateFilterModification.CreateEmptyInstanceForDeserialization();
            else if (type == typeof(DisplayableRemoveDuplicateEpisodesModification).FullName)
                mod = DisplayableRemoveDuplicateEpisodesModification.CreateEmptyInstanceForDeserialization();
            else
                throw new ArgumentException($"Cannot deserialize objects with the TypeName '{type}'.");

            serializer.Populate(obj.CreateReader(), mod);
            return mod;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
