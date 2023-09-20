using Newtonsoft.Json;
using Dapper.Contrib.Extensions;
using System;

namespace Banking.Api.Models
{
    public static class ModelExtension
    {
        /// <summary>
        /// Method untuk mendapatkan nama tabel dari class model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetTableName<T>(this T obj)
        {
            var tableName = string.Empty;
            var type = typeof(T);

            // Get instance of the attribute.
            var tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));

            if (!(tableAttribute == null))
                tableName = tableAttribute.Name;

            return tableName;
        }

        /// <summary>
        /// Method untuk mengecek apakah sebuah tanggal null atau tidak
        /// </summary>
        /// <param name="tanggal"></param>
        /// <returns></returns>
        public static bool IsNull(this Nullable<DateTime> tanggal)
        {
            var result = true;

            try
            {
                result = tanggal == DateTime.MinValue || tanggal == new DateTime(1753, 1, 1) ||
                         tanggal == new DateTime(0001, 1, 1) || tanggal == null;
            }
            catch
            {
            }

            return result;
        }

        /// <summary>
        /// Method untuk mengkonversi tanggal ke format utc
        /// </summary>
        /// <param name="tanggal"></param>
        /// <returns></returns>
        public static Nullable<DateTime> ToUtc(this Nullable<DateTime> tanggal)
        {
            return DateTime.SpecifyKind((DateTime)tanggal, DateTimeKind.Utc);
        }

        /// <summary>
        /// Method untuk mengkonversi nilai object ke format json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Method untuk mengkonversi nilai null menjadi string kosong
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NullToString(this object value)
        {
            return value == null ? "" : value.ToString();
        }

        /// <summary>
        /// Get substring of specified number of characters on the left.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Length > length ? value.Substring(0, length) : value;
        }

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Length > length ? value.Substring(value.Length - length) : value;
        }
    }
}
