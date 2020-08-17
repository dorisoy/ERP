using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data {
	public class WarehouseProductsRepository : BaseRepository<WarehouseProducts> {

		#region 构造函数
		private static WarehouseProductsRepository _instance;
		public static WarehouseProductsRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseProductsRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseProducts>("warehouseProducts", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(WarehouseProducts entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseProducts>("warehouseProducts", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}

		#endregion

		/// <summary>
		/// 删除商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int Delete(string warehouseCode, List<int> productsIDList, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseProducts WHERE WarehouseCode = @0 AND FIND_IN_SET(ProductsID, @1)";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", productsIDList.ToArray());
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据商品ID删除仓库商品
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int DeleteByProductsID(int productsID, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseProducts WHERE ProductsID=@0";
			Object[] objects = new Object[1];
			objects[0] = productsID;
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 商品上下架
		/// </summary>
		/// <param name="warehouseCode">仓库编码 如果传空值，则更新所有仓库</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="productsStatus">商品销售状态 销售中=1 仓库中=2 </param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateProductsStatus(string warehouseCode, List<int> productsIDList, int productsStatus, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere = " and WarehouseCode=@2";
			}
			string sqlStr = @"UPDATE warehouseProducts SET ProductsStatus = @0 WHERE FIND_IN_SET(ProductsID, @1)" + strWhere;
			Object[] objects = new Object[3];
			objects[0] = productsStatus;
			objects[1] = string.Join(",", productsIDList.ToArray());
			objects[2] = warehouseCode;
			return Update(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取行数
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="productsStatus">商品销售状态 销售中=1 仓库中=2 </param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetCount(string warehouseCode, List<int> productsIDList, int productsStatus, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @1";
			}
			if (productsStatus > 0) {
				strWhere += " and ProductsStatus = @2";
			}
			string sqlStr = @"SELECT Count(ID) FROM warehouseProducts WHERE FIND_IN_SET(ProductsID, @0)" + strWhere;
			Object[] objects = new Object[3];
			objects[0] = string.Join(",", productsIDList.ToArray());
			objects[1] = warehouseCode;
			objects[2] = productsStatus;
			return GetCount(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseProducts GetSingleWarehouseProducts(string warehouseCode, int productsID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehouseProducts
                              WHERE WarehouseCode = @0 AND ProductsID = @1";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			return GetQuerySingle(sqlStr, context, objects);
		}

		/// <summary>
		/// 获取商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsCode">商品编码</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseProductsInfo GetSingleWarehouseProductsInfo(string warehouseCode, string productsCode, IDbContext context = null) {
			string sqlStr = @"SELECT p.*,wp.IsBooking,wp.BookingModel
                              FROM warehouseProducts wp INNER JOIN products p ON wp.ProductsID = p.ID
                              WHERE wp.WarehouseCode = @0 AND p.Code = @1 AND p.IsDelete=" + (int)IsEnable.否;
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsCode;
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<WarehouseProductsInfo>();
		}

		/// <summary>
		/// 获取商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehouseProductsInfo GetSingleWarehouseProductsInfo(string warehouseCode, int productsID, IDbContext context = null) {
			string sqlStr = @"SELECT p.*,wp.IsBooking,wp.BookingModel
                              FROM warehouseProducts wp INNER JOIN products p ON wp.ProductsID = p.ID
                              WHERE wp.WarehouseCode = @0 AND wp.ProductsID = @1 AND p.IsDelete=" + (int)IsEnable.否;
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<WarehouseProductsInfo>();
		}

		#region 根据商品ID获取仓库商品列表

		/// <summary>
		/// 根据商品ID获取仓库商品列表
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<WarehouseProducts> GetWarehouseProductsList(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = "SELECT * FROM warehouseProducts WHERE ProductsID=@0";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion
	}
}





