using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastModification
{
    public interface IPodcastElementProvider
    {
        IEnumerable<XElement> GetElements(XDocument podcast);
    }
}