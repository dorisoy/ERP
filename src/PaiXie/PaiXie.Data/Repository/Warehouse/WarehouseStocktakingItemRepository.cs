using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
using PaiXie.Utils;
namespace PaiXie.Data 
{
	public class WarehouseStocktakingItemRepository : BaseRepository<WarehouseStocktakingItem> {

		#region 构造函数

		private static WarehouseStocktakingItemRepository _instance;
		public static WarehouseStocktakingItemRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseStocktakingItemRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(WarehouseStocktakingItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseStocktakingItem>("warehouseStocktakingItem", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update

		public int Update(WarehouseStocktakingItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseStocktakingItem>("warehouseStocktakingItem", entity)
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
		public virtual WarehouseStocktakingItem GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseStocktakingItem WHERE ID=@0";
			WarehouseStocktakingItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM warehouseStocktakingItem WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据盘点记录ID删除盘点商品

		/// <summary>
		/// 根据盘点记录ID删除盘点商品
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Delete(int stocktakingID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = stocktakingID;
			string sqlStr = "DELETE FROM warehouseStocktakingItem WHERE StocktakingID = @0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 获取盘点商品实体

		/// <summary>
		/// 获取盘点商品实体
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="locationCode">库位编码</param>
		/// <param name="productsBatchCode">批次号</param>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public WarehouseStocktakingItem GetSingleWarehouseStocktakingItem(int stocktakingID, string locationCode, string productsBatchCode, string productsSkuCode, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = stocktakingID;
			objects[1] = locationCode;
			objects[2] = productsBatchCode;
			objects[3] = productsSkuCode;
			string sqlStr = "SELECT * FROM warehouseStocktakingItem WHERE StocktakingID = @0 AND LocationCode = @1 AND ProductsBatchCode = @2 AND ProductsSkuCode = @3";
			WarehouseStocktakingItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 根据商品SKUID、批次ID、库位ID 判断是否正在盘点

		/// <summary>
		/// 根据商品SKUID、批次ID、库位ID 判断是否正在盘点
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public bool IsExists(int productsSkuID, int productsBatchID, int locationID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = productsSkuID;
			objects[1] = productsBatchID;
			objects[2] = locationID;
			string sqlStr = @"SELECT COUNT(0) FROM warehouseStocktakingItem wsti INNER JOIN 
			warehouseStocktaking wst ON wsti.StocktakingID = wst.ID
			WHERE wsti.ProductsSkuID=@0 AND wsti.ProductsBatchID=@1 AND wsti.LocationID=@2 AND wst.Status=" + (int)StocktakingStatus.未确认;
			int count = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return count > 0;
		}

		#endregion

		#region 获取未盘点的商品数量

		/// <summary>
		/// 获取未盘点的商品数量
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetNotImportCount(int stocktakingID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = stocktakingID;
			string sqlStr = "SELECT COUNT(1) FROM warehouseStocktakingItem WHERE StocktakingID = @0 AND IsImport = 0";
			return GetCount(sqlStr, context);
		}

		#endregion

		#region 获取盘点商品实体列表

		/// <summary>
		/// 获取盘点商品实体列表
		/// </summary>
		/// <param name="stocktakingID">盘点记录ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<WarehouseStocktakingItem> GetManyWarehouseStocktakingItem(int stocktakingID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = stocktakingID;
			string sqlStr = "SELECT * FROM warehouseStocktakingItem WHERE StocktakingID = @0 and ZkNum <> PdNum";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 获取单个实体 通过StocktakingID

		/// <summary>
		/// 获取单个实体 通过StocktakingID
		/// </summary>
		/// <param name="id">StocktakingID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseStocktakingItem> GetQuerySingleByStocktakingID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseStocktakingItem WHERE StocktakingID=@0";
			List<WarehouseStocktakingItem> obj = GetQueryMany(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取单个实体[盘点单未确认]

		/// <summary>
		/// 获取单个实体[盘点单未确认]
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehouseStocktakingItem GetSingleUnconfirmed(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT s.ID,s.StocktakingID,s.BillNo,s.WarehouseCode,s.TopLocationID,s.SourceID,s.LocationID,s.LocationCode,s.LocationName,s.LocationTypeID,s.LocationProductsID,s.ProductsID,s.ProductsCode,s.ProductsName,s.ProductsNo,s.ProductsSkuID,s.ProductsSkuCode,s.ProductsSkuSaleprop,s.ProductsBatchID,s.ProductsBatchCode,s.ProductionDate,s.ShelfLife,p.KyNum,p.ZyNum,p.DjNum,p.SdNum,p.ZkNum,s.PdNum,s.IsImport,s.CreatePerson,s.CreateDate,s.UpdatePerson,s.UpdateDate,s.CostPrice FROM warehouseStocktakingItem s LEFT JOIN warehouselocationproducts p ON s.SourceID = p.ID WHERE s.ID = @0";
			WarehouseStocktakingItem obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion
	}
}





