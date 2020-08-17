#region using
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System.Collections.Generic;
using System.Web.Mvc;
using PaiXie.Api.Bll;
using PaiXie.Core;
using System.Data;
using System.Threading;
using System.Linq;
using System; 
#endregion

namespace PaiXie.Erp.Areas.Shop {
	public class ShopProductsController : BaseController {

		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 商品类型下拉列表
		
		public ActionResult ProductsStatusList(int shopID) {
			Data.Shop shopInfo = ShopService.GetSingleShop(shopID);
			DataTable dt = new DataTable();
			DataColumn textCol = new DataColumn("TEXT", typeof(string));
			DataColumn valueCol = new DataColumn("VALUE", typeof(string));
			dt.Columns.Add(textCol);
			dt.Columns.Add(valueCol);
			IDictionary<string, string> dic = new Dictionary<string, string>();
			DataRow newDr = dt.NewRow();
			newDr["TEXT"] = "请选择商品类型";
			newDr["VALUE"] = "0";
			dt.Rows.Add(newDr);
			int PlatformType = -1;
			if (shopInfo != null) {
				DataRow newDr1 = dt.NewRow();
				newDr1["TEXT"] = "销售中商品";
				newDr1["VALUE"] = "1";
				dt.Rows.Add(newDr1);
				PlatformType = shopInfo.PlatformType;

				if (PlatformType == (int)ThirdApi.微小店) {
					DataRow newDr2 = dt.NewRow();
					newDr2["TEXT"] = "仓库中商品(除无货下架外)";
					newDr2["VALUE"] = "2";
					DataRow newDr3 = dt.NewRow();
					newDr3["TEXT"] = "仓库中商品(缺货)";
					newDr3["VALUE"] = "3";
					dt.Rows.Add(newDr2);
					dt.Rows.Add(newDr3);
				}
				else {
					DataRow newDr2 = dt.NewRow();
					newDr2["TEXT"] = "仓库中商品";
					newDr2["VALUE"] = "2";
					dt.Rows.Add(newDr2);
				}
			}
			return JsonDate(dt);
		}

		#endregion

		#region 下载网店商品

		public ActionResult Down(int status) {
			//检查是否有任务正在进行中，有的话要显示进度
			ViewBag.Status = status;
			return View();
		}

		/// <summary>
		/// 根据店铺ID和商品类型下载商品
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="productsStatus">1：销售中 2：仓库中</param>
		/// <returns></returns>
		public int DownProducts(int shopID, int productsStatus) {
			DownProductsParam param = new DownProductsParam();
			param.TaskID = Guid.NewGuid().ToString();
			param.ShopID = shopID;
			param.ProductsStatus = productsStatus;
			param.UserCode = FormsAuth.GetUserCode();
			ThreadPool.QueueUserWorkItem(new WaitCallback(thread), param);
			return 1;
		}

		private void thread(object obj) {
			DownProductsParam param = obj as DownProductsParam;
			ShopProductsManager.DownLoad(param);
		}

		#endregion

		#region 获取商品下载进度

		public ActionResult GetProcess(int shopID) {
			ShopTask shopTask = ShopTaskService.GetSingleShopTask(shopID, (int)ShopTaskType.下载商品);
			if (shopTask == null) {
				shopTask = new ShopTask();
			}
			//   构造成Json的格式传递
			var data = new { TotalCount = shopTask.TotalCount, FinshCount = shopTask.FinshCount };
			return JsonDate(data);
		}

		#endregion

		#region 将网店商品导入系统

		/// <summary>
		/// 导入商品
		/// </summary>
		/// <param name="ids">商品ID字符串多个以半角逗号分隔</param>
		/// <returns></returns>
		public ActionResult ImportProducts(string ids = "") {
			if (ids == "") {
				//如果没有选择商品，则默认导入当前查询条件的所有商品
				List<int> idList = new List<int>();
				string whereSql = " WHERE " + GetWhereSql();
				string sqlStr = "SELECT sp.ID FROM shopProducts sp" + whereSql;
				DataTable dt = ShopProductsService.GetDataTable(sqlStr);
				foreach (DataRow dr in dt.Rows) {
					idList.Add(ZConvert.StrToInt(dr["ID"]));
				}
				ids = string.Join(",", idList.ToArray());
			}
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = ShopProductsManager.Import(userCode, ids);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除商品

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = ShopProductsManager.Del(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 店铺商品列表

		/// <summary>
		/// 店铺商品列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "sp.ID DESC";
			data.From = @"shopProducts sp left join shop on sp.ShopID=shop.ID";
			data.Select = "sp.ID, sp.OuterId, sp.ImgUrl, sp.ProTitle, shop.Name as ShopName, sp.Price, IFNULL(sp.ErrorMessage,'') AS ErrorMessage, sp.ProductsStatus, sp.CreateDate";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ShopProductsList> list = BaseService<ShopProductsList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 获取搜索条件

		/// <summary>
		/// 获取搜索条件
		/// </summary>
		/// <returns></returns>
		private string GetWhereSql() {
			int shopID = ZConvert.StrToInt(Request["shopID"]);
			int productsStatus = ZConvert.StrToInt(Request["productsStatus"]);
			//导入成功的不再显示
			string whereSql = "sp.ProductsID=0";
			if (shopID > 0) {
				whereSql += string.Format(" and sp.ShopID={0}", shopID);
			}
			if (productsStatus > 0) {
				whereSql += string.Format(" and sp.ProductsStatus={0}", productsStatus);
			}
			return whereSql;
		}

		#endregion

		#region 店铺商品列表  库存更新

		/// <summary>
		/// 店铺商品列表 库存更新
		/// </summary>
		/// <returns></returns>
		public ActionResult ShopStockUpdateSearch(string shopid) {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = " 1=1 ";
			if (!string.IsNullOrEmpty(shopid)) {
				whereSql += string.Format(" and sp.ShopID={0}", shopid);
			}
			else {
				List<ShopProductsupdateList> listnull = new List<ShopProductsupdateList>();
				var resultnull = new { total = 0, rows = listnull };
				return JsonDate(resultnull);
			}

			if (!string.IsNullOrEmpty(Request["OuterId"])) {
				whereSql += string.Format(" and sp.OuterId={0}", Request["OuterId"].ToString());
			}
			if (!string.IsNullOrEmpty(Request["ProNo"])) {
				whereSql += string.Format(" and sp.ProNo={0}", Request["ProNo"].ToString());
			}
				if (!string.IsNullOrEmpty(Request["ProSKU"])) {
				whereSql += string.Format(" and sp.ProductKucList like '%{0}%'","\"Sku_id\":\""+ Request["ProSKU"].ToString());
			}
		


			

			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "sp.ID DESC";
			data.From = @"shopUpdateproducts sp left join shop on sp.ShopID=shop.ID";
			data.Select = "sp.PlatformType, sp.ShopID, sp.ID, sp.OuterId, sp.ImgUrl, sp.ProTitle, shop.Name as ShopName, sp.Price, IFNULL(sp.ErrorMessage,'') AS ErrorMessage, sp.ProductsStatus, sp.CreateDate,'9999-01-01 00:00:00' as UpdateTime,0 as UpdateStatus,'' as ErrorMsg";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ShopProductsupdateList> list = BaseService<ShopProductsupdateList>.GetQueryManyForPage(data, out total);
			for (int i = 0; i < list.Count; i++) {

				ShopStockUpdate objShopStockUpdate = ShopStockUpdateService.GetQuerySingle("SELECT *  FROM shopStockUpdate WHERE PlatformType=" + list[i].PlatformType + " AND shopid=" + list[i].ShopID + " AND  productscode=" + list[i].OuterId);
			 if (objShopStockUpdate!=null)
			{
				list[i].UpdateTime = objShopStockUpdate.UpdateTime;
				 list[i].UpdateStatus = objShopStockUpdate.UpdateStatus==1?"更新成功":"更新失败";
				 list[i].ErrorMsg =objShopStockUpdate.UpdateStatus==1? "": objShopStockUpdate.ErrorMsg;
			 }
			
			

			}
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion
	}
}