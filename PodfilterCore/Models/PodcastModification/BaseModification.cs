using System.Xml.Linq;
using Newtonsoft.Json;

namespace PodfilterCore.Models.PodcastModification
{
    /// <summary>
    /// Base class for all modifications to a podcast. A modification performs a single task on an <see cref="XDocument"/>.
    /// </summary>
    public abstract class BasePodcastModification
    {
        public abstract void Modify(XDocument podcast);

        [JsonProperty(PropertyName = "Type")]
        public string TypeName => this.GetType().FullName;
    }

    /// <summary>
    /// Extension of <see cref="BasePodcastModification"/> that uses an <see cref="IPodcastElementProvider"/> to select the elements to modify.
    /// </summary>
    public abstract class BasePodcastElementModification : BasePodcastModification
    {
        /// <summary>
        /// Selects matching elements from an <see cref="XDocument"/>.
        /// </summary>
        private readonly IPodcastElementProvider _elementProvider;
        
        /// <summary>
        /// Modifies an <see cref="XElement"/> (may also be deleted).
        /// </summary>
        protected readonly XElementModification _contentModification;

        protected BasePodcastElementModification(IPodcastElementProvider provider, XElementModification modification)
        {
            _elementProvider = provider;
            _contentModification = modification;
        }

        protected BasePodcastElementModification(string xPathSelector, XElementModification modification)
            : this(new XPathPodcastElementProvider(xPathSelector), modification)
        {
            //
        }

        /// <summary>
        /// Modifies matching <see cref="XElement"/>s of a <see cref="XDocument"/>. Modifications are in place, thus no return value is required.
        /// </summary>
        /// <param name="podcast"></param>
        public override void Modify(XDocument podcast)
        {
            // since all the modification of the XElements und XDocuments are performed in place
            // a return value is actually not needed
            var elements = _elementProvider.GetElements(podcast);
            foreach (var element in elements)
            {
                _contentModification.Modify(element);
            }    
        }
    }
}