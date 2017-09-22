using System;
using System.Collections.Generic;
using System.Text;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Dtos
{
    class EpisodePublishDateFilterModificationDto : BasePodcastModificationDto
    {
        public override string FullTypeName => typeof(EpisodePublishDateFilterModification).FullName;

        public override BasePodcastModification ToModification()
        {
            throw new NotImplementedException();
        }
    }
}
