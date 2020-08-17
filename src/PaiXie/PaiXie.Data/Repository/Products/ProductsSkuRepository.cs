using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Data;
using PaiXie.Core;
using PaiXie.Utils;
namespace PaiXie.Data {
	public class ProductsSkuRepository : BaseRepository<ProductsSku> {

		#region 构造函数
		private static ProductsSkuRepository _instance;
		public static ProductsSkuRepository GetInstance() {
			if (_instance == null) {
				_instance = new ProductsSkuRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(ProductsSku entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int id = context.Insert<ProductsSku>("productsSku", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(ProductsSku entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<ProductsSku>("productsSku", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 根据商品ID删除该商品所有SKU

		/// <summary>
		/// 根据商品ID删除该商品所有SKU
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int DelByProductsID(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = "UPDATE productsSku SET IsDelete=" + (int)IsEnable.是 + " WHERE ProductsID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据SKUID删除指定SKU

		/// <summary>
		/// 根据SKUID删除指定SKU
		/// </summary>
		/// <param name="productsSkuID">商品SKU表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;
			string sqlStr = "UPDATE productsSku SET IsDelete=" + (int)IsEnable.是 + " WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKUID获取商品SKU信息

		/// <summary>
		/// 根据商品SKUID获取商品SKU信息
		/// </summary>
		/// <param name="productsSkuID">商品SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ProductsSku GetSingleProductsSku(int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;
			string sqlStr = @"SELECT * FROM productsSku WHERE ID=@0 AND IsDelete=" + (int)IsEnable.否;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKU码获取商品SKU信息

		/// <summary>
		/// 根据商品SKU码获取商品SKU信息
		/// </summary>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ProductsSku GetSingleProductsSku(string productsSkuCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuCode;
			string sqlStr = @"SELECT * FROM productsSku WHERE Code=@0 AND IsDelete=" + (int)IsEnable.否;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKU码获取商品SKU信息 带商品名称和货号

		/// <summary>
		/// 根据商品SKU码获取商品SKU信息 带商品名称和货号
		/// </summary>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetSingleProductsSkuInfo(string productsSkuCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuCode;
			string sqlStr = @"SELECT ps.*,p.Name as ProductsName,p.No as ProductsNo FROM productsSku ps 
			LEFT JOIN products p ON ps.ProductsID=p.ID WHERE ps.Code=@0 AND ps.IsDelete=" + (int)IsEnable.否;
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKU码获取排除指定SKUID之外的商品SKU信息(修改Sku时使用)

		/// <summary>
		/// 根据商品SKU码获取排除指定SKUID之外的商品SKU信息(修改SKU时使用)
		/// </summary>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="exceptProductsSkuID">需要排除的SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ProductsSku GetSingleProductsSku(string productsSkuCode, int exceptProductsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = productsSkuCode;
			objects[1] = exceptProductsSkuID;
			string sqlStr = @"SELECT * FROM productsSku WHERE Code=@0 AND ID<>@1 AND IsDelete=" + (int)IsEnable.否;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品ID返回SKU实体列表

		/// <summary>
		/// 根据商品ID返回SKU实体列表
		/// </summary>
		/// <param name="productsId">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<ProductsSku> GetManyProductsSku(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = @"SELECT * FROM productsSku WHERE ProductsID = @0 AND IsDelete=" + (int)IsEnable.否;
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品SKU码获取SKUID

		/// <summary>
		/// 根据商品SKU码获取SKUID
		/// </summary>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetProductsSkuID(string productsSkuCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuCode;
			string sqlStr = "SELECT ID FROM productsSku WHERE Code=@0 AND IsDelete=" + (int)IsEnable.否;
			ProductsSku productsSku = GetQuerySingle(sqlStr, context, objects);
			int productsSkuID = 0;
			if (productsSku != null) {
				productsSkuID = productsSku.ID;
			}
			return productsSkuID;
		}

		#endregion

		#region 根据商品SKUID获取库存信息

		/// <summary>
		/// 根据商品SKUID获取库存信息
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="warehouseCode">仓库编号</param>
		/// <param name="isFilterStopSelling">是否过滤下架库存 0：否 1：是</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ProductsSkuInventory GetProductsSkuInventory(int productsSkuID, string warehouseCode, int isFilterStopSelling, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and p.WarehouseCode = @1";
			}
			if (isFilterStopSelling == 1) {
				strWhere += " and wp.ProductsStatus = @2";
			}
			Object[] objects = new Object[3];
			objects[0] = productsSkuID;
			objects[1] = warehouseCode;
			objects[2] = 1;

			string sqlStr = @"SELECT 
						         SUM(KyNum) KyNum,
								 SUM(ZyNum) ZyNum,
                                 SUM(DjNum) DjNum,
								 (SELECT SUM(Num) FROM ord_occupy WHERE ProductsSkuID = p.ProductsSkuID) OrdZyNum,
								 (SELECT SUM(CASE WHEN b.BookingModel = 0 THEN b.BookingNum-b.ZyNum-b.CdNum ELSE b.BookingNum END) FROM warehouseBookingProductsSku b INNER JOIN warehouseproducts wp ON wp.WarehouseCode = b.WarehouseCode AND wp.ProductsID = b.ProductsID WHERE ProductsSkuID = @0" + strWhere + @") BookingKyNum
							  FROM warehouseLocationProducts p 
                              INNER JOIN warehouseproducts wp on p.ProductsID = wp.ProductsID and p.WarehouseCode = wp.WarehouseCode
                              WHERE p.LocationTypeID = " + (int)LocationType.发货区 + " AND ProductsSkuID = @0" + strWhere;

			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<ProductsSkuInventory>();
		}

		#endregion

		#region 根据商品SKUID获取仓库可发货库存信息

		/// <summary>
		/// 根据商品SKUID获取仓库可发货库存信息
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="warehouseCode">仓库编号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<ProductsSkuInventory> GetWarehouseSkuInventory(int productsSkuID, string warehouseCode, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and wp.WarehouseCode = @1";
			}
			Object[] objects = new Object[3];
			objects[0] = productsSkuID;
			objects[1] = warehouseCode;
			objects[2] = (int)LocationType.发货区;

			string sqlStr = @"  SELECT
                                w.Name as WarehouseName, 
								w.Code as WarehouseCode,
								SUM(KyNum) KyNum,
								SUM(ZyNum) ZyNum,
								SUM(DjNum) DjNum,
								SUM(BookingKyNum) BookingKyNum
								FROM warehouse w 
								INNER JOIN warehouseproducts wp ON w.`Code` = wp.WarehouseCode
								INNER JOIN warehouseproductsSku wps ON wp.WarehouseCode = wps.WarehouseCode AND wp.ProductsID = wps.ProductsID
								LEFT JOIN
								(
								SELECT WarehouseCode,ProductsSkuID,KyNum,ZyNum,DjNum FROM warehouseLocationProducts WHERE ProductsSkuID = @0 AND LocationTypeID = @2 GROUP BY WarehouseCode,ProductsSkuID
								) AS fhc ON wp.WarehouseCode = fhc.WarehouseCode
								LEFT JOIN
								(
								SELECT WarehouseCode,ProductsSkuID,SUM(CASE WHEN BookingModel = 0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) BookingKyNum FROM warehouseBookingProductsSku WHERE ProductsSkuID = @0 GROUP BY WarehouseCode,ProductsSkuID
								) AS ys ON wp.WarehouseCode = ys.WarehouseCode
								WHERE wp.ProductsStatus = 1 AND wps.ProductsSkuID = @0
                                GROUP BY w.Code";

			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<ProductsSkuInventory>();
		}

		#endregion

		#region 获取商品SKU数量，用于判断SKU是否可删除
		/// <summary>
		/// 获取商品SKU数量，用于判断SKU是否可删除
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetTotalNum(int productsSkuID, IDbContext context = null) {
			int totalNum = 0;
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;

			string sqlStr = @"SELECT 
						         SUM(KyNum) KyNum,
								 SUM(ZyNum) ZyNum,
								 (SELECT SUM(Num) FROM ord_occupy WHERE ProductsSkuID = @0) OrdZyNum,
								 (SELECT SUM(CASE WHEN BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) FROM warehouseBookingProductsSku WHERE ProductsSkuID = @0) BookingKyNum,
								 (SELECT SUM(ZyNum) FROM warehouseBookingProductsSku WHERE ProductsSkuID = @0) BookingZyNum
							  FROM warehouseLocationProducts wlp WHERE wlp.ProductsSkuID = @0";
			DataTable dt = GetDataTable(sqlStr, context, objects);
			if (dt.Rows.Count == 1) {
				totalNum = ZConvert.StrToInt(dt.Rows[0]["KyNum"]) + ZConvert.StrToInt(dt.Rows[0]["ZyNum"]) +
					ZConvert.StrToInt(dt.Rows[0]["OrdZyNum"]) + ZConvert.StrToInt(dt.Rows[0]["BookingKyNum"]) +
					ZConvert.StrToInt(dt.Rows[0]["BookingZyNum"]);
			}
			return totalNum;
		}

		#endregion

		#region 商品sku 信息 添加独享

		/// <summary>
		/// 商品sku 信息 添加独享
		/// </summary>
		/// <param name="ProductsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public List<ProductsSkuList> GetProductsSkuList(int ProductsID, IDbContext context = null) {
			BaseRepository<ProductsSkuList> BaseRepository = new BaseRepository<ProductsSkuList>();
			List<ProductsSkuList> obj = BaseRepository.GetQueryMany("SELECT *, '0' AS kc, '0' AS dxsl   FROM   productsSku WHERE  ProductsID =" + ProductsID + " AND IsDelete=" + (int)IsEnable.否);
			return obj;

		}

		#endregion

		#region 获取 实体

		public ProductsSku GetProductsSku(string WarehouseCode, string skucode, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = WarehouseCode;
			objects[1] = skucode;
			
		return 	GetQuerySingle(@"SELECT *   FROM   productsSku WHERE ID IN 
            (SELECT  productsSkuID  FROM   warehouseProductsSku WHERE warehousecode=@0) AND  Code=@1 AND IsDelete=" + (int)IsEnable.否, context, objects);
		
		}

		#endregion

		#region 获取SKU采购价

		/// <summary>
		/// 获取SKU采购价
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public virtual decimal GetCostPrice(int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsSkuID;
			string sqlStr = @"SELECT CostPrice FROM productsSku WHERE ID=@0";
			decimal costPrice = ZConvert.StrToDecimal(Getobject(sqlStr, context, objects));
			return costPrice;
		}

		#endregion
	}
}





