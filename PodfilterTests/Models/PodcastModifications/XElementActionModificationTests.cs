using FakeItEasy;
using Podfilter.Models.ContentActions;
using Podfilter.Models.PodcastModification;
using System.Xml.Linq;
using Xunit;

namespace PodfilterTests.Models.PodcastModifications
{
    public class XElementActionModificationTests
    {
        [Fact]
        public void Modify_WithNullArgument_ReturnsNull()
        {
            var modification = new XElementActionModification(null);

            var result = modification.Modify(null);

            Assert.Null(result);
        }

        [Fact]
        public void Modify_WithNullArgument_DoesNotCallModify()
        {
            var fakeAction = A.Fake<IContentAction>();
            var modification = new XElementActionModification(fakeAction);

            modification.Modify(null);

            A.CallTo(() => fakeAction.ParseAndModifyContent(null)).MustNotHaveHappened();
        }

        [Fact]
        public void Modify_WithArgument_CallsModify()
        {
            var fakeAction = A.Fake<IContentAction>();
            var modification = new XElementActionModification(fakeAction);
            var fakeElement = A.Fake<XElement>(x => x.WithArgumentsForConstructor(() => new XElement("test")));

            modification.Modify(fakeElement);

            A.CallTo(() => fakeAction.ParseAndModifyContent(fakeElement.Value)).MustHaveHappened();
        }
    }
}