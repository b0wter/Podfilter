using System;
using Podfilter.Models;
using Xunit;

namespace PodfilterTests.Models
{
    public class TimeFilterTests
    {
        [Fact]
        public void PassesFilter_WithNonDateTime_ThrowInvalidOperationException()
        {
            var filter = new TimeFilter(TimeFilter.FilterMethods.Greater, new DateTime(2000, 1, 1));

            Assert.Throws<ArgumentException>(() => filter.PassesFilter(new object()));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndNullMethod_ThrowsInvalidOperationException()
        {
            var filter = new TimeFilter
            {
                Argument = new DateTime(2000, 1, 1)
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(new DateTime(2000, 1, 1)));
        }

        [Fact]
        public void PassesFilter_WithDateTimeAndMethodButNullArgument_ThrowsInvalidOperationException()
        {
            var filter = new TimeFilter
            {
                Method = TimeFilter.FilterMethods.Greater
            };

            Assert.Throws<InvalidOperationException>(() => filter.PassesFilter(new DateTime(2000, 1, 1)));
        }
    }
}