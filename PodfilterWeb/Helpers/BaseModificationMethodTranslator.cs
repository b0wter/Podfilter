using PodfilterCore.Models.ContentFilters;
using static PodfilterCore.Models.PodcastModification.RemoveDuplicateEpisodesModification;

namespace PodfilterWeb.Helpers
{
    public abstract class BaseModificationMethodTranslator
    {
        public abstract string DateFilterMethodToDisplayString(string method);
        public abstract string DurationFilterMethodToDisplayString(string method);
        public abstract string StringFilterMethodToDisplayString(string method);
        public abstract string IntFilterMethodToDisplayString(string method);
        public abstract string DuplicateTimeFrameToDisplayString(string timeFrame);

        public abstract string DateFilterMethodToDisplayString(DateFilter.DateFilterMethods method);
        public abstract string DurationFilterMethodToDisplayString(DurationFilter.DurationFilterMethods method);
        public abstract string StringFilterMethodToDisplayString(StringFilter.StringFilterMethod method);
        public abstract string IntFilterMethodToDisplayString(IntFilter.IntFilterMethods method);
        public abstract string DuplicateTimeFrameToDisplayString(DuplicateTimeFrames timeFrame);
    }
}