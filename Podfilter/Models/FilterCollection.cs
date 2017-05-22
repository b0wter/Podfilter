using System;
using System.Collections.Generic;

namespace Podfilter.Models
{
    public class FilterCollection
    {
        public List<BaseFilter> Filters { get; private set; }

        public FilterCollection()
        {
            Filters = new List<BaseFilter>();
        }
    }

    public abstract class BaseFilter
    {
        public abstract bool PassesFilter(object obj);
        
        protected T ConvertToTDefault<T>(object obj)
        {
            if (typeof(T) != obj.GetType())
                throw new ArgumentException($"");
            else
                return (T) obj;
        }
    }
}