using System;
using System.Collections.Generic;
using Podfilter.Models;
using Xunit;

namespace PodfilterTests.Models
{
    public class TimeFilterTestsDataGenerator : IEnumerable<object[]>
    {
        private static IEnumerable<object[]> AllMethodArgumentAndParameterCombinations
        {
            get
            {
                foreach (var set in GenerateAllCombinations())
                    yield return set;
            }
        }

        private static IEnumerable<object[]> GenerateAllCombinations()
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
        
        public static readonly DateTime EarlyDate = new DateTime(1990, 3, 8);
        public static readonly DateTime MiddleDate = new DateTime(2001, 12, 30);
        public static readonly DateTime LateDate = new DateTime(2017, 10, 5);        
    }
    
    public class TimeFilterTests
    {
        [Fact]
        public void PassesFilter_WithNonDateTime_ThrowInvalidOperationException()
        {
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Greater, TimeFilterTestsDataGenerator.MiddleDate);

            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new TimeFilter
            {
                Argument = TimeFilterTestsDataGenerator.MiddleDate
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(TimeFilterTestsDataGenerator.EarlyDate));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndMethodButNullArgument_ThrowsInvalidOperationException()
        {
            var filter = new TimeFilter
            {
                Method = TimeFilter.TimeFilterMethods.Greater
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(TimeFilterTestsDataGenerator.MiddleDate));
        }

        [Theory]
        [MemberData(typeof())]
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
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Smaller, TimeFilterTestsDataGenerator.MiddleDate);
            var result = filter.PassesFilter(TimeFilterTestsDataGenerator.EarlyDate);
            
            Assert.True(result);
        }

        [Fact]
        public void PassesFilter_WithLaterDateAndSmallerMethod_ReturnsFalse()
        {
            var filter = new TimeFilter(TimeFilter.TimeFilterMethods.Smaller, TimeFilterTestsDataGenerator.MiddleDate);
            var result = filter.PassesFilter(TimeFilterTestsDataGenerator.LateDate);
            
            Assert.False(result);
        }
    }
}