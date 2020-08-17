using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class WarehouseConversionRuleService : BaseService<WarehouseConversionRule> {

		public static int Update(WarehouseConversionRule entity, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseConversionRule entity, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 获取商品转换规则实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="ruleID">规则ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseConversionRule GetSingleWarehouseConversionRule(string warehouseCode, int ruleID, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().GetSingleWarehouseConversionRule(warehouseCode, ruleID, context);
		}

		/// <summary>
		/// 删除转换规则
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">规则ID列表</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> ruleIDList, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().Delete(warehouseCode, ruleIDList, context);
		}

		/// <summary>
		/// 获取商品转换规则实体列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static List<WarehouseConversionRule> GetManyWarehouseConversionRule(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().GetManyWarehouseConversionRule(warehouseCode, productsSkuID, context);
		}
	}
}





