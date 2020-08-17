using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePurchasePlanItemService  : BaseService<WarehousePurchasePlanItem> {

		#region Update

		public static int Update(WarehousePurchasePlanItem entity, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehousePurchasePlanItem entity, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 根据计划单主键ID和商品SKUID获取单个实体

		/// <summary>
		/// 根据计划单主键ID和商品SKUID获取单个实体
		/// </summary>
		/// <param name="planID">计划单主键ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehousePurchasePlanItem GetSingleWarehousePurchasePlanItem(int planID, int productsSkuID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetSingleWarehousePurchasePlanItem(planID, productsSkuID, context);
		}

		#endregion

		#region 添加商品时获取SKU列表

		/// <summary>
		/// 添加商品时获取SKU列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<WarehousePurchasePlanSkuList> GetQueryManyForSkuList(SelectBuilder data, out int count) {
			BaseRepository<WarehousePurchasePlanSkuList> obj = new BaseRepository<WarehousePurchasePlanSkuList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		#endregion

		#region 获取计划数量大于已采购数量的明细列表(生成采购单)

		/// <summary>
		/// 获取计划数量大于已采购数量的明细列表(生成采购单)
		/// </summary>
		/// <param name="planItemIDList">采购计划单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehousePurchasePlanItem> GetWarehousePurchasePlanItemList(List<int> planItemIDList, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetWarehousePurchasePlanItemList(planItemIDList, context);
		}

		#endregion

		#region 获取行数

		/// <summary>
		/// 获取行数
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetCount(string warehouseCode, int planID, int productsID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetCount(warehouseCode, planID, productsID, context);
		}

		#endregion

		#region 根据计划单商品表ID删除记录

		/// <summary>
		/// 根据计划单商品表ID删除记录
		/// </summary>
		/// <param name="projectType">1:管理端 2:仓库端 使用枚举</param>
		/// <param name="ruleItemID">计划单商品表ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(int projectType, List<int> planItemIDList, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().Delete(projectType, planItemIDList, context);
		}

		#endregion

		#region 根据采购计划单ID删除记录

		/// <summary>
		/// 根据采购计划单ID删除记录
		/// </summary>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeleteByPlanID(int planID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().DeleteByPlanID(planID, context);
		}

		#endregion

		#region 设置计划采购商品供应商

		/// <summary>
		/// 设置计划采购商品供应商
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planItemID">计划采购商品表主键ID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateSuppliersID(string userCode, int planItemID, int suppliersID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().UpdateSuppliersID(userCode, planItemID, suppliersID, context);
		}

		#endregion

		#region 更新计划采购商品已采购数量

		/// <summary>
		/// 更新计划采购商品已采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planItemID">计划采购商品表主键ID</param>
		/// <param name="diffNum">已采购数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdatePurchasedNum(string userCode, int planItemID, int diffNum, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().UpdatePurchasedNum(userCode, planItemID, diffNum, context);
		}

		#endregion

		#region 根据采购计划单ID获取未采购完成的明细条数

		/// <summary>
		/// 根据采购计划单ID获取未采购完成的明细条数
		/// </summary>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetNotFinCount(int planID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetNotFinCount(planID, context);
		}

		#endregion
	}
}





