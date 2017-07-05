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
    }
}
