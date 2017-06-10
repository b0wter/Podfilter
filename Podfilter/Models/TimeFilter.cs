using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Podfilter.Models
{
    public class TimeFilter : BaseFilter
    {
        public TimeFilterMethods? Method { get; set; }
        public DateTime? Argument { get; set; }

        public TimeFilter()
        {
            //
        }

        public TimeFilter(TimeFilterMethods method, DateTime argument)
        {
            this.Method = method;
            this.Argument = argument;
        }
        
        public override bool PassesFilter(object obj)
        {
            if(Method == null)
                throw new InvalidOperationException("Method has not been set!");
            
            if(Argument == null)
                throw new InvalidOperationException("Argument has not been set!");

            DateTime toTest = ConvertToTDefault<DateTime>(obj);

            switch (Method)
            {
                case TimeFilterMethods.Greater:
                    return toTest > Argument;
                case TimeFilterMethods.GreaterEquals:
                    return toTest >= Argument;
                case TimeFilterMethods.Smaller:
                    return toTest < Argument;
               case TimeFilterMethods.SmallerEquals:
                   return toTest <= Argument;
                default:
                    throw new InvalidOperationException($"Method {Method} is not implemented.");
            }
        }

        public override bool PassesFilter(string objectAsString)
        {
            return PassesFilter(DateTime.Parse(objectAsString));
        }

        public enum TimeFilterMethods
        {
            Greater,
            GreaterEquals,
            Smaller,
            SmallerEquals
        }
    }
}