using System;

namespace Podfilter.Models
{
    public class DurationFilter : BaseFilter<DurationFilter.DurationFilterMethods, TimeSpan>
    {
        public DurationFilter() : base()
        {
            //
        }

        public DurationFilter(DurationFilterMethods method, TimeSpan argument) : base(method, argument)
        {
            //
        }

        protected override bool PassesFilter(TimeSpan toTest)
        {
            switch (Method)
            {
                case DurationFilterMethods.Greater:
                    return toTest > Argument;
                case DurationFilterMethods.GreaterEquals:
                    return toTest >= Argument;
                case DurationFilterMethods.Equals:
                    return toTest == Argument;
                case DurationFilterMethods.Smaller:
                    return toTest < Argument;
                case DurationFilterMethods.SmallerEquals:
                    return toTest <= Argument;
                default:
                    throw new InvalidOperationException($"Method {Method} is not implemented.");
            }
        }

        protected override TimeSpan ParseString(string stringifiedObject)
        {
            return TimeSpan.Parse(stringifiedObject);
        }

        public enum DurationFilterMethods
        {
            Greater,
            GreaterEquals,
            Equals,
            Smaller,
            SmallerEquals
        }
    }
}