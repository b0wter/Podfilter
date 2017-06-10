using System;
using System.Collections.Generic;

namespace Podfilter.Models
{
    public interface IFilter
    {
        bool PassesFilter(object obj);
    }
    
    public abstract class BaseFilter : IFilter
    {
        public abstract bool PassesFilter(object obj);

        public abstract bool PassesFilter(string objectAsString);

        protected T ConvertToTDefault<T>(object obj)
        {
            if (typeof(T) != obj.GetType())
                throw new ArgumentException($"");
            else
                return (T) obj;
        }
    }
}