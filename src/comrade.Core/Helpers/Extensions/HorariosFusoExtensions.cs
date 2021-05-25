#region

using System;

#endregion

namespace comrade.Core.Helpers.Extensions
{
    public static class HorariosFusoExtensions
    {
        public static DateTime ObterHorarioBrasilia()
        {
            var timeUtc = DateTime.UtcNow;
            var kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var horaBrasilia = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            return horaBrasilia;
        }
    }
}