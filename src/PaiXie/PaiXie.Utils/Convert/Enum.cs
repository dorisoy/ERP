using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
namespace PaiXie.Utils
{
    public partial class ZConvert
    {
        public static T ToEnum<T>(object obj, T defaultEnum)
        {
            string str = To<string>(obj);

            if (Enum.IsDefined(typeof(T),str))
                return (T)Enum.Parse(typeof(T),str);

            int num;
            if (int.TryParse(str, out num))
            {
                if (Enum.IsDefined(typeof(T), num))
                    return (T)Enum.ToObject(typeof(T), num);
            }

            return defaultEnum;
        }
    }	
	public partial class EnumManager<TEnum> {
		/// <summary>
		/// 枚举转DataTable
		/// </summary>
		/// <param name="firstValue">第一项请选择的值 默认0</param>
		/// <param name="showDescription">是否显示说明</param>
		/// <returns></returns>
		public static DataTable GetDataTable(int firstValue = 0, bool showDescription = false) {
			Type enumType = typeof(TEnum);  // 获取类型对象
			FieldInfo[] enumFields = enumType.GetFields();
			DataTable table = new DataTable();
			table.Columns.Add("Name", Type.GetType("System.String"));
			table.Columns.Add("Value", Type.GetType("System.Int32"));
			DataRow trow = table.NewRow();
			trow[0] = "请选择";
			trow[1] = firstValue;
			//row[1] = (int)Enum.Parse(enumType, field.Name); 也可以这样
			table.Rows.Add(trow);

			//遍历集合
			foreach (FieldInfo field in enumFields) {
				if (!field.IsSpecialName) {
					DataRow row = table.NewRow();
					string fieldName = string.Empty;
					if (showDescription) {
						object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
						if (arr != null) {
							fieldName = ((DescriptionAttribute)arr[0]).Description;
						}
					}
					else {
						fieldName = field.Name;
					}
					row[0] = fieldName;
					row[1] = ZConvert.StrToInt(field.GetRawConstantValue());
					//row[1] = (int)Enum.Parse(enumType, field.Name); 也可以这样
					table.Rows.Add(row);
				}
			}
			return table;
		}
	}
}