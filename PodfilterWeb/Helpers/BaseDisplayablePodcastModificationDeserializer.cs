using System;
using System.Collections.Generic;
using PodfilterWeb.Models;

namespace PodfilterWeb.Helpers
{
    public abstract class BaseDisplayablePodcastModificationDeserializer
    {
        public abstract IEnumerable<DisplayableBasePodcastModification> Deserialize(string content);
    }
}