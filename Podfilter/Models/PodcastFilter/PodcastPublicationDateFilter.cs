using System;

namespace Podfilter.Models
{
    public class PodcastPublicationDateFilter : XPathPodcastFilter<DateTime>
    {
        public override string XPath => "//item/pubDate";
    }
}