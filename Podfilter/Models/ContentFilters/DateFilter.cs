using System;
using Podfilter.Helpers;

namespace Podfilter.Models.ContentFilters
{
    public class DateFilter : BaseFilter<DateFilter.DateFilterMethods, DateTime>
    {
        public DateFilter() 
            : base()
        {
            //
        }

        public DateFilter(DateFilterMethods method, DateTime argument) 
            : base(method, argument)
        {
            //
        }

        public DateFilter(DateFilterMethods method, long argumentInSeconds)
            : base(method, DateTimeEpochConverter.EpochToDateTime(argumentInSeconds))
        {
            //
        }
        
        protected override bool PassesFilter(DateTime toTest)
        {
            switch (Method)
            {
                case DateFilterMethods.Greater:
                    return toTest > Argument;
                case DateFilterMethods.GreaterEquals:
                    return toTest >= Argument;
                case DateFilterMethods.Smaller:
                    return toTest < Argument;
                case DateFilterMethods.SmallerEquals:
                    return toTest <= Argument;
                default:
                    throw new InvalidOperationException($"Method {Method} is not implemented.");
            }
        }

        protected override DateTime ParseString(string stringifiedObject)
        {
            return DateTime.Parse(stringifiedObject);
        }

        public enum DateFilterMethods
        {
            Greater,
            GreaterEquals,
            Smaller,
            SmallerEquals
        }
    }
}

