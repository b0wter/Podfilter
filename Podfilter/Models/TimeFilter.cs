using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Podfilter.Models
{
    public class TimeFilter : BaseFilter
    {
        public FilterMethods? Method { get; set; }
        public DateTime? Argument { get; set; }

        public TimeFilter()
        {
            //
        }

        public TimeFilter(FilterMethods method, DateTime argument)
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
                case FilterMethods.Greater:
                    return toTest > Argument;
                case FilterMethods.Smaller:
                    return toTest < Argument;
                default:
                    throw new InvalidOperationException($"Method {Method} is not implemented.");
            }
        }

        public enum FilterMethods
        {
            Greater,
            Smaller
        }
    }
}