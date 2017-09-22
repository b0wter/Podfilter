using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Dtos
{
    abstract class BasePodcastModificationDto
    {
        public string FirstArgument { get; set; }
        public string SecondArgument { get; set; }
        public abstract string FullTypeName { get;}
        public abstract BasePodcastModification ToModification();
    }
}
