using System;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Dtos
{
    class EpisodeTitleFilterModificationDto : BasePodcastModificationDto
    {
        public override string FullTypeName => typeof(EpisodeTitleFilterModification).FullName;

        public override BasePodcastModification ToModification()
        {
            throw new NotImplementedException();
        }
    }
}