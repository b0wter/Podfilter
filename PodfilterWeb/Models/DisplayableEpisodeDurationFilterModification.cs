
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterCore.Models.ContentFilters;
using System.Runtime.Serialization;
using PodfilterCore.Models.PodcastModification.Filters;

namespace PodfilterWeb.Models
{
    public class DisplayableEpisodeDurationFilterModification : DisplayableBasePodcastModification<long>
    {
        public override string Name => "Duration Filter";

        public override string Description => $"Keeps episodes {Method} than {Argument} seconds.";

        protected DisplayableEpisodeDurationFilterModification()
        {
            // for deserialization
        }

        public DisplayableEpisodeDurationFilterModification(string argument, string methodToParse)
            : this(long.Parse(argument), methodToParse)
        {
            //
        }

        public DisplayableEpisodeDurationFilterModification(long argument, string methodToParse)
        {
            this.Argument = argument;
            this.Method = methodToParse;
            CreateModification(argument, methodToParse);
        }

        public static DisplayableEpisodeDurationFilterModification CreateEmptyInstanceForDeserialization()
        {
            return new DisplayableEpisodeDurationFilterModification();
        }

        private void CreateModification(long argument, string methodToParse)
        {
            var method = (DurationFilter.DurationFilterMethods)Enum.Parse(typeof(DurationFilter.DurationFilterMethods), methodToParse);
            this.Modification = new EpisodeDurationFilterModification(method, argument);
        }

        protected override void DeserializeModification()
        {
            CreateModification(Argument, Method);
        }
    }
}