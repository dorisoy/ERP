using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using PaiXie.Core;
using FluentData;
namespace PaiXie.Service {
	public class ProductsService : BaseService<Products> {

		#region Update

		public static int Update(Products entity, out string oldMessage, out string newMessage, IDbContext context = null) {
			return ProductsRepository.GetInstance().Update(entity, out oldMessage, out newMessage, context);
		}

		#endregion

		#region Add

		public static int Add(Products entity, IDbContext context = null) {
			return ProductsRepository.GetInstance().Add(entity, context);
		}
		
		#endregion

		#region 根据商品编码获取商品ID

		/// <summary>
		/// 根据商品编码获取商品ID
		/// </summary>
		/// <param name="productsCode">商品编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsID(string productsCode, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsID(productsCode, context);
		}

		#endregion

		#region 根据商品编码获取排除指定商品ID之外的商品ID

		/// <summary>
		/// 根据商品编码获取排除指定商品ID之外的商品ID
		/// </summary>
		/// <param name="productsCode">商品编码</param>
		/// <param name="exceptProductsID">排除商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsID(string productsCode, int exceptProductsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsID(productsCode, exceptProductsID, context);
		}

		#endregion

		#region 根据商品ID获取商品信息

		/// <summary>
		/// 根据商品ID获取商品信息
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Products GetSingleProducts(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetSingleProducts(productsID,context);
		}

		#endregion

		#region 根据商品编码获取商品信息

		/// <summary>
		/// 根据商品编码获取商品信息
		/// </summary>
		/// <param name="productsID">商品编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Products GetSingleProducts(string productsCode, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetSingleProducts(productsCode, context);
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
		public static int UpdateProductsBrand(string userCode, List<int> productsIDList, int brandID, IDbContext context = null) {
			return ProductsRepository.GetInstance().UpdateProductsBrand(userCode, productsIDList, brandID, context);
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
		public static int UpdateProductsCategory(string userCode, List<int> productsIDList, int categoryID, IDbContext context = null) {
			return ProductsRepository.GetInstance().UpdateProductsCategory(userCode, productsIDList, categoryID, context);
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
		public static int UpdateProductsStatus(string userCode, int productsID, int status, IDbContext context = null) {
			return ProductsRepository.GetInstance().UpdateProductsStatus(userCode, productsID, status, context);
		}

		#endregion

		#region 删除商品

		/// <summary>
		/// 删除商品
		/// </summary>
		/// <param name="productsID">要删除的商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().Del(productsID, context);
		}

		#endregion

		#region 获取指定品牌的商品ID列表

		/// <summary>
		/// 获取指定品牌的商品ID列表
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<int> GetProductsIDListByBrandID(int brandID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsIDListByBrandID(brandID, context);
		}

		#endregion

		#region 获取指定分类的商品ID列表

		/// <summary>
		/// 获取指定分类的商品ID列表
		/// </summary>
		/// <param name="categoryID">分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<int> GetProductsIDListByCategoryID(int categoryID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsIDListByCategoryID(categoryID, context);
		}
		
		#endregion

		#region 根据页面条件获取商品ID列表

		/// <summary>
		/// 根据页面条件获取商品ID列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<int> GetProductsIDListForPage(SelectBuilder data, out int count) {
			BaseRepository<Products> obj = new BaseRepository<Products>();
			List<Products> list = obj.GetQueryManyForPage(data, out  count);
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(list.Select(row => row.ID));
			return productsIDList;
		}

		#endregion

		#region 获取指定商品ID的库存信息

		/// <summary>
		/// 获取指定商品ID的库存信息
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetProductsSkuKucInfo(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsKucInfo(productsID, context);
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
		public static DataTable GetWarehouseProductsSkuKucInfo(string warehouseCode,int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetWarehouseProductsKucInfo(warehouseCode, productsID, context);
		}

		#endregion

		#region 获取指定商品ID物料关联信息 分组统计

		/// <summary>
		/// 获取指定商品ID物料关联信息
		/// </summary>
		/// <param name="productsID">商品表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetProductsMaterialMapInfo(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsMaterialMapInfo(productsID, context);
		}

		#endregion

		#region 获取指定商品SKUID物料关联信息

		/// <summary>
		/// 获取指定商品SKUID物料关联信息
		/// </summary>
		/// <param name="sourceProductsSkuID">商品SKU标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetProductsSkuMaterialMapInfo(int sourceProductsSkuID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsSkuMaterialMapInfo(sourceProductsSkuID, context);
		}

		#endregion

		#region 根据商品ID获取SKU物料关联列表

		/// <summary>
		/// 根据商品ID获取SKU物料关联列表
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<ProductsSkuMaterialMapInfo> GetManyProductsSkuMaterialMapInfo(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetManyProductsSkuMaterialMapInfo(productsID, context);
		}

		#endregion

		#region 获取可发货库存
		/// <summary>
		/// 获取可发货库存
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <returns></returns>
		public static int GetKfhNumByProductsID(int productsID, IDbContext context = null) {
			int KfhNum = 0;
			ProductsInventory inventory = GetProductsInventory(productsID, context);
			if (inventory != null) {
				KfhNum = inventory.KyNum - inventory.ZyNum - inventory.OrdZyNum + inventory.BookingKyNum;
			}
			return KfhNum;
		}

		#endregion

		#region 根据商品ID获取可发货库存信息

		/// <summary>
		/// 根据商品ID获取可发货库存信息
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ProductsInventory GetProductsInventory(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsInventory(productsID, context);
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
		public static int GetTotalNum(int productsID,string warehouseCode, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetTotalNum(productsID, warehouseCode, context);
		}

		#endregion

		#region 获取商品采购价

		/// <summary>
		/// 获取商品采购价
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static decimal GetCostPrice(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetCostPrice(productsID, context);
		}

		#endregion
	}
}





