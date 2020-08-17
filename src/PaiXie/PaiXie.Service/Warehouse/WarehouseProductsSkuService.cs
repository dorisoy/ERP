using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class WarehouseProductsSkuService  : BaseService<WarehouseProductsSku> {

		public static int Update(WarehouseProductsSku entity, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseProductsSku entity, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 删除仓库SKU
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> productsIDList, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().Delete(warehouseCode, productsIDList, context);
		}

		/// <summary>
		/// 根据商品ID删除仓库SKU
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int DeleteByProductsID(int productsID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().DeleteByProductsID(productsID, context);
		}
		/// <summary>
		/// 根据商品SKUID删除仓库SKU
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int DeleteByProductsSkuID(int productsSkuID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().DeleteByProductsSkuID(productsSkuID, context);
		}

		/// <summary>
		/// 根据SKU码获取商品SKU信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuCode">SKU码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseProductsSkuInfo GetSingleWarehouseProductsSkuInfo(string warehouseCode, string productsSkuCode, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuCode, context);
		}

		/// <summary>
		/// 根据SkuID获取商品SKU信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseProductsSkuInfo GetSingleWarehouseProductsSkuInfo(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
		}

		/// <summary>
		/// 根据SkuID获取商品SKU列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseProductsSkuInfo> GetManyWarehouseProductsSkuInfo(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().GetManyWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
		}
	}
}





