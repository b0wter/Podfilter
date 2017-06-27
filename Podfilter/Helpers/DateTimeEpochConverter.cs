using System;

namespace Podfilter.Helpers
{
	public static class DateTimeEpochConverter
	{
		private static DateTime refDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		
		public static DateTime EpochToDateTime(long epoch)
		{
    		return refDate.AddMilliseconds(epoch);
		}

		public static long DateTimeToEpoch(DateTime date)
		{
			return (long)(date.ToUniversalTime() - refDate).TotalMilliseconds;
		}
	}
}