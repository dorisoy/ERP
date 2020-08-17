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

namespace PaiXie.Erp.Areas.Finance
{
    public class FinancialAuditController :BaseController
    {
		#region 财务审核
		//
		// GET: /Finance/FinancialAudit/Index

		public ActionResult Index() {
			return View();
		} 
		#endregion

		#region 入库单明细
		//
		// GET: /Finance/FinancialAudit/InStock
		public ActionResult InStock(string BillNo, int outInStockID) {
			ViewBag.BillNo = BillNo;
			ViewBag.OutInStockID = outInStockID;
			WarehouseOutInStock WarehouseOutInStock=WarehouseOutInStockService.GetModelByBillNo(BillNo);
			if (WarehouseOutInStock != null) {
				ViewBag.SourceNo = WarehouseOutInStock.SourceNo;//采购单号
				ViewBag.IsAuditPrice = WarehouseOutInStock.IsAuditPrice;//是否财审
			}
			ViewBag.ProductsNum = WarehouseOutInStockItemService.GetSumProductsNum(BillNo);//入库数量
			ViewBag.Price = WarehouseOutInStockItemService.GetSumPrice(BillNo); // 入库总金额 												
			return View();
		}
		//   /Finance/FinancialAudit/GetTotalPrice
		public ContentResult GetTotalPrice(string BillNo) {
			string price= WarehouseOutInStockItemService.GetSumPrice(BillNo); // 入库总金额 												
			return Content(price);
		}
		
		#endregion

		#region 出库单明细
		// GET: /Finance/FinancialAudit/OutStock
		public ActionResult OutStock() {
			ViewBag.OutInStockID=Request["id"];
		    WarehouseOutInStock WarehouseOutInStock=	WarehouseOutInStockService.GetQuerySingleByID(ZConvert.StrToInt( Request["id"]));
			if (WarehouseOutInStock != null) {
				//出库单号
				ViewBag.billno = WarehouseOutInStock.BillNo;
			}
			//出库数量
			ViewBag.num = WarehouseOutInStockItemService.GetSumProductsNum(WarehouseOutInStock.BillNo);
        	decimal totalPrice = 0;
			List<WarehouseOutInStockItem> list = WarehouseOutInStockItemService.GetWarehouseOutInStockItemList(ZConvert.StrToInt(Request["id"],0));
           	for (int z = 0; z < list.Count(); z++) {
				WarehouseProductsBatch objWarehouseProductsBatch= WarehouseProductsBatchService.GetSingleWarehouseProductsBatch(list[z].WarehouseCode,ZConvert.StrToInt( list[z].ProductsSkuID,0), list[z].ProductsBatchCode);
            	decimal Price = objWarehouseProductsBatch != null ? objWarehouseProductsBatch.CostPrice : 0;
				totalPrice += Price*list[z].ProductsNum;
			}
			//出库金额
			ViewBag.totalPrice = totalPrice;
			return View();
		}
		#endregion

		#region 财务审核 入库单列表
		// GET: /Finance/FinancialAudit/search
		public ActionResult search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "id desc";
			data.From = @"warehouseOutInStock  a";
			data.Select = "	a.*,'' AS BillTypeName,0 AS OutInStockNum,'' AS StatusName";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseOutInStockList> list = BaseService<WarehouseOutInStockList>.GetQueryManyForPage(data, out total, null, null);
			//   构造成Json的格式传递
			for (int i = 0; i < list.Count(); i++) {
				//入库单类型
				list[i].BillTypeName = BillTypeConvert.GetBillTypeName(list[i].BillType);
				//入库数量  
				string cc = WarehouseOutInStockItemService.GetSumProductsNum(list[i].BillNo); 
				list[i].OutInStockNum = ZConvert.StrToInt(cc, 0);
				//状态名称
				list[i].StatusName = ((WarehouseOutInStockStatus)list[i].Status).ToString();
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string GetWhereSql() {
			string whereSql = "";			
			string type = Request["type"];
			if (type == "gridout" && !string.IsNullOrEmpty(type)) {
				whereSql = " a.BillType IN (" + (int)BillType.CGC + "," + (int)BillType.QTC + ")";				
			}
			else {
				 whereSql = " a.BillType IN (" + (int)BillType.CGR + "," + (int)BillType.QTR + ")";				
			}			   
              whereSql += "  and  a.STATUS IN (2,3)";
				//审核状态
				string status = Request["status"];
				if (status != "0" && !string.IsNullOrEmpty(status)) {
					whereSql += "  and  a.STATUS IN (" + status + ")";
				}
			//单据号
				string txtbillno = Request["txtbillno"];
				if (!string.IsNullOrEmpty(txtbillno)) {
					whereSql += "  and  a.BillNo ='" + txtbillno + "'";
				}
			//确认时间 开始
				string startDate = Request["startDate"];
				if (!string.IsNullOrEmpty(startDate)) {
					whereSql += "  and  a.ConfirmDate >='" + startDate + "'";
				}
		    	//确认时间 结束
	     		string endDate = Request["endDate"];
				if (!string.IsNullOrEmpty(endDate)) {
					whereSql += "  and  a.ConfirmDate <='" + endDate + "'";
				}		
		    	return whereSql;
		}
		#endregion

		#region 入库单 出库单 审核
		// GET: /Finance/FinancialAudit/OutInStockshenhe
		public ActionResult OutInStockshenhe(int id = 0) {
			BaseResult BaseResult = new BaseResult();
			BaseResult = FinancialManager.OutInStockshenhe(id);			
			return JsonDate(BaseResult);
		}
		#endregion

		#region  入库单审核 商品价格修改
		//   /Finance/FinancialAudit/UpdatePrice
		public ActionResult UpdatePrice(Storagelist obj) {
			BaseResult BaseResult = new BaseResult();
			try {
				string userCode = FormsAuth.GetUserCode();
				BaseResult = FinancialManager.UpdatePrice(userCode, obj);
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
			}
			return JsonDate(BaseResult);
		}
		#endregion

		#region 财务审核  盘点管理
		public ActionResult Searchpd() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "a.Status!=" + (int)StocktakingStatus.未确认;

			//审核状态
			string status = Request["status"];
			if (status != "0" && !string.IsNullOrEmpty(status)) {
				if ( "2"== status)
				    whereSql += "  and  a.STATUS IN (" + (int)StocktakingStatus.待审核 + ")";
				else
					whereSql += "  and  a.STATUS IN (" + (int)StocktakingStatus.已审核 + ")";
			}
			//单据号
			string txtbillno = Request["txtbillno"];
			if (!string.IsNullOrEmpty(txtbillno)) {
				whereSql += "  and  a.BillNo ='" + txtbillno + "'";
			}
			//确认时间 开始
			string startDate = Request["startDate"];
			if (!string.IsNullOrEmpty(startDate)) {
				whereSql += "  and  a.ConfirmDate >='" + startDate + "'";
			}
			//确认时间 结束
			string endDate = Request["endDate"];
			if (!string.IsNullOrEmpty(endDate)) {
				whereSql += "  and  a.ConfirmDate <='" + endDate + "'";
			}	
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = @"warehouseStocktaking a left join warehouse b on a.WarehouseCode=b.Code  left join sys_user c on a.UpdatePerson=c.Code  ";
			data.Select = "a.*,b.Name as WarehouseName,c.Name as UpdatePersonName ,'' as StatusName ";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseStocktakingList> list = BaseService <WarehouseStocktakingList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		
		#endregion

		#region 盘点 审核
		// GET: /Finance/FinancialAudit/PDshenhe
		public ActionResult PDshenhe(int id = 0) {
			BaseResult BaseResult = new BaseResult();
			BaseResult = FinancialManager.PDshenhe(id);			
			return JsonDate(BaseResult);
		}
		#endregion

		#region 盘点 审核  详情
	
		public ActionResult pd() {
			ViewBag.ID=Request["id"];
			WarehouseStocktaking WarehouseStocktaking=WarehouseStocktakingService.GetQuerySingleByID(ZConvert.StrToInt(Request["id"], 0));
			if (WarehouseStocktaking != null) {
				ViewBag.BillNo = WarehouseStocktaking.BillNo;
				ViewBag.LocationName = WarehouseStocktaking.LocationName;		
			}

			return View();
		}
		#endregion

		#region  财务审核 盘点记录列表
		/// <summary>
		/// 财务审核 盘点记录列表
		/// </summary>
		/// <returns></returns>
		public ActionResult SearchpdDetail() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = " StocktakingID =" + Request["id"];
			whereSql += GetWhereSqlstr();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "ID DESC";
			data.From = @"warehouseStocktakingItem a";
			data.Select = "a.*  ";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<WarehouseStocktakingItemList> list = BaseService<WarehouseStocktakingItemList>.GetQueryManyForPage(data, out total);
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}
		private string GetWhereSqlstr() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string PdType = ZConvert.ToString(Request["PdType"]);
			string whereSql = "";
			if (keyWord != "") {
				switch (keyWordType) {
					case "商品名称":
						whereSql += string.Format(" and a.ProductsName like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and a.ProductsCode like '%{0}%'", keyWord);
						break;
					case "商品SKU码":
						whereSql += string.Format(" and a.ProductsSkuCode like '%{0}%'", keyWord);
						break;
					case "库位编码":
						whereSql += string.Format(" and a.LocationCode like '%{0}%'", keyWord);
						break;
					case "批次":
						whereSql += string.Format(" and a.ProductsBatchCode like '%{0}%'", keyWord);
						break;
				}
			}

			if (PdType != "") {
				switch (PdType) {
					case "盘亏":
						whereSql += string.Format(" and (a.PdNum - a.ZkNum)<0");
						break;
					case "盘盈":
						whereSql += string.Format(" and (a.PdNum - a.ZkNum)>0");
						break;
					case "相同":
						whereSql += string.Format(" and (a.PdNum - a.ZkNum)=0");
						break;
				
				}
			}

			


			return whereSql;
		}

		#endregion

		#region  盘点 审核 商品价格修改
		//   /Finance/FinancialAudit/UpdatePdPrice
		public ActionResult UpdatePdPrice(WarehouseStocktakingItemList obj) {
			BaseResult BaseResult = new BaseResult();
			try {
				string userCode = FormsAuth.GetUserCode();
				BaseResult = FinancialManager.UpdatePdPrice(userCode, obj);
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
			}
			return JsonDate(BaseResult);
		}
		#endregion
		
    }
}
