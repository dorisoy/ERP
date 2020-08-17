using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
using Newtonsoft.Json;
using PaiXie.Utils;
using Newtonsoft.Json.Converters;
namespace  PaiXie.Service 
{
	public class ProductsSkuService : BaseService<ProductsSku> {

		#region Update

		public static int Update(ProductsSku entity, out string oldMessage, out string newMessage, IDbContext context) {
			oldMessage = string.Empty;
			newMessage = string.Empty;
			//更新之前要先查出来，并存放到 oldMessage
			ProductsSku newProductsSku = GetSingleProductsSku(entity.ID, context);
			oldMessage = JsonConvert.SerializeObject(newProductsSku, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			newProductsSku.BarCode = entity.BarCode;
			newProductsSku.Code = entity.Code;
			newProductsSku.CostPrice = entity.CostPrice;
			newProductsSku.ProductsCode = entity.ProductsCode;
			newProductsSku.ProductsID = entity.ProductsID;
			newProductsSku.Saleprop = entity.Saleprop;
			newProductsSku.SellingPrice = entity.SellingPrice;
			newProductsSku.UpdateDate = entity.UpdateDate;
			newProductsSku.UpdatePerson = entity.UpdatePerson;
			newProductsSku.Weight = entity.Weight;
			int rowsAffected = ProductsSkuRepository.GetInstance().Update(newProductsSku, context);
			if (rowsAffected > 0) {
				newMessage = JsonConvert.SerializeObject(newProductsSku, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
			}
			return rowsAffected;
		}

		#endregion

		#region Add

		public static int Add(ProductsSku entity, IDbContext context) {
			return ProductsSkuRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 根据商品ID删除该商品所有Sku

		/// <summary>
		/// 根据商品ID删除该商品所有Sku
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DelByProductsID(int productsID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().DelByProductsID(productsID, context);
		}

		#endregion

		#region 根据SKUID删除指定Sku

		/// <summary>
		/// 根据SKUID删除指定Sku
		/// </summary>
		/// <param name="productsSkuID">商品SKU表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().Del(productsSkuID, context);
		}

		#endregion

		#region 根据商品SKUID获取商品Sku信息

		/// <summary>
		/// 根据商品SKUID获取商品Sku信息
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ProductsSku GetSingleProductsSku(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetSingleProductsSku(productsSkuID, context);
		}

		#endregion

		#region 根据商品Sku码获取商品Sku信息

		/// <summary>
		/// 根据商品Sku码获取商品Sku信息
		/// </summary>
		/// <param name="productsSkuCode">商品Sku码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ProductsSku GetSingleProductsSku(string productsSkuCode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetSingleProductsSku(productsSkuCode, context);
		}

		#endregion

		#region 根据商品Sku码获取商品Sku信息 带商品名称和货号

		/// <summary>
		/// 根据商品Sku码获取商品Sku信息 带商品名称和货号
		/// </summary>
		/// <param name="productsSkuCode">商品Sku码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ProductsSkuInfo GetSingleProductsSkuInfo(string productsSkuCode, IDbContext context = null) {
			DataTable dt = ProductsSkuRepository.GetInstance().GetSingleProductsSkuInfo(productsSkuCode, context);
			ProductsSkuInfo productsSkuInfo = null;
			if (dt.Rows.Count > 0) {
				productsSkuInfo = new ProductsSkuInfo();
				DataRow dr = dt.Rows[0];
				productsSkuInfo.ID = ZConvert.StrToInt(dr["ID"]);
				productsSkuInfo.Code = ZConvert.ToString(dr["Code"]);
				productsSkuInfo.BarCode = ZConvert.ToString(dr["BarCode"]);
				productsSkuInfo.CostPrice = ZConvert.StrToDecimal(dr["CostPrice"]);
				productsSkuInfo.SellingPrice = ZConvert.StrToDecimal(dr["SellingPrice"]);
				productsSkuInfo.ProductsCode = ZConvert.ToString(dr["ProductsCode"]);
				productsSkuInfo.ProductsID = ZConvert.StrToInt(dr["ProductsID"]);
				productsSkuInfo.ProductsName = ZConvert.ToString(dr["ProductsName"]);
				productsSkuInfo.ProductsNo = ZConvert.ToString(dr["ProductsNo"]);
				productsSkuInfo.Saleprop = ZConvert.ToString(dr["Saleprop"]);
				productsSkuInfo.Weight = ZConvert.StrToDecimal(dr["Weight"]);
				productsSkuInfo.CreatePerson = ZConvert.ToString(dr["CreatePerson"]);
				productsSkuInfo.CreateDate = ZConvert.StrToDateTime(dr["CreateDate"], DateTime.Now);
				productsSkuInfo.UpdatePerson = ZConvert.ToString(dr["UpdatePerson"]);
				productsSkuInfo.UpdateDate = ZConvert.StrToDateTime(dr["UpdateDate"], DateTime.Now);
			}
			return productsSkuInfo;
		}

		#endregion

		#region 根据商品Sku码获取排除指定SkuID之外的商品Sku信息(修改Sku时使用)

		/// <summary>
		/// 根据商品Sku码获取排除指定SkuID之外的商品Sku信息(修改Sku时使用)
		/// </summary>
		/// <param name="productsSkuCode">商品Sku码</param>
		/// <param name="exceptProductsSkuID">需要排除的SkuID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ProductsSku GetSingleProductsSku(string productsSkuCode, int exceptProductsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetSingleProductsSku(productsSkuCode, exceptProductsSkuID, context);
		}

		#endregion

		#region 根据商品ID返回SKU实体列表

		/// <summary>
		/// 根据商品ID返回SKU实体列表
		/// </summary>
		/// <param name="productsId">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<ProductsSku> GetManyProductsSku(int productsID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetManyProductsSku(productsID, context);
		}

		#endregion

		#region 根据商品SKU码获取SKUID

		/// <summary>
		/// 根据商品SKU码获取SKUID
		/// </summary>
		/// <param name="productsSkuCode">商品SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsSkuID(string productsSkuCode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSkuID(productsSkuCode, context);
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
		public static ProductsSkuInventory GetProductsSkuInventory(int productsSkuID,string warehouseCode,int isFilterStopSelling, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSkuInventory(productsSkuID, warehouseCode, isFilterStopSelling, context);
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
		public static List<ProductsSkuInventory> GetWarehouseSkuInventory(int productsSkuID, string warehouseCode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetWarehouseSkuInventory(productsSkuID, warehouseCode, context);
		}

		#endregion

		#region 获取可发货库存

		/// <summary>
		/// 获取可发货库存
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="isFilterStopSelling">是否过滤下架库存 0：否 1：是</param>
		/// <returns></returns>
		public static int GetKfhNumByProductsSkuID(int productsSkuID, int isFilterStopSelling = 0, IDbContext context = null) {
			int KfhNum = 0;
			ProductsSkuInventory skuInventory = GetProductsSkuInventory(productsSkuID, "", isFilterStopSelling, context);
			if (skuInventory != null) {
				KfhNum = skuInventory.KyNum - skuInventory.ZyNum - skuInventory.OrdZyNum + skuInventory.BookingKyNum;
			}
			return KfhNum;
		}

		/// <summary>
		/// 获取可发货库存
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int GetKfhNumByProductsSkuID(string warehouseCode, int productsSkuID, IDbContext context = null) {
			int KfhNum = 0;
			ProductsSkuInventory skuInventory = GetProductsSkuInventory(productsSkuID, warehouseCode, 0, context);
			if (skuInventory != null) {
				KfhNum = skuInventory.KyNum - skuInventory.ZyNum + skuInventory.BookingKyNum;
			}
			return KfhNum;
		}

		#endregion

		#region 获取商品SKU数量，用于判断SKU是否可删除
		/// <summary>
		/// 获取商品SKU数量，用于判断SKU是否可删除
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetTotalNum(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetTotalNum(productsSkuID, context);
		}

		#endregion

		#region 商品sku 信息 添加独享

		/// <summary>
		/// 商品sku 信息 添加独享
		/// </summary>
		/// <param name="ProductsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ProductsSkuList> GetProductsSkuList(int ProductsID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSkuList(ProductsID, context);
		}

		#endregion

		#region 获取 实体

		/// <summary>
		/// 获取 实体
		/// </summary>
		/// <param name="WarehouseCode">仓库代码</param>
		/// <param name="skucode">sku 吗</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static ProductsSku GetProductsSku(string WarehouseCode, string skucode, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetProductsSku(WarehouseCode, skucode, context);
		}

		#endregion

		#region 获取SKU采购价

		/// <summary>
		/// 获取SKU采购价
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static decimal GetCostPrice(int productsSkuID, IDbContext context = null) {
			return ProductsSkuRepository.GetInstance().GetCostPrice(productsSkuID, context);
		}

		#endregion
	}
}





