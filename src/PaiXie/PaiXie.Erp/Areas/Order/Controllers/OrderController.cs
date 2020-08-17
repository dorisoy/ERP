using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Order.Controllers
{
	public class OrderController : BaseController
    {
        //
        // GET: /Order/OrderList/

        public ActionResult Index()
        {
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
			data.Select = "ID,ShopID,OutOrderCode,ErpOrderCode,Created,BuyName,ProductsNum,ReceivableAmount,LogisticsID,IsCod,IsNeedInvoice,BuyMessage,SellerRemark,OrderStatus,CreateType,CreateDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdbaseInfoList> list = BaseService<OrdbaseInfoList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				if (item.ShopID > 0) {
					item.ShopName = ShopService.GetSingleShop(item.ShopID).Name;
				}
				if (item.LogisticsID > 0) {
					item.LogisticsName = LogisticsService.GetLogistics(item.LogisticsID).Name;
				}
				item.StrOrderStatus = Enum.GetName(typeof(OrdBaseStatus), item.OrderStatus);
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
			int orderStatus = ZConvert.StrToInt(Request["orderStatus"], -1);

			string whereSql = string.Format("OrderStatus != {0}", (int)OrdBaseStatus.未生成);

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
			if (isRemark > 0) {
				whereSql += string.Format(" and (BuyMessage <> '' or SellerRemark <> '')");
			}
			if (orderStatus > -1) {
				whereSql += string.Format(" and OrderStatus = {0}", orderStatus);
			}

			return whereSql;
		}

		/// <summary>
		/// 获取订单状态下拉列表
		/// </summary>
		/// <returns></returns>
		public ActionResult GetOrdBaseStatusJson() {
			DataTable dt = EnumManager<OrdBaseStatus>.GetDataTable(-1);
			int index = 0;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
			 if (dt.Rows[i]["Name"].ToString() == "未生成") {
					index = i;
				 break;
				}
			}
			dt.Rows.RemoveAt(index);	
			return JsonDate(dt);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns></returns>
		public ActionResult Delete(int id) {
			BaseResult resultInfo = OrdbaseManager.Delte(id, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
			if (resultInfo.result == 1) {
				resultInfo.message = "删除成功！";
			}
			return JsonDate(resultInfo);
		}
    }
}
