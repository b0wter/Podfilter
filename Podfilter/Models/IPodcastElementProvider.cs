using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models
{
    public interface IPodcastElementProvider
    {
        IEnumerable<XElement> GetElements(XDocument podcast);
    }
}