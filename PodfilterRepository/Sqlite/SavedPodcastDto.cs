using PodfilterCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    public class SavedPodcastDto
    {
        public long Id { get; set; }
        public SavedPodcast SavedPodcast { get; set; }

        public SavedPodcastDto(SavedPodcast podcast)
        {
            SavedPodcast = podcast;
        }
    }
}
