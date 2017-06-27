using Podfilter.Models.PodcastFilters;
using PodfilterTests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PodfilterTests.Models.PodcastFilters
{
    public class PodcastPublicationDateFilterTests
    {
        [Theory]
        [InlineData(0, long.MaxValue, 20)]                
        [InlineData(31553789759999L, long.MaxValue, 0)]  
        [InlineData(31553789659999L, 31553789659999L, 0)]
        public void FilterPodcast_WithArguments_ReturnsExpected(long min, long max, int expectedCount)
        {
            var filter = PodcastPublicationDateFilter.WithEarlierAndLaterFilter(min, max);

            var podcast = SamplePodcasts.CreateGenericSampleNewsPodcast();
            var filteredPodcast = filter.Filter(podcast);

            Assert.Equal(expectedCount, filteredPodcast.Descendants("item").Count());
        }
    }
}
