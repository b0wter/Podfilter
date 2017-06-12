using System.Xml.Linq;
using Podfilter.Models;
using Xunit;
using System.Linq;
using PodfilterTests.Data;

namespace PodfilterTests.Models
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