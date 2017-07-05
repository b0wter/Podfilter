using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Models.PodcastActions
{
    public class PodcastTitleAction : XPathPodcastAction
    {
        public override string XPath => "//item/title";
    }
}
