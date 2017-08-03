using System.Xml.Linq;
using Microsoft.AspNetCore.Razor.Parser;

namespace Podfilter.Models.PodcastModification
{
    public abstract class BaseModification
    {
        /// <summary>
        /// Selects matching elements from an <see cref="XDocument"/>.
        /// </summary>
        private readonly IPodcastElementProvider _elementProvider;
        /// <summary>
        /// Modifies an <see cref="XElement"/> (may also be deleted).
        /// </summary>
        private readonly XElementModification _contentModification;

        protected BaseModification(IPodcastElementProvider provider, XElementModification modification)
        {
            _elementProvider = provider;
            _contentModification = modification;
        }

        protected BaseModification(string xPathSelector, XElementModification modification)
            : this(new XpathPodcastElementProvider(xPathSelector), modification)
        {
            //
        }

        /// <summary>
        /// Modifies matching <see cref="XElement"/>s of a <see cref="XDocument"/>. Modifications are in place, thus no return value is required.
        /// </summary>
        /// <param name="podcast"></param>
        public void Modify(XDocument podcast)
        {
            // since all the modification of the XElements und XDocuments are performed in place
            // a return value is actually not needed
            var elements = _elementProvider.GetElements(podcast);
            foreach(var element in elements)
                _contentModification.Modify(element);
        }
    }
}