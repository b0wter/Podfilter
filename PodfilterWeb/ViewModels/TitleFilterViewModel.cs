using System;
using PodfilterCore.Models.PodcastModification.Filters;
using PodfilterCore.Models.ContentFilters;
using PodfilterWeb.Helpers;

namespace PodfilterWeb.ViewModels
{
    public class TitleFilterViewModel : BaseFilterViewModel
    {
        private string Argument {get;}
        private string Method {get;}

        public override string Name => "Title Filter";
        public override string Description => $"Keep if title {Method} {Argument}.";

        public TitleFilterViewModel(EpisodeTitleFilterModification mod, BaseModificationMethodTranslator translator)
        {
        }
    }
}