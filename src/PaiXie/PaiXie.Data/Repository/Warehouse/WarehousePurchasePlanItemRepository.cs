using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data {
	public class WarehousePurchasePlanItemRepository : BaseRepository<WarehousePurchasePlanItem> {

		#region 构造函数
		private static WarehousePurchasePlanItemRepository _instance;
		public static WarehousePurchasePlanItemRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehousePurchasePlanItemRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(WarehousePurchasePlanItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehousePurchasePlanItem>("warehousePurchasePlanItem", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(WarehousePurchasePlanItem entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehousePurchasePlanItem>("warehousePurchasePlanItem", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
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
		public WarehousePurchasePlanItem GetSingleWarehousePurchasePlanItem(int planID, int productsSkuID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = planID;
			objects[1] = productsSkuID;
			string sqlStr = "SELECT * FROM warehousePurchasePlanItem WHERE PlanID=@0 and ProductsSkuID=@1";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 获取计划数量大于已采购数量的明细列表(生成采购单)

		/// <summary>
		/// 获取计划数量大于已采购数量的明细列表(生成采购单)
		/// </summary>
		/// <param name="planItemIDList">采购计划单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<WarehousePurchasePlanItem> GetWarehousePurchasePlanItemList(List<int> planItemIDList, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", planItemIDList.ToArray());
			string sqlStr = @"SELECT * FROM warehousePurchasePlanItem WHERE FIND_IN_SET(ID, @0) AND Num>PurchasedNum ORDER BY SuppliersID,ID ASC";
			return GetQueryMany(sqlStr, context, objects);
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
		public int GetCount(string warehouseCode, int planID, int productsID, IDbContext context = null) {
			string strWhere = string.Empty;
			if (productsID > 0) {
				strWhere += " and ProductsID = @2";
			}
			string sqlStr = @"SELECT Count(ID) FROM warehousePurchasePlanItem WHERE WarehouseCode = @0 AND PlanID = @1" + strWhere;
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = planID;
			objects[2] = productsID;
			return GetCount(sqlStr, context, objects);
		}

		#endregion

		#region 根据计划单商品表ID删除记录

		/// <summary>
		/// 根据计划单商品表ID删除记录
		/// </summary>
		/// <param name="projectType">1:管理端 2:仓库端 使用枚举</param>
		/// <param name="planItemIDList">计划单商品表ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Delete(int projectType, List<int> planItemIDList, IDbContext context = null) {
			string whereSql = string.Empty;
			if (projectType == (int)ProjectType.仓库端) {
				whereSql = " AND PlanID IN (SELECT ID FROM warehousePurchasePlan WHERE Status=" + (int)PurchasePlanStatus.未提交 + ")";
			}
			else {
				whereSql = " AND PlanID IN (SELECT ID FROM warehousePurchasePlan WHERE PurchaseOrderCount=0 AND Status=" + (int)PurchasePlanStatus.已提交 + ")";
			}
			string sqlStr = "DELETE FROM warehousePurchasePlanItem Where FIND_IN_SET(ID, @0)" + whereSql;
			Object[] objects = new Object[1];
			objects[0] = string.Join(",", planItemIDList.ToArray());
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据采购计划单ID删除记录
		/// <summary>
		/// 根据采购计划单ID删除记录
		/// </summary>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int DeleteByPlanID(int planID, IDbContext context = null) {
			string sqlStr = "DELETE FROM warehousePurchasePlanItem Where PlanID=@0";
			Object[] objects = new Object[1];
			objects[0] = planID;
			return Del(sqlStr, context, objects);
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
		public int UpdateSuppliersID(string userCode, int planItemID, int suppliersID, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = planItemID;
			objects[1] = suppliersID;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchasePlanItem SET SuppliersID=@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0 AND PurchasedNum=0";
			return Update(sqlStr, context, objects);
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
		public int UpdatePurchasedNum(string userCode, int planItemID, int diffNum, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = planItemID;
			objects[1] = diffNum;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchasePlanItem SET PurchasedNum=PurchasedNum+@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据采购计划单ID获取未采购完成的明细条数

		/// <summary>
		/// 根据采购计划单ID获取未采购完成的明细条数
		/// </summary>
		/// <param name="planID">采购计划单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int GetNotFinCount(int planID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = planID;
			string sqlStr = @"SELECT Count(0) FROM warehousePurchasePlanItem WHERE PlanID=@0 AND Num>PurchasedNum";
			return GetCount(sqlStr, context, objects);
		}

		#endregion
	}
}





