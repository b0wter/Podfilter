
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterCore.Models.ContentFilters;
using System.Runtime.Serialization;
using PodfilterCore.Helpers;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterWeb.Models
{
    public class DisplayableEpisodePublishDateFilterModification : DisplayableBasePodcastModification<DateTime>
    {
        public override string Name => "Publish Date Filter";

        public override string Description => $"Keeps episodes published {Method} {Argument}";

        protected DisplayableEpisodePublishDateFilterModification()
        {
            //
        }

        public DisplayableEpisodePublishDateFilterModification(string argumentToParse, string methodToParse)
        {
            this.Method = methodToParse;
            this.Argument = DateTime.Parse(argumentToParse);
            CreateModification(Argument, methodToParse);
        }

        public DisplayableEpisodePublishDateFilterModification(DateTime argument, string methodToParse)
        {
            this.Method = methodToParse;
            this.Argument = argument;
            CreateModification(argument, methodToParse);
        }

        private void CreateModification(DateTime argument, string methodToParse)
        {
            var method = (DateFilter.DateFilterMethods)Enum.Parse(typeof(DateFilter.DateFilterMethods), methodToParse);
            this.Modification = new EpisodePublishDateFilterModification(argument, method);
        }

        protected override void DeserializeModification()
        {
            CreateModification(Argument, Method);
        }

        public static DisplayableEpisodePublishDateFilterModification CreateEmptyInstanceForDeserialization()
        {
            return new DisplayableEpisodePublishDateFilterModification();
        }
    }
}