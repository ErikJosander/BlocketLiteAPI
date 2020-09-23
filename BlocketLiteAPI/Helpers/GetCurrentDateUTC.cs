using System;

namespace BlocketLiteAPI.Helpers
{
    /// <summary>
    /// Static calls that only calls one methode <see cref="GetDateTimeUTC"/>
    /// </summary>
    public static class GetCurrentDateUTC
    {
        /// <summary>
        /// Return current <see cref="DateTimeOffset"/>
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeUTC()
        {
            var date = DateTime.UtcNow;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            return date;
        }
    }
}
