using System;
using System.Collections.Generic;
using Podfilter.Models;
using Xunit;

namespace PodfilterTests.Models
{
    public class DateFilterTests
    {
        public static readonly DateTime EarlyDate = new DateTime(1990, 3, 8);
        public static readonly DateTime MiddleDate = new DateTime(2001, 12, 30);
        public static readonly DateTime LateDate = new DateTime(2017, 10, 5);

        private static IEnumerable<object[]> GenerateAllDateTestCombinations()
        {
            return new List<object[]>
            {
                new object[]{MiddleDate, EarlyDate, DateFilter.DateFilterMethods.Greater, false},
                new object[]{MiddleDate, MiddleDate, DateFilter.DateFilterMethods.Greater, false},
                new object[]{MiddleDate, LateDate, DateFilter.DateFilterMethods.Greater, true},

                new object[]{MiddleDate, EarlyDate, DateFilter.DateFilterMethods.GreaterEquals, false},
                new object[]{MiddleDate, MiddleDate, DateFilter.DateFilterMethods.GreaterEquals, true},
                new object[]{MiddleDate, LateDate, DateFilter.DateFilterMethods.GreaterEquals, true},

                new object[]{MiddleDate, EarlyDate, DateFilter.DateFilterMethods.Smaller, true},
                new object[]{MiddleDate, MiddleDate, DateFilter.DateFilterMethods.Smaller, false},
                new object[]{MiddleDate, LateDate, DateFilter.DateFilterMethods.Smaller, false},

                new object[]{MiddleDate, EarlyDate, DateFilter.DateFilterMethods.SmallerEquals, true},
                new object[]{MiddleDate, MiddleDate, DateFilter.DateFilterMethods.SmallerEquals, true},
                new object[]{MiddleDate, LateDate, DateFilter.DateFilterMethods.SmallerEquals, false},
            };
        }

        [Fact]
        public void PassesFilter_WithNonDateTime_ThrowInvalidOperationException()
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.Greater, MiddleDate);
                       
            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new DateFilter
            {
                Argument = MiddleDate
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(EarlyDate));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndMethodButNullArgument_ThrowsInvalidOperationException()
        {
            var filter = new DateFilter
            {
                Method = DateFilter.DateFilterMethods.Greater
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(MiddleDate));
        }

        [Theory]
        [MemberData(nameof(GenerateAllDateTestCombinations))]
        public void PassesFilter_WithValidMethodArgumentAndParameter_ReturnsBool(DateTime argument, DateTime toTest,
            DateFilter.DateFilterMethods method, bool expected)
        {
            var filter = new DateFilter(method, argument);
            var result = filter.PassesFilter(toTest);
            
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void PassesFilter_WithEarlierDateAndSmallerMethod_ReturnsTrue()
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.Smaller, MiddleDate);
            var result = filter.PassesFilter(EarlyDate);
            
            Assert.True(result);
        }

        [Fact]
        public void PassesFilter_WithLaterDateAndSmallerMethod_ReturnsFalse()
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.Smaller, MiddleDate);
            var result = filter.PassesFilter(LateDate);
            
            Assert.False(result);
        }

        [Theory]
        [InlineData("Sat, 10 Jun 2017 10:10:01 +0200", true)]
        [InlineData("10.06.2017", true)]
        [InlineData("2017.06.10", true)]
        public void PassesFilter_WithStringArguments_ReturnsExpected(string toTest, bool expected)
        {
            var filter = new DateFilter(DateFilter.DateFilterMethods.Greater, MiddleDate);
            var result = filter.PassesFilter(toTest);

            Assert.Equal(expected, result);
        }
    }
}