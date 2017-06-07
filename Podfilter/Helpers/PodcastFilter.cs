using System.Xml.Linq;

namespace Podfilter.Helpers
{
    /// <summary>
    /// Performs the filtering of podcasts.
    /// </summary>
    public class PodcastFilter : IFiltersPodcasts
    {
        private static XNamespace _itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        private static XNamespace _atomNamespace = "http://www.w3.org/2005/Atom";
        
        public XDocument FilterPodcast(XDocument podcast)
        {
            throw new System.NotImplementedException();
        }
    }
}