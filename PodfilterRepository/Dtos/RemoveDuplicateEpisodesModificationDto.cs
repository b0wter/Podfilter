using System;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Dtos
{
    class RemoveDuplicateEpisodesModificationDto : BasePodcastModificationDto
    {
        public override string FullTypeName => typeof(RemoveDuplicateEpisodesModification).FullName;

        public override BasePodcastModification ToModification()
        {
            throw new NotImplementedException();
        }
    }
}