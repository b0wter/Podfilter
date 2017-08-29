﻿using PodfilterCore.Models.PodcastModification.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodfilterWeb.Models
{
    public class DisplayableRemoveDuplicateEpisodesModification : DisplayableBasePodcastModification<bool>
    {
        public override string Name => "Remove Duplicates";
        public override string Description => $"Discards episodes with the same title and release {Method}.";

        public bool KeepFirst { get; set; } = true;

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
            var modification = new RemoveDuplicateEpisodesModification(timeFrame);
        }

        protected override void DeserializeModification()
        {
            CreateModification(KeepFirst, Method);
        }

        public override string ToQueryString()
        {
            return $"removeDuplicates={Method}";
        }

        public static DisplayableRemoveDuplicateEpisodesModification CreateEmptyInstanceForDeserialization()
        {
            return new DisplayableRemoveDuplicateEpisodesModification();
        }
    }
}