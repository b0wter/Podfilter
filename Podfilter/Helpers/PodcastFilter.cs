using Podfilter.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Podfilter.Helpers
{
    /// <summary>
    /// Performs the filtering of podcasts.
    /// </summary>
    public abstract class PodcastFilter : IPodcastFilter
    {
        protected static string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        protected static string AtomNamespace = "http://www.w3.org/2005/Atom";

        public abstract XDocument FilterPodcast(XDocument podcast, IEnumerable<IFilter> filters);
        public abstract void ValidateIFilterTypeMatchesContent(IEnumerable<IFilter> filters);
    }
}