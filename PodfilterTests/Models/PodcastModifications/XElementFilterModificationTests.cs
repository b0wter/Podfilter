using System;
using System.Xml.Linq;
using FakeItEasy;
using Podfilter.Models.ContentFilters;
using Podfilter.Models.PodcastModification;
using Xunit;

namespace PodfilterTests.Models.PodcastModifications
{
    public class XElementFilterModificationTests
    {
        [Fact]
        public void Modify_WithNullArgument_ThrowsArgumentNullException()
        {
            var fakeFilter = A.Fake<IContentFilter>();
            var filterModification = new XElementFilterModification(fakeFilter);
            
            Assert.Throws<ArgumentNullException>(() => filterModification.Modify(null));
        }

        [Fact]
        public void Modify_WithMatchingElement_ReturnsElement()
        {
            var fakeElement = A.Fake<XElement>();
            var fakeFilter = A.Fake<IContentFilter>();
            A.CallTo(() => fakeFilter.PassesFilter(fakeElement)).Returns(true);
            var modification = new XElementFilterModification(fakeFilter);

            var result = modification.Modify(fakeElement);

            Assert.Same(result, fakeElement);
        }

        [Fact]
        public void Modify_WithNonMatchingElement_ReturnsNullAndRemovesFromParent()
        {
            
        }
    }
}