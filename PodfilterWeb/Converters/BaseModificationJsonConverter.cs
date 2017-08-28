﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Models.PodcastModification.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            if (type == typeof(EpisodeDescriptionFilterModification).FullName)
                mod = new EpisodeDescriptionFilterModification();
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
