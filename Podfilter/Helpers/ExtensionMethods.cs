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

            document.Declaration = new XDeclaration("1.0", "utf-8", null);
            StringWriter writer = new Utf8StringWriter();
            document.Save(writer, SaveOptions.None);

            return writer.ToString();
        }
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

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