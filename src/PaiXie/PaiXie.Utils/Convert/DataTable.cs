using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace PaiXie.Utils
{
    public partial class ZConvert
    {
        /// <summary>
        /// 转换 DataTable 对象为 IList 对象
        /// </summary>
        /// <param name="datas">数据集合</param>
        /// <returns>数组对象</returns>
        public static T[] ToArray<T>(DataTable datas) where T : class, new()
        {
            List<T> list = ToList<T>(datas) as List<T>;
            return list.ToArray();
        }

        /// <summary>
        /// 转换IList对象为DataTable对象
        /// </summary>
        /// <param name="datas">数据集合</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ToDataTable<T>(IList<T> datas)
        {
            return ToDataTable<T>(datas, null);
        }

        /// <summary>
        /// 转换IList对象为DataTable对象
        /// </summary>
        /// <param name="datas">数据集合</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ToDataTable<T>(T[] datas)
        {
            return ToDataTable<T>(datas, null);
        }

        /// <summary>
        /// 转换IList对象为DataTable对象
        /// </summary>
        /// <param name="datas">数据集合</param>
        /// <param name="tableName">要创建的表名</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ToDataTable<T>(IList<T> datas, string tableName)
        {
            Type type = typeof(T);
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = type.Name;
            }
            DataTable table = new DataTable(tableName);
            table.BeginLoadData();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                string typeName = info.PropertyType.ToString();
                if (info.PropertyType.IsGenericType)
                {
                    typeName = info.PropertyType.GetGenericArguments()[0].ToString();
                }
                Type type2 = Type.GetType(typeName, false);
                if (type2 != null)
                {
                    table.Columns.Add(info.Name, type2);
                }
            }
            if ((datas != null) && (datas.Count > 0))
            {
                foreach (object obj2 in datas)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyInfo info2 in properties)
                    {
                        if ((Type.GetType(info2.PropertyType.ToString(), false) != null) && (info2.GetValue(obj2, null) != null))
                        {
                            row[info2.Name] = info2.GetValue(obj2, null);
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            table.EndLoadData();
            table.AcceptChanges();
            return table;
        }

        public static DataTable ListToDataTable(object datas, string tableName)
        {
            Type type = ZGeneric.GetGenericType(datas);
            if (string.IsNullOrEmpty(tableName))
                tableName = type.Name;
            
            DataTable table = new DataTable(tableName);
            table.BeginLoadData();

            var properties = ZReflection.GetProperties(type);
            foreach (var p in properties)
            {
                Type colType = p.Value.PropertyType;
                string typeName = colType.ToString();
                if (colType.IsGenericType)
                    typeName = colType.GetGenericArguments()[0].ToString();
                
                Type newType = Type.GetType(typeName, false);
                if (newType != null)
                    table.Columns.Add(p.Value.Name, newType);
            }

            IEnumerator enumerator = ((dynamic)datas).GetEnumerator();
            while (enumerator.MoveNext())
            {
                DataRow row = table.NewRow();
                foreach (var p in properties)
                {
                    var value = ZGeneric.GetValue(enumerator.Current, p.Value.Name);
                    if ((Type.GetType(p.Value.PropertyType.ToString(), false) != null) && (value != null))
                        row[p.Value.Name] = value;
                }
                table.Rows.Add(row);
            }
            table.EndLoadData();
            table.AcceptChanges();
            return table;
        }

        /// <summary>
        /// 转换IList对象为DataTable对象
        /// </summary>
        /// <param name="datas">数据集合</param>
        /// <param name="tableName">要创建的表名</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ToDataTable<T>(T[] datas, string tableName)
        {
            IList<T> list;
            if ((datas == null) || (datas.Length == 0))
            {
                list = new List<T>();
            }
            else
            {
                list = new List<T>(datas);
            }
            return ToDataTable<T>(list, tableName);
        }

        /// <summary>
        /// 转换 DataTable 对象为 IList 对象
        /// </summary>
        /// <param name="datas">数据集合</param>
        /// <returns>IList 对象</returns>
        public static IList<T> ToList<T>(DataTable datas) where T : class, new()
        {
            IList<T> list = new List<T>();
            if ((datas != null) && (datas.Rows.Count != 0))
            {
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (DataRow row in datas.Rows)
                {
                    T local = Activator.CreateInstance<T>();
                    foreach (DataColumn column in datas.Columns)
                    {
                        object obj2 = null;
                        if (row.RowState == DataRowState.Deleted)
                        {
                            obj2 = row[column, DataRowVersion.Original];
                        }
                        else
                        {
                            obj2 = row[column];
                        }
                        if (obj2 != DBNull.Value)
                        {
                            foreach (PropertyInfo info in properties)
                            {
                                if (column.ColumnName.Equals(info.Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    info.SetValue(local, obj2, null);
                                }
                            }
                        }
                    }
                    list.Add(local);
                }
            }
            return list;
        }
    }
}
