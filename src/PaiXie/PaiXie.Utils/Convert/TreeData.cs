using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace PaiXie.Utils
{
    public partial class ZConvert
    {
        public static List<dynamic> ListToTreeData<T>(List<T> source, string ID, string PID) where T : class, new()
        {
            Action<List<dynamic>, T, dynamic> AddItem = (parent, item, Recursive) =>
            {
                var childrens = new List<dynamic>();
                var thisitem = ZGeneric.GetDictionaryValues(item);

                source.FindAll(o => ZGeneric.GetValue(item, ID).Equals(ZGeneric.GetValue(o, PID)))
                      .ForEach(subitem => { Recursive(childrens, subitem, Recursive); });

                thisitem.Add("children", childrens);
                parent.Add(thisitem);
            };

            var target = new List<dynamic>();
            source.FindAll(m => { return !source.Exists(n => ZGeneric.GetValue(n, ID).Equals(ZGeneric.GetValue(m, PID))); })
                  .ForEach(item => AddItem(target, item, AddItem));

            return target;
        }

        public static List<T> TreeDataToList<T>(List<dynamic> source)
        {
            Action<List<dynamic>, List<T>, dynamic> AddItem = (mysource, mytarget, Recursive) =>
            {
                foreach (var oldrow in mysource)
                {
                    var newrow = ZGeneric.CreateNew<T>();
                    var dictionary = (IDictionary<string, object>)ZGeneric.GetDictionaryValues(oldrow);

                    var childern = dictionary["childern"] as List<dynamic>;
                    if (childern.Count > 0) Recursive(mysource, mytarget, Recursive);

                    foreach (var property in dictionary)
                        if (property.Key != "children")
                            ZGeneric.SetValue(newrow, property.Key, property.Value);

                    mytarget.Add(newrow);
                }
            };

            var target = new List<T>();
            AddItem(source, target, AddItem);

            return target;
        }
    }
}
