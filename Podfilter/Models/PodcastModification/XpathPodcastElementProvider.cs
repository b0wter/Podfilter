using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Podfilter.Models.PodcastModification
{
    /// <summary>
    /// Uses xpath expressions to filter elements from <see cref="XDocument"/>s.
    /// </summary>
    public class XpathPodcastElementProvider : IPodcastElementProvider
    {
        // Default namespaces for podcasts.
        protected static string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        protected static string AtomNamespace = "http://www.w3.org/2005/Atom";
        
        public string XPath { get; set; }
        
        private XmlNamespaceManager _namespaceManager;
         
        public XpathPodcastElementProvider(string xpath)
        {
            this.XPath = xpath;
        }

        public XpathPodcastElementProvider(Dictionary<string, string> namespaces)
        {
            CreateNamespaceManager(namespaces);    
        }
        
        private void CreateNamespaceManager(Dictionary<string, string> namespaces)
        {
            _namespaceManager = new XmlNamespaceManager(new NameTable());
            _namespaceManager.AddNamespace("itunes", ItunesNamespace);
            _namespaceManager.AddNamespace("atom", AtomNamespace);
            
            if(namespaces != null)
                foreach (var pair in namespaces)
                    _namespaceManager.AddNamespace(pair.Key, pair.Value);
        }
        
        public IEnumerable<XElement> GetElements(XDocument podcast, string selector)
        {
            var matchingElements = podcast.XPathSelectElements(selector, _namespaceManager);
            return matchingElements;
        }

        public IEnumerable<XElement> GetElements(XDocument podcast)
        {
            return GetElements(podcast, XPath);
        }
    }
}