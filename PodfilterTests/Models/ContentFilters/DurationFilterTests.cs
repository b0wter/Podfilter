using System;
using System.Collections.Generic;
using Podfilter.Models.ContentFilters;
using Xunit;

namespace PodfilterTests.Models.ContentFilters
{
    public class DurationFilterTests
    {
        public static readonly TimeSpan ShortDuration = new TimeSpan(0, 5, 0);
        public static readonly TimeSpan MediumDuration = new TimeSpan(1, 1, 1);
        public static readonly TimeSpan LongDuration = new TimeSpan(3, 40, 50);

        private static IEnumerable<object[]> GenerateAllTimeTestCombinations()
        {
            return new List<object[]>
            {
                new object[]{ MediumDuration, ShortDuration, DurationFilter.DurationFilterMethods.Equals, false },
                new object[]{ MediumDuration, MediumDuration, DurationFilter.DurationFilterMethods.Equals, true },
                new object[]{ MediumDuration, LongDuration, DurationFilter.DurationFilterMethods.Equals, false },

                new object[]{ MediumDuration, ShortDuration, DurationFilter.DurationFilterMethods.Greater, false },
                new object[]{ MediumDuration, MediumDuration, DurationFilter.DurationFilterMethods.Greater, false },
                new object[]{ MediumDuration, LongDuration, DurationFilter.DurationFilterMethods.Greater, true },

                new object[]{ MediumDuration, ShortDuration, DurationFilter.DurationFilterMethods.GreaterEquals, false },
                new object[]{ MediumDuration, MediumDuration, DurationFilter.DurationFilterMethods.GreaterEquals, true },
                new object[]{ MediumDuration, LongDuration, DurationFilter.DurationFilterMethods.GreaterEquals, true },

                new object[]{ MediumDuration, ShortDuration, DurationFilter.DurationFilterMethods.Smaller, true },
                new object[]{ MediumDuration, MediumDuration, DurationFilter.DurationFilterMethods.Smaller, false },
                new object[]{ MediumDuration, LongDuration, DurationFilter.DurationFilterMethods.Smaller, false },

                new object[]{ MediumDuration, ShortDuration, DurationFilter.DurationFilterMethods.SmallerEquals, true },
                new object[]{ MediumDuration, MediumDuration, DurationFilter.DurationFilterMethods.SmallerEquals, true },
                new object[]{ MediumDuration, LongDuration, DurationFilter.DurationFilterMethods.SmallerEquals, false },
            };
        }

        [Theory]
        [MemberData(nameof(GenerateAllTimeTestCombinations))]
        public void PassesFilter_WithValidMethodAndArgumentAndParameter_ReturnsBool(TimeSpan argument, TimeSpan toTest,
            DurationFilter.DurationFilterMethods method, bool expected)
        {
            var filter = new DurationFilter(method, argument);
            var result = filter.PassesFilter(toTest);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PassesFilter_WithNonDateTime_ThrowInvalidOperationException()
        {
            var filter = new DurationFilter(DurationFilter.DurationFilterMethods.Greater, MediumDuration);

            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new DurationFilter
            {
                Argument = MediumDuration
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(ShortDuration));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndMethodButNullArgument_ThrowsInvalidOperationException()
        {
            var filter = new DurationFilter
            {
                Method = DurationFilter.DurationFilterMethods.Greater
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(MediumDuration));
        }
    }
}
