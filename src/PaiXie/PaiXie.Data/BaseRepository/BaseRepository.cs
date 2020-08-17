using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class BaseRepository<T> where T : class {
		/// <summary>
		/// 返回单个实体
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual T GetQuerySingle(string sqlStr, IDbContext context = null, params Object[] objects) {
			
			
			if (context == null) context = Db.GetInstance().Context();
			T obj = context.Sql(sqlStr, objects).QuerySingle<T>();
			return obj;
		}
		

		/// <summary>
		/// 返回实体列表
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<T> GetQueryMany(string sqlStr, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			List<T> objlist = context.Sql(sqlStr, objects).QueryMany<T>();
			return objlist;
		}

		/// <summary>
		/// 返回DataTable
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual DataTable GetDataTable(string sqlStr, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			DataTable dt = context.Sql(sqlStr, objects).QuerySingle<DataTable>();
			return dt;
		}

		/// <summary>
		/// 返回条数
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int GetCount(string sqlStr, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			int count = context.Sql(sqlStr, objects).QuerySingle<int>();
			return count;
		}
		/// <summary>
		/// 获取单个字段的值 
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		public virtual string Getobject(string sqlStr, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			string count = context.Sql(sqlStr, objects).QuerySingle<string>();
			return count;
		}

		/// <summary>
		/// 更新操作
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int Update(string sqlStr, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Sql(sqlStr, objects).Execute();
			return rowsAffected;
		}
		/// <summary>
		/// 删除操作
		/// </summary>
		/// <param name="sqlStr"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int Del(string sqlStr, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Sql(sqlStr, objects)
					.Execute();
			return rowsAffected;
		}
		

		/// <summary>
		/// 分页列表
		/// </summary>
		/// <param name="data"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<T> GetQueryManyForPage(SelectBuilder data, out int count, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			string sqlStr = Db.GetInstance().GetSqlForSelectBuilder(data);
		   string sqlStrCount = Db.GetInstance().GetSqlForTotalBuilder(data);
			count = context.Sql(sqlStrCount, objects).QuerySingle<int>();
			List<T> objlist = context.Sql(sqlStr, objects).QueryMany<T>();
			return objlist;
		}

		/// <summary>
		/// 返回DataTable
		/// </summary>
		/// <param name="data"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public DataTable GetDataTableForPage(SelectBuilder data, IDbContext context = null, params Object[] objects) {
			if (context == null) context = Db.GetInstance().Context();
			string sqlStr = Db.GetInstance().GetSqlForSelectBuilder(data);
			return GetDataTable(sqlStr, context, objects);
		}
	}
}