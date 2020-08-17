using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
	public class SuppliersRepository : BaseRepository<Suppliers> {

		#region 构造函数

		private static SuppliersRepository _instance;
		public static SuppliersRepository GetInstance() {
			if (_instance == null) {
				_instance = new SuppliersRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(Suppliers entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<Suppliers>("suppliers", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(Suppliers entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Suppliers>("suppliers", entity)
					.AutoMap(x => x.ID)
					.Where(x => x.ID)
					.Execute();
			return rowsAffected;
		}
		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual Suppliers GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM suppliers WHERE ID=@0";
			Suppliers obj = GetQuerySingle(sqlStr, context, objects);
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
			int rowsAffected = context.Sql("DELETE  FROM  suppliers   WHERE ID=" + ID)
					.Execute();
			return rowsAffected;
		}

		#endregion

		#region 根据供应商名称获取单个实体

		/// <summary>
		/// 根据供应商名称获取单个实体
		/// </summary>
		/// <param name="suppliersName">供应商名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual Suppliers GetQuerySingleByName(string suppliersName, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = suppliersName;
			string sqlStr = "SELECT * FROM suppliers WHERE Name=@0";
			Suppliers obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据供应商简称获取单个实体

		/// <summary>
		/// 根据供应商简称获取单个实体
		/// </summary>
		/// <param name="aliasName">供应商简称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual Suppliers GetQuerySingleByAliasName(string aliasName, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = aliasName;
			string sqlStr = "SELECT * FROM suppliers WHERE AliasName=@0";
			Suppliers obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据供应商名称获取供应商ID

		/// <summary>
		/// 根据供应商名称获取供应商ID
		/// </summary>
		/// <param name="name">供应商名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetIDByName(string name, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = name;
			string sqlStr = "SELECT ID FROM suppliers WHERE Name=@0";
			Suppliers suppliers = GetQuerySingle(sqlStr, context, objects);
			int suppliersID = 0;
			if (suppliers != null) {
				suppliersID = suppliers.ID;
			}
			return suppliersID;
		}

		#endregion

		#region 根据供应商名称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)

		/// <summary>
		/// 根据供应商名称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)
		/// </summary>
		/// <param name="name">供应商名称</param>
		/// <param name="exceptSuppliersID">需要排除供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetIDByName(string name, int exceptSuppliersID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = name;
			objects[1] = exceptSuppliersID;
			string sqlStr = "SELECT ID FROM suppliers WHERE Name=@0 and ID<>@1";
			Suppliers suppliers = GetQuerySingle(sqlStr, context, objects);
			int suppliersID = 0;
			if (suppliers != null) {
				suppliersID = suppliers.ID;
			}
			return suppliersID;
		}

		#endregion

		#region 根据供应商简称获取供应商ID

		/// <summary>
		/// 根据供应商简称获取供应商ID
		/// </summary>
		/// <param name="aliasName">供应商简称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetIDByAliasName(string aliasName, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = aliasName;
			string sqlStr = "SELECT ID FROM suppliers WHERE AliasName=@0";
			Suppliers suppliers = GetQuerySingle(sqlStr, context, objects);
			int suppliersID = 0;
			if (suppliers != null) {
				suppliersID = suppliers.ID;
			}
			return suppliersID;
		}

		#endregion

		#region 根据供应商简称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)

		/// <summary>
		/// 根据供应商简称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)
		/// </summary>
		/// <param name="aliasName">供应商简称</param>
		/// <param name="exceptSuppliersID">需要排除供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetIDByAliasName(string aliasName, int exceptSuppliersID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = aliasName;
			objects[1] = exceptSuppliersID;
			string sqlStr = "SELECT ID FROM suppliers WHERE AliasName=@0 and ID<>@1";
			Suppliers suppliers = GetQuerySingle(sqlStr, context, objects);
			int suppliersID = 0;
			if (suppliers != null) {
				suppliersID = suppliers.ID;
			}
			return suppliersID;
		}

		#endregion
	}
}





