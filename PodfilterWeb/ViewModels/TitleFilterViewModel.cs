using System;
using PodfilterCore.Models.PodcastModification.Filters;
using PodfilterCore.Models.ContentFilters;

namespace PodfilterWeb.ViewModels
{
    public class TitleFilterViewModel : BaseFilterViewModel<EpisodeTitleFilterModification>
    {
        private string Argument {get;}
        private string Method {get;}

        public override string Name => "Title Filter";
        public override string Description => $"Keep if title {Method} {Argument}.";

        public TitleFilterViewModel(EpisodeTitleFilterModification mod)
        {
            Argument = mod.Argument;
        }

    }
}