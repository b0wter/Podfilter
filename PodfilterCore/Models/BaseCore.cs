using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterCore.Models
{
    public abstract class BaseCore
    {
        public abstract Task<XDocument> Modify(string url, IEnumerable<BasePodcastModification> modifications);
        public abstract Task<XDocument> Modify(string url, long fromEpoch, long toEpoch, bool removeDuplicateEpisodes, string titleMustNotContain, string titleMustContain, int minDuration, int maxDuration, string removeDuplicates);
    }
}