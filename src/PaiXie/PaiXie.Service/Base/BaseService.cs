using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PaiXie.Data;
using FluentData;
namespace PaiXie.Service {
	public class BaseService<T> where T : class {

		#region BaseRepository
		private static BaseRepository<T> m_DataBase;
		private static BaseRepository<T> DataBase {
			get {
				if (m_DataBase == null) {
					m_DataBase = new BaseRepository<T>();
				}
				return m_DataBase;
			}
		}

		#endregion

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="sqlStr">sql语句</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static T GetQuerySingle(string sqlStr, IDbContext context = null, params Object[] objects) {
			return DataBase.GetQuerySingle(sqlStr, context, objects);
		}
	


		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="sqlStr">sql语句</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static List<T> GetQueryMany(string sqlStr, IDbContext context = null, params Object[] objects) {
			return DataBase.GetQueryMany(sqlStr, context, objects);
		}
		/// <summary>
		/// 分页数据
		/// </summary>
		/// <param name="data"></param>
		/// <param name="count"></param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static List<T> GetQueryManyForPage(SelectBuilder data, out int count, IDbContext context = null, params Object[] objects) {
			return DataBase.GetQueryManyForPage(data, out  count, context, objects);
		}

		/// <summary>
		/// 返回 DataTable
		/// </summary>
		/// <param name="sqlStr">sql语句</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static DataTable GetDataTable(string sqlStr, IDbContext context = null, params Object[] objects) {
			DataTable objlist = DataBase.GetDataTable(sqlStr, context, objects);
			return objlist;
		}

		/// <summary>
		/// 行数
		/// </summary>
		/// <param name="sqlStr">sql语句</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static int GetCount(string sqlStr, IDbContext context = null, params Object[] objects) {
			return DataBase.GetCount(sqlStr, context, objects);
		}
		/// <summary>
		/// 获取单个字段的值
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		public static string Getobject(string sqlStr, IDbContext context = null, params Object[] objects) {
			return DataBase.Getobject(sqlStr, context, objects);
		}

		
		/// <summary>
		/// 更新  sqlStr
		/// </summary>
		/// <param name="sqlStr">sql语句</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static int Update(string sqlStr, IDbContext context = null, params Object[] objects) {
			return DataBase.Update(sqlStr, context, objects);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sqlStr">sql语句</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="objects">参数化数组</param>
		/// <returns></returns>
		public static int Del(string sqlStr, IDbContext context = null, params Object[] objects) {
			return DataBase.Del(sqlStr, context, objects);
		}
	
		/// <summary>
		/// 返回DataTable
		/// </summary>
		/// <param name="data"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable GetDataTableForPage(SelectBuilder data, IDbContext context = null, params Object[] objects) {
			return DataBase.GetDataTableForPage(data, context, objects);
		}
	}
}
