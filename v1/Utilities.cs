using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities.LinqBridge;
using System.Text;
using System.Reflection;

namespace Moosend.API.Client
{
    public class Utilities
    {
        private static void CopyProperties(Type type, object source, object target)
        {
            typeof(Utilities).GetMethod("CopyProperties", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(type).Invoke(null, new object[] { source, target });
        }

        public static void CopyProperties<T>(T source, T target)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    property.SetValue(target, property.GetValue(source, null), null);
                }
                else if (property.PropertyType.DerivesFrom<IEnumerable>())
                {
                    IEnumerable list1 = (IEnumerable)property.GetValue(source, null);
                    IEnumerable list2 = (IEnumerable)property.GetValue(target, null);
                    if (list1 != null && list2 != null && list1.GetType() == list2.GetType())
                    {
                        IEnumerator enum1 = list1.GetEnumerator();
                        IEnumerator enum2 = list2.GetEnumerator();
                        while (true)
                        {
                            bool moved1 = enum1.MoveNext();
                            bool moved2 = enum2.MoveNext();
                            if (moved1 && moved2)
                            {
                                if (enum1.Current != null && enum2.Current != null)
                                {
                                    CopyProperties(enum1.Current.GetType(), enum1.Current, enum2.Current);
                                }
                            }
                            else if (moved1 != moved2)
                            {
                                throw new InvalidOperationException(string.Format("Failed to copy values for property {0}\nReason: IEnumerable count mismatch", property.Name));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("Failed to copy values for property {0}\nReason: null state or type mismatch", property.Name));
                    }
                }
                else
                {
                    object value1 = property.GetValue(source, null);
                    object value2 = property.GetValue(target, null);
                    if (value1 != null || value2 != null)
                    {
                        if (value1 != null && value2 != null && value1.GetType() == value2.GetType())
                        {
                            CopyProperties(value1.GetType(), value1, value2);
                        }
                        else
                        {
                            throw new InvalidOperationException(string.Format("Failed to copy values for property {0}\nReason: null state or type mismatch", property.Name));
                        }
                    }
                }
            }
        }
    }
}
