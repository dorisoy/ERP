using PaiXie.Core;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Api.Bll;
using System.Data;
namespace PaiXie.Erp.Areas.Warehouse
{
    public class OrderRefundController : BaseController {
		
		#region Index

		public ActionResult Index()
        {
            return View();
		}

		#endregion

		#region 售后列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "oRe.ID DESC";
			data.From = @"ord_refund oRe LEFT JOIN shop on oRe.ShopID=shop.ID
			LEFT JOIN warehouse w ON oRe.WarehouseCode=w.Code";
			data.Select = @"oRe.ID, oRe.BillNo, oRe.ErpOrderCode, oRe.RefundType, oRe.Duty, oRe.DutyOther, oRe.RefundAmount, oRe.RefundFreight, oRe.CreateDate, oRe.Status, 
			shop.Name AS ShopName, w.Name AS WarehouseName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdRefundList> list = BaseService<OrdRefundList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.StatusName = OrdrefundManager.GetStatusName(item.Status);
				item.DutyName = OrdrefundManager.GetDutyName(item.Duty, item.DutyOther);
				item.RefundTypeName = OrdrefundManager.GetRefundTypeName(item.RefundType);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#region 获取页面搜索条件

		/// <summary>
		/// 获取页面搜索条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int shopID = ZConvert.StrToInt(Request["shopID"]);
			int refundType = ZConvert.StrToInt(Request["refundType"], -1);
			int duty = ZConvert.StrToInt(Request["duty"]);
			string statusStr = ZConvert.ToString(Request["statusStr"]);
			string whereSql = string.Format("oRe.WarehouseCode = '{0}'", FormsAuth.GetWarehouseCode());
			if (statusStr != "") {
				whereSql += string.Format(" AND oRe.Status in ({0})", statusStr);
			}
			else {
				whereSql += "AND 1<>1";
			}
			if (keyWord != "") {
				switch (keyWordType) {
					case "订单编号":
						whereSql += string.Format(" AND oRe.ErpOrderCode like '%{0}%'", keyWord);
						break;
					case "外部订单号":
						whereSql += string.Format(" AND oRe.OutOrderCode like '%{0}%'", keyWord);
						break;
					case "售后单号":
						whereSql += string.Format(" AND oRe.BillNo like '%{0}%'", keyWord);
						break;
					case "退回运单号":
						whereSql += string.Format(" AND oRe.WaybillNo like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" AND oRe.ID IN (SELECT OrdRefundID FROM ord_refundItem Where ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" AND oRe.ID IN (SELECT OrdRefundID FROM ord_refundItem Where ProductsNo like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" AND oRe.ID IN (SELECT OrdRefundID FROM ord_refundItem Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
					case "收件人姓名":
						whereSql += string.Format(" AND oRe.BuyName like '%{0}%'", keyWord);
						break;
					case "收件人手机":
						whereSql += string.Format(" AND oRe.BuyMtel like '%{0}%'", keyWord);
						break;
				}
			}
			if (shopID > 0) {
				whereSql += " AND oRe.ShopID=" + shopID;
			}
			if (refundType > -1) {
				whereSql += " AND oRe.RefundType=" + refundType;
			}
			if (duty > 0) {
				whereSql += " AND oRe.Duty=" + duty;
			}
			return whereSql;
		}

		#endregion

		#endregion

		#region 添加售后

		public ActionResult Add() {
			return View();
		}

		/// <summary>
		/// 根据订单号或出库单号查询出库单
		/// </summary>
		/// <param name="billNo">订单号或出库单号</param>
		/// <returns></returns>
		public ActionResult SearchOutbound(string billNo) {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundByBillNoOrErpOrderCode(warehouseCode, billNo);
			WarehouseConfig warehouseConfig = new WarehouseConfig();
			if (outboundList.Count == 1) {
				warehouseConfig = WarehouseConfigService.GetQuerySingleByWarehouseCode(warehouseCode);
			}
			var result = new { outboundList = outboundList, warehouseConfig = warehouseConfig };
			return JsonDate(result);
		}

		/// <summary>
		/// 查询出库单拣货明细 
		/// </summary>
		/// <param name="id">出库单ID</param>
		/// <returns></returns>
		public ActionResult SearchPickItem(int id) {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			DataTable pickItemDt = WarehouseOutboundPickItemService.GetBatchInfoByOutboundID(warehouseCode, id);
			return JsonDate(pickItemDt);
		}

		#endregion

		#region 获取售后责任方下拉列表

		/// <summary>
		/// 获取售后责任方下拉列表
		/// </summary>
		/// <param name="hasSelectedDefault">是否选中默认值</param>
		/// <returns></returns>
		public ActionResult GetOrderRefundDutyJson(int hasSelectedDefault = 0) {
			DataTable dt = EnumManager<OrdRefundDuty>.GetDataTable(-1);
			if (hasSelectedDefault == 1) {
				dt.Rows.Remove(dt.Rows[0]);
			}
			return JsonDate(dt);
		}

		#endregion

		#region 获取售后类型下拉列表

		/// <summary>
		/// 获取售后类型下拉列表
		/// </summary>
		/// <param name="hasSelectedDefault">是否选中默认值</param>
		/// <returns></returns>
		public ActionResult GetOrderRefundTypeJson(int hasSelectedDefault = 0) {
			DataTable dt = EnumManager<OrdRefundType>.GetDataTable(-1);
			if (hasSelectedDefault == 1) {
				dt.Rows.Remove(dt.Rows[0]);
			}
			return JsonDate(dt);
		}

		#endregion

		#region 保存
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Save(OrdRefundWebInfo objWebInfo) {
			string userCode = FormsAuth.GetUserCode();
			string userName = FormsAuth.GetUserName();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OrdrefundManager.Save(userCode, userName, warehouseCode, objWebInfo);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 查看售后单详情

		public ActionResult Details(string billNo) {
			ViewBag.billNo = billNo;
			return View();
		}

		#region 获取售后单基本信息和商品信息

		public ActionResult GetOrdRefundInfo(string billNo) {
			string statusName = string.Empty;
			string dutyName = string.Empty;
			string refundTypeName = string.Empty;
			string warehouseCode = FormsAuth.GetWarehouseCode();
			Ordrefund ordRefund = OrdrefundService.GetQuerySingleByBillNo(warehouseCode, billNo);
			List<OrdrefundItem> ordRefundItemList = new List<OrdrefundItem>();
			if (ordRefund != null) {
				statusName = OrdrefundManager.GetStatusName(ordRefund.Status);
				dutyName = OrdrefundManager.GetDutyName(ordRefund.Duty, ordRefund.DutyOther);
				refundTypeName = OrdrefundManager.GetRefundTypeName(ordRefund.RefundType);
				ordRefundItemList = OrdrefundItemService.GetOrdRefundItemList(warehouseCode, ordRefund.ID);
			}
			var result = new { statusName = statusName, dutyName = dutyName, refundTypeName = refundTypeName, ordRefund = ordRefund, ordRefundItemList = ordRefundItemList };
			return JsonDate(result);
		}

		#endregion

		#endregion

		#region 设置售后物流信息

		public ActionResult Setlogistics(int ordRefundID) {
			ViewBag.OrdRefundID = ordRefundID;
			return View();
		}

		[HttpPost]
		public ActionResult Savelogistics(int ordRefundID, string expressCompany, string waybillNo, decimal returnFreight) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = OrdrefundManager.Savelogistics(userCode, ordRefundID, expressCompany, waybillNo, returnFreight);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 确认收货

		public ActionResult ConfirmReceiveIndex(int ordRefundID) {
			string warehouseCode = FormsAuth.GetWarehouseCode();
			Ordrefund ordRefund = OrdrefundService.GetQuerySingleByID(ordRefundID);
			List<OrdrefundItem> ordRefundItemList = new List<OrdrefundItem>();
			if (ordRefund != null) {
				ordRefundItemList = OrdrefundItemService.GetOrdRefundItemList(warehouseCode, ordRefund.ID);
			}
			ViewBag.Ordrefund = ordRefund;
			ViewBag.OrdRefundItemList = ordRefundItemList;
			return View();
		}

		public ActionResult ConfirmReceive(OrdRefundReceiveInfo objReceiveInfo) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = OrdrefundManager.ConfirmReceive(userCode, objReceiveInfo);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 取消售后

		public ActionResult Cancel(string billNo) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OrdrefundManager.Cancel(userCode, warehouseCode, billNo);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
