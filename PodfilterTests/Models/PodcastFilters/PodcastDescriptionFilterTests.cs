using Podfilter.Models.ContentFilter;
using Podfilter.Models.PodcastFilters.PodcastFilters;
using PodfilterTests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PodfilterTests.Models.PodcastFilters
{
    public class PodcastDescriptionFilterTests
    {
        [Theory]
        [InlineData(StringFilter.StringFilterMethod.Contains, 1)]
        [InlineData(StringFilter.StringFilterMethod.Matches, 0)]
        [InlineData(StringFilter.StringFilterMethod.DoesNotContain, 10)]
        public void PodcastDescriptionFilter_TestWithDifferentMethods_ReturnsFiltered(StringFilter.StringFilterMethod method, int expectedHits)
        {
            var filter = new PodcastDescriptionFilter
            {
                Filters = new List<IContentFilter> { new StringFilter(":)", method, true) }
            };

            var podcast = SamplePodcasts.CreateSampleNewsPodcastForDescriptionFilter();

            var filteredPodcast = filter.Filter(podcast);

            Assert.Equal(expectedHits, filteredPodcast.Descendants("item").Count());
        }
    }
}
