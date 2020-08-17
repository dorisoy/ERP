using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseConversionRuleItemService  : BaseService<WarehouseConversionRuleItem> {
    
		public static int Update(WarehouseConversionRuleItem entity, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseConversionRuleItem entity, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 获取商品转换规则商品实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="ruleID">商品SKU码</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseConversionRuleItem GetSingleWarehouseConversionRuleItem(string warehouseCode, int ruleID, string productsSkuCode, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().GetSingleWarehouseConversionRuleItem(warehouseCode, ruleID, productsSkuCode, context);
		}

		/// <summary>
		/// 获取转换规则商品列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static List<WarehouseConversionRuleItem> GetManyWarehouseConversionRuleItem(string warehouseCode, int ruleID, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().GetManyWarehouseConversionRuleItem(warehouseCode, ruleID, context);
		}

		/// <summary>
		/// 根据规则商品表ID删除记录
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param> 
		/// <param name="ruleItemID">规则商品表ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, int ruleItemID, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Delete(warehouseCode, ruleItemID, context);
		}

			/// <summary>
		/// 根据规则ID删除记录
		/// </summary>
		/// <param name="ruleItemID">规则商品表ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> ruleIDList, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Delete(warehouseCode, ruleIDList, context);
		}

		/// <summary>
		/// 获取转换规则商品列表(商品转换时使用)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static List<WarehouseConversionRuleItemInfo> GetManyWarehouseConversionRuleItemInfo(string warehouseCode, int ruleID, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().GetManyWarehouseConversionRuleItemInfo(warehouseCode, ruleID, context);
		}
	}
}





