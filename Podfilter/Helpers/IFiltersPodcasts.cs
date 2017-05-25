using System.Xml.Linq;

namespace Podfilter.Helpers
{
    public interface IFiltersPodcasts
    {
        XDocument FilterPodcast(XDocument podcast);
    }
}