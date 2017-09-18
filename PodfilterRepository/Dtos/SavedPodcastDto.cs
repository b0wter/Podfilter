using System;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterRepository.Dtos
{
    public class SavedPodcastDto
    {
        public long Id {get;set;}
        public string Url {get;set;}
        public string Method {get;set;}
        public string Type {get;set;}
        public string Argument {get;set;}

        public BasePodcastModification ToModification()
        {
            var modification = (DisplayableBasePodcastModification)Activator.CreateInstance(System.Type.GetType(typeAsString), new object[] { Argument, Method });
            return modification;
        }
    }
}