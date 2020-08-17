using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data {
	public class WarehouseProductsSkuRepository : BaseRepository<WarehouseProductsSku> {

		#region 构造函数
		private static WarehouseProductsSkuRepository _instance;
		public static WarehouseProductsSkuRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseProductsSkuRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehouseProductsSku entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseProductsSku>("warehouseProductsSku", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehouseProductsSku entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseProductsSku>("warehouseProductsSku", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		/// <summary>
		/// 删除仓库SKU
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int Delete(string warehouseCode, List<int> productsIDList, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseProductsSku
                              WHERE WarehouseCode = @0 AND FIND_IN_SET(ProductsID, @1)";
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", productsIDList.ToArray());
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据商品ID删除仓库SKU
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int DeleteByProductsID(int productsID, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseProductsSku WHERE ProductsID=@0";
			Object[] objects = new Object[1];
			objects[0] = productsID;
			return Del(sqlStr, context, objects);
		}
		/// <summary>
		/// 根据商品SKUID删除仓库SKU
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public int DeleteByProductsSkuID(int productsSkuID, IDbContext context = null) {
			string sqlStr = @"DELETE FROM warehouseProductsSku WHERE ProductsSkuID=@0";
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;
			return Del(sqlStr, context, objects);
		}

		/// <summary>
		/// 根据SKU码获取商品SKU信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuCode">SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public WarehouseProductsSkuInfo GetSingleWarehouseProductsSkuInfo(string warehouseCode, string productsSkuCode, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsSkuCode;
			string sqlStr = @"SELECT ps.*,p.Name as ProductsName,p.No as ProductsNo FROM warehouseProducts wp 
			INNER JOIN products p ON wp.ProductsID = p.ID
			INNER JOIN productsSku ps ON wp.ProductsID = ps.ProductsID 
			WHERE wp.WarehouseCode = @0 and ps.Code = @1 AND ps.IsDelete=" + (int)IsEnable.否;
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<WarehouseProductsSkuInfo>();
		}

		/// <summary>
		/// 根据SkuID获取商品SKU信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public WarehouseProductsSkuInfo GetSingleWarehouseProductsSkuInfo(string warehouseCode, int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsSkuID;
			string sqlStr = @"SELECT ps.*,p.Name as ProductsName,p.No as ProductsNo FROM warehouseProducts wp 
			INNER JOIN products p ON wp.ProductsID = p.ID
			INNER JOIN warehouseProductsSku wps ON wp.ProductsID = wps.ProductsID 
			INNER JOIN productsSku ps ON wp.ProductsID = ps.ProductsID 
			WHERE wp.WarehouseCode = @0 and ps.ID = @1 AND ps.IsDelete=" + (int)IsEnable.否;
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<WarehouseProductsSkuInfo>();
		}

		/// <summary>
		/// 根据SkuID获取商品SKU列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<WarehouseProductsSkuInfo> GetManyWarehouseProductsSkuInfo(string warehouseCode, int productsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			string sqlStr = @"SELECT ps.*,p.Name as ProductsName,p.No as ProductsNo FROM warehouseProducts wp 
			INNER JOIN products p ON wp.ProductsID = p.ID
			INNER JOIN warehouseProductsSku wps ON wp.ProductsID = wps.ProductsID 
			INNER JOIN productsSku ps ON wp.ProductsID = ps.ProductsID 
			WHERE wp.WarehouseCode = @0 and ps.ProductsID = @1 AND ps.IsDelete=" + (int)IsEnable.否;
			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<WarehouseProductsSkuInfo>();
		}
	}
}





