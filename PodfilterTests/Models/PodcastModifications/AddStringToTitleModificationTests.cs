using System.Linq;
using Podfilter.Models.PodcastModification;
using Xunit;

namespace PodfilterTests.Models.PodcastModifications
{
    public class AddStringToTitleModificationTests
    {
        [Theory]
        [InlineData("[Prefix]", null, "[Prefix]Presseschau - Deutschlandfunk")]
        [InlineData("[Prefix]", "", "[Prefix]Presseschau - Deutschlandfunk")]
        [InlineData(null, "[Suffix]", "Presseschau - Deutschlandfunk[Suffix]")]
        [InlineData("", "[Suffix]", "Presseschau - Deutschlandfunk[Suffix]")]
        [InlineData("[Prefix]", "[Suffix]", "[Prefix]Presseschau - Deutschlandfunk[Suffix]")]
        public void Modify_WithValidParameters_ReturnsExpected(string prefix, string suffix, string expected)
        {
            var modfication = new AddStringToTitleModification(prefix, suffix);
            var podcast = Data.SamplePodcasts.CreateGenericSampleNewsPodcast();

            modfication.Modify(podcast);
            
            Assert.Equal(expected, podcast.Descendants("title").First().Value);
        }
    }
}