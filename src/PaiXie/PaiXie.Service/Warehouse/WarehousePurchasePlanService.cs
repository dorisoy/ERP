using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePurchasePlanService  : BaseService<WarehousePurchasePlan> {

		#region Update

		public static int Update(WarehousePurchasePlan entity, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehousePurchasePlan entity, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Add(entity, context);
		}

		#endregion
		
		#region 根据采购计划单ID删除记录

		/// <summary>
		/// 根据采购计划单ID删除记录
		/// </summary>
		/// <param name="projectType">1:管理端 2:仓库端 使用枚举</param>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(int projectType, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Delete(projectType, planID, context);
		}

		#endregion

		#region 根据采购计划单ID结束记录

		/// <summary>
		/// 根据采购计划单ID结束记录
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int End(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().End(userCode, planID, context);
		}

		#endregion

		#region 根据采购计划单ID把已结束状态还原为已提交

		/// <summary>
		/// 根据采购计划单ID把已结束状态还原为已提交
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Restore(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Restore(userCode, planID, context);
		}

		#endregion

		#region  获取采购计划单实体

		/// <summary>
		/// 获取采购计划单实体
		/// </summary>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehousePurchasePlan GetSingleWarehousePurchasePlan(int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().GetSingleWarehousePurchasePlan(planID, context);
		}

		#endregion

		#region 更新采购计划单的计划数量

		/// <summary>
		/// 更新采购计划单的计划数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planID">计划单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateNum(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().UpdateNum(userCode, planID, context);
		}

		#endregion

		#region 更新采购计划单的已采购数量

		/// <summary>
		/// 更新采购计划单的已采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planID">计划单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdatePurchasedNum(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().UpdatePurchasedNum(userCode, planID, context);
		}

		#endregion

		#region 更新采购计划单的采购单条数

		/// <summary>
		/// 更新采购计划单的采购单条数
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="planID">计划单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdatePurchaseOrderCount(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().UpdatePurchaseOrderCount(userCode, planID, context);
		}

		#endregion
	}
}





