using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using PaiXie.Core;
using PaiXie.Utils;
using System.Data;
namespace PaiXie.Data {
	public class WarehouseOutboundRepository : BaseRepository<WarehouseOutbound> {

		#region 构造函数

		private static WarehouseOutboundRepository _instance;
		public static WarehouseOutboundRepository GetInstance() {
			if (_instance == null) {
				_instance = new WarehouseOutboundRepository();
			}
			return _instance;
		}

		#endregion

		#region Add

		public int Add(WarehouseOutbound entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<WarehouseOutbound>("warehouseOutbound", entity)
					.AutoMap(x => x.ID)
					.ExecuteReturnLastId<int>();
			return Id;
		}

		#endregion

		#region Update

		public int Update(WarehouseOutbound entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<WarehouseOutbound>("warehouseOutbound", entity)
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
		public virtual WarehouseOutbound GetQuerySingleByID(int id, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = id;
			string sqlStr = "SELECT * FROM warehouseOutbound WHERE ID=@0";
			WarehouseOutbound obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 获取单个实体 通过出库单号

		/// <summary>
		/// 获取单个实体 通过出库单号
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual WarehouseOutbound GetQuerySingleByBillNo(string warehouseCode, string billNo, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @0";
			}
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			string sqlStr = "SELECT * FROM warehouseOutbound WHERE BillNo=@1" + strWhere;
			WarehouseOutbound obj = GetQuerySingle(sqlStr, context, objects);
			return obj;
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">主键ID</param>
		/// <param name="canDelStatus">可以删除的状态，用于WHERE校验</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int DelByID(string warehouseCode, int id, int canDelStatus, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = id;
			objects[2] = canDelStatus;
			string sqlStr = "DELETE FROM warehouseOutbound WHERE WarehouseCode=@0 AND ID=@1 AND Status=@2";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 将出库单由一状态改为另外一个状态 (已经过滤申请退款)

		/// <summary>
		/// 将出库单由一状态改为另外一个状态 (已经过滤申请退款)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="deliveryDate">发货时间</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateStatus(string userCode, string warehouseCode, int outboundID, int oldStatus, int newStatus, DateTime? deliveryDate = null, IDbContext context = null) {
			Object[] objects = new Object[7];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = oldStatus;
			objects[3] = newStatus;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			objects[6] = deliveryDate;
			string fieldStr = string.Empty;
			string whereSql = " AND IsApplyRefund=0";
			if (deliveryDate != null) {
				fieldStr = ",DeliveryDate=@6";
			}
			string sqlStr = @"UPDATE warehouseOutbound SET Status=@3,UpdatePerson=@4,UpdateDate=@5" + fieldStr + " WHERE WarehouseCode=@0 AND ID=@1 AND Status=@2" + whereSql;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据系统订单号检查是否还有未出库的出库单，排除已取消

		/// <summary>
		/// 根据系统订单号查询是否还有未出库的出库单，排除已取消
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual bool IsExistsNoDelivery(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = erpOrderCode;
			objects[1] = (int)WarehouseOutboundStatus.已发货;
			objects[2] = (int)WarehouseOutboundStatus.已取消;
			string sqlStr = @"SELECT Count(0) FROM warehouseOutbound WHERE ErpOrderCode=@0 AND Status<>@1 AND Status<>@2";
			int count = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return count > 0;
		}

		#endregion

		#region 获取安排打印出库单的快递个数

		/// <summary>
		/// 获取安排打印出库单的快递个数
		/// </summary>
		/// <param name="ids">出库单主键ID，多个以半角逗号隔开</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetExpressCount(string ids, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = ids;
			string sqlStr = "SELECT DISTINCT ExpressID FROM warehouseOutbound WHERE FIND_IN_SET(ID, @0)";
			DataTable dt = GetDataTable(sqlStr, context, objects);
			return dt.Rows.Count;
		}

		#endregion

		#region 安排打印 更新打印批次、安排打印时间、出库单状态

		/// <summary>
		/// 安排打印 更新打印批次、安排打印时间、出库单状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单主键ID</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="deliveryExpressID">实际发货快递ID，传0表示使用下单选择快递发货</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int ArrangePrint(string userCode, string warehouseCode, int outboundID, string printBatchCode, int deliveryExpressID, IDbContext context = null) {
			Object[] objects = new Object[8];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = (int)WarehouseOutboundStatus.待拣货;
			objects[3] = (int)WarehouseOutboundStatus.待打印;
			objects[4] = printBatchCode;
			objects[5] = userCode;
			objects[6] = DateTime.Now;
			objects[7] = deliveryExpressID;
			string fiedStr = string.Empty;
			if (deliveryExpressID > 0) {
				fiedStr = ",DeliveryExpressID=@7";
			}
			else {
				fiedStr = ",DeliveryExpressID=ExpressID";
			}
			string whereSql = " AND IsHang=0 AND IsApplyRefund=0";
			string sqlStr = @"UPDATE warehouseOutbound SET Status=@3,PrintBatchCode=@4,UpdatePerson=@5,ArrangePrintDate=@6,UpdateDate=@6" + fiedStr + " WHERE WarehouseCode=@0 AND ID=@1 AND Status=@2" + whereSql;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 待打印返回待拣货 清空打印批次、安排打印时间、实际发货快递ID

		/// <summary>
		/// 待打印返回待拣货
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int ReturnWaitPick(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = (int)WarehouseOutboundStatus.待打印;
			objects[3] = (int)WarehouseOutboundStatus.待拣货;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET Status=@3,PrintBatchCode='',ArrangePrintDate=NULL,PickPrintDate=NULL,DeliveryPrintDate=NULL,ExpressPrintDate=NULL,DeliveryExpressID=0,WaybillNo='',ReturnCount=ReturnCount+1,UpdatePerson=@4,UpdateDate=@5 WHERE warehouseCode=@0 AND ID=@1 AND Status=@2";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 待发货返回待打印

		/// <summary>
		/// 待发货返回待打印
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int ReturnWaitPrint(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = (int)WarehouseOutboundStatus.待发货;
			objects[3] = (int)WarehouseOutboundStatus.待打印;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET Status=@3,ReturnCount=ReturnCount+1,UpdatePerson=@4,UpdateDate=@5 WHERE warehouseCode=@0 AND ID=@1 AND Status=@2";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 挂起和取消挂起

		/// <summary>
		/// 挂起和取消挂起
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单主键ID</param>
		/// <param name="isHang">0取消挂起 1挂起</param>
		/// <param name="hangRemark">挂起备注</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateIsHang(string userCode, string warehouseCode, int outboundID, int isHang, string hangRemark, IDbContext context = null) {
			Object[] objects = new Object[6];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = isHang;
			objects[3] = hangRemark;
			objects[4] = userCode;
			objects[5] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET IsHang=@2,HangRemark=@3,UpdatePerson=@4,UpdateDate=@5 WHERE WarehouseCode=@0 AND ID = @1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单状态获取申请退款出库单笔数

		/// <summary>
		/// 根据出库单状态获取申请退款出库单笔数
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="status">出库单状态 枚举</param>
		/// <param name="printBatchCode">打印批次 如不需要可传""</param>
		/// <param name="isWaitPurchase">是否待采出库单 0否 1是</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetApplyRefundCount(string warehouseCode, int status, string printBatchCode, int isWaitPurchase, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = status;
			objects[2] = isWaitPurchase;
			string whereSql = "";
			if (printBatchCode != "") {
				objects[3] = printBatchCode;
				whereSql = " AND PrintBatchCode=@3";
			}
			string sqlStr = @"SELECT COUNT(0) FROM warehouseOutbound WHERE WarehouseCode=@0 AND Status=@1 AND IsWaitPurchase=@2 AND IsApplyRefund=1" + whereSql;
			int count = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return count;
		}

		#endregion

		#region 根据单据类型获取打印批次的已打印数量

		/// <summary>
		/// 根据单据类型获取打印批次的已打印数量
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="status">出库单状态枚举值</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="printTemplateType">单据枚举类型 0发货单 1拣货单 2快递单</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetPrintCount(string warehouseCode, int status, string printBatchCode, int printTemplateType, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = status;
			objects[2] = printBatchCode;
			string whereSql = "";
			switch (printTemplateType) {
				case (int)PrintTemplateType.发货单:
					whereSql = " AND DeliveryPrintDate IS NOT NULL";
					break;
				case (int)PrintTemplateType.拣货单:
					whereSql = " AND PickPrintDate IS NOT NULL";
					break;
				case 2:
					whereSql = " AND ExpressPrintDate IS NOT NULL";
					break;
			}
			string sqlStr = @"SELECT COUNT(0) FROM warehouseOutbound WHERE WarehouseCode=@0 AND Status=@1 AND PrintBatchCode=@2" + whereSql;
			int count = ZConvert.StrToInt(Getobject(sqlStr, context, objects));
			return count;
		}

		#endregion

		#region 根据出库单ID更新数量、重量和金额

		/// <summary>
		/// 根据出库单ID更新数量、重量和金额
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateProductsNumWeightAndAmount(string userCode, int id, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = id;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET ProductsNum=IFNULL((SELECT SUM(ProductsNum) FROM ord_item WHERE OutboundID=warehouseOutbound.ID),0),
			ProductsWeight=IFNULL((SELECT SUM(ProductsNum*ProductsWeight) FROM ord_item WHERE OutboundID=warehouseOutbound.ID),0),
			ProductsAmount=IFNULL((SELECT SUM(ProductsNum*ActualSellingPrice) FROM ord_item WHERE OutboundID=warehouseOutbound.ID),0),
			UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据打印批次和状态获取出库单列表 (按照安排打印时的顺序排序)

		/// <summary>
		/// 根据打印批次和状态获取出库单列表 (按照安排打印时的顺序排序)
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="status">出库单状态 枚举</param>
		/// <param name="context">数据库连接对象</param>
		public virtual List<WarehouseOutbound> GetWarehouseOutboundList(string warehouseCode, string printBatchCode, int status, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = printBatchCode;
			objects[2] = status;
			string sqlStr = @"SELECT wob.* FROM warehouseOutbound wob
			LEFT JOIN warehouseOutboundPrintBatch wobpb ON wob.ID=wobpb.OutboundID
			WHERE wob.WarehouseCode=@0 AND wob.BillType=" + (int)BillType.XSC + @" AND wob.PrintBatchCode=@1 AND wob.Status=@2 
			ORDER BY wobpb.ID ASC,wob.ID DESC";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单ID列表获取出库单列表

		/// <summary>
		/// 根据出库单ID列表获取出库单列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundIDList">出库单ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutbound> GetWarehouseOutboundList(string warehouseCode, List<int> outboundIDList, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = string.Join(",", outboundIDList.ToArray());
			string sqlStr = @"SELECT * FROM warehouseOutbound WHERE WarehouseCode=@0 AND FIND_IN_SET(ID, @1) ORDER BY FIELD(ID," + string.Join(",", outboundIDList.ToArray()) + ")";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 设置拣货单打印时间

		/// <summary>
		/// 设置拣货单打印时间
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SetPickPrintDate(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET UpdatePerson=@2,UpdateDate=@3,PickPrintDate=@3 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 设置发货单打印时间

		/// <summary>
		/// 设置发货单打印时间
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SetDeliveryPrintDate(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET UpdatePerson=@2,UpdateDate=@3,DeliveryPrintDate=@3 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 设置快递单打印时间

		/// <summary>
		/// 设置快递单打印时间
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int SetExpressPrintDate(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET UpdatePerson=@2,UpdateDate=@3,ExpressPrintDate=@3 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更换发货快递

		/// <summary>
		/// 更换发货快递
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="deliveryExpressID">发货快递ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateDeliveryExpressID(string userCode, string warehouseCode, int outboundID, int deliveryExpressID, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = deliveryExpressID;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET DeliveryExpressID=@2,WaybillNo='',UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 修改出库单运单号

		/// <summary>
		/// 修改出库单运单号
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">出库单ID</param>
		/// <param name="waybillNo">运单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateWaybillNo(string userCode, string warehouseCode, int outboundID, string waybillNo, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = outboundID;
			objects[2] = waybillNo;
			objects[3] = userCode;
			objects[4] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET WaybillNo=@2,UpdatePerson=@3,UpdateDate=@4 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 获取打印批次的发货快递ID

		/// <summary>
		/// 获取打印批次的发货快递ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int GetDeliveryExpressID(string warehouseCode, string printBatchCode, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = printBatchCode;
			string sqlStr = @"SELECT DeliveryExpressID FROM warehouseOutbound WHERE WarehouseCode=@0 AND PrintBatchCode=@1 LIMIT 0,1";
			return ZConvert.StrToInt(Getobject(sqlStr, context, objects));
		}

		#endregion

		#region 根据出库单号或运单号获取出库单信息

		/// <summary>
		/// 根据出库单号或运单号获取出库单信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号或运单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutbound> GetWarehouseOutboundByBillNoOrWaybillNo(string warehouseCode, string billNo, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			string sqlStr = @"SELECT * FROM warehouseOutbound WHERE WarehouseCode=@0 AND (BillNo=@1 OR WaybillNo=@1)";
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 根据出库单号或订单号获取出库单信息

		/// <summary>
		/// 根据出库单号或订单号获取出库单信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号或订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual List<WarehouseOutbound> GetWarehouseOutboundByBillNoOrErpOrderCode(string warehouseCode, string billNo, IDbContext context = null) {
			string strWhere = string.Empty;
			if (!string.IsNullOrEmpty(warehouseCode)) {
				strWhere += " and WarehouseCode = @0";
			}
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			string sqlStr = @"SELECT * FROM warehouseOutbound WHERE (BillNo=@1 OR ErpOrderCode=@1)" + strWhere;
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 更新实际包裹重量

		/// <summary>
		/// 更新实际包裹重量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号</param>
		/// <param name="totalWeight">实际包裹重量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateTotalWeight(string userCode, string warehouseCode, string billNo, decimal totalWeight, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			objects[2] = totalWeight;
			objects[3] = userCode;
			objects[4] = DateTime.Now.ToString();
			string sqlStr = @"UPDATE warehouseOutbound SET TotalWeight=@2, UpdatePerson=@3, UpdateDate=@4 WHERE WarehouseCode=@0 AND BillNo=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新参考运费

		/// <summary>
		/// 更新参考运费
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">出库单ID</param>
		/// <param name="expressPrice">参考运费</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateExpressFreight(string userCode, string warehouseCode, int id, decimal expressPrice, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = id;
			objects[2] = expressPrice;
			objects[3] = userCode;
			objects[4] = DateTime.Now.ToString();
			string sqlStr = @"UPDATE warehouseOutbound SET ExpressFreight=@2, UpdatePerson=@3, UpdateDate=@4 WHERE WarehouseCode=@0 AND ID=@1";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新为已校验

		/// <summary>
		/// 更新为已校验
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateScanCheck(string userCode, string warehouseCode, string billNo, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			objects[2] = userCode;
			objects[3] = DateTime.Now.ToString();
			string sqlStr = @"UPDATE warehouseOutbound SET IsScanCheck=1, ScanCheckDate=@3, UpdatePerson=@2, UpdateDate=@3 WHERE WarehouseCode=@0 AND BillNo=@1 AND IsScanCheck=0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 根据订单号获取出库单信息

		/// <summary>
		/// 根据订单号获取出库单信息
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual List<WarehouseOutbound> GetWarehouseOutboundByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = erpOrderCode;
			string sqlStr = @"SELECT * FROM warehouseOutbound WHERE ErpOrderCode = @0 AND Status != " + (int)WarehouseOutboundStatus.已取消;
			return GetQueryMany(sqlStr, context, objects);
		}

		#endregion

		#region 取消出库单

		/// <summary>
		/// 取消出库单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="billNo">出库单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int Cancel(string userCode, string warehouseCode, string billNo, IDbContext context = null) {
			Object[] objects = new Object[5];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			objects[2] = userCode;
			objects[3] = DateTime.Now.ToString();
			objects[4] = (int)WarehouseOutboundStatus.已取消;
			string sqlStr = @"UPDATE warehouseOutbound SET Status = @4,CancelDate = @3,UpdateDate=@3,UpdatePerson=@2 WHERE BillNo=@1 AND WarehouseCode=@0 AND Status = " + (int)WarehouseOutboundStatus.待拣货;
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新出库单是否待采

		/// <summary>
		/// 更新出库单是否待采
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出库单ID</param>
		/// <param name="isWaitPurchase">是否待采出库单 0否 1是</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateIsWaitPurchase(string userCode, int id, int isWaitPurchase, IDbContext context = null) {
			Object[] objects = new Object[4];
			objects[0] = id;
			objects[1] = isWaitPurchase;
			objects[2] = userCode;
			objects[3] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET IsWaitPurchase=@1,UpdatePerson=@2,UpdateDate=@3 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion

		#region 更新出库单为已生成采购计划单

		/// <summary>
		/// 更新出库单为已生成采购计划单
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public virtual int UpdateIsPurchasePlan(string userCode, int id, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = id;
			objects[1] = userCode;
			objects[2] = DateTime.Now;
			string sqlStr = @"UPDATE warehouseOutbound SET IsPurchasePlan = 1,UpdatePerson=@1,UpdateDate=@2 WHERE ID=@0";
			return Update(sqlStr, context, objects);
		}

		#endregion
	}
}





