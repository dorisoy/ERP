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

namespace PaiXie.Erp.Areas.Warehouse
{
    public class MoveLocationController : BaseController {
		
		#region Index

		public ActionResult Index()
        {
            return View();
		}

		#endregion

		#region 移位单列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wml.ID desc";
			data.From = @"warehouseMoveLocation wml";
			data.Select = "	wml.ID, wml.BillNo, wml.Remark,wml.CreateDate,wml.CreatePerson,wml.Status";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseMoveLocationList> list = BaseService<WarehouseMoveLocationList>.GetQueryManyForPage(data, out total, null, null);
			//   构造成Json的格式传递
			for (int i = 0; i < list.Count(); i++) {
				list[i].Num = WarehouseMoveLocationItemService.GetNumByMoveLocationID(list[i].ID);
				//状态名称
				list[i].StatusName = ((MoveLocationStatus)list[i].Status).ToString();
				Sysuser user = SysuserService.GetSysuserbyUserCode(list[i].CreatePerson);
				if (user != null) {
					//创建人
					list[i].CreUserName = user.Name;
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = "wml.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "'";
			if (keyWord != "") {
				switch (keyWordType) {
					case "移位单号":
						whereSql += "  AND wml.BillNo like '%" + keyWord + "%'";
						break;
					case "商品编码":
						whereSql += "  AND wml.ID in (SELECT MoveLocationID FROM warehouseMoveLocationItem WHERE ProductsCode like '%" + keyWord + "%')";
						break;
					case "商品SKU码":
						whereSql += "  AND wml.ID in (SELECT MoveLocationID FROM warehouseMoveLocationItem WHERE ProductsSkuCode like '%" + keyWord + "%')";
						break;
					case "移位说明":
						whereSql += "  AND wml.Remark like '%" + keyWord + "%'";
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 添加移位单

		/// <summary>
		/// 添加移位单
		/// </summary>
		/// <returns></returns>
		public ActionResult Add() {
			return View();
		}

		#endregion

		#region 保存

		public ActionResult Save(string remark = "") {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = MoveLocationManager.AddMoveLocation(userCode, warehouseCode, remark);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = MoveLocationManager.DelMoveLocation(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 确认

		public ActionResult Confirm(int id) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = MoveLocationManager.Confirm(userCode, id);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
