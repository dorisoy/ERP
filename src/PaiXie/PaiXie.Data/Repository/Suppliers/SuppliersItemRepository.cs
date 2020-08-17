using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
using PaiXie.Utils;
namespace PaiXie.Data 
{
	public class SuppliersItemRepository : BaseRepository<SuppliersItem> {

		#region 构造函数

		private static SuppliersItemRepository _instance;
		public static SuppliersItemRepository GetInstance() {
			if (_instance == null) {
				_instance = new SuppliersItemRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(SuppliersItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<SuppliersItem>("suppliersItem", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(SuppliersItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<SuppliersItem>("suppliersItem", entity)
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
		public virtual SuppliersItem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM suppliersItem WHERE ID=@0";
			SuppliersItem obj = GetQuerySingle(sqlStr, context, objects);
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
			int rowsAffected = context.Sql("DELETE  FROM  suppliersItem   WHERE ID=" + ID)
					.Execute();
			return rowsAffected;
		}

		#endregion

		#region 根据商品ID查询关联供应商

		/// <summary>
		/// 根据商品ID查询关联供应商
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual DataTable GetQueryManyByProductsID(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = @"  SELECT suppi.*,supp.AliasName FROM suppliersItem suppi 
LEFT JOIN suppliers supp ON suppi.SuppliersID=supp.ID WHERE suppi.ProductsID=@0";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKUID查询关联供应商

		/// <summary>
		/// 根据商品SKUID查询关联供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual DataTable GetQueryManyByProductsSkuID(int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;
			string sqlStr = @"  SELECT suppi.*,supp.AliasName FROM suppliersItem suppi 
LEFT JOIN suppliers supp ON suppi.SuppliersID=supp.ID WHERE suppi.ProductsSkuID=@0";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 设置供应商为SKU默认供应商，并清除之前的默认供应商

		/// <summary>
		/// 设置当前供应商为SKU默认供应商，并清除之前的默认供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateIsDefault(int productsSkuID, int suppliersID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = productsSkuID;
			objects[1] = suppliersID;
			string sqlStr = @"UPDATE suppliersItem SET IsDefault=1 WHERE ProductsSkuID=@0 AND SuppliersID=@1";
			int count = Update(sqlStr, context, objects);
			if (count > 0) {
				sqlStr = @"UPDATE suppliersItem SET IsDefault=0 WHERE ProductsSkuID=@0 AND SuppliersID<>@1";
				Update(sqlStr, context, objects);
			}
			return count;
		}

		#endregion

		#region 根据商品SKUID获取默认供应商

		/// <summary>
		/// 根据商品SKUID获取默认供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetDefaultSuppliersID(int productsSkuID, IDbContext context = null) {
			int defaultSuppliersID = 0;
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;
			string sqlStr = @"SELECT IFNULL(suppi.SuppliersID,0) AS SuppliersID, IsDefault FROM suppliersItem suppi WHERE suppi.ProductsSkuID=@0";
			DataTable dt = GetDataTable(sqlStr, context, objects);
			if (dt.Rows.Count == 1) {
				//如果只有一条，就当作默认供应商
				defaultSuppliersID = ZConvert.StrToInt(dt.Rows[0]["SuppliersID"]);
			}
			else {
				DataRow[] dr = dt.Select("IsDefault=1");
				if (dr.Length > 0) {
					defaultSuppliersID = ZConvert.StrToInt(dr[0]["SuppliersID"]);
				}
			}
			return defaultSuppliersID;
		}

		#endregion

		#region 根据商品SKUID和供应商ID获取单个实体

		/// <summary>
		/// 根据商品SKUID和供应商ID获取单个实体
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public SuppliersItem GetSingleSuppliersItem(int productsSkuID, int suppliersID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = productsSkuID;
			objects[1] = suppliersID;
			string sqlStr = "SELECT * FROM suppliersItem WHERE ProductsSkuID=@0 AND SuppliersID=@1";
			SuppliersItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取供应商商品数量

		/// <summary>
		/// 获取供应商商品数量
		/// </summary>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetProductsCount(int suppliersID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = suppliersID;
			string sqlStr = "SELECT COUNT(DISTINCT ProductsID) FROM suppliersItem WHERE suppliersID=@0";
			return GetCount(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品ID删除供应商商品

		/// <summary>
		/// 根据商品ID删除供应商商品
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int DelByProductsID(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = "DELETE FROM suppliersItem WHERE ProductsID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion
		
		#region 根据供应商ID和商品ID获取到货周期

		/// <summary>
		/// 根据供应商ID和商品ID获取到货周期
		/// </summary>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public string GetArrivalCycle(int suppliersID, int productsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = suppliersID;
			objects[1] = productsID;
			string sqlStr = "SELECT ArrivalCycle FROM suppliersItem WHERE SuppliersID=@0 AND ProductsID=@1 ORDER BY ArrivalCycle DESC LIMIT 0,1";
			return Getobject(sqlStr, context, objects);
		}

		#endregion
	}
}





