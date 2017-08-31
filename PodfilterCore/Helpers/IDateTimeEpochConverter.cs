using System;

namespace PodfilterCore.Helpers
{
    public interface IDateTimeEpochConverter
    {
        long DateTimeToEpoch(DateTime date);
        DateTime EpochToDateTime(long epoch);
    }
}