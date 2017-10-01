﻿using PodfilterCore.Models;
using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    public class SavedPodcastDto
    {
        public long Id { get; set; }
        public long SavedPodcastId { get; set; }
        public SavedPodcast SavedPodcast { get; set; }
        public ICollection<ModificationDto> Modifications { get; set; }

        /// <summary>
        /// Parameterless constructor for use with the entity framework.
        /// </summary>
        public SavedPodcastDto()
        {
            // 
        }

        /// <summary>
        /// Constructor for use within the application.
        /// </summary>
        /// <param name="podcast"></param>
        public SavedPodcastDto(SavedPodcast podcast)
        {
            SavedPodcast = podcast;
            Modifications = podcast.Modifications.Select(x => new ModificationDto(x)).ToList();
        }
    }
}
