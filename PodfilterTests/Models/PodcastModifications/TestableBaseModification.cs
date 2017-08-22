using PodfilterCore.Models.PodcastModification;

namespace PodfilterCoreTests.Models.PodcastModifications
{
    public class TestableBaseModification : BasePodcastElementModification
    {
        public IPodcastElementProvider Provider { get; }
        public XElementModification Modification { get; }
        
        public TestableBaseModification(IPodcastElementProvider provider, XElementModification modification) : base(provider, modification)
        {
            Provider = provider;
            Modification = modification;
        }

        public TestableBaseModification(string xPathSelector, XElementModification modification) : base(xPathSelector, modification)
        {
            Provider = new XPathPodcastElementProvider(xPathSelector);
            Modification = modification;
        }
    }
}