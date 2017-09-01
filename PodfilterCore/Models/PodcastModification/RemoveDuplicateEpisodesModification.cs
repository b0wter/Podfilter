using PodfilterCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodfilterCore.Models.PodcastModification.Others
{
    public class RemoveDuplicateEpisodesModification : BasePodcastModification
    {
        /// <summary>
        /// Removes duplicate episodes from a podcast using episode titles and pub dates.
        /// </summary>
        /// <remarks>
        /// Setting this option to false may cause a lot of issues with any podcast aggregator since the feed will be updated continiously.
        /// </remarks>
        public bool KeepFirstEpisode { get; private set; } = true;

        /// <summary>
        /// Timeframe that is used to check wether an episode is a duplicate.
        /// </summary>
        public DuplicateTimeFrames TimeFrame { get; private set; }

        public RemoveDuplicateEpisodesModification(bool keepFirstEpisode, DuplicateTimeFrames timeFrame)
        {
            KeepFirstEpisode = keepFirstEpisode;
            TimeFrame = timeFrame;
        }

        public RemoveDuplicateEpisodesModification(bool keepFirstEpisode) 
            : this(keepFirstEpisode, DuplicateTimeFrames.Day)
        {
            //
        }

        public RemoveDuplicateEpisodesModification(DuplicateTimeFrames timeFrame) 
            : this(true, timeFrame)
        {
            //
        }

        public RemoveDuplicateEpisodesModification()
            : this(true, DuplicateTimeFrames.Day)
        {
            //
        }

        /// <summary>
        /// Removes duplicate episodes from a podcast using the <see cref="TimeFrame"/> and the episode title.
        /// </summary>
        /// <param name="podcast"></param>
        public override void Modify(XDocument podcast)
        {
            if (podcast == null)
                throw new ArgumentNullException("podcast");

            // create new tuples for easier access to the properites pubDate and title
            var dateTimeItems = podcast.Descendants("item")
                .Select(item => new PodcastTriple(
                    item.Elements("title").First().Value,
                    DateTime.Parse(item.Elements("pubDate").First().Value),
                    item,
                    TimeFrame));

            var groupedTriples = dateTimeItems.GroupBy(item => item.TimeFramePubDateString);

            foreach(var group in groupedTriples)
            {
                var orderedGroup = group.OrderBy(g => g.PubDate);
                orderedGroup.Skip(1)?.ToList().ForEach(element => element.XElement.Remove());
            }
        }


        private class PodcastTriple
        {
            public string Title { get; }
            public DateTime PubDate { get; }
            public long PubWeek { get; set; }
            public XElement XElement { get; }
            public DuplicateTimeFrames TimeFrame { get; }
            /// <summary>
            /// Creates a string representation of the publication date that includes the week number. E.g.: 2017|08|105222|10|18 (year, month, week, day, hour).
            /// </summary>
            public string TotalPubDateString { get; }
            /// <summary>
            /// Cuts the <see cref="TotalPubDateString"/> so that only the significant parts remain (according to the <see cref="TimeFrame"/>).
            /// </summary>
            public string TimeFramePubDateString { get; }

            public PodcastTriple(string title, DateTime pubDate, XElement xElement, RemoveDuplicateEpisodesModification.DuplicateTimeFrames timeFrame)
            {
                this.Title = title;
                this.PubDate = pubDate;
                this.XElement = xElement;
                this.PubWeek = pubDate.Week(timeFrame);
                TotalPubDateString = CreateDateString(pubDate);
                TimeFramePubDateString = CreateTimeFramePubDateStringFromDateString(TotalPubDateString, timeFrame);
            }

            private string CreateDateString(DateTime date)
            {
                var dateAsString = $"{date.Year.ToString("00")}{date.Month.ToString("00")}{date.Week().ToString("000000")}{date.Day.ToString("00")}{date.Hour.ToString("00")}";
                return dateAsString;
            }

            private string CreateTimeFramePubDateStringFromDateString(string totalDateString, DuplicateTimeFrames timeFrame)
            {
                // two actions required:
                // 1. cut the last digits, according to the time frame
                // 2. replace the month with 00 if the format is weeks (because they can stretch over months)
                var shortString = new string(totalDateString.Reverse().Skip((int)timeFrame).Reverse().ToArray());
                if (timeFrame == DuplicateTimeFrames.Week)
                    shortString = shortString.Remove(4, 2).Insert(4, "00");
                return shortString;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public enum DuplicateTimeFrames
        {
            Hour = 0,
            Day = 2,
            Week = 4,
            Month = 10
        }
    }
}
