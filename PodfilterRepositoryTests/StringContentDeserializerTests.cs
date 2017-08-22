using PodfilterRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PodfilterRepositoryTests
{
    public class StringContentDeserializerTests
    {
        [Fact]
        public void DeserializeString_WithValidArgument_ReturnsArgument()
        {
            string toDeserialize = "test";
            var deserializer = new StringContentDeserializer();

            var result = deserializer.DeserializeString(toDeserialize);

            Assert.Equal(toDeserialize, result);
        }

        [Fact]
        public void DeserializeString_WithNullArgument_ThrowsNullArgumentException()
        {
            var deserializer = new StringContentDeserializer();

            Assert.Throws<ArgumentNullException>(() => deserializer.DeserializeString(null));
        }
    }
}
