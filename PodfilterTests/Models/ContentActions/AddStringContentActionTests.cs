using PodfilterCore.Models.ContentActions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PodfilterCoreTests.Models.ContentActions
{
    public class AddStringContentActionTests
    {
        [Fact]
        public void ModifyContent_WithNullArgument_ReturnsNull()
        {
            var action = new AddStringContentAction(null, null);
            var result = action.ModifyContent(null);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("", "", "[Example String]", "[Example String]")]
        [InlineData("[Prefix]", "", "[Example String]", "[Prefix][Example String]")]
        [InlineData("", "[Suffix]", "[Example String]", "[Example String][Suffix]")]
        [InlineData("[Prefix]", "[Suffix]", "[Example String]", "[Prefix][Example String][Suffix]")]
        public void ModifyContent_WithValidArgument_AddsStrings(string prefix, string suffix, string toModify, string expected)
        {
            var action = new AddStringContentAction(prefix, suffix);
            var result = action.ModifyContent(toModify);

            Assert.Equal(expected, result);
        }
    }
}
