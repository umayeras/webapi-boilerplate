using System;
using Newtonsoft.Json;

namespace WebApp.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToNullOrString(this object o)
        {
            return o?.ToString();
        }

        /// <summary>
        /// Verilen nesne değerini Convert.Boolean metodunu kullanarak System.Boolean tipine çevirir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static bool ToBoolean(this object value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Verilen nesne değerini Convert.ToInt32 metodunu kullanarak System.Int32 tipine çevirir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static int ToInt32(this object value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Verilen nesne değerini Convert.ToDouble metodunu kullanarak System.Double tipine çevirir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static double ToDouble(this object value)
        {
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Verilen nesne değerini Convert.ToByte metodunu kullanarak System.Byte tipine çevirir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static byte ToByte(this object value)
        {
            return Convert.ToByte(value);
        }

        /// <summary>
        /// Verilen nesne değerini Convert.ToDateTime metodunu kullanarak System.DateTime tipine çevirir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static DateTime ToDateTime(this object value)
        {
            return Convert.ToDateTime(value);
        }
        
        /// <summary>
        /// Verilen nesneyi Json olarak serilize eder.
        /// </summary>
        /// <param name="entityObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJson<T>(this T entityObject) where T : class, new()
        {
            return JsonConvert.SerializeObject(entityObject);
        }
        
        /// <summary>
        /// Verilen string değerini json objesi olarak deserilize eder.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T JsonToObject<T>(this string jsonString) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
