using PodfilterCore.Models.PodcastModification;
using PodfilterRepository.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    /// <summary>
    /// Dto storing only the information needed to recreate a <see cref="BasePodcastModification"/>
    /// </summary>
    internal class PodcastModificationDto
    {
        public long Id { get; set; }
        /// <summary>
        /// Full name of the modification this represents, excluding the assembly name.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Reference to the dto of the podcast this belongs to.
        /// </summary>
        public SavedPodcastDto SavedPodcastDto { get; set; }
        /// <summary>
        /// List of parameters needed to create an instance of the modification.
        /// </summary>
        public IEnumerable<BaseDbParameter> Parameters {get;set;}

        public PodcastModificationDto(BasePodcastModification modification, SavedPodcastDto savedPodcast)
        {
            TypeName = modification.GetType().FullName;
            SavedPodcastDto = savedPodcast;
        }

        private IEnumerable<BaseDbParameter> CreateParametersFromModification(BasePodcastModification modification)
        {
            var paramters = new List<BaseDbParameter>(4);

            if (modification is EpisodeDescriptionFilterModification)
            {

            }
            else if (modification is EpisodeDurationFilterModification)
            {

            }
            else if (modification is EpisodePublishDateFilterModification)
            {

            }
            else if(modification is EpisodeTitleFilterModification)
            {

            }
            else if(modification is RemoveDuplicateEpisodesModification)
            {

            }
            else
            {
                throw new ArgumentException($"There is no logic to handle the type: {modification.GetType().FullName}");
            }
        }

        public BasePodcastModification ToModification(ModificationTypeDiscriminator discriminator)
        {
            var type = System.Type.GetType($"{TypeName}, PodfilterCore");
            var modification = (BasePodcastModification)Activator.CreateInstance(type, Parameters.Select(x => x.Value).ToArray());
            return modification;
        }
    }
}
