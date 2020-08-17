#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Utils;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using Newtonsoft.Json;
using PaiXie.Core;
using System.Threading;
using PaiXie.Api.Bll;
#endregion
namespace PaiXie.Erp.Controllers {
		[MvcMenuFilter(false)]
	public class HomeController : Controller {

		#region Index
		// 缓存页面
		 //[CacheFilter]
		public ActionResult Index() {
		
			if (!string.IsNullOrEmpty(Request["wid"])) {
				var loginer = new LoginerBase { IsSupper = FormsAuth.GetUserData().IsSupper, UserCode = FormsAuth.GetUserCode(), UserName = FormsAuth.GetUserName(), ModeType = FormsAuth.GetModeType(), WarehouseCode = Request["wid"].ToString() };
				new Users().UpdateUserLoginInfo(loginer);
			}
			Thread.Sleep(1000);
			#region 菜单
			string str = "";// ZFiles.ReadFile(Request.MapPath("~/Content/menu/menu.json"));
			MenuList obj = new MenuList();
			List<menusItem> menusItemlist = new List<menusItem>();
			string pcode = "999";
			int modetype = FormsAuth.GetModeType();
			//(int)ProjectType.管理端;
			string wid = Request["wid"];
			if (modetype == (int)ProjectType.仓库端) {
				modetype = (int)ProjectType.仓库端;
				pcode = "9999";
			}
			if (!string.IsNullOrEmpty(wid)) {
				modetype = (int)ProjectType.仓库端;
				pcode = "9999";
			}
			bool IsSupper = FormsAuth.GetIsSupper(); //是否超管用户
			List<Sysmenu> list =
				SysmenuService.GetSysMenuByUserCode(FormsAuth.GetUserCode(), pcode, modetype, IsSupper);
			foreach (var item in list) {
				menusItem a = new menusItem();
				a.menuid = item.Code;
				a.icon = item.IconClass;
				a.menuname = item.Name;
				List<menusItems> menusItemslist = new List<menusItems>();
				List<Sysmenu> clist =
					SysmenuService.GetSysMenuByUserCode(FormsAuth.GetUserCode(), item.Code, modetype, IsSupper);
				foreach (var citem in clist) {
					menusItems b = new menusItems();
					b.menuid = citem.Code;
					b.icon = citem.IconClass;
					b.menuname = citem.Name;
					b.url = citem.URL;
					menusItemslist.Add(b);
				}
				a.menus = menusItemslist;
				menusItemlist.Add(a);
			}
			obj.menus = menusItemlist;
			str = JsonConvert.SerializeObject(obj, Formatting.Indented);
			ViewBag.js = "<script type='text/javascript'>" + "var  _menus =" + str + ";</script>";
			#endregion
			if (FormsAuth.GetModeType() == (int)ProjectType.管理端) {
				ViewBag.username = "管理端[" + FormsAuth.GetUserName() + "]";
			}
			else  {
				Warehouse objWarehouse = WarehouseService.GetwarehousebyCode(FormsAuth.GetWarehouseCode());
				if (objWarehouse != null) {
					ViewBag.username = "" + objWarehouse.Name + "[" + FormsAuth.GetUserName() + "]";
				}
				
			
			}
			if ( !string.IsNullOrEmpty(Request["wid"])) {
				Warehouse objWarehouse = WarehouseService.GetwarehousebyCode(Request["wid"].ToString().Trim());
				if (objWarehouse != null) {
					ViewBag.username = "" + objWarehouse.Name + "[" + FormsAuth.GetUserName() + "]";
				}

			}


			return View();
		}
		#endregion
	}
}