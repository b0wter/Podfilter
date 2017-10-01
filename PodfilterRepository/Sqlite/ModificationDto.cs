using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    public class ModificationDto
    {
        public long Id { get; set; }
        /// <summary>
        /// List of parameters required to create a new instance of <see cref="TypeName"/> to create an instance of a <see cref="BasePodcastModification"/>.
        /// </summary>
        public List<BaseDbParameter> Parameters { get; set; }
        /// <summary>
        /// Name of the type of <see cref="BasePodcastModification"/> that needs to be instantiated.
        /// </summary>
        public string TypeName { get; set; }

        public ModificationDto(BasePodcastModification modification)
        {
            this.TypeName = modification.GetType().FullName;
            Parameters = BaseDbParameter.FromModification(modification, this);
        }

        public BasePodcastModification ToModification()
        {
            var parameters = Parameters.Select(x => x.Value);
            var type = Type.GetType($"{TypeName}, PodfilterCore");

            var modification = (BasePodcastModification)Activator.CreateInstance(type, parameters);
            return modification;
        }
    }
}
