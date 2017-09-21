using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodfilterCore.Models.ContentFilters;
using System.Runtime.Serialization;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterWeb.Models
{
    public class DisplayableEpisodeDescriptionFilterModification : DisplayableBasePodcastModification<string>
    {
        public override string Name => "Description Filter";
        public override string Description => $"Keeps elements {Method} {Argument}."; 
        public bool CaseInvariant {get;set;} = true;

        protected DisplayableEpisodeDescriptionFilterModification()
        {
            // for deserialization
        }

        public DisplayableEpisodeDescriptionFilterModification(string argument, string methodToParse)
        {
            this.Argument = argument;
            this.Method = methodToParse;
            CreateModification(argument, methodToParse, CaseInvariant);
        }

        protected override void DeserializeModification()
        {
            CreateModification(Argument, Method, CaseInvariant);
        }

        private void CreateModification(string argument, string methodToParse, bool caseInvariant)
        {
            var method = (StringFilter.StringFilterMethod)Enum.Parse(typeof(StringFilter.StringFilterMethod), methodToParse);
            this.Modification = new EpisodeDescriptionFilterModification(argument, method, caseInvariant);        
        }

        public static DisplayableEpisodeDescriptionFilterModification CreateEmptyInstanceForDeserialization(){
            return new DisplayableEpisodeDescriptionFilterModification();
        }
    }
}
