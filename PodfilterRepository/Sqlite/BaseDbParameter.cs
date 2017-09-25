using System;
using PodfilterCore.Models.ContentFilters;

namespace PodfilterRepository.Sqlite
{
    internal abstract class BaseDbParameter
    {
        public abstract Type Type {get;}
        public abstract object Value {get;}
    }

    internal abstract class BaseDbParameter<T> : BaseDbParameter
    {
        public long Id {get;set;}
        public T TypedValue {get;set;}
        public override Type Type => typeof(T);
        public override object Value => TypedValue;

        public BaseDbParameter(T value)
        {
            TypedValue = value;
        }
    }

    internal class BoolParameter : BaseDbParameter<bool>
    {
        public BoolParameter(bool value) : base(value)
        {
        }
    }

    internal class IntParameter : BaseDbParameter<int>
    {
        public IntParameter(int value) : base(value)
        {
        }
    }

    internal class LongParameter : BaseDbParameter<long>
    {
        public LongParameter(long value) : base(value)
        {
        }
    }

    internal class StringParameter : BaseDbParameter<string>
    {
        public StringParameter(string value) : base(value)
        {
        }
    }

    internal class DateTimeParameter : BaseDbParameter<DateTime>
    {
        public DateTimeParameter(DateTime value) : base(value)
        {
        }
    }

    internal class TimeSpanParameter : BaseDbParameter<TimeSpan>
    {
        public TimeSpanParameter(TimeSpan value) : base(value)
        {
        }
    }

    internal class StringFilterMethodParameter : BaseDbParameter<StringFilter.StringFilterMethod>
    {
        public StringFilterMethodParameter(StringFilter.StringFilterMethod value) : base(value)
        {
        }
    }

    internal class IntFilterMethodParameter : BaseDbParameter<IntFilter.IntFilterMethods>
    {
        public IntFilterMethodParameter(IntFilter.IntFilterMethods value) : base(value)
        {
        }
    }

    internal class DateTimeFilterMethodParameter : BaseDbParameter<DateFilter.DateFilterMethods>
    {
        public DateTimeFilterMethodParameter(DateFilter.DateFilterMethods value) : base(value)
        {
        }
    }

    internal class DurationFilterMethodParameter : BaseDbParameter<DurationFilter.DurationFilterMethods>
    {
        public DurationFilterMethodParameter(DurationFilter.DurationFilterMethods value) : base(value)
        {
        }
    }
}