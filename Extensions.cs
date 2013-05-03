using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel;

namespace Moosend.API.Client
{
    internal static class Extensions
    {
        public static bool DerivesFrom<T>(this Type type)
        {
            return type.DerivesFrom(typeof(T));
        }

        public static bool IsOrDerivesFrom<T>(this Type type)
        {
            if (object.ReferenceEquals(type, typeof(T))) return true;
            return type.DerivesFrom(typeof(T));
        }

        private static bool DerivesFrom(this Type type, Type baseType)
        {
            if (object.ReferenceEquals(type, baseType)) return false;
            if (type.IsInterface && !baseType.IsInterface) return false;
            if (type == null || baseType == null) return false;
            if (baseType.IsInterface) return type.GetInterfaces().Contains(baseType);

            while ((!object.ReferenceEquals(type, baseType)))
            {
                if (object.ReferenceEquals(type, typeof(object))) return false;
                type = type.BaseType;
            }
            return true;
        }

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

        /// <summary>
        /// Convert the given object to an object of type T. 
        /// </summary>
        public static T Convert<T>(this object from)
        {
            return Convert<T>(from, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert the given object to an object of type T. 
        /// </summary>
        public static T Convert<T>(this object from, CultureInfo info)
        {
            if (from == null) return default(T);

            try
            {
                Type t = typeof(T);
                if (t.IsEnum) return (T)Enum.Parse(t, from.ToString());
                else if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    // UnderlyingType will equal System.DateTime
                    NullableConverter nc = new NullableConverter(t);
                    return (T)System.Convert.ChangeType(from, nc.UnderlyingType, info);
                }
                else return (T)System.Convert.ChangeType(from, t, info);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot convert from {0} to {1}: {2}", from.GetType().Name, typeof(T).Name, ex.Message));
            }
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
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}

