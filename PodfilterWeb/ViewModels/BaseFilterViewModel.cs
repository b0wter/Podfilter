using System;
using PodfilterCore.Models.PodcastModification;
using PodfilterCore.Models.PodcastModification.Filters;

namespace PodfilterWeb.ViewModels
{
    public abstract class BaseFilterViewModel 
    {
        // display name of the filter
        public abstract string Name {get;}
        // short sentence that explains what the current parameters of the filter are
        public abstract string Description {get;}

        public static BaseFilterViewModel FromBasePodcastModification(BasePodcastModification modification)
        {
            throw new NotImplementedException();
            /*
            var modType = modification.GetType();

            if (modType == typeof(EpisodeTitleFilterModification))
                return new TitleFilterViewModel((EpisodeTitleFilterModification)modification);

            throw new ArgumentException($"The modification {modification.GetType()} is unknown.");
            */
        }
    }
}