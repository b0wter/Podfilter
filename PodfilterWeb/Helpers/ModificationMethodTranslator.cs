using System;
using PodfilterCore.Models.ContentFilters;
using PodfilterCore.Models.PodcastModification;
using static PodfilterCore.Models.PodcastModification.RemoveDuplicateEpisodesModification;

namespace PodfilterWeb.Helpers
{
    public class ModificationMethodTranslator : BaseModificationMethodTranslator
    {
        public override string StringFilterMethodToDisplayString(string method)
        {
            var enumMethod = Enum.Parse<StringFilter.StringFilterMethod>(method);
            return StringFilterMethodToDisplayString(enumMethod);
        }
        public override string StringFilterMethodToDisplayString(StringFilter.StringFilterMethod method)
        {
            switch(method){
                case StringFilter.StringFilterMethod.Contains:
                    return "is containing";
                case StringFilter.StringFilterMethod.DoesNotContain:
                    return "is not containing";
                case StringFilter.StringFilterMethod.Matches:
                    return "is matching";
                case StringFilter.StringFilterMethod.DoesNotMatch:
                    return "is not matching";
                default:
                    throw new ArgumentException($"StringFilterMethod {method} is unknown.");
            }
        }

        public override string DurationFilterMethodToDisplayString(string method)
        {
            var enumMethod = Enum.Parse<DurationFilter.DurationFilterMethods>(method);
            return DurationFilterMethodToDisplayString(enumMethod);
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

        public override string DateFilterMethodToDisplayString(string method)
        {
            var enumMethod = Enum.Parse<DateFilter.DateFilterMethods>(method);
            return DateFilterMethodToDisplayString(enumMethod);
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

        public override string IntFilterMethodToDisplayString(string method)
        {
            var enumMethod = Enum.Parse<IntFilter.IntFilterMethods>(method);
            return IntFilterMethodToDisplayString(enumMethod);
        }
        public override string IntFilterMethodToDisplayString(IntFilter.IntFilterMethods method)
        {
            switch (method)
            {
                case IntFilter.IntFilterMethods.Equal:
                    return "equals";
                case IntFilter.IntFilterMethods.Greater:
                    return "more than";
                case IntFilter.IntFilterMethods.GreaterEquals:
                    return "more or equal than";
                case IntFilter.IntFilterMethods.Smaller:
                    return "less than";
                case IntFilter.IntFilterMethods.SmallerEquals:
                    return "less or equal than";
                case IntFilter.IntFilterMethods.Unequal:
                    return "unequal to";
                default:
                    throw new ArgumentException($"IntFilterMethod {method} is unknown.");
            }
        }

        public override string DuplicateTimeFrameToDisplayString(string timeFrame)
        {
            var enumMethod = Enum.Parse<DuplicateTimeFrames>(timeFrame);
            return DuplicateTimeFrameToDisplayString(enumMethod);
        }
        public override string DuplicateTimeFrameToDisplayString(RemoveDuplicateEpisodesModification.DuplicateTimeFrames timeFrame)
        {
            switch(timeFrame)
            {
                case DuplicateTimeFrames.Day:
                    return "day";
                case DuplicateTimeFrames.Hour:
                    return "hour";
                case DuplicateTimeFrames.Month:
                    return "month";
                case DuplicateTimeFrames.Week:
                    return "week";
                default:
                    throw new ArgumentException($"DuplicateTimeFrames {timeFrame} is unknown.");
            }
        }
    }
}