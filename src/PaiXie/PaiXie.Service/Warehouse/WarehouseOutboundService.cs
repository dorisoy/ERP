using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
using PaiXie.Core;
namespace PaiXie.Service {
	public class WarehouseOutboundService : BaseService<WarehouseOutbound> {

		#region Update

		public static int Update(WarehouseOutbound entity, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseOutbound entity, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseOutbound GetQuerySingleByID(int id, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetQuerySingleByID(id, context);
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
		public static WarehouseOutbound GetQuerySingleByBillNo(string warehouseCode, string billNo, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetQuerySingleByBillNo(warehouseCode, billNo, context);
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">主键ID</param>
		/// <param name="canDelStatus">可以删除的状态，用于WHERE校验</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static int DelByID(string warehouseCode, int id, int canDelStatus, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().DelByID(warehouseCode, id, canDelStatus, context);
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
		public static int UpdateStatus(string userCode, string warehouseCode, int outboundID, int oldStatus, int newStatus, DateTime? deliveryDate = null, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateStatus(userCode, warehouseCode, outboundID, oldStatus, newStatus, deliveryDate, context);
		}

		#endregion

		#region 根据系统订单号检查是否还有未出库的出库单，排除已取消

		/// <summary>
		/// 根据系统订单号查询是否还有未出库的出库单，排除已取消
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static bool IsExistsNoDelivery(string erpOrderCode, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().IsExistsNoDelivery(erpOrderCode, context);
		}

		#endregion

		#region 获取安排打印出库单的快递个数

		/// <summary>
		/// 获取安排打印出库单的快递个数
		/// </summary>
		/// <param name="ids">出库单主键ID，多个以半角逗号隔开</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetExpressCount(string ids, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetExpressCount(ids, context);
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
		public static int ArrangePrint(string userCode, string warehouseCode, int outboundID, string printBatchCode, int deliveryExpressID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().ArrangePrint(userCode, warehouseCode, outboundID, printBatchCode, deliveryExpressID, context);
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
		public static int ReturnWaitPick(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().ReturnWaitPick(userCode, warehouseCode, outboundID, context);
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
		public static int ReturnWaitPrint(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().ReturnWaitPrint(userCode, warehouseCode, outboundID, context);
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
		public static int UpdateIsHang(string userCode, string warehouseCode, int outboundID, int isHang, string hangRemark, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateIsHang(userCode, warehouseCode, outboundID, isHang, hangRemark, context);
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
		public static int GetApplyRefundCount(string warehouseCode, int status, string printBatchCode, int isWaitPurchase, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetApplyRefundCount(warehouseCode, status, printBatchCode, isWaitPurchase, context);
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
		public static int GetPrintCount(string warehouseCode, int status, string printBatchCode, int printTemplateType, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetPrintCount(warehouseCode, status, printBatchCode, printTemplateType, context);
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
		public static int UpdateProductsNumWeightAndAmount(string userCode, int id, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateProductsNumWeightAndAmount(userCode, id, context);
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
		public static List<WarehouseOutbound> GetWarehouseOutboundList(string warehouseCode, string printBatchCode, int status, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetWarehouseOutboundList(warehouseCode, printBatchCode, status, context);
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
		public static List<WarehouseOutbound> GetWarehouseOutboundList(string warehouseCode, List<int> outboundIDList, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetWarehouseOutboundList(warehouseCode, outboundIDList, context);
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
		public static int SetPickPrintDate(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().SetPickPrintDate(userCode, warehouseCode, outboundID, context);
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
		public static int SetDeliveryPrintDate(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().SetDeliveryPrintDate(userCode, warehouseCode, outboundID, context);
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
		public static int SetExpressPrintDate(string userCode, string warehouseCode, int outboundID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().SetExpressPrintDate(userCode, warehouseCode, outboundID, context);
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
		public static int UpdateDeliveryExpressID(string userCode, string warehouseCode, int outboundID, int deliveryExpressID, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateDeliveryExpressID(userCode, warehouseCode, outboundID, deliveryExpressID, context);
		}

		#endregion

		#region 修改出库单运单号

		/// <summary>
		/// 修改出库单运单号
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="outboundID">出库单ID</param>
		/// <param name="waybillNo">运单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateWaybillNo(string userCode, string warehouseCode, int outboundID, string waybillNo, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateWaybillNo(userCode, warehouseCode, outboundID, waybillNo, context);
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
		public static int GetDeliveryExpressID(string warehouseCode, string printBatchCode, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetDeliveryExpressID(warehouseCode, printBatchCode, context);
		}

		#endregion

		#region 根据打印批次和状态获取打印快递单需要的出库单列表
		/// <summary>
		/// 根据打印批次和状态获取打印快递单需要的出库单列表
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="status">出库单状态 枚举</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseOutboundList> GetPrintWarehouseOutboundList(string warehouseCode, string printBatchCode, int status, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = warehouseCode;
			objects[1] = printBatchCode;
			objects[2] = (int)WarehouseOutboundStatus.待打印;
			string sqlStr = @"SELECT wob.*,shop.Name AS ShopName,'' as ProList,'' AS UncollectedeAmountChinese FROM warehouseOutbound wob
			LEFT JOIN warehouseOutboundPrintBatch wobpb ON wob.ID=wobpb.OutboundID
			LEFT JOIN shop ON wob.ShopID=shop.ID
			WHERE wob.WarehouseCode=@0 AND wob.BillType=" + (int)BillType.XSC + @" AND wob.PrintBatchCode=@1 AND wob.Status=@2 
			ORDER BY wobpb.ID ASC,wob.ID DESC";
			return BaseService<WarehouseOutboundList>.GetQueryMany(sqlStr, context, objects);
		}
		#endregion

		#region 根据出库单ID获取打印快递单需要的出库单

		/// <summary>
		/// 根据出库单ID获取打印快递单需要的出库单
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="id">出库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseOutboundList GetPrintWarehouseOutbound(string warehouseCode, int id, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = id;
			string sqlStr = @"SELECT wob.*,shop.Name AS ShopName,'' as ProList,'' AS UncollectedeAmountChinese FROM warehouseOutbound wob 
			LEFT JOIN shop ON wob.ShopID=shop.ID
			WHERE wob.WarehouseCode=@0 AND wob.BillType=" + (int)BillType.XSC + @" AND wob.ID=@1";
			return BaseService<WarehouseOutboundList>.GetQuerySingle(sqlStr, context, objects);
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
		public static List<WarehouseOutbound> GetWarehouseOutboundByBillNoOrWaybillNo(string warehouseCode, string billNo, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetWarehouseOutboundByBillNoOrWaybillNo(warehouseCode, billNo, context);
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
		public static List<WarehouseOutbound> GetWarehouseOutboundByBillNoOrErpOrderCode(string warehouseCode, string billNo, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetWarehouseOutboundByBillNoOrErpOrderCode(warehouseCode, billNo, context);
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
		public static int UpdateTotalWeight(string userCode, string warehouseCode, string billNo, decimal totalWeight, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateTotalWeight(userCode, warehouseCode, billNo, totalWeight, context);
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
		public static int UpdateExpressFreight(string userCode, string warehouseCode, int id, decimal expressPrice, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateExpressFreight(userCode, warehouseCode, id, expressPrice, context);
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
		public static int UpdateScanCheck(string userCode, string warehouseCode, string billNo, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateScanCheck(userCode, warehouseCode, billNo, context);
		}

		#endregion

		#region 根据订单号获取出库单信息

		/// <summary>
		/// 根据订单号获取出库单信息
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<WarehouseOutbound> GetWarehouseOutboundByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().GetWarehouseOutboundByErpOrderCode(erpOrderCode, context);
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
		public static int Cancel(string userCode, string warehouseCode, string billNo, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().Cancel(userCode, warehouseCode, billNo, context);
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
		public static int UpdateIsWaitPurchase(string userCode, int id, int isWaitPurchase, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateIsWaitPurchase(userCode, id, isWaitPurchase, context);
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
		public static int UpdateIsPurchasePlan(string userCode, int id, IDbContext context = null) {
			return WarehouseOutboundRepository.GetInstance().UpdateIsPurchasePlan(userCode, id, context);
		}

		#endregion
	}
}





