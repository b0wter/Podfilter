using System;
using System.Collections.Generic;
using System.Text;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Dtos
{
    class EpisodeDurationFilterModificationDto : BasePodcastModificationDto
    {
        public override string FullTypeName => typeof(EpisodeDurationFilterModification).FullName;

        public override BasePodcastModification ToModification()
        {
            throw new NotImplementedException();
        }
    }
}
