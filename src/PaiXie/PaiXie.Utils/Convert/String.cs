using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Utils
{
    public partial class ZConvert
    {
        /// <summary>
        /// 转换为string类型 defult为string.Empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
             return To<string>(obj,string.Empty);
        }
    }
}
