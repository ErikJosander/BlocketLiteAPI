using System;

namespace BlocketLiteEFCoreDB.Helpers
{
    /// <summary>
    /// A helper method.
    /// </summary>
    public static class GetCurrentDateUTC
    {
        /// <summary>
        /// Gets the current datetime in UTC.
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
