using System;

namespace BlocketLiteEFCoreDB.Helpers
{
    public static class GetCurrentDateUTC
    {
        public static DateTimeOffset GetDateTimeUTC()
        {
            var date = DateTime.UtcNow;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            return date;
        }
    }
}
