using PodfilterCore.Models;
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
        public const int MaximumSavedPodcastCount = 1000;

        /// <summary>
        /// Parameterless constructor for use with the entity framework.
        /// </summary>
        public SavedPodcastDto()
        {
            // 
        }

        /// <summary>
        /// Constructor for use within the application. Does not add the <see cref="SavedPodcast.Modifications"/> to the <see cref="Modifications"/>.
        /// </summary>
        /// <param name="podcast"></param>
        public SavedPodcastDto(SavedPodcast podcast)
        {
            SavedPodcast = podcast;
        }
    }
}
