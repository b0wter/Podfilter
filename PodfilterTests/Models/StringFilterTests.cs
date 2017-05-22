using Podfilter.Models;
using System;
using Xunit;

namespace PodfilterTests.Models
{
    public class StringFilterTests
    {
        private const string long_string = "This is a string for TESTING.";
        private const string long_string_lower = "this is a string for testing.";

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
        // matches
        [InlineData(long_string_lower, StringFilter.StringFilterMethod.Matches, false, false)]
        [InlineData(long_string_lower, StringFilter.StringFilterMethod.Matches, true, false)]
        [InlineData(long_string, StringFilter.StringFilterMethod.Matches, false, true)]
        [InlineData(long_string, StringFilter.StringFilterMethod.Matches, true, true)]
        // does not match
        [InlineData(long_string_lower, StringFilter.StringFilterMethod.Matches, false, true)]
        [InlineData(long_string_lower, StringFilter.StringFilterMethod.Matches, true, true)]
        [InlineData(long_string, StringFilter.StringFilterMethod.Matches, false, false)]
        [InlineData(long_string, StringFilter.StringFilterMethod.Matches, true, false)]
        // contains
        [InlineData("String", StringFilter.StringFilterMethod.Contains, false, true)]
        [InlineData("String", StringFilter.StringFilterMethod.Contains, true, false)]
        [InlineData("unknown", StringFilter.StringFilterMethod.Contains, false, false)]
        [InlineData("unknown", StringFilter.StringFilterMethod.Contains, true, false)]
        // does not contain
        public void PassesFilter_WithStringAndMethod_ReturnsTrue(string toTest, StringFilter.StringFilterMethod method, bool caseInvariant, bool expected)
        {
            var filter = new StringFilter("This is a string for TESTING.", method, caseInvariant);
            var result = filter.PassesFilter(toTest);

            Assert.Equal(expected, result);
        }
    }
}