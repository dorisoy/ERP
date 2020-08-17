using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace PaiXie.Utils
{
    public class ZGeneric
    {
        public static bool IsDynamicType(Type type)
        {
            return type.Equals(typeof(ExpandoObject)) || type.Equals(typeof(object));
        }

        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }  

        public static Type GetGenericType(object list)
        {
            return list.GetType().GetGenericArguments()[0];
        }

        public static bool IsTypeIgoreNullable<T>(object value)
        {
            if (null == value) return false;

            Type type = value.GetType();
            if (type.IsGenericType)
                type = type.GetGenericArguments()[0];

            return type.Equals(typeof(T));
        }

        public static T CreateNew<T>()
        {
            if (IsDynamicType(typeof(T)))
                return (T)(IDictionary<string, object>)new ExpandoObject();

            return Activator.CreateInstance<T>();
        }

        public static object GetValue(object item, string name)
        {
            if (IsDynamicType(item.GetType()))
                return ZReflection.GetPropertyValueDynamic(item, name);
            
            return ZReflection.GetPropertyValue(item, name);
        }

        public static void SetValue(object item,string name,object value)
        {
            Type type = item.GetType();
            if (IsDynamicType(type))
                ((IDictionary<string, object>)item).Add(name, value);

            var property = type.GetProperty(name);
            property.SetValue(item, value, null);
        }

        public static Dictionary<string, Type> GetListProperties(dynamic list)
        {
            var type = ZGeneric.GetGenericType(list);
            var names = new Dictionary<string,Type>();

            if (ZGeneric.IsDynamicType(type))
            {
                if (list.Count > 0)
                    foreach (var item in ZGeneric.GetDictionaryValues(list[0]))
                        names.Add(item.Key, (item.Value ?? string.Empty).GetType());
            }
            else
            {
                foreach (var p in ZReflection.GetProperties(type))
                    names.Add(p.Value.Name, p.Value.PropertyType);
            }

            return names;
        }

        public static IDictionary<string, object> GetDictionaryValues(object item)
        {
            if (IsDynamicType(item.GetType()))
                return item as IDictionary<string, object>;

            var expando = (IDictionary<string, object>)new ExpandoObject();
            var properties = ZReflection.GetProperties(item.GetType());
            foreach (var p in properties)
                expando.Add(p.Value.Name, p.Value.GetValue(item, null));
            return expando;
        }
    }
}
