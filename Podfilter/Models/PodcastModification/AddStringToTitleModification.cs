using Podfilter.Models.ContentActions;

namespace Podfilter.Models.PodcastModification
{
    public class AddStringToTitleModification : BaseModification
    {
        public AddStringToTitleModification(string prefix = null, string suffix = null) 
            : base(
                "//channel/title", 
                new XElementActionModification(new AddStringContentAction(prefix, suffix)))
        {
            //
        }
    }
}