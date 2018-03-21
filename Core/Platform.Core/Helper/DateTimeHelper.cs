using System;

namespace Platform.Core.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime GetNow()
        {
            return DateTime.Now;
        }


        public static DateTime ToUtcTime(this DateTime datetime)
        {
            return datetime.ToUniversalTime();
        }

        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// 供求有效期策略，以天为单位，当天有效
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime GetValid(int days)
        {
            return GetNow().Date.AddDays(days);
        }
    }
}
