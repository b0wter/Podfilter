using PodfilterCore.Models.ContentFilters;
using PodfilterCore.Models.PodcastModification;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Sqlite
{
    public abstract class BaseDbParameter
    {
        public long Id { get; set; }
        public abstract Type Type { get; }
        public abstract object Value { get; }
        public int Order { get; protected set; }
        
        public static List<BaseDbParameter> FromModification(BasePodcastModification modification, ModificationDto dto)
        {
            List<BaseDbParameter> parameters = new List<BaseDbParameter>();

            if(modification is EpisodeDescriptionFilterModification description)
            {
                parameters.Add(new StringParameter(description.Argument, parameters.Count));
                parameters.Add(new StringFilterMethodParameter(description.Method, parameters.Count));
                parameters.Add(new BoolParameter(description.CaseInvariant, parameters.Count));
            }
            else if(modification is EpisodeDurationFilterModification duration)
            {
                parameters.Add(new DurationFilterMethodParameter(duration.Method, parameters.Count));
                parameters.Add(new LongParameter(duration.Duration, parameters.Count));
            }
            else if(modification is EpisodePublishDateFilterModification publish)
            {
                parameters.Add(new DateTimeParameter(publish.Date, parameters.Count));
                parameters.Add(new DateTimeFilterMethodParameter(publish.Method, parameters.Count));
            }
            else if(modification is EpisodeTitleFilterModification title)
            {
                parameters.Add(new StringParameter(title.Argument, parameters.Count));
                parameters.Add(new StringFilterMethodParameter(title.Method, parameters.Count));
                parameters.Add(new BoolParameter(title.CaseInvariant, parameters.Count));
            }
            else if(modification is RemoveDuplicateEpisodesModification remove)
            {
                parameters.Add(new BoolParameter(remove.KeepFirstEpisode, parameters.Count));
                parameters.Add(new RemoveDuplicateEpisodesMethodParameter(remove.TimeFrame, parameters.Count));
            }
            else
            {
                throw new ArgumentException($"The type {modification.GetType()} is not yet implemented.");
            }

            return parameters;
        }
    }

    public abstract class BaseDbParameter<T> : BaseDbParameter
    {
        public T TypedValue { get; set; }
        public override Type Type => typeof(T);
        public override object Value => TypedValue;

        public BaseDbParameter(T value, int order)
        {
            TypedValue = value;
            Order = order;
        }

        public BaseDbParameter()
        {
            // Parameterless constructor for use with the entity framework.
        }
    }

    public class BoolParameter : BaseDbParameter<bool>
    {
        public BoolParameter()
        {
            //
        }

        public BoolParameter(bool value, int order) : base(value, order)
        {
            //
        }
    }

    public class IntParameter : BaseDbParameter<int>
    {
        public IntParameter()
        {
            //
        }

        public IntParameter(int value, int order) : base(value, order)
        {
            //
        }
    }

    public class LongParameter : BaseDbParameter<long>
    {
        public LongParameter()
        {
            //
        }

        public LongParameter(long value, int order) : base(value, order)
        {
            //
        }
    }

    public class StringParameter : BaseDbParameter<string>
    {
        public StringParameter()
        {
            //
        }

        public StringParameter(string value, int order) : base(value, order)
        {
            //
        }
    }

    public class DateTimeParameter : BaseDbParameter<DateTime>
    {
        public DateTimeParameter()
        {
            //
        }

        public DateTimeParameter(DateTime value, int order) : base(value, order)
        {
            //
        }
    }

    public class TimeSpanParameter : BaseDbParameter<TimeSpan>
    {
        public TimeSpanParameter()
        {
            //
        }

        public TimeSpanParameter(TimeSpan value, int order) : base(value, order)
        {
            //
        }
    }

    public class StringFilterMethodParameter : BaseDbParameter<StringFilter.StringFilterMethod>
    {
        public StringFilterMethodParameter()
        {
            //
        }

        public StringFilterMethodParameter(StringFilter.StringFilterMethod value, int order) : base(value, order)
        {
            //
        }
    }

    public class IntFilterMethodParameter : BaseDbParameter<IntFilter.IntFilterMethods>
    {
        public IntFilterMethodParameter()
        {
            //
        }

        public IntFilterMethodParameter(IntFilter.IntFilterMethods value, int order) : base(value, order)
        {
            //
        }
    }

    public class DateTimeFilterMethodParameter : BaseDbParameter<DateFilter.DateFilterMethods>
    {
        public DateTimeFilterMethodParameter()
        {
            //   
        }

        public DateTimeFilterMethodParameter(DateFilter.DateFilterMethods value, int order) : base(value, order)
        {
            //
        }
    }

    public class DurationFilterMethodParameter : BaseDbParameter<DurationFilter.DurationFilterMethods>
    {
        public DurationFilterMethodParameter()
        {
            //
        }

        public DurationFilterMethodParameter(DurationFilter.DurationFilterMethods value, int order) : base(value, order)
        {
            //
        }
    }

    public class RemoveDuplicateEpisodesMethodParameter : BaseDbParameter<RemoveDuplicateEpisodesModification.DuplicateTimeFrames>
    {
        public RemoveDuplicateEpisodesMethodParameter()
        {
            //
        }

        public RemoveDuplicateEpisodesMethodParameter(RemoveDuplicateEpisodesModification.DuplicateTimeFrames value, int order) : base(value, order)
        {
            //
        }
    }
}
