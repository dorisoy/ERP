#region using
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PaiXie.Utils;
using PaiXie.Api.Bll;
#endregion

namespace PaiXie.Erp.Areas.Shop
{
	public class ShopStockUpdateController : BaseController
    {
		#region index
		//
		// GET: /Shop/ShopStockUpdate/
		public ActionResult index() {
			return View();
		} 
		#endregion

		#region 更新库存
		/// <summary>
		/// 
		/// </summary>
		/// <param name="shopid">店铺id</param>
		/// <returns></returns>
		public ActionResult StockUpdate(string shopid = "", string OuterId="") {
			
			BaseResult BaseResult = new BaseResult();
			BaseResult o= ShopProductsManager.IsShopStockUpdate(ZConvert.StrToInt(shopid)) ;
			if (o.result == 0) {
				 return JsonDate(o);
			}
			if (OuterId != "") {
				ShopProductsService.DelshopStockUpdateSinge(ZConvert.StrToInt(shopid), OuterId);
				ShopProductsManager.ShopStockUpdate(ZConvert.StrToInt(shopid), OuterId);
				return JsonDate(BaseResult);
			}
			//删除旧的数据
			ShopProductsService.DelbyshopID(ZConvert.StrToInt(shopid));
			DownProductsParam param = new DownProductsParam();
			param.TaskID = Guid.NewGuid().ToString();
			param.ShopID = ZConvert.StrToInt(shopid);
			param.ProductsStatus = 0;
			param.UserCode = FormsAuth.GetUserCode();
			ThreadPool.QueueUserWorkItem(new WaitCallback(thread), param);	
			return JsonDate(BaseResult);
		}
		private void thread(object obj) {
			DownProductsParam param = obj as DownProductsParam;
			ShopProductsManager.DownLoadUpdate(param);
			//更新操作
			ShopProductsManager.ShopStockUpdate(param.ShopID);
		}
		#endregion

		#region 获取提示信息	
		public ActionResult GetUpdateInfo(string shopid = "") {
			shopStockUpdateMsg BaseResult = new shopStockUpdateMsg();
			if(shopid=="")
				return JsonDate(BaseResult);
			BaseResult.result = 1;
		   var Shop=	ShopService.GetSingleShop(ZConvert.StrToInt(shopid));
		   string PlatformType =Shop!=null? Shop.PlatformType.ToString():"0";
			if (string.IsNullOrEmpty(PlatformType)) {
				PlatformType = "0";
			}
			List<ShopStockUpdate> ShopStockUpdatelist = ShopStockUpdateService.ShopStockUpdatelist(ZConvert.StrToInt(shopid) ,ZConvert.StrToInt(PlatformType) );
		
			
			BaseResult.pronum = ShopStockUpdatelist.Count();
			BaseResult.skunum = ShopStockUpdatelist.Select(o => o.SkuNum).Sum();
			BaseResult.sucesssku = ShopStockUpdatelist.Where(p=>p.UpdateStatus==1).Select(o => o.SkuNum).Sum();
			BaseResult.errorsku = ShopStockUpdatelist.Where(p => p.UpdateStatus == 0).Select(o => o.SkuNum).Sum();
			BaseResult.updatetime =ShopStockUpdatelist.Count()>0?ShopStockUpdatelist.OrderByDescending(o => o.UpdateTime).FirstOrDefault().UpdateTime.ToString():"";
			return JsonDate(BaseResult);
		}	
		#endregion

		#region 更新库存  进度条
		/// <summary>
		/// 
		/// </summary>
		/// <param name="shopid">店铺id</param>
		/// <returns></returns>
		public ActionResult StockUpdatepross(string shopid = "") {
			BaseResult BaseResult = new BaseResult();
			BaseResult.result =1;
            //总的
			string k1 = ShopUpdateProductsService.shopProductscount(ZConvert.StrToInt(shopid));			
            //完成
			string k2 =  ShopStockUpdateService.shopStockUpdatecount( ZConvert.StrToInt(shopid)); 
			BaseResult.message = k2+"/"+k1;

			if (k1 == k2 && k2 != "0") {
				//DateTime t=DateTime.Now;
				// object obj= ShopStockUpdateService.LastTimeshopStockUpdate(ZConvert.StrToInt(shopid));
				// if (ZConvert.StrToDateTime(obj, t) != t) {
				//	 if (ZConvert.StrToDateTime(obj, t) > t) {
						 BaseResult.result = 99;
				 //	}
				 //}

			}
				

			return JsonDate(BaseResult);
		}	
		#endregion
    }
}