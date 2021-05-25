#region

using System;
using System.ComponentModel;

#endregion

namespace comrade.Domain.Enums
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString<T>(this T val)
            where T : Enum
        {
            var attributes = (DescriptionAttribute[]) val
                .GetType()
                .GetField(val.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes is {Length: > 0} ? attributes[0].Description : string.Empty;
        }
    }
}