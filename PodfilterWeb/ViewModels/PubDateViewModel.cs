using PodfilterCore.Models.PodcastModification.Filters;
using PodfilterWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodfilterWeb.ViewModels
{
    public class PubDateViewModel : BaseFilterViewModel
    {
        private string Argument { get; }
        private string Method { get; }

        public override string Name => "Publication Date Filter";
        public override string Description => $"Keep episodes released {Method} {Argument}";

        public PubDateViewModel(EpisodePublishDateFilterModification mod, BaseModificationMethodTranslator translator)
        {
            
        }
    }
}
