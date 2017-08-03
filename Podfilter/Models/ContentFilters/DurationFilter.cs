using System;
using System.Linq;

namespace Podfilter.Models.ContentFilters
{
    public class DurationFilter : BaseFilter<DurationFilter.DurationFilterMethods, TimeSpan>
    {
        public DurationFilter() 
            : base()
        {
            //
        }

        public DurationFilter(DurationFilterMethods method, TimeSpan argument) 
            : base(method, argument)
        {
            //
        }

        public DurationFilter(DurationFilterMethods method, long argumentInSeconds) 
            : base(method,TimeSpan.FromSeconds(argumentInSeconds))
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
            // to use the regular parse method of the TimeSpan class we 
            // need to make sure it's of the format: hh:mm:ss
            if (stringifiedObject.Count(c => c == ':') < 2)
                stringifiedObject = $"00:{stringifiedObject}";
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