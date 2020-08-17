using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PaiXie.Core {
	public class Common {
		/// <summary>
		/// 执行线程异步回调方法
		/// 内部发生异常IIS不受影响 
		/// </summary>
		/// <param name="call">异步回调方法</param>
		/// <param name="par">回调方法的传入参数</param>
		public static void RunAsyn(System.Threading.WaitCallback call, object par) {
			call.BeginInvoke(par, null, null);
		}

		#region 创建自定义表

		/// <summary>
		/// 创建DataTable
		/// </summary>
		/// <param name="TableName">表名</param>
		/// <param name="Fields">自定义字段</param>
		/// <returns></returns>
		public static DataTable CreateCustomTable(string TableName, string Fields) {
			return CreateCustomTable(TableName, Fields.Split(','));
		}

		/// <summary>
		/// 创建DataTable
		/// </summary>
		/// <param name="TableName">表名</param>
		/// <param name="Fields">自定义字段</param>
		/// <returns></returns>
		/// 
		public static DataTable CreateCustomTable(string TableName, string[] Fields) {
			DataTable dt = new DataTable(TableName);
			for (int i = 0; i < Fields.Length; i++) {
				DataColumn addcol = new DataColumn(Fields[i], Type.GetType("System.String"));
				dt.Columns.Add(addcol);

			}
			return dt;
		}
		#endregion
	}
}
