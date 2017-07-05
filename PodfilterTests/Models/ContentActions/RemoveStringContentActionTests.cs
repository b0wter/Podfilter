using Podfilter.Models.ContentActions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PodfilterTests.Models.ContentActions
{
    public class RemoveStringContentActionTests
    {
        [Fact]
        public void ModifyContent_WithNullParameter_ThrowsArgumentException()
        {
            var action = new RemoveStringContentAction(null);

            Assert.Throws<ArgumentException>(() => action.ModifyContent("[Example String]"));
        }

        [Fact]
        public void ModifyContent_WithEmptyToReplace_ThrowsArgumentException()
        {
            var action = new RemoveStringContentAction("");

            Assert.Throws<ArgumentException>(() => action.ModifyContent("[Example String]"));
        }

        [Theory]
        [InlineData("Example", "[Example String]", "[ String]")]
        [InlineData("NotIncludedInModify", "[Example String]", "[Example String]")]
        public void ModifyContent_WithValid_ReturnsString(string toRemove, string toModify, string expected)
        {
            var action = new RemoveStringContentAction(toRemove);
            var result = action.ModifyContent(toModify);

            Assert.Equal(expected, result);
        }
    }
}
