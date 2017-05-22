using System;

namespace Podfilter.Models
{
    public class IntFilter : BaseFilter
    {
        public IntFilterMethods? Method { get; set; }
        public int Argument { get; set; }

        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        public IntFilter()
        {
            //
        }

        public IntFilter(int argument, IntFilterMethods method)
        {
            this.Argument = argument;
            this.Method = method;
        }
        
        public override bool PassesFilter(object obj)
        {
            if(Method == null)
                throw new InvalidOperationException($"Method has not been set!");

            int toTest = ConvertToTDefault<int>(obj);
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