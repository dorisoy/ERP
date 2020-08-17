#region using
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
using PaiXie.Api.Bll;

#endregion
namespace PaiXie.Erp.Areas.shop {
	public class ShopsController : BaseController {
		#region Index
		//
		// GET: /ShopManage/shop/

		public ActionResult Index() {

			return View();
		}
		#endregion

		#region 平台名称
		//
		// GET: /shop/shops/GetdtPlatformType

		public ActionResult GetdtPlatformType() {
			DataTable dtPlatformType = EnumManager<ThirdApi>.GetDataTable();
			List<CListItem> treeList = new List<CListItem>();
			   for (int i = 0; i < dtPlatformType.Rows.Count; i++) {
				   CListItem cListItem = new CListItem();
				   cListItem.Text = dtPlatformType.Rows[i]["Name"].ToString();
				   cListItem.Value = dtPlatformType.Rows[i]["Value"].ToString();
				   treeList.Add(cListItem);
                }
			return JsonDate(treeList);
		}
		#endregion

		#region 店铺列表
		/// <summary>
		/// 店铺列表
		/// </summary>
		/// <returns></returns>
		public ActionResult search() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = "  a.IsEnable=1  ";
			#region whereSql
			Object[] objects = new Object[3];
			string Name = Request["Name"];
			if (!string.IsNullOrEmpty(Name)) {
				whereSql += " and  a.Name LIKE @0";
				objects[0] = "%" + Name + "%";
			}
			string Type = Request["Type"];
			if (Type != "0" && !string.IsNullOrEmpty(Type)) {
				whereSql += " and  a.Type = @1";
				objects[1] = Type;
			}
			string PlatformType = Request["PlatformType"];
			if (PlatformType != "0" && !string.IsNullOrEmpty(PlatformType)) {
				whereSql += "  and  a.PlatformType = @2";
				objects[2] = PlatformType;
			} 
			#endregion
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = @" shop  a
        	LEFT JOIN  sys_user b ON a.CreatePerson=b.Code	
	        LEFT JOIN  sys_user c ON a.UpdatePerson=c.Code ";
			data.Select = "a.*,b.Name AS cname,c.Name AS uname, ''AS part1 ,''AS part2";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<ShopList> list = BaseService<ShopList>.GetQueryManyForPage(data, out total, null, objects);
			//   构造成Json的格式传递			
			for (int i = 0; i < list.Count(); i++) {
				Syscode z = SyscodeService.GetSyscodeByCodetype(list[i].Type, "002");
				if (z != null) {
					list[i].part1 = z.Text;
				}
			}
			for (int i = 0; i < list.Count(); i++) {
				list[i].part2 = ((ThirdApi)list[i].PlatformType).ToString();
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 编辑

		public ActionResult Edit(string id) {
			DataTable dtPlatformType = EnumManager<ThirdApi>.GetDataTable();
			ViewBag.dtPlatformTypes = dtPlatformType;
			//店铺平台   
			DataTable Type = SyscodeService.GetDataTableCode();
			ViewBag.Type = Type;
			PaiXie.Data.Shop objSysuser = new Data.Shop();
			if (id != "0") objSysuser = ShopService.GetSingleShop(ZConvert.StrToInt(id));
			ViewBag.Sysuser = objSysuser;
			return View();
		}

		#endregion

		#region 保存

		[HttpPost]
		public ActionResult Save(PaiXie.Data.Shop obj) {
			BaseResult BaseResult = new BaseResult();
			int result = 1;
			try {
				if (obj.ID == 0) {
					obj.Code = PaiXie.Api.Bll.Sys.GetBillNo("shop");
					obj.CreatePerson = FormsAuth.GetUserCode();
					obj.CreateDate = System.DateTime.Now;
					obj.UpdatePerson = FormsAuth.GetUserCode();
					obj.UpdateDate = System.DateTime.Now;
					obj.IsEnable = 1;
					result = ShopService.Add(obj);
				}
				else {
					PaiXie.Data.Shop objSysuser = ShopService.GetSingleShop(ZConvert.StrToInt(obj.ID));
					objSysuser.Code = obj.Code;
					objSysuser.Name = obj.Name;
					objSysuser.IsEnable = obj.IsEnable;
					objSysuser.StoreAddr = obj.StoreAddr;
					objSysuser.Longitude = obj.Longitude;
					objSysuser.Latitude = obj.Latitude;
					objSysuser.AppKey = obj.AppKey;
					objSysuser.AppSecret = obj.AppSecret;
					objSysuser.AppSession = obj.AppSession;
					objSysuser.RefreshToken = obj.RefreshToken;
					objSysuser.ContactPerson = obj.ContactPerson;
					objSysuser.ContactTel = obj.ContactTel;
					objSysuser.Type = obj.Type;
					objSysuser.PlatformType = obj.PlatformType;
					objSysuser.Website = obj.Website;
					objSysuser.Remark = obj.Remark;
					objSysuser.UpdatePerson = FormsAuth.GetUserCode();
					objSysuser.UpdateDate = System.DateTime.Now;
					result = ShopService.Update(objSysuser);
					if (result == 0) {
						BaseResult.result = -1;
						BaseResult.message = "操作失败";
					}
				}
			}
			catch (Exception ex) {
				BaseResult.result = -1;
				BaseResult.message = ex.Message;
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "保存店铺", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);
		}

		#endregion

		#region 删除
		/// <summary>
		/// 禁用店铺
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Delete(string id) {
			BaseResult BaseResult = new BaseResult();
			int result = ShopService.DeleteShop(id);
			if (result == 0) {
				BaseResult.result = -1;
				BaseResult.message = "操作失败";				
			}
			return JsonDate(BaseResult);
		}

		#endregion

		#region 检查代码唯一性

		public ActionResult CheckCode(string Code, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
				if (ShopService.CheckCode(Code, ID) > 0) {
					BaseResult.result = -1;
				}
			}
			else {
				if (ShopService.CheckCode2(Code) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}

		public ActionResult CheckName(string Name, int ID) {
			BaseResult BaseResult = new BaseResult();
			if (ID > 0) {
				if (ShopService.CheckName(Name, ID) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (ShopService.CheckName2(Name) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}
		#endregion
	}
}