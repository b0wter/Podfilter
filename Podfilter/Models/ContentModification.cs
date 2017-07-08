using System.Collections.Generic;
using System.Xml.Linq;
using Podfilter.Models.ContentActions;

namespace Podfilter.Models
{
    public class ContentModification
    {
        protected IPodcastElementProvider ElementProvider { get; }
        protected IContentAction Action { get; }

        public ContentModification(IPodcastElementProvider elementProvider, IContentAction action)
        {
            this.ElementProvider = elementProvider;
            this.Action = action;
        }
        
        /// <summary>
        /// Selects elements and modifies their values (in place).
        /// </summary>
        /// <param name="document"></param>
        public void ModifyXDocument(XDocument document)
        {
            var matchingElements = GetMatchingElements(document);
            foreach (var element in matchingElements)
            {
                Action.ParseAndModifyContent(element.Value);
            }
        }

        /// <summary>
        /// Retrieves all <see cref="XElement"/>s that are given by the <see cref="ElementProvider"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private IEnumerable<XElement> GetMatchingElements(XDocument document)
        {
            var matchingElements = ElementProvider.GetElements(document);
            return matchingElements;
        }
    }
}