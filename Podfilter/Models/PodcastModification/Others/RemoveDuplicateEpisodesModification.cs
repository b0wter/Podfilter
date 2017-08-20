using Podfilter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastModification.Others
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

            var grouped = podcast.Descendants("item").GroupBy(item => DateTime.Parse(item.Elements("pubDate").First().Value));

            // create new tuples for easier access to the properites pubDate and title
            var dateTimeItems = podcast.Descendants("item")
                .Select(item => new PodcastTriple(
                    item.Elements("title").First().Value,
                    DateTime.Parse(item.Elements("pubDate").First().Value),
                    item));

            var groupedDateTimeItems = dateTimeItems.GroupBy(
                item => new DateTime(
                    item.PubDate.Year, 
                    item.PubDate.Month, 
                    TimeFrame < DuplicateTimeFrames.Month ? item.PubDate.Day : 0, 
                    TimeFrame < DuplicateTimeFrames.Day ? item.PubDate.Hour : 0,
                    0, 0));

            foreach(var group in groupedDateTimeItems)
            {
                var titleGroups = group.GroupBy(item => item.Title);

                foreach(var tGroup in titleGroups)
                {
                    var orderedGroup = tGroup.OrderBy(t => t.PubDate.Year).ThenBy(t => t.PubDate.Month).ThenBy(t => t.PubDate.Week(TimeFrame)).ThenBy(t => t.PubDate.Day).ThenBy(t => t.PubDate.Hour);
                    orderedGroup.Skip(1)?.ToList().ForEach(g => g.XElement.Remove());
                }
            } 
        }

        private class PodcastTriple
        {
            public string Title { get; set; }
            public DateTime PubDate { get; set; }
            public XElement XElement { get; set; }

            public PodcastTriple(string title, DateTime pubDate, XElement xElement)
            {
                this.Title = title;
                this.PubDate = pubDate;
                this.XElement = xElement;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public enum DuplicateTimeFrames
        {
            Hour = 0,
            Day = 1,
            Week = 2,
            Month = 3
        }
    }
}
