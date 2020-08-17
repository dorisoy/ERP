using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using PaiXie.Api.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace PaiXie.Erp.Areas.Warehouse
{
    public class OutboundController : BaseController {

		#region 出库单速查
		
		#region SearchIndex

		public ActionResult SearchAllIndex() {
			return View();
		}

		public ActionResult SearchAll() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql(-1);
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wob.ID DESC";
			data.From = @" warehouseOutbound wob LEFT JOIN shop ON wob.ShopID=shop.ID
			LEFT JOIN warehouseExpress we ON wob.ExpressID=we.ID
			LEFT JOIN warehouseExpress we1 ON wob.DeliveryExpressID=we1.ID";
			data.Select = @"wob.ID,wob.BillNo,shop.Name AS ShopName,wob.ErpOrderCode,wob.IsApplyRefund,wob.OutOrderCode,wob.CreateDate,wob.BuyName,
			we.Name As ExpressName,we1.Name As DeliveryExpressName,wob.ProductsNum,wob.ExpressID,wob.DeliveryExpressID,wob.WaybillNo,wob.PrintBatchCode,wob.IsScanCheck,wob.TotalWeight,wob.PickPrintDate,wob.DeliveryPrintDate,wob.ExpressPrintDate,wob.IsNeedInvoice,IFNULL(wob.BuyMessage,'') As BuyMessage,IFNULL(wob.SellerRemark,'') As SellerRemark,wob.Status,wob.IsHang,wob.HangRemark,wob.ReturnCount";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutboundList> list = BaseService<WarehouseOutboundList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.StatusName = OutboundManager.GetStatusName(item.Status);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#endregion

		#region 待拣货出库单

		#region WaitPickIndex

		/// <summary>
		/// WaitPickIndex
		/// </summary>
		/// <param name="isWaitPurchase">是否待采 0否 1是</param>
		/// <returns></returns>
		public ActionResult WaitPickIndex(int isWaitPurchase = 0)
        {
			ViewBag.IsWaitPurchase = isWaitPurchase;
            return View();
		}

		public ActionResult SearchWaitPick() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql((int)WarehouseOutboundStatus.待拣货);
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wob.ID DESC";
			data.From = @" warehouseOutbound wob LEFT JOIN shop ON wob.ShopID=shop.ID
			LEFT JOIN warehouseExpress we ON wob.ExpressID=we.ID";
			data.Select = @"wob.ID,wob.BillNo,shop.Name AS ShopName,wob.ErpOrderCode,IsApplyRefund,wob.IsPurchasePlan,wob.OutOrderCode,wob.CreateDate,wob.BuyName,
			we.Name As ExpressName,wob.ProductsNum,wob.ExpressID,wob.IsNeedInvoice,IFNULL(wob.BuyMessage,'') As BuyMessage,IFNULL(wob.SellerRemark,'') As SellerRemark,ExpectedDeliDate,wob.Status,wob.IsHang,wob.HangRemark,wob.ReturnCount";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutboundList> list = BaseService<WarehouseOutboundList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 驳回

		public ActionResult Reject(string ids) {
			ViewBag.IDs = ids;
			return View();
		}

		public ActionResult SetReject(string ids, string rejectRemark) {
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.Reject(userCode, userName, warehouseCode, idList, rejectRemark);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 安排打印

		public ActionResult ArrangePrint(string ids) {
			ViewBag.OutboundIDs = ids;
			return View();
		}

		/// <summary>
		/// 获取安排打印出库单的快递个数
		/// </summary>
		/// <param name="ids">出库单主键ID</param>
		/// <returns></returns>
		public ActionResult GetExpressCount(string ids) {
			string userCode = FormsAuth.GetUserCode();
			int expressCount = OutboundManager.GetExpressCount(userCode, ids);
			var result = new { expressCount = expressCount };
			return JsonDate(result);
		}

		/// <summary>
		/// 保存打印批次，并将出库单状态改为待打印
		/// </summary>
		/// <param name="ids">出库单主键ID</param>
		/// <param name="deliveryID">实际发货快递ID</param>
		/// <returns></returns>
		public ActionResult SavePrintBatch(string ids, int deliveryID = 0) {
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.SavePrintBatch(userCode, userName, warehouseCode, idList, deliveryID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 按照待采出库单生成采购计划单

		public ActionResult Generation(string ids) {
			List<int> idList = new List<int>();
			if (ids == "") {
				//如果没有选择出库单，则默认当前查询条件的所有出库单
				string whereSql = " WHERE " + GetWhereSql((int)WarehouseOutboundStatus.待拣货);
				string sqlStr = "SELECT wob.ID FROM warehouseOutbound wob" + whereSql;
				DataTable dt = WarehouseOutboundService.GetDataTable(sqlStr);
				foreach (DataRow dr in dt.Rows) {
					idList.Add(ZConvert.StrToInt(dr["ID"]));
				}
			}
			else {
				idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			}
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.Generation(userCode, userName, warehouseCode, idList);
			return JsonDate(resultInfo);
		}

		public ActionResult GetPurchasedCount(string ids) {
			int purchasedCount = 0;
			string sqlStr = string.Empty;
			Object[] objects = new Object[1];
			if (ids == "") {
				//如果没有选择出库单，则默认当前查询条件的所有出库单
				string whereSql = " WHERE wob.IsPurchasePlan=1 AND " + GetWhereSql((int)WarehouseOutboundStatus.待拣货);
				sqlStr = "SELECT Count(0) FROM warehouseOutbound wob" + whereSql;
			}
			else {
				objects[0] = ids;
				sqlStr = "SELECT Count(0) FROM warehouseOutbound wob WHERE FIND_IN_SET(ID,@0) AND wob.IsPurchasePlan=1";
			}
			purchasedCount = Db.GetInstance().Context().Sql(sqlStr, objects).QuerySingle<int>();
			var result = new { purchasedCount = purchasedCount };
			return JsonDate(result);
		}

		#endregion

		#region 拆分出库单

		public ActionResult Split(int id) {
			ViewBag.ID = id;
			return View();
		}

		public ActionResult SplitOutbound(int id) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.SplitOutbound(userCode, userName, warehouseCode, id);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 查询库存

		public ActionResult SearchStock(int id) {
			//List<int> idList = new List<int>();
			//idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = OutboundManager.SearchStock(userCode, id);
			return JsonDate(resultInfo);
		}

		#endregion

		#endregion

		#region 待打印出库单

		#region WaitPrintIndex

		public ActionResult WaitPrintIndex() {
			return View();
		}

		public ActionResult SearchWaitPrint() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql((int)WarehouseOutboundStatus.待打印);
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wob.ID DESC";
			data.From = @" warehouseOutbound wob LEFT JOIN shop ON wob.ShopID=shop.ID
			LEFT JOIN warehouseExpress we ON wob.ExpressID=we.ID
			LEFT JOIN warehouseExpress we1 ON wob.DeliveryExpressID=we1.ID";
			data.Select = @"wob.ID,wob.BillNo,shop.Name AS ShopName,wob.ErpOrderCode,wob.IsApplyRefund,wob.OutOrderCode,wob.CreateDate,wob.BuyName,
			we.Name As ExpressName,we1.Name As DeliveryExpressName,wob.ProductsNum,wob.ExpressID,wob.DeliveryExpressID,wob.WaybillNo,wob.PrintBatchCode,wob.ArrangePrintDate,wob.PickPrintDate,wob.DeliveryPrintDate,wob.ExpressPrintDate,wob.IsNeedInvoice,IFNULL(wob.BuyMessage,'') As BuyMessage,IFNULL(wob.SellerRemark,'') As SellerRemark,wob.Status,wob.ReturnCount";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			//选择了批次，则按照批次表ID排序
			string printBatchCode = ZConvert.ToString(Request["printBatchCode"]);
			if (printBatchCode != "" && printBatchCode != "0") {
				data.From += " LEFT JOIN warehouseOutboundPrintBatch wobpb ON wob.ID=wobpb.OutboundID";
				data.OrderBy = "wobpb.ID ASC,wob.ID DESC";
			}
			int total = 0;
			List<WarehouseOutboundList> list = BaseService<WarehouseOutboundList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 返回待拣货

		public ActionResult ReturnWaitPick(string ids) {
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.ReturnWaitPick(userCode, userName, warehouseCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取运单号 热敏

		public ActionResult GetWaybillNo(string printBatchCode) {
			BaseResult resultInfo = new BaseResult();
			return JsonDate(resultInfo);
		}

		#endregion

		#region 打印拣货单

		public ActionResult PrintPickDialog(int id, string printBatchCode) {
			ViewBag.ID = id;
			ViewBag.PrintBatchCode = printBatchCode;
			return View();
		}

		#endregion

		#region 打印发货单

		public ActionResult PrintDeliveryDialog(int id, string printBatchCode) {
			ViewBag.ID = id;
			ViewBag.PrintBatchCode = printBatchCode;
			return View();
		}

		#endregion

		#region 打印快递单

		public ActionResult PrintExpressDialog(int id, string printBatchCode) {
			ViewBag.ID = id;
			ViewBag.PrintBatchCode = printBatchCode;
			int deliveryExpressID = 0;
			if (id > 0) {
				WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(id);
				if (outbound != null) {
					deliveryExpressID = outbound.DeliveryExpressID;
				}
			}
			else {
				string warehouseCode = FormsAuth.GetWarehouseCode();
				deliveryExpressID = WarehouseOutboundService.GetDeliveryExpressID(warehouseCode, printBatchCode);
			}
			ViewBag.DeliveryExpressID = deliveryExpressID;
			return View();
		}

		#endregion

		#region 保存运单号 针式

		public ActionResult SetWaybillNo(string printBatchCode, int deliveryExpressID) {
			WarehouseExpress warehouseExpress = WarehouseExpressService.GetQuerySingleByID(deliveryExpressID);
			if (warehouseExpress == null) warehouseExpress = new WarehouseExpress();
			Logistics logistics = LogisticsService.GetLogistics(warehouseExpress.LogisticsID);
			if (logistics == null) logistics = new Logistics();
			Object[] objects = new Object[3];
			objects[0] = FormsAuth.GetWarehouseCode();
			objects[1] = printBatchCode;
			objects[2] = WarehouseOutboundStatus.待打印;
			string sqlStr = @"SELECT wob.ID,wob.BillNo,shop.Name AS ShopName,wob.ProductsNum,wob.BuyName,wob.BuyAddr,wob.BuyTel,wob.BuyMtel,wob.BuyPostCode,wob.WaybillNo,wob.Status 
			FROM warehouseOutbound wob LEFT JOIN shop ON wob.ShopID=shop.ID
			LEFT JOIN warehouseOutboundPrintBatch wobpb ON wob.ID=wobpb.OutboundID
			WHERE wob.WarehouseCode=@0 AND wob.BillType=" + (int)BillType.XSC + @" AND wob.PrintBatchCode=@1 AND wob.Status=@2 ORDER BY wobpb.ID ASC,wob.ID DESC";
			List<WarehouseOutboundList> outboundList = BaseService<WarehouseOutboundList>.GetQueryMany(sqlStr, null, objects);
			ViewBag.OutboundList = outboundList;
			ViewBag.PrintBatchCode = printBatchCode;
			ViewBag.ExpressName = warehouseExpress.Name;
			ViewBag.LogisticsCode = logistics.Code;
			ViewBag.DeliveryExpressID = deliveryExpressID;
			return View();
		}

		public ActionResult SaveWaybillNo(string ids, string waybillNos, int deliveryExpressID) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<int> idList=new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)));
			List<string> waybillNoList=new List<string>();
			waybillNoList.AddRange(waybillNos.Split(',').Select(waybillNo => ZConvert.ToString(waybillNo)));
			BaseResult resultInfo = OutboundManager.SaveWaybillNo(userCode, userName, warehouseCode, idList, waybillNoList, deliveryExpressID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 修改出库单运单号

		/// <summary>
		/// 修改出库单运单号
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <param name="deliveryExpressID">发货快递ID</param>
		/// <param name="waybillNo">运单号</param>
		/// <returns></returns>
		public ActionResult EditWaybillNo(int id, int deliveryExpressID, string waybillNo) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<int> idList = new List<int> { id };
			List<string> waybillNoList = new List<string> { waybillNo };
			BaseResult resultInfo = OutboundManager.SaveWaybillNo(userCode, userName, warehouseCode, idList, waybillNoList, deliveryExpressID);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 打印完毕

		public ActionResult PrintFinish(int id, string printBatchCode) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.PrintFinish(userCode, userName, warehouseCode, id, printBatchCode);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 更换发货快递

		public ActionResult ChangeDeliveryExpress(string printBatchCode, int deliveryExpressID) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.ChangeDeliveryExpress(userCode, userName, warehouseCode, printBatchCode, deliveryExpressID);
			return JsonDate(resultInfo);
		}

		#endregion

		#endregion

		#region 待发货出库单

		#region WaitDeliveryIndex

		public ActionResult WaitDeliveryIndex() {
			return View();
		}

		public ActionResult SearchWaitDelivery() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql((int)WarehouseOutboundStatus.待发货);
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wob.ID DESC";
			data.From = @" warehouseOutbound wob LEFT JOIN shop ON wob.ShopID=shop.ID
			LEFT JOIN warehouseExpress we ON wob.ExpressID=we.ID
			LEFT JOIN warehouseExpress we1 ON wob.DeliveryExpressID=we1.ID";
			data.Select = @"wob.ID,wob.BillNo,shop.Name AS ShopName,wob.ErpOrderCode,wob.IsApplyRefund,wob.OutOrderCode,wob.CreateDate,wob.BuyName,
			we.Name As ExpressName,we1.Name As DeliveryExpressName,wob.ProductsNum,wob.ExpressID,wob.DeliveryExpressID,wob.WaybillNo,wob.PrintBatchCode,wob.IsScanCheck,wob.TotalWeight,wob.PickPrintDate,wob.DeliveryPrintDate,wob.ExpressPrintDate,wob.IsNeedInvoice,IFNULL(wob.BuyMessage,'') As BuyMessage,IFNULL(wob.SellerRemark,'') As SellerRemark,wob.Status,wob.IsHang,wob.HangRemark,wob.ReturnCount";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			//选择了批次，则按照批次表ID排序
			string printBatchCode = ZConvert.ToString(Request["printBatchCode"]);
			if (printBatchCode != "" && printBatchCode != "0") {
				data.From += " LEFT JOIN warehouseOutboundPrintBatch wobpb ON wob.ID=wobpb.OutboundID";
				data.OrderBy = "wobpb.ID ASC,wob.ID DESC";
			}
			int total = 0;
			List<WarehouseOutboundList> list = BaseService<WarehouseOutboundList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 返回待打印

		public ActionResult ReturnWaitPrint(string ids) {
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.ReturnWaitPrint(userCode, userName, warehouseCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 确认发货

		public ActionResult ConfirmDelivery(string ids) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			int successCount = 0;
			int errorCount = 0;
			BaseResult resultInfo = OutboundManager.Delivery(userCode, userName, warehouseCode, idList, out successCount, out errorCount);
			var result = new { result = resultInfo.result, message = resultInfo.message, successCount = successCount, errorCount = errorCount };
			return JsonDate(result);
		}

		#endregion

		#region 扫描发货

		public ActionResult ScanDeliveryDialog() {
			return View();
		}

		public ActionResult ScanDelivery(int id) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<int> idList = new List<int>();
			idList.Add(id);
			int successCount = 0;
			int errorCount = 0;
			int deliveryMode = 1;
			BaseResult resultInfo = OutboundManager.Delivery(userCode, userName, warehouseCode, idList, out successCount, out errorCount, deliveryMode);
			var result = new { result = resultInfo.result, message = resultInfo.message, successCount = successCount, errorCount = errorCount };
			return JsonDate(result);
		}

		#endregion

		#region 出库单校验

		public ActionResult ScanCheckIndex(string billNo = "") {
			ViewBag.billNo = billNo;
			return View();
		}

		/// <summary>
		/// 根据出库单号或运单号查询出库单
		/// </summary>
		/// <param name="billNo">出库单号或运单号</param>
		/// <returns></returns>
		public ActionResult SearchOutbound(string billNo) {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundByBillNoOrWaybillNo(warehouseCode, billNo);
			return JsonDate(outboundList);
		}

		/// <summary>
		/// 查询出库单商品
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <returns></returns>
		public ActionResult SearchProducts(int id) {
			List<Orditem> orditemList = OrditemService.GetQueryManyByOutboundID(id);
			return JsonDate(orditemList);
		}

		/// <summary>
		/// 设置为已校验
		/// </summary>
		/// <param name="billNo">出库单号</param>
		/// <returns></returns>
		public ActionResult SetScanCheck(string billNo) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.SetScanCheck(userCode, userName, warehouseCode, billNo);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 出库单称重

		public ActionResult ScalageIndex(string billNo = "") {
			ViewBag.BillNo = billNo;
			return View();
		}

		/// <summary>
		/// 设置实际包裹重量
		/// </summary>
		/// <param name="billNo">出库单号</param>
		/// <param name="totalWeight">实际包裹重量</param>
		/// <returns></returns>
		public ActionResult SetTotalWeight(string billNo, decimal totalWeight) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.SetTotalWeight(userCode, userName, warehouseCode, billNo, totalWeight);
			return JsonDate(resultInfo);
		}

		#endregion

		#endregion

		#region 已发货出库单

		#region AlreadyDeliveryIndex

		public ActionResult AlreadyDeliveryIndex() {
			return View();
		}

		public ActionResult SearchAlreadyDelivery() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql((int)WarehouseOutboundStatus.已发货);
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wob.ID DESC";
			data.From = @" warehouseOutbound wob LEFT JOIN shop ON wob.ShopID=shop.ID
			LEFT JOIN warehouseExpress we ON wob.ExpressID=we.ID
			LEFT JOIN warehouseExpress we1 ON wob.DeliveryExpressID=we1.ID";
			data.Select = @"wob.ID,wob.BillNo,shop.Name AS ShopName,wob.ErpOrderCode,wob.OutOrderCode,wob.DeliveryDate,wob.BuyName,
			we.Name As ExpressName,we1.Name As DeliveryExpressName,wob.ProductsNum,wob.ExpressID,wob.DeliveryExpressID,wob.WaybillNo,wob.IsNeedInvoice,IFNULL(wob.BuyMessage,'') As BuyMessage,IFNULL(wob.SellerRemark,'') As SellerRemark,wob.Status,wob.IsCod";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutboundList> list = BaseService<WarehouseOutboundList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#endregion

		#region 挂起、取消挂起

		public ActionResult Hang(int id) {
			ViewBag.ID = id;
			return View();
		}

		public ActionResult SetHang(int type, int id, string hangRemark = "") {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutboundManager.Hang(userCode, userName, warehouseCode, type, id, hangRemark);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 根据出库单状态获取页面搜索条件

		/// <summary>
		/// 根据出库单状态获取页面搜索条件
		/// </summary>
		/// <param name="outboundStatus">出库单状态 枚举 待拣货、待打印、待发货</param>
		/// <returns></returns>
		private string GetWhereSql(int outboundStatus) {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int expressID = ZConvert.StrToInt(Request["expressID"]);
			int isNeedInvoice = ZConvert.StrToInt(Request["isNeedInvoice"], -1);
			int messageRemark = ZConvert.StrToInt(Request["messageRemark"], -1);
			//0非待采出库单 1待采出库单 默认显示非待采出库单
			int isWaitPurchase = ZConvert.StrToInt(Request["isWaitPurchase"], -1);
			//0非申请退款出库单 1申请退款出库单 默认显示 全部
			int isApplyRefund = ZConvert.StrToInt(Request["isApplyRefund"], -1);
			int isCod = ZConvert.StrToInt(Request["isCod"], -1);
			int codStatus = ZConvert.StrToInt(Request["codStatus"], -1);
			int shopID = ZConvert.StrToInt(Request["shopID"]);
			string printBatchCode = ZConvert.ToString(Request["printBatchCode"]);
			bool containNormal = ZConvert.StrToInt(Request["containNormal"]) == 1;
			bool containApplyRefund = ZConvert.StrToInt(Request["containApplyRefund"]) == 1;
			bool containHang = ZConvert.StrToInt(Request["containHang"]) == 1;
			string whereSql = string.Format("wob.WarehouseCode = '{0}' AND wob.BillType={1}", FormsAuth.GetWarehouseCode(), (int)BillType.XSC);
			if (outboundStatus > -1) {
				whereSql += string.Format(" AND wob.Status = {0}", outboundStatus);
			}
			if (isWaitPurchase > -1) {
				whereSql += string.Format(" AND wob.IsWaitPurchase = {0}", isWaitPurchase);
			}
			if (keyWord != "") {
				switch (keyWordType) {
					case "订单编号":
						whereSql += string.Format(" AND wob.ErpOrderCode like '%{0}%'", keyWord);
						break;
					case "外部订单号":
						whereSql += string.Format(" AND wob.OutOrderCode like '%{0}%'", keyWord);
						break;
					case "出库单号":
						whereSql += string.Format(" AND wob.BillNo like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" AND wob.ID IN (SELECT OutboundID FROM ord_item Where ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" AND wob.ID IN (SELECT OutboundID FROM ord_item Where ProductsNo like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" AND wob.ID IN (SELECT OutboundID FROM ord_item Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
					case "收件人姓名":
						whereSql += string.Format(" AND wob.BuyName like '%{0}%'", keyWord);
						break;
					case "收件人手机":
						whereSql += string.Format(" AND wob.BuyMtel like '%{0}%'", keyWord);
						break;
					case "运单号":
						whereSql += string.Format(" AND wob.WaybillNo like '%{0}%'", keyWord);
						break;
				}
			}
			if (expressID > 0) {
				if (outboundStatus == (int)WarehouseOutboundStatus.待拣货) {
					whereSql += " AND wob.ExpressID=" + expressID;
				}
				else {
					whereSql += " AND wob.DeliveryExpressID=" + expressID;
				}
			}
			if (isNeedInvoice > -1) {
				whereSql += " AND wob.IsNeedInvoice=" + isNeedInvoice;
			}
			if (messageRemark == 0) {
				//没有留言和备注
				whereSql += " AND IFNULL(wob.BuyMessage,'')='' AND IFNULL(wob.SellerRemark,'')=''";
			}
			else if (messageRemark == 1) {
				//有留言或备注
				whereSql += " AND (IFNULL(wob.BuyMessage,'')<>'' OR IFNULL(wob.SellerRemark,'')<>'')";
			}
			if (isCod > -1) {
				whereSql += " AND wob.IsCod=" + isCod;
			}
			if (codStatus > -1) {
				whereSql += " AND wob.CodStatus=" + isCod;
			}
			if (shopID > 0) {
				whereSql += " AND wob.ShopID=" + shopID;
			}

			if (isApplyRefund > -1) {
				whereSql += " AND wob.IsApplyRefund=" + isApplyRefund;
			}
			else {
				if ((containNormal || containApplyRefund || containHang) || !(containNormal && containApplyRefund && containHang)) {
					string strSql = string.Empty;
					if (containNormal) {
						strSql += " (wob.IsApplyRefund=0 AND wob.IsHang=0)";
					}
					if (containApplyRefund) {
						if (strSql != string.Empty) strSql += " OR";
						strSql += " wob.IsApplyRefund=1";
					}
					if (containHang) {
						if (strSql != string.Empty) strSql += " OR";
						strSql += " wob.IsHang=1";
					}
					if (strSql != string.Empty) whereSql += " AND (" + strSql + ")";
				}
			}

			if (printBatchCode.Trim() != "" && printBatchCode.Trim() != "0") {
				whereSql += " AND wob.PrintBatchCode='" + printBatchCode + "'";
			}
			return whereSql;
		}

		#endregion

		#region 根据出库单状态获取申请退款出库单笔数

		/// <summary>
		/// 根据出库单状态获取申请退款出库单笔数
		/// </summary>
		/// <param name="status">出库单状态 枚举</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="isWaitPurchase">是否待采出库单 0否 1是</param>
		/// <returns></returns>
		public ActionResult GetApplyRefundCount(int status, string printBatchCode = "", int isWaitPurchase = 0) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			int appRefundCount = OutboundManager.GetApplyRefundCount(userCode, warehouseCode, status, printBatchCode, isWaitPurchase);
			var result = new { appRefundCount = appRefundCount };
			return JsonDate(result);
		}

		#endregion

		#region 根据打印批次和出库单状态获取已经打印出库单笔数

		/// <summary>
		/// 根据打印批次和出库单状态获取已经打印出库单笔数
		/// </summary>
		/// <param name="status">出库单状态 枚举</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="printTemplateType">单据枚举类型 0发货单 1拣货单 2快递单</param>
		/// <returns></returns>
		public ActionResult GetPrintCount(int status, string printBatchCode, int printTemplateType) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			int printCount = OutboundManager.GetPrintCount(userCode, warehouseCode, status, printBatchCode, printTemplateType);
			var result = new { printCount = printCount };
			return JsonDate(result);
		}

		#endregion

		#region 判断出库单是否申请退款

		public ActionResult CheckIsApplyRefund(int id) {
			int isApplyRefund = 0;
			WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(id);
			if (outbound != null) {
				isApplyRefund = outbound.IsApplyRefund;
			}
			var result = new { isApplyRefund = isApplyRefund };
			return JsonDate(result);
		}

		#endregion

		#region 判断出库单是否已经打印

		/// <summary>
		/// 判断出库单是否已经打印
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <param name="printTemplateType">单据枚举类型 0发货单 1拣货单 2快递单</param>
		/// <returns></returns>
		public ActionResult CheckIsPrint(int id, int printTemplateType) {
			int isPrint = 0;
			WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByID(id);
			if (outbound != null) {
				switch (printTemplateType) {
					case (int)PrintTemplateType.发货单:
						isPrint = outbound.DeliveryPrintDate.ToString("yyyy-MM-dd") == "0001-01-01" ? 0 : 1;
						break;
					case (int)PrintTemplateType.拣货单:
						isPrint = outbound.PickPrintDate.ToString("yyyy-MM-dd") == "0001-01-01" ? 0 : 1;
						break;
					case 2:
						isPrint = outbound.ExpressPrintDate.ToString("yyyy-MM-dd") == "0001-01-01" ? 0 : 1;
						break;
				}
			}
			var result = new { isPrint = isPrint };
			return JsonDate(result);
		}

		#endregion

		#region 根据状态获取打印批次下拉列表

		public ActionResult GetPrintBatch(int status) {
			CListItem cListItem = new CListItem();
			cListItem.Text = "请选择";
			cListItem.Value = "0";
			List<CListItem> treeList = new List<CListItem>();
			treeList.Add(cListItem);
			Object[] objects = new Object[2];
			objects[0] = FormsAuth.GetWarehouseCode();
			objects[1] = status;
			string sqlStr = @"SELECT DISTINCT PrintBatchCode AS Value ,PrintBatchCode AS Text FROM warehouseOutbound WHERE WarehouseCode=@0 AND Status=@1 AND PrintBatchCode IS NOT NULL ORDER BY PrintBatchCode DESC";
			List<CListItem> objlist = BaseService<CListItem>.GetQueryMany(sqlStr, null, objects);
			treeList.AddRange(objlist);
			return JsonDate(treeList);
		}

		#endregion

		#region 根据快递ID获取物流代码

		public ActionResult GetLogisticsCode(int expressID) {
			string logisticsCode = string.Empty;
			WarehouseExpress warehouseExpress = WarehouseExpressService.GetQuerySingleByID(expressID);
			if (warehouseExpress != null) {
				Logistics logistics = LogisticsService.GetLogistics(warehouseExpress.LogisticsID);
				if (logistics != null) {
					logisticsCode = logistics.Code;
				}
			}
			return Content(logisticsCode);
		}

		#endregion

		#region 根据打印模版类型获取打印模版默认打印机

		/// <summary>
		/// 根据打印模版类型获取打印模版默认打印机
		/// </summary>
		/// <param name="deliveryExpressID">发货快递ID 打印模版类型是快递单时必传，否则传0</param>
		/// <param name="printTemplateID">打印模版ID 打印模版类型是拣货单和发货单必传，否则传0</param>
		/// <param name="printTemplateType">打印模版类型 0发货单 1拣货单 2快递单</param>
		/// <returns></returns>
		public ActionResult GetDefaultPrinterName(int deliveryExpressID, int printTemplateID, int printTemplateType) {
			string printerName = string.Empty;
			if (printTemplateType == 2) {
				WarehouseExpress express = WarehouseExpressService.GetQuerySingleByID(deliveryExpressID);
				if (express != null) {
					printerName = express.PrinterName;
				}
			}
			else {
				WarehousePrintTemplate printTemplate = WarehousePrintTemplateService.GetQuerySingleByID(printTemplateID);
				if (printTemplate != null) {
					printerName = printTemplate.PrinterName;
				}
			}
			return Content(printerName);
		}

		#endregion

		#region 根据打印模版类型获取打印数据

		/// <summary>
		/// 根据打印模版类型获取打印数据
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <param name="printBatchCode">打印批次</param>
		/// <param name="deliveryExpressID">发货快递ID 快递单</param>
		/// <param name="printTemplateID">打印模版ID 拣货单和发货单</param>
		/// <param name="printTemplateType">打印模版类型 0发货单 1拣货单 2快递单</param>
		/// <returns></returns>
		public ActionResult GetPrintData(int id, string printBatchCode, int deliveryExpressID, int printTemplateID, int printTemplateType) {
			ContentResult result = new ContentResult();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
			string templateXmlUrl = string.Empty;
			string logisticsCode = string.Empty;
			//是否打印商品明细
			int isPrintPro = 0;
			string printProField = string.Empty;
			List<string> fieldList = new List<string>();
			string printProFieldWidth = string.Empty;
			List<string> fieldWidthList = new List<string>();
			WarehousePrintTemplate printTemplateInfo = new WarehousePrintTemplate();
			WarehouseExpress expressInfo = new WarehouseExpress();
			#region 获取打印模版 未设置模版读取默认模版
			if (printTemplateType == (int)PrintTemplateType.发货单 || printTemplateType == (int)PrintTemplateType.拣货单) {
				if (printTemplateID > 0) printTemplateInfo = WarehousePrintTemplateService.GetQuerySingleByID(printTemplateID);
				if (printTemplateInfo == null) printTemplateInfo = new WarehousePrintTemplate();
				if (ZConvert.ToString(printTemplateInfo.TemplateContent) == string.Empty || ZConvert.ToString(printTemplateInfo.PrintProField) == string.Empty) {
					templateXmlUrl = "../../Xml/PrintTemplate/PrintTemplate.xml";
					decimal width = 0;
					decimal height = 0;
					decimal secondPageOffset = 0;
					string templateContent = PrintTemplateManager.GetPrintTemplate(printTemplateType, templateXmlUrl, out width, out height, out secondPageOffset, out printProField, out printProFieldWidth);
					if (ZConvert.ToString(printTemplateInfo.TemplateContent) == string.Empty) {
						//模版未设置全部读默认
						printTemplateInfo.Width = width;
						printTemplateInfo.Height = height;
						printTemplateInfo.SecondPageOffset = secondPageOffset;
						printTemplateInfo.TemplateContent = templateContent;
						//默认打印商品明细
						printTemplateInfo.IsPrintPro = 1;
						printTemplateInfo.PrintProField = printProField;
						printTemplateInfo.PrintProFieldWidth = printProFieldWidth;
					}
					else if (ZConvert.ToString(printTemplateInfo.PrintProField) == string.Empty) {
						//模版有设置但商品明细字段未设置，只读商品明细字段
						printTemplateInfo.PrintProField = printProField;
						printTemplateInfo.PrintProFieldWidth = printProFieldWidth;
					}
				}
				isPrintPro = printTemplateInfo.IsPrintPro;
				printProField = printTemplateInfo.PrintProField;
				printProFieldWidth = printTemplateInfo.PrintProFieldWidth;
				fieldList.AddRange(printProField.Split(',').Select(field => ZConvert.ToString(field)).Where(field => field.Length > 0));
				fieldWidthList.AddRange(printProFieldWidth.Split(',').Select(fieldWidth => ZConvert.ToString(fieldWidth)).Where(fieldWidth => fieldWidth.Length > 0));
				if (printTemplateInfo.Height == 0) {
					//自适应高度，不能使用 ADD_PRINT_TABLE，要使用 ADD_PRINT_HTM
					printTemplateInfo.TemplateContent = printTemplateInfo.TemplateContent.Replace("ADD_PRINT_TABLE", "ADD_PRINT_HTM");
				}
			}
			if (printTemplateType == (int)PrintTemplateType.快递单) {
				expressInfo = WarehouseExpressService.GetQuerySingleByID(deliveryExpressID);
				if (expressInfo != null) {
					Logistics logistics = LogisticsService.GetLogistics(expressInfo.LogisticsID);
					if (logistics != null) {
						logisticsCode = logistics.Code;
						if (ZConvert.ToString(expressInfo.TemplateContent) != string.Empty) {
							isPrintPro = expressInfo.IsPrintPro;
							printProField = expressInfo.PrintProField;
						}
						else {
							//未设置模版读取默认模版
							templateXmlUrl = "../../Xml/PrintTemplate/Express.xml";
							decimal width = 0;
							decimal height = 0;
							expressInfo.TemplateContent = ExpressManager.GetExpressPrintTemplate(logisticsCode, expressInfo.PrinterType, templateXmlUrl, out width, out height);
							expressInfo.Width = width;
							expressInfo.Height = height;
						}
					}
				}
				fieldList.AddRange(printProField.Split(',').Select(field => ZConvert.ToString(field)).Where(field => field.Length > 0));
			}

			#endregion

			#region 根据出库单ID或批次号获取出库单列表
			List<WarehouseOutboundList> outboundList = new List<WarehouseOutboundList>();
			if (id > 0) {
				WarehouseOutboundList warehouseOutboundList = WarehouseOutboundService.GetPrintWarehouseOutbound(warehouseCode, id);
				outboundList.Add(warehouseOutboundList);
			}
			else {
				outboundList = WarehouseOutboundService.GetPrintWarehouseOutboundList(warehouseCode, printBatchCode, (int)WarehouseOutboundStatus.待打印);
			}
			#endregion
			switch (printTemplateType) {
				case (int)PrintTemplateType.发货单:
					#region 获取发货单打印数据
					DeliveryOrderInfo deliveryOrderInfo = new DeliveryOrderInfo();
					deliveryOrderInfo.CurrentDate = currentDate;
					deliveryOrderInfo.CurrentTime = currentTime;
					//打印模版信息
					deliveryOrderInfo.PrintTemplateInfo = printTemplateInfo;
					//出库单列表
					deliveryOrderInfo.OutboundList = outboundList;
					if (outboundList != null) {
						foreach (var outbound in outboundList) {
							outbound.UncollectedeAmountChinese = ZConvert.ToChinese(outbound.UncollectedeAmount);
							if (isPrintPro == 1) {
								outbound.ProList = OutboundManager.GetPrintDeliveryPro(warehouseCode, fieldList, fieldWidthList, outbound.ID);
							}
						}
					}
					result = JsonDate(deliveryOrderInfo);
					#endregion
					break;
				case (int)PrintTemplateType.拣货单:
					#region 获取拣货单打印数据
					PickOrderInfo pickOrderInfo = new PickOrderInfo();
					pickOrderInfo.CurrentDate = currentDate;
					pickOrderInfo.CurrentTime = currentTime;
					if (outboundList != null) {
						List<int> idList = new List<int>();
						pickOrderInfo.PrintBatchCode = outboundList[0].PrintBatchCode;
						foreach (var outbound in outboundList){
							pickOrderInfo.ProductsNum += outbound.ProductsNum;
							idList.Add(outbound.ID);
						}
						pickOrderInfo.outboundIDs = idList.ToArray();
						if (isPrintPro == 1) {
							pickOrderInfo.ProList = OutboundManager.GetPrintPickPro(warehouseCode, fieldList, fieldWidthList, idList);
						}
					}
					//打印模版信息
					pickOrderInfo.PrintTemplateInfo = printTemplateInfo;
					result = JsonDate(pickOrderInfo);
					#endregion
					break;
				case (int)PrintTemplateType.快递单:
					#region 获取快递单打印数据
					ExpressOrderInfo expressOrderInfo = new ExpressOrderInfo();
					expressOrderInfo.CurrentTime = currentTime;
					expressOrderInfo.CurrentDate = currentDate;
					expressOrderInfo.LogisticsCode = logisticsCode;
					//打印模版信息
					expressOrderInfo.ExpressInfo = expressInfo;
					//寄件人信息
					expressOrderInfo.SendInfo = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode);
					if (expressOrderInfo.SendInfo == null) expressOrderInfo.SendInfo = new WarehouseConfig();
					//出库单列表
					expressOrderInfo.OutboundList = outboundList;
					
					foreach (var outbound in outboundList) {
						outbound.UncollectedeAmountChinese = ZConvert.ToChinese(outbound.UncollectedeAmount);
						if (isPrintPro == 1) {
							outbound.ProList = OutboundManager.GetPrintExpressPro(warehouseCode, fieldList, outbound.ID);
						}
					}
					result = JsonDate(expressOrderInfo);
					#endregion
					break;
			}
			return result;
		}

		#endregion

		#region 根据打印模版类型设置打印时间

		public ActionResult SetPrintDate(string ids, int printTemplateType) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = OutboundManager.SetPrintDate(userCode, userName, warehouseCode, idList, printTemplateType);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 获取货到付款状态下拉列表

		public ActionResult GetCodStatusJson() {
			DataTable dt = EnumManager<CodStatus>.GetDataTable(-1);
			return JsonDate(dt);
		}

		#endregion

		#region 出库单详情

		public ActionResult Details(string billNo) {
			ViewBag.billNo = billNo;
			return View();
		}

		#region 获取出库单基本信息和拣货信息

		public ActionResult GetOutboundInfo(string billNo) {
			string expressName = string.Empty;
			string statusName = string.Empty;
			string warehouseCode = FormsAuth.GetWarehouseCode();
			WarehouseOutbound outbound = WarehouseOutboundService.GetQuerySingleByBillNo(warehouseCode, billNo);
			List<WarehouseOutboundPickItemWebInfo> pickItemList = new List<WarehouseOutboundPickItemWebInfo>();
			if (outbound != null) {
				statusName = OutboundManager.GetStatusName(outbound.Status);
				int expressID = outbound.DeliveryExpressID > 0 ? outbound.DeliveryExpressID : outbound.ExpressID;
				WarehouseExpress warehouseExpress = WarehouseExpressService.GetQuerySingleByID(expressID);
				if (warehouseExpress != null) expressName = warehouseExpress.Name;
				Object[] objects = new Object[1];
				objects[0] = billNo;
				string sqlStr = @"SELECT wobpi.OrdItemID,wobpi.ProductsCode,wobpi.ProductsName,wobpi.ProductsSkuSaleprop,
				wobpi.ProductsSkuCode,wobpi.Num,wobpi.LocationCode,wobpi.LocationID,wobpi.ProductsBatchCode,
				p.Weight as ProductsWeight,IFNULL(u.Text,'－') as ProductsUnit FROM warehouseOutboundPickItem wobpi
				LEFT JOIN products p on wobpi.ProductsID=p.ID
				LEFT JOIN (SELECT Code,Text FROM sys_code WHERE CodeType='001') as u on p.MeasurementUnitID=u.Code
				WHERE wobpi.OutboundBillNo=@0 ORDER BY wobpi.ID DESC";
				pickItemList = BaseService<WarehouseOutboundPickItemWebInfo>.GetQueryMany(sqlStr, null, objects);
			}
			var result = new { expressName = expressName, statusName = statusName, outbound = outbound, pickItemList = pickItemList };
			return JsonDate(result);
		}

		#endregion

		#region 获取出库单日志

		public ActionResult GetOutboundLog(string billNo) {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			Object[] objects = new Object[2];
			objects[0] = warehouseCode;
			objects[1] = billNo;
			string sqlStr = "SELECT CreateDate,Message,UserName,UserCode FROM ord_log WHERE WarehouseCode=@0 AND BillNo=@1 ORDER BY ID DESC";
			List<Ordlog> ordLogList = BaseService<Ordlog>.GetQueryMany(sqlStr, null, objects);
			var result = new { ordLogList = ordLogList };
			return JsonDate(result);
		}

		#endregion



		#endregion
	}
}

