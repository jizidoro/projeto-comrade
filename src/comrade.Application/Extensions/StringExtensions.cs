#region

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace comrade.Application.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string source)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return new string(
                new CultureInfo("pt-BR", false)
                    .TextInfo
                    .ToTitleCase(
                        string.Join(" ", pattern.Matches(source)).ToLower()
                    )
                    .Replace(@" ", "")
                    .Select((x, i) => i == 0 ? char.ToLower(x) : x)
                    .ToArray()
            );
        }

        public static string ToKebabCase(this string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return string.Join("-", pattern.Matches(str)).ToLower();
        }

        public static string ToSnakeCase(this string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return string.Join("_", pattern.Matches(str)).ToLower();
        }

        public static string ToTitleCase(this string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return new CultureInfo("pt-BR", false)
                .TextInfo
                .ToTitleCase(
                    string.Join(" ", pattern.Matches(str)).ToLower()
                );
        }

        public static string ToPascalCase(this string source)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return new string(
                new CultureInfo("pt-BR", false)
                    .TextInfo
                    .ToTitleCase(
                        string.Join(" ", pattern.Matches(source)).ToLower()
                    )
                    .Replace(@" ", "")
                    .Select((x, i) => i == 0 ? char.ToUpper(x) : x)
                    .ToArray()
            );
        }

        public static string ToProperCase(this string source)
        {
            if (source == null) return source;
            if (source.Length < 2) return source.ToUpper();

            var words = source.ToLower().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            var result = "";
            foreach (var word in words)
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1) + " ";

            return result.Trim();
        }

        public static string ToSlug(this string phrase)
        {
            var str = phrase.RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }

        public static int ToInt32(this string s)
        {
            int.TryParse(s, out var i);
            return i;
        }

        public static double ToInt64(this string s)
        {
            long.TryParse(s, out var i);
            return i;
        }

        public static decimal ToDecimal(this string s)
        {
            decimal.TryParse(s, out var i);
            return i;
        }

        public static DateTime ToDoubleToDateTime(this string s)
        {
            var dateTime = DateTime.FromOADate(s.ToInt64());
            return dateTime;
        }
    }
}