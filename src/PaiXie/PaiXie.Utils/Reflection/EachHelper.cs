using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace PaiXie.Utils
{
    public class EachHelper
    {
        public static void EachListHeader(object list, Action<int, string,Type> handle)
        {
            var index = 0;
            var dict = ZGeneric.GetListProperties(list);
            foreach(var item in dict)
                handle(index++, item.Key, item.Value); 
        }

        public static void EachListRow(object list, Action<int, object> handle)
        {
            var index = 0;
            IEnumerator enumerator = ((dynamic)list).GetEnumerator();
            while (enumerator.MoveNext())
                handle(index++, enumerator.Current);
        }

        public static void EachObjectProperty(object row, Action<int, string, object> handle)
        {
            var index = 0;
            var dict = ZGeneric.GetDictionaryValues(row);
            foreach (var item in dict)
                handle(index++, item.Key, item.Value);
        }
    }
}
