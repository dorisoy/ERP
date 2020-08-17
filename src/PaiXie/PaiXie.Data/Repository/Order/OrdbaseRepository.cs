using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
using System.Data;
namespace PaiXie.Data {
	public class OrdbaseRepository : BaseRepository<Ordbase> {

		#region 构造函数

		private static OrdbaseRepository _instance;
		public static OrdbaseRepository GetInstance() {
			if (_instance == null) {
				_instance = new OrdbaseRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(Ordbase entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<Ordbase>("ord_base", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update

		public int Update(Ordbase entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Ordbase>("ord_base", entity)
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
		public virtual Ordbase GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM ord_base WHERE ID=@0";
			Ordbase obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取单个实体 通过系统订单号

		/// <summary>
		/// 获取单个实体 通过系统订单号
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual Ordbase GetQuerySingleByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = "SELECT * FROM ord_base WHERE ErpOrderCode=@0";
			Ordbase obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 将订单由一状态改为另外一状态 (发货时间和物流信息可不传)

		/// <summary>
		/// 将订单由一状态改为另外一状态 (发货时间和物流信息可不传)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="oldOrderStatus">旧状态</param>
		/// <param name="newOrderStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="deliveryDate">发货时间</param>
		/// <param name="deliveryExpressID">发货快递公司ID</param>
		/// <param name="waybillNo">运单号</param>
		/// <returns></returns>
		public virtual int UpdateOrderStatus(string userCode, string erpOrderCode, int oldOrderStatus, int newOrderStatus, IDbContext context = null, DateTime? deliveryDate = null, int deliveryExpressID = 0, string waybillNo = "") {
			Object[] objects = new Object[8];
			objects[0] = erpOrderCode;
			objects[1] = oldOrderStatus;
			objects[2] = newOrderStatus;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			objects[5] = deliveryDate;
			objects[6] = deliveryExpressID;
			objects[7] = waybillNo;
			string fieldStr = string.Empty;
			if (deliveryDate != null && deliveryExpressID > 0 && waybillNo != "") {
				fieldStr = ",DeliveryDate=@5,DeliveryExpressID=@6,WaybillNo=@7";
			}
			string sqlStr = @"UPDATE ord_base SET OrderStatus=@2,UpdatePerson=@3,UpdateDate=@4" + fieldStr + " WHERE ErpOrderCode=@0 AND OrderStatus=@1";
			return Update(sqlStr, context, objects);
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
			string sqlStr = "DELETE FROM ord_base WHERE ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 获取未审核订单数量

		/// <summary>
		/// 获取未审核订单数量
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int getUncommittedCount(IDbContext context = null) {
			string sqlStr = "SELECT * FROM ord_base WHERE CreateType = " + (int)OrdCreateType.手动 + " and OrderStatus = '" + OrdBaseStatus.未生成 + "'";
			return GetCount(sqlStr, context);
		}

		#endregion

		#region 更新指定的发货物流

		/// <summary>
		/// 更新物流ID
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="logisticsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int UpdateLogisticsID(string erpOrderCode, int logisticsID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = erpOrderCode;
			objects[1] = logisticsID;
			string sqlStr = "UPDATE ord_base SET LogisticsID = @1 WHERE ErpOrderCode = @0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 订单是否生成完成

		/// <summary>
		/// 订单是否生成完成
		/// </summary>
		/// <param name="ordbaseID"></param>
		/// <param name="context"></param>
		/// <returns>1：是 0：否</returns>
		public virtual int IsGenerateComplete(int ordbaseID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ordbaseID;
			string sqlStr = "SELECT 1 FROM ord_item WHERE ID = @0 AND OutboundID = 0";
			return GetCount(sqlStr, context, objects) > 0 ? 0 : 1;
		}

		#endregion

		#region 更新订单主表商品金额和数量

		/// <summary>
		/// 更新订单主表商品金额和数量
		/// </summary>
		/// <param name="erpOrderCode">订单编号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateOrdbaseAmount(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"UPDATE ord_base o LEFT JOIN (SELECT ErpOrderCode,SUM(ActualSellingPrice * ProductsNum) ActualSellingPrice,SUM(SellingPrice * ProductsNum) SellingPrice,SUM(DiscountAmount * ProductsNum) DiscountAmount,SUM(ProductsNum) ProductsNum,SUM(SellingPrice * ProductsNum * TaxRate) TaxRevenue FROM ord_item where ErpOrderCode = @0 GROUP BY ErpOrderCode) AS i ON o.ErpOrderCode = i.ErpOrderCode
							  SET o.OrderDiscount = IFNULL(i.DiscountAmount,0),
                                  o.ProductsAmount = IFNULL(i.SellingPrice,0),
                                  o.ProductsNum = IFNULL(i.ProductsNum,0),
                                  o.TaxRevenue = IFNULL(i.TaxRevenue,0),
                                  o.ReceivableAmount = IFNULL(i.SellingPrice,0) - IFNULL(i.DiscountAmount,0) + o.Freight
							  WHERE o.ErpOrderCode = @0 and IFNULL(i.DiscountAmount,0) <= IFNULL(i.SellingPrice,0)";

			return Update(sqlStr, context, objects);
		}

		/// <summary>
		/// 更新订单主表实收金额
		/// </summary>
		/// <param name="erpOrderCode">订单编号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateRealAmount(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"UPDATE ord_base o LEFT JOIN (SELECT ErpOrderCode,SUM(CASE WHEN BillType = " + (int)BillType.SK + " AND Status > 0 THEN Amount ELSE 0 END) RealAmount,SUM(CASE WHEN BillType = " + (int)BillType.SK + " AND Status = 0 THEN Amount ELSE 0 END) UncollectedeAmount,SUM(CASE WHEN BillType = " + (int)BillType.TK + @" THEN Amount ELSE 0 END) RefundAmount FROM ord_accountsBill WHERE ErpOrderCode = @0) AS i ON o.ErpOrderCode = i.ErpOrderCode
							  SET o.RealAmount = IFNULL(i.RealAmount,0),
                                  o.RefundAmount = IFNULL(i.RefundAmount,0),
								  o.UncollectedeAmount = IFNULL(i.UncollectedeAmount,0)
						      WHERE o.ErpOrderCode = @0";

			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取符合整单原则的仓库列表

		/// <summary>
		/// 获取符合整单原则的仓库列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual DataTable GetMatchingWarehouse(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"  SELECT T1.ID,T1.Code,T1.Name FROM(
                                SELECT T.ID,T.Code,T.Name,T.KfhNum FROM (
								SELECT w.ID,w.Name,w.Code,o.ProductsSkuID,(IFNULL(SUM(wlp.KyNum),0) - IFNULL(SUM(wlp.ZyNum),0)) KfhNum,(SELECT IFNULL(SUM(CASE WHEN BookingModel = 0 THEN BookingNum - ZyNum - CdNum ELSE BookingNum END),0) FROM warehouseBookingProductsSku WHERE WarehouseCode = wps.WarehouseCode AND ProductsSkuID = wps.ProductsSkuID)  YsNum
								FROM warehouseProductsSku wps 
								INNER JOIN warehouseProducts wp ON wps.ProductsID = wp.ProductsID AND wps.WarehouseCode = wp.WarehouseCode AND ProductsStatus = 1
								INNER JOIN warehouse w ON wps.WarehouseCode = w.Code AND IsEnable = 1
								INNER JOIN ord_occupy o ON wps.ProductsSkuID = o.ProductsSkuID
								LEFT JOIN warehouseLocationProducts wlp ON o.ProductsSkuID = wlp.ProductsSkuID AND wlp.WarehouseCode = wps.WarehouseCode
								WHERE ErpOrderCode = @0
								GROUP BY wps.WarehouseCode,o.ProductsSkuID
								) AS T INNER JOIN ord_occupy O ON T.ProductsSkuID = O.ProductsSkuID
                                WHERE O.ErpOrderCode = @0 AND (KfhNum + YsNum) >= O.Num
                                ) AS T1
								GROUP BY T1.ID,T1.Code,T1.Name
								HAVING COUNT(*) >= (SELECT COUNT(*) FROM ord_occupy WHERE ErpOrderCode = @0)
								ORDER BY KfhNum DESC";

			return GetDataTable(sqlStr, context, objects);
		}

		#endregion

		#region 修改订单为已驳回，并记录驳回备注

		/// <summary>
		/// 修改订单为已驳回，并记录驳回备注
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="rejectRemark">驳回备注</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateRejectStatus(string erpOrderCode, string rejectRemark, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = erpOrderCode;
			objects[1] = rejectRemark;
			string sqlStr = @"UPDATE ord_base SET IsReject=1, RejectRemark=@1 WHERE ErpOrderCode = @0";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





