using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data {
	public class WarehousePurchasePlanRepository : BaseRepository<WarehousePurchasePlan> {

		#region 构造函数
		private static WarehousePurchasePlanRepository _instance;
		public static WarehousePurchasePlanRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehousePurchasePlanRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehousePurchasePlan entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehousePurchasePlan>("warehousePurchasePlan", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehousePurchasePlan entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehousePurchasePlan>("warehousePurchasePlan", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
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
		public int Delete(int projectType, int planID, IDbContext context = null) {
			string whereSql = "";
			if (projectType ==(int)ProjectType.仓库端) {
				whereSql = " AND Status = " + (int)PurchasePlanStatus.未提交;
			}
			else {
				whereSql = " AND PurchaseOrderCount = 0  AND Status =" + (int)PurchasePlanStatus.已提交;
			}
			string sqlStr = "DELETE FROM warehousePurchasePlan WHERE ID=@0" + whereSql;
			Object[] objects = new Object[1];
			objects[0] = planID;
			return Del(sqlStr, context, objects);
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
		public int End(string userCode, int planID, IDbContext context = null) {
			string sqlStr = @"UPDATE warehousePurchasePlan SET Status=" + (int)PurchasePlanStatus.已结束 + @",
			UpdatePerson=@1,UpdateDate=@2
			WHERE ID=@0 AND PurchaseOrderCount > 0  AND Status =" + (int)PurchasePlanStatus.已提交;
			Object[] objects = new Object[3];
			objects[0] = planID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			return Update(sqlStr, context, objects);
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
		public int Restore(string userCode, int planID, IDbContext context = null) {
			string sqlStr = @"UPDATE warehousePurchasePlan SET Status=" + (int)PurchasePlanStatus.已提交 + @",
			UpdatePerson=@1,UpdateDate=@2
			WHERE ID=@0 AND PurchaseOrderCount > 0  AND Status =" + (int)PurchasePlanStatus.已结束;
			Object[] objects = new Object[3];
			objects[0] = planID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取采购计划单实体

		/// <summary>
		/// 获取采购计划单实体
		/// </summary>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public WarehousePurchasePlan GetSingleWarehousePurchasePlan(int planID, IDbContext context = null) {
			string sqlStr = @"SELECT *
                              FROM warehousePurchasePlan
                              WHERE ID = @0";
			Object[] objects = new Object[1];
			objects[0] = planID;
			return GetQuerySingle(sqlStr, context, objects);
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
		public int UpdateNum(string userCode, int planID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = planID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchasePlan SET Num=IFNULL((SELECT SUM(Num) FROM warehousePurchasePlanItem WHERE PlanID=warehousePurchasePlan.ID),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
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
		public int UpdatePurchasedNum(string userCode, int planID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = planID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchasePlan SET PurchasedNum=IFNULL((SELECT SUM(PurchasedNum) FROM warehousePurchasePlanItem WHERE PlanID=warehousePurchasePlan.ID),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
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
		public int UpdatePurchaseOrderCount(string userCode, int planID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = planID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchasePlan SET PurchaseOrderCount=IFNULL((SELECT COUNT(0) FROM warehousePurchase WHERE PlanID=warehousePurchasePlan.ID),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





