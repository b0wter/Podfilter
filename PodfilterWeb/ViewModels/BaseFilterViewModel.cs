using System;
using PodfilterCore.Models.PodcastModification;

namespace PodfilterWeb.ViewModels
{
    public abstract class BaseFilterViewModel<T> where T : BasePodcastModification
    {
        // display name of the filter
        public abstract string Name {get;}
        // short sentence that explains what the current parameters of the filter are
        public abstract string Description {get;}
    }
}