using System;
using System.Collections.Generic;
using System.Text;
using static PodfilterCore.Models.PodcastModification.RemoveDuplicateEpisodesModification;

namespace PodfilterCore.Helpers
{
    public static class ExtensionsMethods
    {
        public static long Week(this DateTime dateTime)
        {
            var duration = TimeSpan.FromTicks(dateTime.Ticks);
            var weeks = (long)Math.Floor(duration.TotalDays / 7);
            return weeks;
        }

        public static long Week(this DateTime dateTime, DuplicateTimeFrames timeFrame)
        {
            if (timeFrame >= DuplicateTimeFrames.Month)
                return 0;
            else
                return dateTime.Week();
        }
    }
}
