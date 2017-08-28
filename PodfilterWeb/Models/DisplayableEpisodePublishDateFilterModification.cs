
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterCore.Models.ContentFilters;
using System.Runtime.Serialization;
using PodfilterCore.Helpers;
using PodfilterCore.Models.PodcastModification.Filters;

namespace PodfilterWeb.Models
{
    public class DisplayableEpisodePublishDateFilterModification : DisplayableBasePodcastModification<long>
    {
        public override string Name => "Publish Date Filter";

        public override string Description => $"Keeps episodes published {Method} {ArgumentAsDate}";
        public string ArgumentAsDate => DateTimeEpochConverter.EpochToDateTime(Argument).ToShortDateString();

        protected DisplayableEpisodePublishDateFilterModification()
        {
            //
        }

        public DisplayableEpisodePublishDateFilterModification(string argument, string methodToParse)
        {
            var date = DateTime.Parse(argument);
            var ticks = DateTimeEpochConverter.DateTimeToEpoch(date);

            this.Method = methodToParse;
            this.Argument = ticks;
            CreateModification(ticks, methodToParse);
        }

        public DisplayableEpisodePublishDateFilterModification(long argument, string methodToParse)
        {
            this.Method = methodToParse;
            this.Argument = argument;
            CreateModification(argument, methodToParse);
        }

        private void CreateModification(long argument, string methodToParse)
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

        public override string ToQueryString()
        {
            return $"pubDate{Method}={Argument}";
        }
    }
}