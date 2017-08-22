using System;
using PodfilterCore.Models.ContentFilters;
using Xunit;

namespace PodfilterCoreTests.Models.ContentFilters
{
    public class StringFilterTests
    {
        [Fact]
        public void PassesFilter_WithNonString_ThrowArgumentException()
        {
            var filter = new StringFilter("This is a string for testing.", StringFilter.StringFilterMethod.Contains, false);

            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithStringAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new StringFilter();

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(10));
        }

        [Theory]
        [InlineData("This is a string for TESTING.", false, true)]
        [InlineData("This is a string for TESTING.", true, true)]
        [InlineData("this is a string for testing.", false, false)]
        [InlineData("this is a string for testing.", true, true)]
        [InlineData("unknown", false, false)]
        [InlineData("unknown", true, false)]
        public void PassesFilter_Matches_ReturnsTrue(string toTest, bool caseInvariant, bool expected)
        {
            var filter = new StringFilter("This is a string for TESTING.", StringFilter.StringFilterMethod.Matches, caseInvariant);
            var result = filter.PassesFilter(toTest);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("This is a string for TESTING.", false, false)]
        [InlineData("This is a string for TESTING.", true, false)]
        [InlineData("this is a string for testing.", false, true)]
        [InlineData("this is a string for testing.", true, false)]
        [InlineData("unknown", false, true)]
        [InlineData("unknown", true, true)]
        public void PassesFilter_DoesNotMatch_ReturnsTrue(string toTest, bool caseInvariant, bool expected)
        {
            var filter = new StringFilter("This is a string for TESTING.", StringFilter.StringFilterMethod.DoesNotMatch, caseInvariant);
            var result = filter.PassesFilter(toTest);

            Assert.Equal(expected, result);
            
        }

        [Theory]
        [InlineData("String", false, false)]
        [InlineData("String", true, true)]
        [InlineData("string", false, true)]
        [InlineData("string", true, true)]
        [InlineData("unknown", false, false)]
        [InlineData("unknown", true, false)]
        public void PassesFilter_Contains_ReturnsTrue(string argument, bool caseInvariant, bool expected)
        {
            var filter = new StringFilter(argument, StringFilter.StringFilterMethod.Contains, caseInvariant);
            var result = filter.PassesFilter("This is a string for TESTING.");
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("String", false, true)]
        [InlineData("String", true, false)]
        [InlineData("string", false, false)]
        [InlineData("string", true, false)]
        [InlineData("unknown", false, true)]
        [InlineData("unknown", true, true)]
        public void PassesFilter_DoesNotContain_ReturnsTrue(string argument, bool caseInvariant, bool expected)
        {
            var filter = new StringFilter(argument, StringFilter.StringFilterMethod.DoesNotContain, caseInvariant);
            var result = filter.PassesFilter("This is a string for TESTING.");
            
            Assert.Equal(expected, result);
        }
    }
}