using PodfilterCore.Models.ContentFilters;

namespace PodfilterWeb.Helpers
{
    public abstract class BaseModificationMethodTranslator
    {
        public abstract string DateFilterMethodToDisplayString(DateFilter.DateFilterMethods method);
        public abstract string DurationFilterMethodToDisplayString(DurationFilter.DurationFilterMethods method);
        public abstract string StringFilterMethodToDisplayString(StringFilter.StringFilterMethod method);
    }
}