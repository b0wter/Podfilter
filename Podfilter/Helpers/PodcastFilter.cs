using System.Xml.Linq;

namespace Podfilter.Helpers
{
    /// <summary>
    /// Performs the filtering of podcasts.
    /// </summary>
    public class PodcastFilter : IFiltersPodcasts
    {
        protected static string _itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        protected static string _atomNamespace = "http://www.w3.org/2005/Atom";
        
        public XDocument FilterPodcast(XDocument podcast)
        {
            throw new System.NotImplementedException();
        }
    }
}