using System;
using System.Security.Cryptography.X509Certificates;
using Podfilter.Models.ContentActions;

namespace Podfilter.Models.PodcastActions
{
    public class AddStringToTitlePodcastAction : XPathPodcastAction
    {
        protected override Type TargetElementType => typeof(string);
        public override string XPath => "//channel/title";

        public static AddStringToTitlePodcastAction WithSuffixAction(string suffix)
        {
            return WithPrefixAndSuffix(null, suffix);
        }

        public static AddStringToTitlePodcastAction WithPrefixAction(string prefix)
        {
            return WithPrefixAndSuffix(prefix, null);
        }

        public static AddStringToTitlePodcastAction WithPrefixAndSuffix(string prefix, string suffix)
        {
            var podcastAction = new AddStringToTitlePodcastAction();
            podcastAction.Actions.Add(new AddStringContentAction(prefix, suffix));
            return podcastAction;
        }
    }
}