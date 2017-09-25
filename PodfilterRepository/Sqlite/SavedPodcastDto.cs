using PodfilterCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PodfilterRepository.Dtos;

namespace PodfilterRepository.Sqlite
{
    internal class SavedPodcastDto
    {
        public long Id { get; set; }
        public SavedPodcast SavedPodcast { get; set; }
        public IEnumerable<PodcastModificationDto> Modifications { get; set; }

        public SavedPodcastDto(SavedPodcast podcast)
        {
            SavedPodcast = podcast;
            SavedPodcast.Modifications.Select(x => new PodcastModificationDto(x));
        }

        public SavedPodcast ToSavedPodcast()
        {
            SavedPodcast.Modifications = Modifications.Select(x => x.ToModification());
            return SavedPodcast;
        }
    }
}
