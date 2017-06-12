using System;

namespace Podfilter.Models
{
    public class IntFilter : BaseFilter<IntFilter.IntFilterMethods, int>
    {
        public IntFilter() : base()
        {
            //
        }

        public IntFilter(IntFilterMethods method, int argument) : base(method, argument)
        {
            //
        }
        
        protected override bool PassesFilter(int toTest)
        {
            ValidateMethodAndArgumentSet();

            switch (Method)
            {
                case IntFilterMethods.Equal:
                    return toTest == Argument;
                case IntFilterMethods.Greater:
                    return toTest > Argument;
                case IntFilterMethods.GreaterEquals:
                    return toTest >= Argument;
                case IntFilterMethods.Smaller:
                    return toTest < Argument;
                case IntFilterMethods.SmallerEquals:
                    return toTest <= Argument;
                case IntFilterMethods.Unequal:
                    return toTest != Argument;
                default:
                    throw new InvalidOperationException($"The given method: {Method} is unknown.");
            }
        }

        protected override int ParseString(string objectAsString)
        {
            return int.Parse(objectAsString);
        }

        public enum IntFilterMethods
        {
            Equal,
            Unequal,
            Greater,
            GreaterEquals,
            Smaller,
            SmallerEquals
        }
    }
}