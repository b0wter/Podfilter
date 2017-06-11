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
        public void FilterPodcast_WithStringFilter_ReturnsTrue()
        {
            var itemFilter = new StringFilter("Nachricht", StringFilter.StringFilterMethod.DoesNotContain, false);
            var podcastFilter = new PodcastTitleFilter();
            var podcast = SamplePodcasts.CreateSampleNewsPodcast();

            podcast = podcastFilter.FilterPodcast(podcast, itemFilter);

            Assert.Equal(0, podcast.Descendants("item").Count());
        }
    }
}