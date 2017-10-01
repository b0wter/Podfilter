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
        public int Order { get; }
        
        public static List<BaseDbParameter> FromModification(BasePodcastModification modification, ModificationDto dto)
        {
            List<BaseDbParameter> parameters = new List<BaseDbParameter>();

            if(modification is EpisodeDescriptionFilterModification description)
            {
                parameters.Add(new StringParameter(description.Argument));
                parameters.Add(new StringFilterMethodParameter(description.Method));
                parameters.Add(new BoolParameter(description.CaseInvariant));
            }
            else if(modification is EpisodeDurationFilterModification duration)
            {
                parameters.Add(new DurationFilterMethodParameter(duration.Method));
                parameters.Add(new LongParameter(duration.Duration));
            }
            else if(modification is EpisodePublishDateFilterModification publish)
            {
                parameters.Add(new DateTimeParameter(publish.Date));
                parameters.Add(new DateTimeFilterMethodParameter(publish.Method));
            }
            else if(modification is EpisodeTitleFilterModification title)
            {
                parameters.Add(new StringParameter(title.Argument));
                parameters.Add(new StringFilterMethodParameter(title.Method));
                parameters.Add(new BoolParameter(title.CaseInvariant));
            }
            else if(modification is RemoveDuplicateEpisodesModification remove)
            {
                parameters.Add(new BoolParameter(remove.KeepFirstEpisode));
                parameters.Add(new RemoveDuplicateEpisodesMethodParameter(remove.TimeFrame));
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

        public BaseDbParameter(T value)
        {
            TypedValue = value;
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

        public BoolParameter(bool value) : base(value)
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

        public IntParameter(int value) : base(value)
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

        public LongParameter(long value) : base(value)
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

        public StringParameter(string value) : base(value)
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

        public DateTimeParameter(DateTime value) : base(value)
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

        public TimeSpanParameter(TimeSpan value) : base(value)
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

        public StringFilterMethodParameter(StringFilter.StringFilterMethod value) : base(value)
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

        public IntFilterMethodParameter(IntFilter.IntFilterMethods value) : base(value)
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

        public DateTimeFilterMethodParameter(DateFilter.DateFilterMethods value) : base(value)
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

        public DurationFilterMethodParameter(DurationFilter.DurationFilterMethods value) : base(value)
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

        public RemoveDuplicateEpisodesMethodParameter(RemoveDuplicateEpisodesModification.DuplicateTimeFrames value) : base(value)
        {
            //
        }
    }
}
