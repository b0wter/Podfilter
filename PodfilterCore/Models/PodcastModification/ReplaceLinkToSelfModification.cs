using System;
using PodfilterCore.Models.ContentActions;

namespace PodfilterCore.Models.PodcastModification.Actions
{
    public class ReplaceLinkToSelfModification : BasePodcastElementModification
    {
        public ReplaceLinkToSelfModification(string newUrl)
            : base( new XPathPodcastElementProvider("//channel/atom:link"),
                    new XElementActionModification(new ReplaceExistingStringContentAction(newUrl), "href"))
        {
            //
        }
    }
}