using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastModification
{
    /// <summary>
    /// Defines access to nodes of an xml document.
    /// </summary>
    public interface IPodcastElementProvider
    {
        IEnumerable<XElement> GetElements(XDocument podcast);
    }
}