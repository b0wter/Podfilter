using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Parsing;
using Podfilter.Models.PodcastFilters;

namespace Podfilter.Models.PodcastFilters
{
    public class PodcastTitleFilter : PodcastStringPropertyFilter
    {
        public override string XPath => "//item/title";
        public override string Description => "Filters podcast items based on their title.";
    }
}