using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WebApp.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// string değeri null yada boş ise true döner.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
            => string.IsNullOrEmpty(value?.Trim());

        public static bool IsNotNullOrEmpty(this string value)
            => !string.IsNullOrEmpty(value);

        /// <summary>
        /// Birleşik kelimelerin Snake Case formatında dönüşümü sağlanır.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example>
        /// var name = "MyNameIsUmay";
        /// name.ToSnakeCase(); // => "my_name_is_umay"
        /// </example>
        public static string ToSnakeCase(this string value)
        {
            var list = new List<string>();
            foreach (Match match in Regex.Matches(value, "([A-Z][^A-Z]+)"))
                list.Add(match.Value.ToLower());
            return string.Join("_", list);
        }

        public static string DownloadString(this string url)
        {
            using var web = new WebClient();
            return web.DownloadString(url);
        }

        public static bool IsMatch(this string value, string pattern)
            => Regex.IsMatch(value, pattern);

        public static string GetMatchedFirstValue(this string value,
            params (string Pattern, Func<string, string> Func)[] patterns)
        {
            foreach (var pattern in patterns)
            {
                var res = Regex.Match(value, pattern.Pattern).Value;
                if (!string.IsNullOrEmpty(res))
                    return pattern.Func != null ? pattern.Func.Invoke(res) : res;
            }

            return "";
        }

        public static T Download<T>(this string url)
            => url.DownloadString().ToDeserialize<T>();

        public static T ToDeserialize<T>(this string url)
            => JsonConvert.DeserializeObject<T>(url);

        public static string ReplaceAll(this string value, string replaceValue, params string[] args)
        {
            args.ForEach(a => value = value.Replace(a, replaceValue));
            return value;
        }

        public static bool IsNumeric(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            return decimal.TryParse(str, out _);
        }
    }
}