using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Helpers
{
    public class ModificationTypeDiscriminator
    {
        private static Dictionary<string, Type> _typeDiscriminators;

        private void FillDiscriminatorDictionaryIfEmpty()
        {
            if (_typeDiscriminators == null || _typeDiscriminators.Count == 0)
                FillDiscriminatorDictionary();
        }

        private void FillDiscriminatorDictionary()
        {
            if (_typeDiscriminators == null)
                _typeDiscriminators = new Dictionary<string, Type>();
            else
                _typeDiscriminators.Clear();

            foreach (var type in CreateTypeList())
                _typeDiscriminators.Add(type.FullName, type);
        }

        private List<Type> CreateTypeList()
        {
            var types = new List<Type>();
            types.Add(typeof(EpisodeDescriptionFilterModification));
            types.Add(typeof(EpisodeDurationFilterModification));
            types.Add(typeof(EpisodePublishDateFilterModification));
            types.Add(typeof(EpisodeTitleFilterModification));
            types.Add(typeof(RemoveDuplicateEpisodesModification));
            return types;
        }

        public Type GetTypeForSqlModificationDiscriminator(string discriminator)
        {
            FillDiscriminatorDictionaryIfEmpty();
            return _typeDiscriminators[discriminator];
        }
    }
}
