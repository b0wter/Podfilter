using Podfilter.Models.ContentActions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PodfilterTests.Models.ContentActions
{
    public class ReplaceStringContentActionTests
    {
        [Fact]
        public void ModifyContent_WithNullToReplace_ThrowsArgumentException()
        {
            var action = new ReplaceStringContentAction(null, null);

            Assert.Throws<ArgumentException>(() => action.ModifyContent("[Example String]"));
        }

        [Fact]
        public void ModifyContent_WithEmptyToReplace_ThrowsArgumentException()
        {
            var action = new ReplaceStringContentAction("", "{replaced}");

            Assert.Throws<ArgumentException>(() => action.ModifyContent("[Example String]"));
        }

        [Theory]
        [InlineData("Example", "{replaced}", "[Example String]", "[{replaced} String]")]
        [InlineData("Example", "", "[Example String]", "[ String]")]
        public void ModifyContent_WithValidParameters_ReturnsReplacedString(string toReplace, string replaceWith, string toModify, string expected)
        {
            var action = new ReplaceStringContentAction(toReplace, replaceWith);
            var result = action.ModifyContent(toModify);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        public void ModifyContent_WithNullAndEmptyToModify_ThrowsArgumentException(string toModify)
        {
            var action = new ReplaceStringContentAction("Example", "{replaced}");

            Assert.Throws<ArgumentException>(() => action.ModifyContent(toModify));
        }
    }
}
