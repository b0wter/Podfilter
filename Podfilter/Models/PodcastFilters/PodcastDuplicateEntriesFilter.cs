using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Podfilter.Helpers;
using Podfilter.Models.ContentFilter;

namespace Podfilter.Models.PodcastFilters
{
    /// <summary>
    /// Filter that removes duplicate entries from a podcast feed. Duplicates are entries that share the same
    /// title and pubDate (only checks the date, not the time). Keeps the first element.
    /// </summary>
    public class PodcastDuplicateEntriesFilter : PodcastFilter
    {
        public override string Description => "Filters any duplicate items in the feed. Duplicates are items that share the same publication date (not time) and title. Keeps the oldest item in the feed since keeping the new item will most likely result in problems with your podcast app.";

        public override XDocument Filter(XDocument podcast)
        {
            // doesnt require the usage of filters

            var grouped = podcast.Descendants("item").GroupBy(item => DateTime.Parse(item.Elements("pubDate").First().Value));

            // create new tuples for easier access to the properites pubDate and title
            var dateTimeItems = podcast.Descendants("item")
                .Select(item => new PodcastTriple(
                    item.Elements("title").First().Value,
                    DateTime.Parse(item.Elements("pubDate").First().Value), 
                    item));

            // group the items by year, month and day
            var groupedDateTimeItems =
                dateTimeItems.GroupBy(item => new DateTime(item.PubDate.Year, item.PubDate.Month, item.PubDate.Day));

            // remove any items that share group and title
            foreach (var group in groupedDateTimeItems)
            {
                var titleGroups = group.GroupBy(triple => triple.Title);

                foreach (var tGroup in titleGroups)
                {
                    var orderedGroup = tGroup.OrderBy(triple => triple.PubDate);
                    orderedGroup.Skip(1)?.ToList().ForEach(g => g.XElement.Remove());
                }
            }

            return podcast;
        }

        public override XDocument FilterWithCustomFilters(XDocument podcast, IEnumerable<IContentFilter> filters)
        {
            return Filter(podcast);
        }

        public override void ValidateIFilterTypeMatchesContent(IEnumerable<IContentFilter> filters)
        {
            // no logic required for this filter
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

            public PodcastTriple(string title, string pubDate, XElement xElement)
                : this(title, DateTime.Parse(pubDate), xElement)
            {
                //
            }
        }
    }
}