using Podfilter.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Helpers
{
    public interface IFiltersPodcasts
    {
        XDocument FilterPodcast(XDocument podcast, IEnumerable<IFilter> filters);
    }
}