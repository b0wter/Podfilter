using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models
{
    public interface IPodcastFilter
    {
        XDocument FilterPodcast(XDocument podcast, IEnumerable<IFilter> filters);
    }
}