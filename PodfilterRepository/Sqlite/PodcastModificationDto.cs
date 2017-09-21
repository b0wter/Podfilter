using PodfilterCore.Models.PodcastModification;
using PodfilterRepository.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    /// <summary>
    /// Dto storing only the information needed to recreate a <see cref="BasePodcastModification"/>
    /// </summary>
    public class PodcastModificationDto
    {
        public long Id { get; set; }
        public SavedPodcastDto SavedPodcastDto { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string Argument { get; set; }

        public BasePodcastModification ToModification(ModificationTypeDiscriminator discriminator)
        {
            var type = discriminator.GetTypeForSqlModificationDiscriminator(Type);
            var constructors = type.GetConstructors();

            foreach(var info in constructors)
            {
                    
            }

            throw new NotImplementedException();
        }
    }
}
