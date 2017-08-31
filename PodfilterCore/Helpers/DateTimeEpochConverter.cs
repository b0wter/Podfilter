using System;

namespace PodfilterCore.Helpers
{
	public class DateTimeEpochConverter : IDateTimeEpochConverter
    {
		private readonly DateTime refDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		
		public DateTime EpochToDateTime(long epoch)
		{
    		return refDate.AddMilliseconds(epoch);
		}

		public long DateTimeToEpoch(DateTime date)
		{
			return (long)(date.ToUniversalTime() - refDate).TotalMilliseconds;
		}
	}
}