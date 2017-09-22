using System;
using System.Collections.Generic;
using System.Text;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Dtos
{
    class EpisodeDescriptionFilterModificationDto : BasePodcastModificationDto
    {
        public override string FullTypeName => typeof(EpisodeDescriptionFilterModification).FullName;

        public override BasePodcastModification ToModification()
        {
            throw new NotImplementedException();
        }
    }
}
