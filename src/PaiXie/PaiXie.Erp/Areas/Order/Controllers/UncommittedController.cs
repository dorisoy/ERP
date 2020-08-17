using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Order.Controllers {
	public class UncommittedController : BaseController {
		//
		// GET: /Order/Uncommitted/

		public ActionResult Index() {
			int count = OrdbaseService.getUncommittedCount();
			if (count == 0) {
				return RedirectToAction("Index", "AddOrder");
			}
			return View();
		}

		/// <summary>
		/// 未提交订单列表
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
			data.Select = "ID,ErpOrderCode,ShopID,BuyName,BuyMtel,RealAmount,Freight,ReceivableAmount,ProductsNum,LogisticsID,CreateDate,CreatePerson";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<OrdbaseInfoList> list = BaseService<OrdbaseInfoList>.GetQueryManyForPage(data, out total);
			foreach (var item in list) {
				Data.Shop shop = ShopService.GetSingleShop(item.ShopID);
				if (shop != null)
					item.ShopName = shop.Name;
				Logistics logistics = LogisticsService.GetLogistics(item.LogisticsID);
				if(logistics!=null)
					item.LogisticsName = logistics.Name;

			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			int shopID = ZConvert.StrToInt(Request["shopID"]);

			string whereSql = string.Format("CreateType = {0} AND OrderStatus = {1}", (int)OrdCreateType.手动, (int)OrdBaseStatus.未生成);

			if (keyWord != "") {
				switch (keyWordType) {
					case "收件人姓名":
						whereSql += string.Format(" and BuyName like '%{0}%'", keyWord);
						break;
					case "订单编号":
						whereSql += string.Format(" and ErpOrderCode like '%{0}%'", keyWord);
						break;
					case "外部订单号":
						whereSql += string.Format(" and OutOrderCode like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and ID IN (SELECT OrdbaseID FROM ord_item Where ProductsCode like '%{0}%')", keyWord);
						break;
					case "商品货号":
						whereSql += string.Format(" and ID IN (SELECT OrdbaseID FROM ord_item Where ProductsNo like '%{0}%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and ID IN (SELECT OrdbaseID FROM ord_item Where ProductsSkuCode like '%{0}%')", keyWord);
						break;
				}
			}

			if (shopID > 0) {
				whereSql += string.Format(" and ShopID = {0}", shopID);
			}

			return whereSql;
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
