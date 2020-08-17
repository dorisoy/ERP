using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Data 
{
	public class WarehousePurchaseRepository : BaseRepository<WarehousePurchase> {

		#region 构造函数

		private static WarehousePurchaseRepository _instance;
		public static WarehousePurchaseRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehousePurchaseRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(WarehousePurchase entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehousePurchase>("warehousePurchase", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update
		public int Update(WarehousePurchase entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehousePurchase>("warehousePurchase", entity)
					.AutoMap(x => x.ID)
					.Where(x => x.ID)
					.Execute();
			return rowsAffected;
		}
		#endregion

		#region 获取单个实体 通过主键ID
		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehousePurchase GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehousePurchase WHERE ID=@0";
			WarehousePurchase obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取单个实体 通过采购单号

	    /// <summary>
		/// 获取单个实体 通过采购单号
	    /// </summary>
		/// <param name="billNo">采购单号</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public virtual WarehousePurchase GetQuerySingleByBillNo(string billNo, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = billNo;
			string sqlStr = "SELECT * FROM warehousePurchase WHERE BillNo=@0";
			WarehousePurchase obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 删除操作  通过ID
		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "DELETE FROM warehousePurchase WHERE ID=@0 AND InStockOrderCount=0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 确认采购单

		/// <summary>
		/// 确认采购单
		/// </summary>
		/// <param name="purchaseID">主键ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public virtual int Confirm(int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = purchaseID;
			string sqlStr = @"UPDATE warehousePurchase SET Status=" + (int)PurchaseStatus.已确认 + @" 
			WHERE ID=@0 AND Status=" + (int)PurchaseStatus.未确认 + " AND Num>0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 结束采购单

		/// <summary>
		/// 结束采购单
		/// </summary>
		/// <param name="purchaseID">主键ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public virtual int End(int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = purchaseID;
			string sqlStr = @"UPDATE warehousePurchase SET Status=" + (int)PurchaseStatus.已结束 + @" 
			WHERE ID=@0 AND InStockOrderCount>0 AND Status=" + (int)PurchaseStatus.已确认;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据采购单ID把已结束状态还原为已确认

		/// <summary>
		/// 根据采购单ID把已结束状态还原为已确认
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int Restore(string userCode, int purchaseID, IDbContext context = null) {
			string sqlStr = @"UPDATE warehousePurchase SET Status=" + (int)PurchaseStatus.已确认 + @",
			UpdatePerson=@1,UpdateDate=@2
			WHERE ID=@0 AND InStockOrderCount > 0  AND Status =" + (int)PurchaseStatus.已结束;
			Object[] objects = new Object[3];
			objects[0] = purchaseID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新采购单的采购数量

		/// <summary>
		/// 更新采购单的采购数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateNum(string userCode, int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = purchaseID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchase SET Num=IFNULL((SELECT SUM(Num) FROM warehousePurchaseItem WHERE PurchaseID=warehousePurchase.ID),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新采购单的已入库数量

		/// <summary>
		/// 更新采购单的已入库数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateInStockNum(string userCode, int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = purchaseID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchase SET InStockNum=IFNULL((SELECT SUM(InStockNum) FROM warehousePurchaseItem WHERE PurchaseID=warehousePurchase.ID),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新采购单的入库单条数

		/// <summary>
		/// 更新采购单的入库单条数
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="purchaseID">采购单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateInStockOrderCount(string userCode, int purchaseID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = purchaseID;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehousePurchase SET InStockOrderCount=IFNULL((SELECT COUNT(0) FROM warehouseOutInStock WHERE BillType=" + (int)BillType.CGR + @" AND SourceID=@0),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新采购单库存

	/// <summary>
		/// 更新采购单库存
	/// </summary>
	/// <param name="rkts">订单数</param>
	/// <param name="rknum">入库数量</param>
	/// <param name="id">主键id</param>
	/// <param name="context"></param>
	/// <returns></returns>
		public virtual int UpdatewarehousePurchasekc(int rkts, int rknum, int id, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = rkts;
			objects[1] = rknum;
			objects[2] = id;
			string strsql2 = "UPDATE   warehousePurchase SET InStockOrderCount=@0, InStockNum=@1 WHERE id=@2";
			return context.Sql(strsql2, objects).Execute();
		}

		#endregion		
	}
}