using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.PodcastFilters.PodcastFilters
{
    public class PodcastDescriptionFilter : PodcastStringPropertyFilter
    {
        public override string XPath => "//item/description";
        public override string Description => "Filters podcast items based on their description.";
    }
}
