using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class SyslogRepository : BaseRepository<Syslog> {

		#region 构造函数
		private static SyslogRepository _instance;
		public static SyslogRepository GetInstance() {
			if (_instance == null) {
				_instance = new SyslogRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Syslog entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<Syslog>("sys_log", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Syslog GetQuerySingleByID(int ID, IDbContext context = null) {

			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = ID;
			string sql = "SELECT  *  FROM sys_log  WHERE ID=@0";
			Syslog obj = context.Sql(sql, objects).QuerySingle<Syslog>();

			return obj;
		}
		#endregion

		#region 删除操作  通过ID
		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int DelByID(int ID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = ID;
			int rowsAffected = context.Sql("DELETE  FROM  sys_log   WHERE ID=@0", objects)
					.Execute();
			return rowsAffected;
		}

		#endregion

		#region Update
		public int Update(Syslog entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Syslog>("sys_log", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion
	}
}