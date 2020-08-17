using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
using PaiXie.Core;
using PaiXie.Utils;
namespace PaiXie.Data 
{
	public class WarehouseOutInStockLogRepository : BaseRepository<WarehouseOutInStockLog> {

		#region 构造函数

		private static WarehouseOutInStockLogRepository _instance;
		public static WarehouseOutInStockLogRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseOutInStockLogRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(WarehouseOutInStockLog entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseOutInStockLog>("warehouseOutInStockLog", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(WarehouseOutInStockLog entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseOutInStockLog>("warehouseOutInStockLog", entity)
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
		public virtual WarehouseOutInStockLog GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM WarehouseOutInStockLog WHERE ID=@0";
			WarehouseOutInStockLog obj = GetQuerySingle(sqlStr, context, objects);
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
			int rowsAffected = context.Sql("DELETE  FROM  warehouseOutInStockLog   WHERE ID=" + ID)
					.Execute();
			return rowsAffected;
		}

		#endregion

		#region 获取出入库数量统计
		
		/// <summary>
		/// 获取出入库数量统计
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public DataTable GetManyOutInStockLog(string warehouseCode, int productsID, int productsSkuID, string startDate, string endDate, IDbContext context = null) {
			string strWhere = string.Empty;
			if (productsID != 0) {
				strWhere += " and ProductsID = @1";
			}
			if (productsSkuID != 0) {
				strWhere += " and ProductsSkuID = @2";
			}
			DateTime now = DateTime.Now;
			if (ZConvert.StrToDateTime(startDate, now) != now) {
				strWhere += " and CreateDate >= @3";
			}
			if (ZConvert.StrToDateTime(endDate, now) != now) {
				strWhere += " and CreateDate < @4";
				endDate = ZConvert.StrToDateTime(endDate, now).AddDays(1).ToString();
			}

			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			objects[2] = productsSkuID;
			objects[3] = startDate;
			objects[4] = endDate;

			string sqlStr = @"SELECT 
								  SUM(CASE WHEN BillType = " + (int)BillType.CGC + " OR BillType = " + (int)BillType.QTC + @" THEN Num * StockWay ELSE 0 END) AS OutboundNum,
								  SUM(CASE WHEN BillType = " + (int)BillType.CGR + " OR BillType = " + (int)BillType.QTR + @" THEN Num * StockWay ELSE 0 END) AS StorageNum,
								  SUM(CASE WHEN BillType = " + (int)BillType.PD + @" THEN Num * StockWay ELSE 0 END) AS AdjustQuantity,
								  SUM(CASE WHEN BillType = " + (int)BillType.ZH + @" AND StockWay = " + (int)StockWay.出库 + @" THEN Num ELSE 0 END) AS RollOutQuantity,
								  SUM(CASE WHEN BillType = " + (int)BillType.ZH + @" AND StockWay = " + (int)StockWay.入库 + @" THEN Num ELSE 0 END) AS QuantityOfTransfer
							  FROM warehouseOutInStockLog WHERE WarehouseCode = @0" + strWhere;

			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 获取期初或期末信息

		/// <summary>
		/// 获取期初或期末信息
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public DataTable GetInitialOutInStockLog(string warehouseCode, int productsID, int productsSkuID, string startDate, string endDate, IDbContext context = null) {
			string strWhere = string.Empty;
			if (productsID != 0) {
				strWhere += " and l.ProductsID = @1";
			}
			if (productsSkuID != 0) {
				strWhere += " and l.ProductsSkuID = @2";
			}
			DateTime now = DateTime.Now;
			if (ZConvert.StrToDateTime(startDate, now) != now) {
				strWhere += " and l.CreateDate < @3";
			}
			if (ZConvert.StrToDateTime(endDate, now) != now) {
				strWhere += " and l.CreateDate < @4";
				endDate = ZConvert.StrToDateTime(endDate, now).AddDays(1).ToString();
			}

			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = productsID;
			objects[2] = productsSkuID;
			objects[3] = startDate;
			objects[4] = endDate;

			string sqlStr = @"SELECT SUM(l.Num * l.StockWay) AS InitialInventory,SUM(l.Num * l.StockWay * b.CostPrice) AS InitialCost 
                              FROM warehouseOutInStockLog  l INNER JOIN warehouseProductsBatch b ON l.ProductsBatchID = b.ID 
                              WHERE l.WarehouseCode = @0" + strWhere;

			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 获取批次号出入库数量统计

		/// <summary>
		/// 获取批次号出入库数量统计
		/// </summary>
		/// <param name="productBatchCode"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public DataTable GetManyOutInStockLog(string productBatchCode, int productsID, int productsSkuID, IDbContext context = null) {
			string strWhere = string.Empty;
			if (productsID != 0) {
				strWhere += " and ProductsID = @1";
			}
			if (productsSkuID != 0) {
				strWhere += " and ProductsSkuID = @2";
			}

			Object[] objects = new Object[3];
			objects[0] = productBatchCode;
			objects[1] = productsID;
			objects[2] = productsSkuID;

			string sqlStr = @"SELECT 
								  SUM(CASE WHEN BillType = " + (int)BillType.CGC + " OR BillType = " + (int)BillType.QTC + @" THEN Num * StockWay ELSE 0 END) AS OutboundNum,
								  SUM(CASE WHEN BillType = " + (int)BillType.CGR + " OR BillType = " + (int)BillType.QTR + @" THEN Num * StockWay ELSE 0 END) AS StorageNum,
								  SUM(CASE WHEN BillType = " + (int)BillType.PD + @" THEN Num * StockWay ELSE 0 END) AS AdjustQuantity,
								  SUM(CASE WHEN BillType = " + (int)BillType.ZH + @" AND StockWay = " + (int)StockWay.出库 + @" THEN Num ELSE 0 END) AS RollOutQuantity,
								  SUM(CASE WHEN BillType = " + (int)BillType.ZH + @" AND StockWay = " + (int)StockWay.入库 + @" THEN Num ELSE 0 END) AS QuantityOfTransfer,
                                  SUM(CASE WHEN BillType = " + (int)BillType.XSC + @" AND StockWay = " + (int)StockWay.出库 + @" THEN Num ELSE 0 END) AS SalesVolumes,
                                  COUNT(DISTINCT ProductsID) AS ProductsCount,
                                  MIN(CreateDate) AS CreateDate
							  FROM warehouseOutInStockLog WHERE ProductsBatchCode = @0" + strWhere;

			return GetDataTable(sqlStr, context, objects);
		}

		#endregion
	}
}





