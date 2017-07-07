using System.Linq;
using Podfilter.Models.PodcastActions;
using Xunit;
using Podfilter.Helpers;
    
namespace PodfilterTests.Models.PodcastAction
{
    public class AddStringToTitlePodcastActionTests
    {
        [Theory]
        [InlineData("[Prefix]", null, "[Prefix]Presseschau - Deutschlandfunk")]
        [InlineData(null, "[Suffix]", "Presseschau - Deutschlandfunk[Suffix]")]
        [InlineData("[Prefix]", "[Suffix]", "[Prefix]Presseschau - Deutschlandfunk[Suffix]")]
        public void PerformAction_WithValidParameters_ReturnsExpected(string prefix, string suffix, string expected)
        {
            var action = AddStringToTitlePodcastAction.WithPrefixAndSuffix(prefix, suffix);
            var podcast = Data.SamplePodcasts.CreateGenericSampleNewsPodcast();

            var result = action.PerformAction(podcast);

            Assert.Equal(expected, result.Descendants("title").First().Value);
        }
    }
}