using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastFilters
{
    public class PodcastFilterCollection
    {
        public List<IPodcastFilter> Filters { get; } = new List<IPodcastFilter>();

        public PodcastFilterCollection()
        {
            // empty constructor for deserialization
        }
        
        public PodcastFilterCollection(IEnumerable<IPodcastFilter> filters)
        {
            Filters.AddRange(filters);
        }

        public XDocument FilterPodcast(XDocument podcast)
        {
            foreach (var filter in Filters)
                podcast = filter.Filter(podcast);

            return podcast;
        }
    }
}