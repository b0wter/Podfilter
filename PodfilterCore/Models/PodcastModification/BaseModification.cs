using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using PodfilterCore.Data;

namespace PodfilterCore.Models.PodcastModification
{
    /// <summary>
    /// Base class for all modifications to a podcast. A modification performs a single task on an <see cref="XDocument"/>.
    /// </summary>
    public abstract class BasePodcastModification
    {
        /// <summary>
        /// Primary key for identification purposes.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id of the podcast this modification belongs to.
        /// </summary>
        public long SavedPodcastId { get; set; }

        /// <summary>
        /// Reference to the podcast this modification belongs to.
        /// </summary>
        public SavedPodcast SavedPodcast {get;set;}

        /// <summary>
        /// Performs the modification on an <see cref="XDocument"/>.
        /// </summary>
        /// <param name="podcast"></param>
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
            elements.ToList().ForEach(e => _contentModification.Modify(e));
        }
    }
}