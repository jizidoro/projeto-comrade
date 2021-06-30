#region

using System;

#endregion

namespace comrade.Domain.Extensions
{
    public static class DateTimeBrasilia
    {
        public static DateTime GetDateTimeBrasilia()
        {
            var timeUtc = DateTime.UtcNow;
            var kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var horaBrasilia = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            return horaBrasilia;
        }
    }
}