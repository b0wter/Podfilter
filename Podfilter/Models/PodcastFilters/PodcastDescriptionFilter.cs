namespace Podfilter.Models.PodcastFilters
{
    public class PodcastDescriptionFilter : PodcastStringPropertyFilter
    {
        public override string XPath => "//item/description";
        public override string Description => "Filters podcast items based on their description.";
    }
}
