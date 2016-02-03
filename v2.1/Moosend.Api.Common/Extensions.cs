using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Moosend.Api.Common
{
    public static class Extensions
    {
        public static object GetDefault(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        public static void CheckNotNull(this object value, string paramName)
        {
            if (value == null || object.Equals(value, GetDefault(value.GetType())))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        private static string ToFormattedString(this object value)
        {
            if (value == null)
                return null;
            else if (value.GetType() == typeof(DateTime))
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            else
                return value.ToString();
        }

        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<string>().ToArray());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToFormattedString()))).ToArray());
        }
    }
}

