using System;

namespace BlocketLiteAPI.Helpers
{
    /// <summary>
    /// Static class that only calls one methode <see cref="GetCurrentYear"/>
    /// </summary>
    public class GetCurrentYearHelper
    {
        /// <summary>
        /// Return the current year as a 4 digit <see cref="int"/>
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public static int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }
    }
}
