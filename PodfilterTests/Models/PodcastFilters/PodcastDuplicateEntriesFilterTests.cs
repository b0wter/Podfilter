using Podfilter.Models;
using PodfilterTests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PodfilterTests.Models
{
    public class PodcastDuplicateEntriesFilterTests
    {
        [Fact]
        public void FilterPodcast_WithDuplicateEntriesPodcast_RemovesDuplicates()
        {
            var duplicatesFilter = new PodcastDuplicateEntriesFilter();
            var podcast = SamplePodcasts.CreateSampleNewsPodcastForStringFilter();

            podcast = duplicatesFilter.Filter(podcast);

            Assert.Equal(2, podcast.Descendants("item").Count());
        }

        [Fact]
        public void FilterPodcast_WithDuplicateEntriesPodcast_SelectsEarliestItem()
        {
            var duplicatesFilter = new PodcastDuplicateEntriesFilter();
            var podcast = SamplePodcasts.CreateSampleNewsPodcastForStringFilter();

            var groupedItems = podcast.Descendants("item").GroupBy(i => i.Element("title"));

            var earliestDates = new Dictionary<DateTime, string>();
            //TODO: fix possible crash with podcast that contains elements with the same pubdate but different titles
            foreach(var group in groupedItems)
            {
                var sortedGroup = group.OrderByDescending(g => g.Element("pubDate"));
                earliestDates.Add(DateTime.Parse(sortedGroup.First().Element("pubDate").Value), sortedGroup.First().Element("title").Value);
            }

            podcast = duplicatesFilter.Filter(podcast);

            foreach(var item in podcast.Descendants("item"))
            {
                var title = item.Element("title").Value;
                var pubDate = DateTime.Parse(item.Element("pubDate").Value);

                Assert.True(earliestDates.ContainsKey(pubDate));
                Assert.Equal(earliestDates[pubDate], title);

            }
        }
    }
}
