using Podfilter.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Helpers
{
    /// <summary>
    /// Performs the filtering of podcasts.
    /// </summary>
    public abstract class PodcastFilter : IFiltersPodcasts
    {
        protected static string _itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        protected static string _atomNamespace = "http://www.w3.org/2005/Atom";

        public abstract XDocument FilterPodcast(XDocument podcast, IEnumerable<IFilter> filters);
    }
}