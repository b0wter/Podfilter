using System;
using PodfilterCore.Models.ContentFilters;
using Xunit;

namespace PodfilterCoreTests.Models.ContentFilters
{
    public class IntFilterTests
    {
        [Fact]
        public void PassesFilter_WithNonInt_ThrowArgumentException()
        {
            var filter = new IntFilter(IntFilter.IntFilterMethods.Equal, 10);

            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithIntAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new IntFilter();

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(10));
        }

        [Theory]
        // Equal
        [InlineData(9,  IntFilter.IntFilterMethods.Equal, false)]
        [InlineData(10, IntFilter.IntFilterMethods.Equal, true)]
        [InlineData(11, IntFilter.IntFilterMethods.Equal, false)]
        // Greater
        [InlineData(9,  IntFilter.IntFilterMethods.Greater, false)]
        [InlineData(10, IntFilter.IntFilterMethods.Greater, false)]
        [InlineData(11, IntFilter.IntFilterMethods.Greater, true)]
        // GreaterEquals
        [InlineData(9,  IntFilter.IntFilterMethods.GreaterEquals, false)]
        [InlineData(10, IntFilter.IntFilterMethods.GreaterEquals, true)]
        [InlineData(11, IntFilter.IntFilterMethods.GreaterEquals, true)]
        // Smaller
        [InlineData(9,  IntFilter.IntFilterMethods.Smaller, true)]
        [InlineData(10, IntFilter.IntFilterMethods.Smaller, false)]
        [InlineData(11, IntFilter.IntFilterMethods.Smaller, false)]
        // SmallerEquals
        [InlineData(9,  IntFilter.IntFilterMethods.SmallerEquals, true)]
        [InlineData(10, IntFilter.IntFilterMethods.SmallerEquals, true)]
        [InlineData(11, IntFilter.IntFilterMethods.SmallerEquals, false)]
        // Unequal
        [InlineData(9,  IntFilter.IntFilterMethods.Unequal, true)]
        [InlineData(10, IntFilter.IntFilterMethods.Unequal, false)]
        [InlineData(11, IntFilter.IntFilterMethods.Unequal, true)]
        public void PassesFilter_WithIntAndMethod_ReturnsTrue(int toTest, IntFilter.IntFilterMethods method, bool expected)
        {
            var filter = new IntFilter(method, 10);
            var result = filter.PassesFilter(toTest);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PassesFilter_WithValidStringAndMethod_ReturnsTrue()
        {
            var filter = new IntFilter(IntFilter.IntFilterMethods.Greater, 10);
            var result = filter.PassesFilter("20");

            Assert.True(result);
        }

        [Fact]
        public void PassesFilter_WithInvalidStringAndMethod_ThrowsFormatException()
        {
            var filter = new IntFilter(IntFilter.IntFilterMethods.Greater, 10);

            Assert.Throws<FormatException>(() => filter.PassesFilter("not_a_number"));
        }
    }
}