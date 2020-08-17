using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using PaiXie.Core;
using PaiXie.Utils;
using FluentData;
namespace  PaiXie.Service 
{
	public class WarehouseProductsService : BaseService<WarehouseProducts> {

		public static int Update(WarehouseProducts entity, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseProducts entity, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 删除仓库商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> productsIDList, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().Delete(warehouseCode, productsIDList, context);
		}

		/// <summary>
		/// 根据商品ID删除仓库商品
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int DeleteByProductsID(int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().DeleteByProductsID(productsID, context);
		}


		/// <summary>
		/// 商品上下架
		/// </summary>
		/// <param name="warehouseCode">仓库编码 如果传空值，则更新所有仓库</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="productsStatus">商品销售状态 销售中=1 仓库中=2 </param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateProductsStatus(string warehouseCode, List<int> productsIDList, int productsStatus, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().UpdateProductsStatus(warehouseCode, productsIDList, productsStatus, context);
		}

		/// <summary>
		/// 获取行数
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="productsStatus">商品销售状态 销售中=1 仓库中=2 </param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetCount(string warehouseCode, List<int> productsIDList, int productsStatus, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetCount(warehouseCode, productsIDList, productsStatus, context);
		}

		/// <summary>
		/// 获取商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseProducts GetSingleWarehouseProducts(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetSingleWarehouseProducts(warehouseCode, productsID, context);
		}

		/// <summary>
		/// 获取商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsCode">商品编码</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseProductsInfo GetSingleWarehouseProductsInfo(string warehouseCode, string productsCode, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetSingleWarehouseProductsInfo(warehouseCode, productsCode, context);
		}

		/// <summary>
		/// 获取商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseProductsInfo GetSingleWarehouseProductsInfo(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetSingleWarehouseProductsInfo(warehouseCode, productsID, context);
		}

		#region 根据商品ID获取仓库商品列表

		/// <summary>
		/// 根据商品ID获取仓库商品列表
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseProducts> GetWarehouseProductsList(int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetWarehouseProductsList(productsID, context);
		}

		#endregion
	}

}





