using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;

namespace PaiXie.Utils
{
    public partial class ZConvert
    {
        /// <summary>
        /// 转换object为 T 值   
        /// </summary>
        /// <typeparam name="T">T 类型</typeparam>
        /// <param name="obj">要被转换的值</param>
        /// <returns>T 类型值</returns>
        public static T To<T>(object obj)
        {
            return To<T>(obj, default(T));
        }

        /// <summary>
        /// 转换object为 T 值   
        /// </summary>
        /// <typeparam name="T">T 类型</typeparam>
        /// <param name="obj">要被转换的值</param>
        /// <returns>T 类型值</returns>
        public static T To<T>(object obj,T defaultValue)
        {
            if (obj==null)
            {
                return defaultValue;
            }
            else if (obj is T)
            {
                return (T)obj;
            }
            else
            {
                try
                {
                    Type conversionType = typeof(T);
                    object obj2 = null;
                    if (conversionType.Equals(typeof(Guid)))
                        obj2 = new Guid(Convert.ToString(obj));
                    else
                        obj2 = Convert.ChangeType(obj, conversionType);
                    return (T)obj2;
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 填充客户端提交的值到 T 对象  如appinfo = AppConvert.To<Appinfo>(context.Request.Form);
        /// </summary>
        /// <typeparam name="T">T 类</typeparam>
        /// <param name="datas">客户端提交的值</param>
        /// <returns>T 对象</returns>
        public static T To<T>(NameValueCollection datas) where T : class, new()
        {
            Type type = typeof(T);
            string[] strArray = type.FullName.Split(new char[] { '.' });
            string str = strArray[strArray.Length - 1];
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            T local = Activator.CreateInstance<T>();
            foreach (string str2 in datas.AllKeys)
            {
                string str3 = datas[str2];
                if (!string.IsNullOrEmpty(str3))
                {
                    str3 = str3.TrimEnd(new char[0]);
                }
                foreach (PropertyInfo info in properties)
                {
                    string str4 = string.Format("{0}.{1}", str, info.Name);
                    if (str2.Equals(info.Name, StringComparison.CurrentCultureIgnoreCase) || str2.Equals(str4, StringComparison.CurrentCultureIgnoreCase))
                    {
                        string typeName = info.PropertyType.ToString();
                        if (info.PropertyType.IsGenericType)
                        {
                            typeName = info.PropertyType.GetGenericArguments()[0].ToString();
                        }
                        object nullInternal = GetNullInternal(info.PropertyType);
                        Type conversionType = Type.GetType(typeName, false);
                        if (!string.IsNullOrEmpty(str3))
                        {
                            nullInternal = Convert.ChangeType(str3, conversionType);
                        }
                        info.SetValue(local, nullInternal, null);
                    }
                }
            }
            return local;
        }

        #region 获取类型的默认值
        //另一种获取默认值的方法
        private static object GetDefaultValue(Type type)
        {
            object value = null;

            if (type.IsValueType)
                value = Activator.CreateInstance(type);
            else
                value = null;

            return value;
        }

        // 获取指定类型的默认值.引用类型(包含String)的默认值为null
        private static T DefaultValue<T>()
        {
            return default(T);
        }

        //获取默认值
        private static object GetNullInternal(Type type)
        {
            if (type.IsValueType)
            {
                if (type.IsEnum)
                {
                    return GetNullInternal(Enum.GetUnderlyingType(type));
                }
                if (type.IsPrimitive)
                {
                    if (type == typeof(int))
                    {
                        return 0;
                    }
                    if (type == typeof(double))
                    {
                        return 0.0;
                    }
                    if (type == typeof(short))
                    {
                        return (short)0;
                    }
                    if (type == typeof(sbyte))
                    {
                        return (sbyte)0;
                    }
                    if (type == typeof(long))
                    {
                        return 0L;
                    }
                    if (type == typeof(byte))
                    {
                        return (byte)0;
                    }
                    if (type == typeof(ushort))
                    {
                        return (ushort)0;
                    }
                    if (type == typeof(uint))
                    {
                        return 0;
                    }
                    if (type == typeof(ulong))
                    {
                        return (ulong)0L;
                    }
                    if (type == typeof(ulong))
                    {
                        return (ulong)0L;
                    }
                    if (type == typeof(float))
                    {
                        return 0f;
                    }
                    if (type == typeof(bool))
                    {
                        return false;
                    }
                    if (type == typeof(char))
                    {
                        return '\0';
                    }
                }
                else
                {
                    if (type == typeof(DateTime))
                    {
                        return DateTime.MinValue;
                    }
                    if (type == typeof(decimal))
                    {
                        return 0M;
                    }
                    if (type == typeof(Guid))
                    {
                        return Guid.Empty;
                    }
                    if (type == typeof(TimeSpan))
                    {
                        return new TimeSpan(0, 0, 0);
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
