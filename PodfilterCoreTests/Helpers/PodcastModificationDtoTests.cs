using PodfilterRepository.Helpers;
using PodfilterRepository.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PodfilterCoreTests.Helpers
{
    public class PodcastModificationDtoTests
    {
        [Fact]
        public void ToModification_WithValidArgument_ReturnsInstantiatedModification()
        {
            var dto = new PodcastModificationDto
            {
                Argument = "argument",
                Method = "DoesNotContain",
                Type = "PodfilterCore.Models.PodcastModification.EpisodeTitleFilterModification",
            };

            var discriminator = new ModificationTypeDiscriminator();
            dto.ToModification(discriminator);
        }
    }
}
