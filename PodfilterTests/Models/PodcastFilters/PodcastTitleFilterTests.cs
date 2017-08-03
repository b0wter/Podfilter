using System.Linq;
using Podfilter.Models.ContentFilters;
using Podfilter.Models.PodcastFilters;
using PodfilterTests.Data;
using Xunit;

namespace PodfilterTests.Models.PodcastFilters
{
    public class PodcastTitleFilterTests
    {
        [Fact]
        public void FilterPodcast_WithStringFilter_ReturnsCorrectNumberOfEpisodes()
        {
            var itemFilter = new StringFilter("Nachricht", StringFilter.StringFilterMethod.DoesNotContain, false);
            var podcastFilter = new PodcastTitleFilter();
            var podcast = SamplePodcasts.CreateSampleNewsPodcastForStringFilter();

            podcast = podcastFilter.FilterWithCustomFilter(podcast, itemFilter);

            Assert.Equal(1, podcast.Descendants("item").Count());
        }
    }
}