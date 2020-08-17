using FluentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
public	class Db {

	#region 构造函数

	private static Db _instance;
	public static Db GetInstance() {
		if (_instance == null) {
			_instance = new Db();
		}
		return _instance;
	}

	#endregion

	#region Context

	public IDbContext Context(string connstr = "conn") {
		return new DbContext().ConnectionStringName(connstr,
				new MySqlProvider());
	}
	
	#endregion

	#region 分页语句拼接
	public string GetSqlForSelectBuilder(SelectBuilder data) {
		var sql = "";
		sql = "select " + data.Select;
		sql += " from " + data.From;
		if (data.WhereSql.Length > 0)
			sql += " where " + data.WhereSql;
		if (data.GroupBy.Length > 0)
			sql += " group by " + data.GroupBy;
		if (data.Having.Length > 0)
			sql += " having " + data.Having;
		if (data.OrderBy.Length > 0)
			sql += " order by " + data.OrderBy;
		if (data.PagingItemsPerPage > 0
			&& data.PagingCurrentPage > 0) {
			sql += string.Format(" limit {0}, {1}", ((data.PagingCurrentPage * data.PagingItemsPerPage) - data.PagingItemsPerPage + 1) - 1, data.PagingItemsPerPage);
		}
		return sql;
	}
	public string GetSqlForTotalBuilder(SelectBuilder data) {
		var sql = "";
		sql = "select count(*)";
		sql += " from " + data.From;
		if (data.WhereSql.Length > 0)
			sql += " where " + data.WhereSql;
		return sql;
	} 
	#endregion


	}
}

#region SelectBuilder
public class SelectBuilder {
	public int PagingCurrentPage { get; set; }
	public int PagingItemsPerPage { get; set; }
	public string Having { get; set; }
	public string GroupBy { get; set; }
	public string OrderBy { get; set; }
	public string From { get; set; }
	public string Select { get; set; }
	public string WhereSql { get; set; }
} 
#endregion