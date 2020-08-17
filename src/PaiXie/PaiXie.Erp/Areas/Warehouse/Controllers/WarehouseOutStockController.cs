using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Api.Bll;
using FluentData;
namespace PaiXie.Erp.Areas.Warehouse
{
	public class WarehouseOutStockController : BaseController
    {
		#region Index
		
		public ActionResult index() {
			return View();
		}

		#endregion

		#region 出库单列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "wois.ID desc";
			data.From = @"warehouseOutInStock  wois";
			data.Select = "	wois.ID,wois.BillNo,wois.BillType,wois.SourceNo,wois.CreateDate,wois.CreatePerson,wois.Status,wois.Remark,'' AS BillTypeName,0 AS OutInStockNum,'' AS StatusName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutInStockList> list = BaseService<WarehouseOutInStockList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			for (int i = 0; i < list.Count(); i++) {
				//出库单类型
				list[i].BillTypeName = BillTypeConvert.GetBillTypeName(list[i].BillType);
				//出库数量  
				string cc = WarehouseOutInStockItemService.Getobject("SELECT  SUM(ProductsNum) FROM warehouseOutInStockItem WHERE OutInStockID=" + list[i].ID);
				list[i].OutInStockNum = ZConvert.StrToInt(cc, 0);
				//状态名称
				list[i].StatusName = ((WarehouseOutInStockStatus)list[i].Status).ToString();
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string state = Request["state"];
			string whereSql = "wois.WarehouseCode = '" + FormsAuth.GetWarehouseCode() + "' and wois.BillType IN (" + (int)BillType.CGC + "," + (int)BillType.QTC + ")";
			if (keyWord != "") {
				switch (keyWordType) {
					case "出库单号":
						whereSql += string.Format(" AND wois.BillNo like '%{0}%'", keyWord);
						break;
					case "出库单备注":
						whereSql += string.Format(" AND wois.Remark like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format("  AND wois.ID in (SELECT OutInStockID FROM warehouseOutInStockItem WHERE ProductsCode like '%" + keyWord + "%')", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format("  AND wois.ID in (SELECT OutInStockID FROM warehouseOutInStockItem WHERE ProductsSkuCode like '%" + keyWord + "%')", keyWord);
						break;
				}
			}
			if (!string.IsNullOrEmpty(state)) {
				whereSql += " AND wois.STATUS IN (" + state.Substring(0, state.Length - 1) + ")";
			}
			return whereSql;
		}
		#endregion

		#region 添加出库单

		/// <summary>
		/// 添加出库单
		/// </summary>
		/// <returns></returns>
		public ActionResult Add() {
			return View();
		}

		#endregion

		#region 检查入库单号是否存在且已提交
		
		/// <summary>
		/// 检查入库单号是否存在且已提交
		/// </summary>
		/// <param name="billNo">入库单号</param>
		/// <returns></returns>
		public ActionResult CheckSourceNo(string billNo) {
			BaseResult resultInfo = new BaseResult();
			int sourceID = 0;
			WarehouseOutInStock obj = WarehouseOutInStockService.GetQuerySingleByBillNo(billNo);
			if (obj == null) {
				resultInfo.result = 0;
				resultInfo.message = "入库单号不存在！";
			}
			else {
				if (obj.BillType != (int)BillType.CGR) {
					resultInfo.result = 0;
					resultInfo.message = "该入库单不是采购入库！";
				}
				else {
					if (obj.Status == (int)WarehouseOutInStockStatus.未提交) {
						resultInfo.result = 0;
						resultInfo.message = "采购入库单号未提交！";
					}
					else {
						sourceID = obj.ID;
					}
				}
			}
			var result = new { result = resultInfo.result, message = resultInfo.message, sourceID = sourceID };
			return JsonDate(result);
		}

		#endregion

		#region 保存出库单

		[HttpPost]
		public ActionResult SaveOutStock(WarehouseOutInStock obj) {
			string userCode = FormsAuth.GetUserCode();
			string warehouseCode = FormsAuth.GetWarehouseCode();
			BaseResult resultInfo = OutInStockManager.SaveOutInStock(userCode, warehouseCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除出库单

		public ActionResult DelOutStock(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = OutInStockManager.DelOutInStock(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 提交出库单

		/// <summary>
		/// 提交出库单
		/// </summary>
		/// <param name="id">出库单主键ID</param>
		/// <returns></returns>
		public ActionResult submit(int id) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = OutInStockManager.SubmitOutStock(userCode, id);
			return JsonDate(resultInfo);
		}

		#endregion
	}
}
