using System;
using System.Collections.Generic;
using FluentData;
using PaiXie.Core;
using Newtonsoft.Json;
using System.Data;
using PaiXie.Utils;
using Newtonsoft.Json.Converters;
namespace PaiXie.Data {
	public class ProductsRepository : BaseRepository<Products> {

		#region 构造函数
		private static ProductsRepository _instance;
		public static ProductsRepository GetInstance() {
			if (_instance == null) {
				_instance = new ProductsRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Products entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int id = context.Insert<Products>("products", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(Products entity, out string oldMessage, out string newMessage, IDbContext context = null) {
			oldMessage = string.Empty;
			newMessage = string.Empty;
			if (context == null) context = Db.GetInstance().Context();
			//更新之前要先查出来，并存放到 oldMessage
			Products newProduct = GetSingleProducts(entity.ID, context);
			oldMessage = JsonConvert.SerializeObject(newProduct, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			newProduct.MeasurementUnitID = entity.MeasurementUnitID;
			newProduct.Name = entity.Name;
			newProduct.No = entity.No;
			newProduct.Remark = entity.Remark;
			newProduct.SaleType = entity.SaleType;
			newProduct.SellingPrice = entity.SellingPrice;
			newProduct.ShelfLife = entity.ShelfLife;
			newProduct.SmallPic = entity.SmallPic;
			newProduct.TaxRate = entity.TaxRate;
			newProduct.UpdatePerson = entity.UpdatePerson;
			newProduct.UpdateDate = entity.UpdateDate;
			newProduct.Weight = entity.Weight;
			newProduct.BarCode = entity.BarCode;
			newProduct.BrandID = entity.BrandID;
			newProduct.CategoryID = entity.CategoryID;
			newProduct.Code = entity.Code;
			newProduct.CostPrice = entity.CostPrice;
			int rowsAffected = context.Update<Products>("products", newProduct)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			if (rowsAffected > 0) {
				newMessage = JsonConvert.SerializeObject(newProduct, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			}
			return rowsAffected;
		}
		#endregion

		#region 根据商品编码获取商品ID

		/// <summary>
		/// 根据商品编码获取商品ID
		/// </summary>
		/// <param name="productsCode">商品编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetProductsID(string productsCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsCode;
			string sqlStr = "SELECT ID FROM products WHERE Code=@0 AND IsDelete=" + (int)IsEnable.否;
			Products products= GetQuerySingle(sqlStr, context, objects);
			int productsID = 0;
			if (products != null) {
				productsID = products.ID;
			}
			return productsID;
		}

		#endregion

		#region 根据商品编码获取排除指定商品ID之外的商品ID(修改商品时使用)
		
		/// <summary>
		/// 根据商品编码获取排除指定商品ID之外的商品ID(修改商品时使用)
		/// </summary>
		/// <param name="productsCode">商品编码</param>
		/// <param name="exceptProductsID">需要排除商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetProductsID(string productsCode, int exceptProductsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = productsCode;
			objects[1] = exceptProductsID;
			string sqlStr = "SELECT ID FROM products WHERE Code=@0 and ID<>@1 AND IsDelete=" + (int)IsEnable.否;
			Products products = GetQuerySingle(sqlStr, context, objects);
			int productsID = 0;
			if (products != null) {
				productsID = products.ID;
			}
			return productsID;
		}

		#endregion

		#region 根据商品ID获取商品信息

		/// <summary>
        /// 根据商品ID获取商品信息
        /// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
        /// <returns></returns>
		public Products GetSingleProducts(int productsID, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = @"SELECT * FROM products WHERE ID = @0 AND IsDelete=" + (int)IsEnable.否;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品编码获取商品信息

		/// <summary>
		/// 根据商品编码获取商品信息
		/// </summary>
		/// <param name="productsCode">商品编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public Products GetSingleProducts(string productsCode, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			Object[] objects = new Object[1];
			objects[0] = productsCode;
			string sqlStr = @"SELECT * FROM products WHERE Code = @0 AND IsDelete=" + (int)IsEnable.否;
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 更新商品所属品牌
		/// <summary>
		/// 更新商品所属品牌
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateProductsBrand(string userCode, List<int> productsIDList, int brandID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = brandID;
			objects[1] = string.Join(",", productsIDList.ToArray());
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = "UPDATE products SET BrandID=@0,UpdatePerson=@2,UpdateDate=@3 WHERE FIND_IN_SET(ID, @1)";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新商品所属分类

		/// <summary>
		/// 更新商品所属分类
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="categoryID">分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateProductsCategory(string userCode, List<int> productsIDList, int categoryID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = categoryID;
			objects[1] = string.Join(",", productsIDList.ToArray());
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = "UPDATE products SET CategoryID=@0,UpdatePerson=@2,UpdateDate=@3 WHERE FIND_IN_SET(ID, @1)";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 商品上下架
		/// <summary>
		/// 商品上下架
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="status">销售中=1   仓库中=2 枚举类型</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int UpdateProductsStatus(string userCode, int productsID, int status, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = status;
			objects[1] = productsID;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = "UPDATE products SET Status=@0,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 删除商品

		/// <summary>
		/// 删除商品
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = "UPDATE products SET IsDelete=" + (int)IsEnable.是 + ",Code=CONCAT_WS('_DELETE_',`Code`,ID) WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取指定品牌的商品ID列表

		/// <summary>
		/// 获取指定品牌的商品ID列表
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<int> GetProductsIDListByBrandID(int brandID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = brandID;
			string sqlStr = "SELECT ID FROM products WHERE BrandID=@0";
			List<Products> productsList = GetQueryMany(sqlStr, context, objects);
			List<int> productsIDList = new List<int>();
			foreach (var item in productsList) {
				productsIDList.Add(item.ID);
			}
			return productsIDList;
		}

		#endregion

		#region 获取指定分类的商品ID列表

		/// <summary>
		/// 获取指定分类的商品ID列表
		/// </summary>
		/// <param name="categoryID">分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<int> GetProductsIDListByCategoryID(int categoryID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = categoryID;
			string sqlStr = "SELECT ID FROM products WHERE CategoryID=@0";
			List<Products> productsList = GetQueryMany(sqlStr, context, objects);
			List<int> productsIDList = new List<int>();
			foreach (var item in productsList) {
				productsIDList.Add(item.ID);
			}
			return productsIDList;
		}

		#endregion

		#region 获取指定商品ID库存信息

		/// <summary>
		/// 获取指定商品ID库存信息
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetProductsKucInfo(int productsID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = productsID;
			objects[1] = (int)LocationType.发货区;
			objects[2] = (int)LocationType.备用区;
			string sqlStr = @"SELECT ps.ID AS ProductsSkuID,ps.Saleprop,ps.Code AS ProductsSkuCode,IFNULL(Fhc.KyNum,0) AS KyNum,IFNULL(Fhc.ZyNum,0) AS ZyNum,IFNULL(Fhc.DjNum,0) AS DjNum ,
IFNULL(OrderOccupy.Num,0) AS OrderZyNum ,IFNULL(Ysc.YsNum,0) AS YsNum,
IFNULL(BkjYsZyNum,0) AS BkjYsZyNum,IFNULL(Ysc.ZyNum,0) AS YsZyNum,IFNULL(Ysc.CdNum,0) AS YsCdNum,IFNULL(Byc.ByNum,0) AS ByNum 
FROM productsSku ps
LEFT JOIN (SELECT ProductsSkuID,SUM(KyNum) AS KyNum,SUM(ZyNum) AS ZyNum,SUM(DjNum) AS DjNum FROM warehouseLocationProducts wlp WHERE wlp.ProductsID=@0 AND LocationTypeID=@1 GROUP BY ProductsSkuID) AS Fhc
ON ps.ID=Fhc.ProductsSkuID
LEFT JOIN (SELECT ProductsSkuID,SUM(KyNum) AS ByNum FROM warehouseLocationProducts wlp WHERE wlp.ProductsID=@0 AND LocationTypeID=@2 GROUP BY ProductsSkuID) AS Byc
ON ps.ID=Byc.ProductsSkuID
LEFT JOIN (SELECT ProductsSkuID, SUM(CASE WHEN wbps.BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) AS YsNum,
SUM(CASE WHEN wbps.BookingModel=1 THEN ZyNum ELSE 0 END) AS BkjYsZyNum
,SUM(ZyNum) AS ZyNum,SUM(CdNum) AS CdNum FROM warehouseBookingProductsSku wbps WHERE wbps.ProductsID=@0 GROUP BY ProductsSkuID) AS Ysc
ON ps.ID=Ysc.ProductsSkuID
LEFT JOIN (SELECT ProductsSkuID,SUM(Num) AS Num FROM ord_occupy WHERE ProductsID=@0 GROUP BY ProductsSkuID) AS OrderOccupy
ON ps.ID=OrderOccupy.ProductsSkuID
WHERE ps.ProductsID=@0";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 获取指定商品ID各仓库库存信息

		/// <summary>
		/// 获取指定商品ID各仓库库存信息
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetWarehouseProductsKucInfo(string warehouseCode, int productsID, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere = " and wh.Code=@3";
			}
			Object[] objects = new Object[4];
			objects[0] = productsID;
			objects[1] = (int)LocationType.发货区;
			objects[2] = (int)LocationType.备用区;
			objects[3] = warehouseCode;
			string sqlStr = @"SELECT wh.Code AS WarehouseCode,wh.Name AS WarehouseName,wp.ProductsStatus,ps.Saleprop,ps.Code AS ProductsSkuCode,IFNULL(Fhc.KyNum,0) AS KyNum,IFNULL(Fhc.ZyNum,0) AS ZyNum,IFNULL(Fhc.DjNum,0) AS DjNum,
IFNULL(OrderOccupy.Num,0) AS OrderZyNum ,IFNULL(Ysc.YsNum,0) AS YsNum,
IFNULL(BkjYsZyNum,0) AS BkjYsZyNum,IFNULL(Ysc.ZyNum,0) AS YsZyNum,IFNULL(Ysc.CdNum,0) AS YsCdNum,IFNULL(Byc.ByNum,0) AS ByNum FROM warehouseProductsSku wps INNER JOIN warehouseProducts wp ON wps.WarehouseCode=wp.WarehouseCode AND wps.ProductsID=wp.ProductsID
LEFT JOIN productsSku ps ON wps.ProductsSkuID=ps.ID
LEFT JOIN warehouse wh ON wps.WarehouseCode=wh.Code
LEFT JOIN (SELECT WarehouseCode,ProductsSkuID,SUM(KyNum) AS KyNum,SUM(ZyNum) AS ZyNum,SUM(DjNum) AS DjNum FROM warehouseLocationProducts WHERE ProductsID=@0 AND LocationTypeID=@1 GROUP BY WarehouseCode,ProductsSkuID) AS Fhc
ON wps.ProductsSkuID=Fhc.ProductsSkuID AND wps.WarehouseCode=Fhc.WarehouseCode
LEFT JOIN (SELECT WarehouseCode,ProductsSkuID,SUM(KyNum) AS ByNum FROM warehouseLocationProducts WHERE ProductsID=@0 AND LocationTypeID=@2 GROUP BY WarehouseCode,ProductsSkuID) AS Byc
ON wps.ProductsSkuID=Byc.ProductsSkuID AND wps.WarehouseCode=Byc.WarehouseCode
LEFT JOIN (SELECT WarehouseCode,ProductsSkuID,SUM(CASE WHEN BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) AS YsNum,
SUM(CASE WHEN BookingModel=1 THEN ZyNum ELSE 0 END) AS BkjYsZyNum,
SUM(ZyNum) AS ZyNum,SUM(CdNum) AS CdNum FROM warehouseBookingProductsSku WHERE ProductsID=@0 GROUP BY WarehouseCode,ProductsSkuID) AS Ysc
ON wps.ProductsSkuID=Ysc.ProductsSkuID AND wps.WarehouseCode=Ysc.WarehouseCode
LEFT JOIN (SELECT WarehouseCode,ProductsSkuID,SUM(Num) AS Num FROM ord_occupy WHERE ProductsID=@0 GROUP BY WarehouseCode,ProductsSkuID) AS OrderOccupy
ON wps.ProductsSkuID=OrderOccupy.ProductsSkuID AND wps.WarehouseCode=OrderOccupy.WarehouseCode
WHERE ps.ProductsID=@0" + strWhere;
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 获取指定商品ID物料关联信息

		/// <summary>
		/// 获取指定商品ID物料关联信息
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <returns></returns>
		public DataTable GetProductsMaterialMapInfo(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = @"SELECT ps.ID AS ProductsSkuID,ps.Saleprop,ps.Code AS ProductsSkuCode,pmm.ProductsMaterialMapCount FROM productsSku ps 
LEFT JOIN (SELECT SourceProductsSkuID,COUNT(0) AS ProductsMaterialMapCount FROM productsMaterialMap GROUP BY SourceProductsSkuID) pmm
ON ps.ID=pmm.SourceProductsSkuID
WHERE ps.ProductsID=@0";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 获取指定商品SKUID物料关联信息

		/// <summary>
		/// 获取指定商品SKUID物料关联信息
		/// </summary>
		/// <param name="sourceProductsSkuID">商品SKU标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public DataTable GetProductsSkuMaterialMapInfo(int sourceProductsSkuID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = sourceProductsSkuID;
			string sqlStr = @"SELECT p.Name AS ProductsName,p.Code AS ProductsCode,p.SmallPic,ps.Saleprop,ps.Code AS ProductsSkuCode,pmm.ID,pmm.SourceProductsSkuID,pmm.FromProductsSkuID,pmm.FromNum,pmm.CreateDate FROM productsMaterialMap pmm
LEFT JOIN productsSku ps ON ps.ID=pmm.FromProductsSkuID
LEFT JOIN products p ON ps.ProductsID=p.ID
WHERE pmm.SourceProductsSkuID=@0";
			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 根据商品ID获取SKU物料关联列表

		/// <summary>
		/// 根据商品ID获取SKU物料关联列表
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<ProductsSkuMaterialMapInfo> GetManyProductsSkuMaterialMapInfo(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = @"SELECT ps.Saleprop,ps.Code AS ProductsSkuCode,ps.BarCode AS ProductsSkuBarCode,ps.SellingPrice AS ProductsSkuSellingPrice,ps.CostPrice AS ProductsSkuCostPrice,pmm.FromNum,fps.Code AS FromProductsSkuCode 
                              FROM productsSku ps 
                              LEFT JOIN productsMaterialMap pmm ON ps.ID = pmm.SourceProductsSkuID 
                              LEFT JOIN productsSku fps ON fps.ID = pmm.FromProductsSkuID
                              WHERE ps.ProductsID = @0";

			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QueryMany<ProductsSkuMaterialMapInfo>();
		}

		#endregion

		#region 根据商品ID获取可发货库存信息

		/// <summary>
		/// 根据商品ID获取可发货库存信息
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public ProductsInventory GetProductsInventory(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;

			string sqlStr = @"SELECT 
						         SUM(KyNum) KyNum,
								 SUM(ZyNum) ZyNum,
								 (SELECT SUM(Num) FROM ord_occupy WHERE ProductsID = @0) OrdZyNum,
								 (SELECT SUM(CASE WHEN BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) FROM warehouseBookingProductsSku WHERE ProductsID = @0) BookingKyNum
							  FROM warehouseLocationProducts wlp WHERE wlp.LocationTypeID = " + (int)LocationType.发货区 + " AND wlp.ProductsID = @0";

			if (context == null) context = Db.GetInstance().Context();
			return context.Sql(sqlStr, objects).QuerySingle<ProductsInventory>();
		}

		#endregion

		#region 获取商品数量，用于判断商品是否可删除
		/// <summary>
		/// 获取商品数量，用于判断商品是否可删除
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="warehouseCode">仓库编号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetTotalNum(int productsID,string warehouseCode, IDbContext context = null) {
			int totalNum = 0;
			Object[] objects = new Object[2];
			objects[0] = productsID;
			objects[1] = warehouseCode;

			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere = " and wlp.WarehouseCode = @2";
			}

			string sqlStr = @"SELECT 
						         SUM(KyNum) KyNum,
								 SUM(ZyNum) ZyNum,
								 (SELECT SUM(Num) FROM ord_occupy WHERE ProductsID = @0) OrdZyNum,
								 (SELECT SUM(CASE WHEN BookingModel=0 THEN BookingNum-ZyNum-CdNum ELSE BookingNum END) FROM warehouseBookingProductsSku WHERE ProductsID = @0) BookingKyNum,
								 (SELECT SUM(ZyNum) FROM warehouseBookingProductsSku WHERE ProductsID = @0) BookingZyNum
							  FROM warehouseLocationProducts wlp WHERE wlp.ProductsID = @0" + strWhere;
			
			DataTable dt = GetDataTable(sqlStr, context, objects);
			if (dt.Rows.Count == 1) {
				totalNum = ZConvert.StrToInt(dt.Rows[0]["KyNum"]) + ZConvert.StrToInt(dt.Rows[0]["ZyNum"]) +
					       ZConvert.StrToInt(dt.Rows[0]["OrdZyNum"]) + ZConvert.StrToInt(dt.Rows[0]["BookingKyNum"]) +
					       ZConvert.StrToInt(dt.Rows[0]["BookingZyNum"]);
			}
			return totalNum;
		}

		#endregion

		#region 获取商品采购价

		/// <summary>
		/// 获取商品采购价
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public virtual decimal GetCostPrice(int productsID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = productsID;
			string sqlStr = @"SELECT CostPrice FROM products WHERE ID=@0";
			decimal costPrice = ZConvert.StrToDecimal(Getobject(sqlStr, context, objects));
			return costPrice;
		}

		#endregion
	}
}





