using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using static Podfilter.Models.PodcastModification.Others.RemoveDuplicateEpisodesModification;

namespace Podfilter.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToStringWithDeclaration(this XDocument document)
        {
            if(document == null)
                throw new NullReferenceException("The document must not be null.");

            var builder = new StringBuilder();
            using(var writer = new StringWriter(builder))
            {
                document.Save(writer);
            }

            return builder.ToString();
        }

        public static long Week(this DateTime dateTime)
        {
            return (long)TimeSpan.FromTicks(dateTime.Ticks).TotalDays / 7;
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