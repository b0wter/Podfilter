using System;
using PodfilterCore.Models.ContentFilters;

namespace PodfilterWeb.Helpers
{
    public class ModificationMethodTranslator : BaseModificationMethodTranslator
    {
        public override string StringFilterMethodToDisplayString(StringFilter.StringFilterMethod method)
        {
            switch(method){
                case StringFilter.StringFilterMethod.Contains:
                    return "contains";
                case StringFilter.StringFilterMethod.DoesNotContain:
                    return "does not contain";
                case StringFilter.StringFilterMethod.Matches:
                    return "matches";
                case StringFilter.StringFilterMethod.DoesNotMatch:
                    return "does not match";
                default:
                    throw new ArgumentException($"StringFilterMethod {method} is unknown.");
            }
        }

        public override string DurationFilterMethodToDisplayString(DurationFilter.DurationFilterMethods method)
        {
            switch(method)
            {
                case DurationFilter.DurationFilterMethods.Greater:
                    return "more than";
                case DurationFilter.DurationFilterMethods.Equals:
                    return "exactly";
                case DurationFilter.DurationFilterMethods.GreaterEquals:
                    return "at least";
                case DurationFilter.DurationFilterMethods.Smaller:
                    return "less than";
                case DurationFilter.DurationFilterMethods.SmallerEquals:
                    return "at most";
                default:
                    throw new ArgumentException($"DurationFilterMethod {method} is unknown.");
            }
        }

        public override string DateFilterMethodToDisplayString(DateFilter.DateFilterMethods method)
        {
            switch(method)
            {
                case DateFilter.DateFilterMethods.Greater:
                    return "after";
                case DateFilter.DateFilterMethods.GreaterEquals:
                    return "at or after";
                case DateFilter.DateFilterMethods.Smaller:
                    return "before";
                case DateFilter.DateFilterMethods.SmallerEquals:
                    return "at or before";
                default:
                    throw new ArgumentException($"DateFilterMethod {method} is unknown.");
            }
        }
    }
}