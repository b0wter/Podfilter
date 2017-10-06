using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterWeb.Helpers;

namespace PodfilterWeb.Models
{
    public class DisplayableRemoveDuplicateEpisodesModification : DisplayableBasePodcastModification<bool>
    {
        public override string Name => "Remove Duplicates";
        public override string Description => $"Discards episodes with the same title and release {Method}.";

        protected DisplayableRemoveDuplicateEpisodesModification()
        {
            // for deserialization
        }

        public DisplayableRemoveDuplicateEpisodesModification(string keepFirst, string timeFrameAsString)
        {
            this.Argument = bool.Parse(keepFirst);
            this.Method = timeFrameAsString;
            CreateModification(keepFirst, timeFrameAsString);
        }

        private void CreateModification(string keepFirst, string timeFrameAsString)
        {
            var boolean = bool.Parse(keepFirst);
            CreateModification(boolean, timeFrameAsString);
        }

        private void CreateModification(bool keepFirst, string timeFrameAsString)
        {
            var timeFrame = (RemoveDuplicateEpisodesModification.DuplicateTimeFrames)Enum.Parse(typeof(RemoveDuplicateEpisodesModification.DuplicateTimeFrames), timeFrameAsString);
            this.Modification = new RemoveDuplicateEpisodesModification(timeFrame);
        }

        protected override void DeserializeModification()
        {
            CreateModification(Argument, Method);
        }

        public static DisplayableRemoveDuplicateEpisodesModification CreateEmptyInstanceForDeserialization()
        {
            return new DisplayableRemoveDuplicateEpisodesModification();
        }

        public override string DescribeWithTranslator(BaseModificationMethodTranslator translator)
        {
            return $"Discards episodes with the same title and release {translator.DuplicateTimeFrameToDisplayString(Method)}.";
        }
    }
}
