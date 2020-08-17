using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Order.Controllers {
	public class WaitAuditController : BaseController {

		//
		// GET: /Order/PendingAudit/
		public ActionResult Index() {
			return View();
		}

		/// <summary>
		/// 订单列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = "ord_base";
			data.Select = "ID,ShopID,OutOrderCode,ErpOrderCode,Created,BuyName,ProductsNum,ReceivableAmount,LogisticsID,IsCod,IsNeedInvoice,BuyMessage,SellerRemark,OrderStatus,CreateType,CreateDate,IsReject";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdbaseInfoList> list = BaseService<OrdbaseInfoList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				item.ShopName = ShopService.GetSingleShop(item.ShopID).Name;
				item.LogisticsName = LogisticsService.GetLogistics(item.LogisticsID).Name;
				List<Ordremark> ordremarkList = OrdremarkService.GetManyOrdremark(item.ErpOrderCode);
				if (ordremarkList.Count > 0) {
					item.IsSysRemark = 1;
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		/// <summary>
		/// 获取查询条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int shopID = ZConvert.StrToInt(Request["shopID"]);
			int isCod = ZConvert.StrToInt(Request["isCod"]);
			int isNeedInvoice = ZConvert.StrToInt(Request["isNeedInvoice"]);
			int isRemark = ZConvert.StrToInt(Request["isRemark"]);
			bool isNormal = ZConvert.StrToInt(Request["isNormal"]) == 1;
			bool isRefund = ZConvert.StrToInt(Request["isRefund"]) == 1;
			bool isReject = ZConvert.StrToInt(Request["isReject"]) == 1;
			bool isHang = ZConvert.StrToInt(Request["isHang"]) == 1;

			string whereSql = string.Format("OrderStatus in ({0},{1},{2})", (int)OrdBaseStatus.待审核, (int)OrdBaseStatus.发货中, (int)OrdBaseStatus.部分发货);
			whereSql += " and EXISTS(SELECT 1  FROM ord_item WHERE OrdbaseID = ord_base.ID AND OutboundID = 0)";

			if (keyWord != "") {
				switch (keyWordType) {
					case "订单编号":
						whereSql += string.Format(" and ErpOrderCode like '%{0}%'", keyWord);
						break;
					case "外部订单号":
						whereSql += string.Format(" and OutOrderCode like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_item Where OrdbaseID = ord_base.ID and ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_item Where OrdbaseID = ord_base.ID and ProductsNo like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and EXISTS(SELECT 1 FROM ord_item Where OrdbaseID = ord_base.ID and ProductsSkuCode like '%{0}%')", keyWord);
						break;
					case "收件人姓名":
						whereSql += string.Format(" and BuyName like '%{0}%'", keyWord);
						break;
					case "收件人手机":
						whereSql += string.Format(" and BuyMtel like '%{0}%'", keyWord);
						break;
				}
			}


			if (shopID > 0) {
				whereSql += string.Format(" and ShopID = {0}", shopID);
			}
			if (isCod > 0) {
				whereSql += string.Format(" and IsCod = {0}", isCod);
			}
			if (isNeedInvoice > 0) {
				whereSql += string.Format(" and IsNeedInvoice = {0}", isNeedInvoice);
			}
			if (isRemark == 0) {
				whereSql += string.Format(" and (IFNULL(BuyMessage,'') = '' && IFNULL(SellerRemark,'') = '')");
			}
			if (isRemark == 1) {
				whereSql += string.Format(" and (BuyMessage <> '' or SellerRemark <> '')");
			}

			string strFilter= "";
			if ((isNormal || isRefund || isReject || isHang) && !(isNormal && isRefund && isReject && isHang)) {
				if (isNormal) {
					strFilter += "(IsApplyRefund = 0 and IsReject = 0 and IsHang = 0)";
				}
				if (isRefund) {
					strFilter += (strFilter == "" ? "" : " or ") + "IsApplyRefund = 1";
				}
				if (isReject) {
					strFilter += (strFilter == "" ? "" : " or ") + "IsReject = 1";
				}
				if (isHang) {
					strFilter += (strFilter == "" ? "" : " or ") + "IsHang = 1";
				}
			}
			if (strFilter != "") {
				whereSql += string.Format(" and ({0})", strFilter);
			}
			return whereSql;
		}

		/// <summary>
		/// 取消订单
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <returns></returns>
		public ActionResult CancelOrder(string erpOrderCode) {
			BaseResult resultInfo = OrdbaseManager.Cancel(erpOrderCode, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			return JsonDate(resultInfo);
		}

		/// <summary>
		/// 批量分配仓库
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <returns></returns>
		public ActionResult BatchDistributionWarehouse(string ids) {
			BaseResult resultInfo = new BaseResult();
			List<int> ordbaseIDList = new List<int>();
			ordbaseIDList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			int successCount = 0;
			foreach (var ordouterID in ordbaseIDList) {
				resultInfo = OrdbaseManager.DistributionWarehouse(ordouterID, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
				if (resultInfo.result == 1) {
					successCount++;
				}
			}
			if (successCount == 0) {
				resultInfo.result = 0;
				resultInfo.message = "分配仓库失败！";
			}
			else {
				resultInfo.result = 1;
				resultInfo.message = "成功分配" + successCount + "单！";
			}
			var result = new { result = resultInfo.result, message = resultInfo.message };
			return JsonDate(result);
		}
	}
}
