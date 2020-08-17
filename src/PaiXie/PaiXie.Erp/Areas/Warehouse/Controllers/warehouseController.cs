using FluentData;
using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PaiXie.Erp.Areas.Warehouse {
	public class WarehouseController : BaseController {
		//
		// GET: /Warehouse/Warehouse/
		public ActionResult Index() {
			return View();
		}

		#region 设置配送区域
		public ActionResult SetArea() {
			PaiXie.Data.Warehouse obj = WarehouseService.Getwarehouse(Request["id"].ToString());
			ViewBag.wname = obj.Name;  //仓库名称
			string lstr = "";
			string str = obj.Librand;
			if (!string.IsNullOrEmpty(str)) {
				string[] sArray = str.Split(',');
				foreach (string n in sArray) {
					string obj2 = "";
					Brand objBrand = BrandService.GetSingleBrand(ZConvert.StrToInt(n, 0));
					if (objBrand != null) obj2 = objBrand.Name;
					if (!string.IsNullOrEmpty(obj2)) {
						lstr += obj2 + "、";
					}
				}
				if (!string.IsNullOrEmpty(lstr))
					ViewBag.Librand = lstr.Substring(0, lstr.Length - 1);  //授权品牌
			}
			ViewBag.wid = Request["id"].ToString(); //仓库id
			return View();
		}

		#endregion

		#region  禁用  启用 仓库
		public ActionResult IsEnablewarehouse(string wid, string sf) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				PaiXie.Data.Warehouse objSysuser = WarehouseService.Getwarehouse(wid);
				if (objSysuser != null) {				
						if (objSysuser.IsEnable == 0) {
							result = WarehouseService.IsEnablewarehouse(ZConvert.StrToInt(wid), 1);
						}					
					else  //禁用						
						if (objSysuser.IsEnable == 1) {
							result = WarehouseService.IsEnablewarehouse(ZConvert.StrToInt(wid), 0);
						}					
				}
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "操作失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "禁用启用仓库", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);
		}

		#endregion

		#region 设置仓库配送区域

		public ActionResult Setwarea(string mids, string wid) {
			BaseResult BaseResult = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					WarehouseAreaMapService.DeleteWarehouseAreaMap(wid, context);
					string[] sArray = mids.Split(',');
					foreach (string i in sArray) {
						WarehouseAreaMap SysroleMenuMap = new WarehouseAreaMap();
						SysroleMenuMap.AreaID = ZConvert.StrToInt(i);
						SysroleMenuMap.WarehouseID = ZConvert.StrToInt(wid);
						int result = WarehouseAreaMapService.Add(SysroleMenuMap, context);
						if (result == 0) {
							BaseResult.result = -1;
							BaseResult.message = "操作失败";
							break;
						}
					}
					if (BaseResult.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "设置仓库配送区域", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);

		}
		#endregion

		#region 地区列表

		public ActionResult Gettreegrid(string wid = "") {
			string wcode = wid;
			EasyUITree EUItree = new EasyUITree();
			DataTable dt = SysareaService.GetDataTable();
			for (int i = 0; i < dt.Rows.Count; i++) {
				string menucode = dt.Rows[i]["ID"].ToString();
				int c = WarehouseAreaMapService.IswareaMap(menucode, wid);
				if (c > 0)
					dt.Rows[i]["attr"] = "1";
			}
			List<JsonTree> list = EUItree.initTree(dt);
			return JsonDate(list);
		}

		#endregion

		#region 仓库列表
		/// <summary>
		/// 仓库列表
		/// </summary>
		/// <returns></returns>
		public ActionResult search() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "";
			Object[] objects = new Object[1];
			string Code = Request["Code"];
			if (!string.IsNullOrEmpty(Code)) {
				whereSql += "a.CODE LIKE @0";
				objects[0] = "%" + Code + "%";
			}
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @"warehouse  a
	LEFT JOIN  sys_user b ON a.CreatePerson=b.Code	
	LEFT JOIN  sys_user c ON a.UpdatePerson=c.Code";
			data.Select = "	a.*,b.Name AS cname,c.Name AS uname ";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<PaiXie.Data.WarehouseInfo> list = BaseService<PaiXie.Data.WarehouseInfo>.GetQueryManyForPage(data, out total, null, objects);
			//   构造成Json的格式传递
			//授权品牌
			for (int i = 0; i < list.Count(); i++) {
				string lstr = "";
				string str = list[i].Librand;
				if (!string.IsNullOrEmpty(str)) {
					string[] sArray = str.Split(',');
					foreach (string n in sArray) {
						Brand Brand= BrandService.GetSingleBrand(ZConvert.StrToInt(n));
						string obj =Brand!=null? Brand.Name:"";
						if (!string.IsNullOrEmpty(obj)) {
							lstr += obj + "、";
						}
					}
					if (!string.IsNullOrEmpty(lstr))
						list[i].part1 = lstr.Substring(0, lstr.Length - 1);
				}
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 用户编辑

		public ActionResult Edit(string id) {
			ViewBag.Syscodelist = BrandService.GetBrand();
			PaiXie.Data.Warehouse objSysuser = new PaiXie.Data.Warehouse();
			if (id != "0") objSysuser = WarehouseService.Getwarehouse(id);
			ViewBag.Sysuser = objSysuser;
			return View();
		}

		#endregion

		#region 保存
		[HttpPost]
		public ActionResult Save(PaiXie.Data.Warehouse obj, string pwd) {
			PlanLog.WriteLog(pwd,"tt");
			BaseResult BaseResult = new BaseResult();
			BaseResult = WarehouseManager.WarehouseSave(obj,pwd);
			return JsonDate(BaseResult);
		}
		#endregion

		#region 删除

		public ActionResult Delete(string id) {
			BaseResult BaseResult = new BaseResult();
			try {
				int result = WarehouseService.Deletewarehouse(id);
				if (result == 0) {
					BaseResult.result = -1;
					BaseResult.message = "删除失败";
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "删除仓库", FormsAuth.GetUserCode());
			}

			return JsonDate(BaseResult);

		}

		#endregion

		#region 检查代码唯一性

		public ActionResult CheckCode(string Code, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
				if (WarehouseService.Getwarehousecount(ID, Code) > 0) {
					BaseResult.result = -1;
				}
			}
			else {
				if (WarehouseService.Getwarehousecount(Code) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}
		#endregion

		#region 省市区

		/// <summary>
		/// 获取省市区下拉列表
		/// </summary>
		/// <param name="grade">0省 1市 2区</param>
		/// <param name="pid">父级地区ID</param>
		/// <returns></returns>
		public ActionResult GetAreaJson(int grade=0, int pid = 0) {
			string sqlStr = "";
			CListItem cListItem = new CListItem();
			string areaName = "";
			switch (grade) {
				case 0:
					areaName = "省";
					sqlStr = "SELECT ID AS VALUE ,Name AS  TEXT   FROM  sys_area WHERE ParentID=0";
					break;
				case 1:
					areaName = "市";
					if (pid > 0) {
						sqlStr = "SELECT ID AS VALUE ,Name AS  TEXT   FROM  sys_area WHERE ParentID=" + pid;
					}
					break;
				case 2:
					areaName = "区/县";
					if (pid > 0) {
						sqlStr = "SELECT ID AS VALUE ,Name AS  TEXT   FROM  sys_area WHERE ParentID=" + pid;
					}
					break;
			}
			cListItem.Text = "请选择" + areaName;
			cListItem.Value = "0";
			List<CListItem> treeList = new List<CListItem>();
			treeList.Add(cListItem);
			if (!string.IsNullOrEmpty(sqlStr)) {
				List<CListItem> objlist = Db.GetInstance().Context().Sql(sqlStr).QueryMany<CListItem>();
				treeList.AddRange(objlist);
			}
			return JsonDate(treeList);
		}
		#endregion

		#region 仓库设置区域

		public ActionResult SelectArea(string id = "") {


			PaiXie.Data.Warehouse obj = WarehouseService.Getwarehouse(id);
			ViewBag.wname = obj.Name;  //仓库名称
			string lstr = "";
			string str = obj.Librand;
			if (!string.IsNullOrEmpty(str)) {
				string[] sArray = str.Split(',');
				foreach (string n in sArray) {
					string obj2 = "";
					Brand objBrand = BrandService.GetSingleBrand(ZConvert.StrToInt(n, 0));
					if (objBrand != null) obj2 = objBrand.Name;
					if (!string.IsNullOrEmpty(obj2)) {
						lstr += obj2 + "、";
					}
				}
				if (!string.IsNullOrEmpty(lstr))
					ViewBag.Librand = lstr.Substring(0, lstr.Length - 1);  //授权品牌
			}
			ViewBag.wid = id; //仓库id





			string checkedIDs = "";
			List<WarehouseAreaMap> _selectList = WarehouseAreaMapService.GetWarehouseAreaMapList(ZConvert.StrToInt(id));
			foreach (var item in _selectList) {
				checkedIDs += item.AreaID + ",";
			}
			if (!string.IsNullOrEmpty(checkedIDs)) {
				checkedIDs = checkedIDs.Substring(0, checkedIDs.Length - 1);
			}
			//	WarehouseExpressPrice warehouseExpressPrice = new WarehouseExpressPrice();
			List<int> checkedAreaIDList = new List<int>();
			//if (warehouseExpressPriceID > 0) {
			//	warehouseExpressPrice = WarehouseExpressPriceService.GetQuerySingleByID(warehouseExpressPriceID);
			//	checkedAreaIDList.AddRange(warehouseExpressPrice.SysAreaIDs.Split(',').Select(areaID => ZConvert.StrToInt(areaID)));
			//}
			//else 
			//	if (rowIndex > 0) {
			checkedAreaIDList.AddRange(checkedIDs.Split(',').Select(areaID => ZConvert.StrToInt(areaID)));
			//}
			SelectAreaWebInfo selectAreaWebInfo = new SelectAreaWebInfo();
			selectAreaWebInfo.LargeAreaList = new List<LargeArea>();
			List<Sysarea> largeAreaList = SysareaService.GetLargeAreaList();
			foreach (var largeAreaItem in largeAreaList) {
				LargeArea largeArea = new LargeArea();
				largeArea.Name = largeAreaItem.LargeArea;
				largeArea.ProvinceList = new List<Province>();
				List<Sysarea> provinceList = SysareaService.GetProvinceList(largeArea.Name);
				foreach (var provinceItem in provinceList) {
					Province province = new Province();
					province.ID = provinceItem.ID;
					province.AliasName = provinceItem.AliasName;
					province.IsChecked = checkedAreaIDList.Contains(province.ID);
					province.CityList = new List<City>();
					List<Sysarea> cityList = SysareaService.GetCityList(province.ID);
					foreach (var cityItem in cityList) {
						City city = new City();
						city.ID = cityItem.ID;
						city.Name = cityItem.Name;
						city.IsChecked = checkedAreaIDList.Contains(city.ID);
						province.CityList.Add(city);
					}
					largeArea.ProvinceList.Add(province);
				}
				selectAreaWebInfo.LargeAreaList.Add(largeArea);
			}
			ViewBag.SelectAreaWebInfo = selectAreaWebInfo;
			//	ViewBag.WarehouseExpressPrice = warehouseExpressPrice;
			//	ViewBag.RowIndex = rowIndex;
			return View();
		}
		
		#endregion
	}
}