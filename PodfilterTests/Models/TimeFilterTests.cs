using System;
using System.Collections.Generic;
using Podfilter.Models;
using Xunit;

namespace PodfilterTests.Models
{
    public class TimeFilterTests
    {
        public static readonly DateTime EarlyDate = new DateTime(1990, 3, 8);
        public static readonly DateTime MiddleDate = new DateTime(2001, 12, 30);
        public static readonly DateTime LateDate = new DateTime(2017, 10, 5);

        public static readonly TimeSpan ShortDuration = new TimeSpan(0, 5, 0);
        public static readonly TimeSpan MediumDuration = new TimeSpan(1, 1, 1);
        public static readonly TimeSpan LongDuration = new TimeSpan(3, 40, 50);

        private static IEnumerable<object[]> GenerateAllDateTestCombinations()
        {
            return new List<object[]>
            {
                new object[]{MiddleDate, EarlyDate, TimeFilter.TimeFilterMethods.Greater, false},
                new object[]{MiddleDate, MiddleDate, TimeFilter.TimeFilterMethods.Greater, false},
                new object[]{MiddleDate, LateDate, TimeFilter.TimeFilterMethods.Greater, true},

                new object[]{MiddleDate, EarlyDate, TimeFilter.TimeFilterMethods.GreaterEquals, false},
                new object[]{MiddleDate, MiddleDate, TimeFilter.TimeFilterMethods.GreaterEquals, true},
                new object[]{MiddleDate, LateDate, TimeFilter.TimeFilterMethods.GreaterEquals, true},

                new object[]{MiddleDate, EarlyDate, TimeFilter.TimeFilterMethods.Smaller, true},
                new object[]{MiddleDate, MiddleDate, TimeFilter.TimeFilterMethods.Smaller, false},
                new object[]{MiddleDate, LateDate, TimeFilter.TimeFilterMethods.Smaller, false},

                new object[]{MiddleDate, EarlyDate, TimeFilter.TimeFilterMethods.SmallerEquals, true},
                new object[]{MiddleDate, MiddleDate, TimeFilter.TimeFilterMethods.SmallerEquals, true},
                new object[]{MiddleDate, LateDate, TimeFilter.TimeFilterMethods.SmallerEquals, false},
            };
        }

        private static IEnumerable<object[]> GenerateAllTimeTestCombinations()
        {
            return new List<object[]>
            {
                new object[]{ },
            };
        }

        [Fact]
        public void PassesFilter_WithNonDateTime_ThrowInvalidOperationException()
        {
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Greater, MiddleDate);

            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new TimeFilter
            {
                Argument = MiddleDate
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(EarlyDate));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndMethodButNullArgument_ThrowsInvalidOperationException()
        {
            var filter = new TimeFilter
            {
                Method = TimeFilter.TimeFilterMethods.Greater
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(MiddleDate));
        }

        [Theory]
        [MemberData(nameof(GenerateAllDateTestCombinations))]
        public void PassesFilter_WithValidMethodArgumentAndParameter_ReturnsBool(DateTime argument, DateTime toTest,
            TimeFilter.TimeFilterMethods method, bool expected)
        {
            var filter = new TimeFilter(method, argument);
            var result = filter.PassesFilter(toTest);
            
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void PassesFilter_WithEarlierDateAndSmallerMethod_ReturnsTrue()
        {
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Smaller, MiddleDate);
            var result = filter.PassesFilter(EarlyDate);
            
            Assert.True(result);
        }

        [Fact]
        public void PassesFilter_WithLaterDateAndSmallerMethod_ReturnsFalse()
        {
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Smaller, MiddleDate);
            var result = filter.PassesFilter(LateDate);
            
            Assert.False(result);
        }

        [Theory]
        [InlineData("Sat, 10 Jun 2017 10:10:01 +0200", true)]
        [InlineData("10.06.2017", true)]
        [InlineData("2017.06.10", true)]
        public void PassesFilter_WithStringArguments_ReturnsExpected(string toTest, bool expected)
        {
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Greater, MiddleDate);
            var result = filter.PassesFilter(toTest);

            Assert.Equal(expected, result);
        }
    }
}