using Podfilter.Models.PodcastModification;

namespace PodfilterTests.Models.PodcastModifications
{
    public class TestableBaseModification : BaseModification
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
            Provider = new XpathPodcastElementProvider(xPathSelector);
            Modification = modification;
        }
    }
}