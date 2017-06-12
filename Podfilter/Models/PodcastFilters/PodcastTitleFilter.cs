using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Parsing;

namespace Podfilter.Models
{
    public class PodcastTitleFilter : XPathPodcastFilter<string>
    {
        public override string XPath => "//item/title";
    }
}