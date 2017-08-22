using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PodfilterCore.Models.PodcastModification
{
    /// <summary>
    /// Uses xpath expressions to filter elements from <see cref="XDocument"/>s.
    /// </summary>
    public class XPathPodcastElementProvider : IPodcastElementProvider
    {
        // Default namespaces for podcasts.
        protected static string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        protected static string AtomNamespace = "http://www.w3.org/2005/Atom";
        
        public string XPath { get; set; }
        
        public XmlNamespaceManager NamespaceManager { private set; get; }
         
        public XPathPodcastElementProvider(string xpath)
        {
            this.XPath = xpath;
            CreaterDefaultNamespaceManager();
        }

        public XPathPodcastElementProvider(string xpath, Dictionary<string, string> namespaces)
        {
            this.XPath = xpath;
            CreateNamespaceManager(namespaces);    
        }

        /// <summary>
        /// Initializes the <see cref="NamespaceManager"/> with the two default namespaces.
        /// </summary>
        private void CreaterDefaultNamespaceManager()
        {
            CreateNamespaceManager(null);
        }

        /// <summary>
        /// Creates a <see cref="NamespaceManager"/> with the default namespaces as well as <paramref name="namespaces"/>.
        /// </summary>
        /// <param name="namespaces"></param>
        private void CreateNamespaceManager(Dictionary<string, string> namespaces)
        {
            NamespaceManager = new XmlNamespaceManager(new NameTable());
            NamespaceManager.AddNamespace("itunes", ItunesNamespace);
            NamespaceManager.AddNamespace("atom", AtomNamespace);
            
            if(namespaces != null)
                foreach (var pair in namespaces)
                    NamespaceManager.AddNamespace(pair.Key, pair.Value);
        }
        
        /// <summary>
        /// Gets all matching <see cref="XElement"/>s from <paramref name="podcast"/> using a custom XPath selector.
        /// </summary>
        /// <param name="podcast"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IEnumerable<XElement> GetElements(XDocument podcast, string selector)
        {
            var matchingElements = podcast.XPathSelectElements(selector, NamespaceManager);
            return matchingElements;
        }

        /// <summary>
        /// Gets all matching <see cref="XElement"/>s from <paramref name="podcast"/> using the Xpath selector supplied during the initialization.
        /// </summary>
        /// <param name="podcast"></param>
        /// <returns></returns>
        public IEnumerable<XElement> GetElements(XDocument podcast)
        {
            return GetElements(podcast, XPath);
        }
    }
}