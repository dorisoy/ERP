#region using
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaiXie.Utils;
using FluentData;
using PaiXie.Api.Bll;
#endregion

namespace PaiXie.Erp.Areas.Shop {
	public class ShopStockController : BaseController {

		#region prompt1  设置店铺库存比例 提示
		// GET: /Shop/ShopStock/prompt1
		public ActionResult prompt1() {
			return View();
		}
		#endregion

		#region prompt2    设置独享库存 提示
		// GET: /Shop/ShopStock/prompt2
		public ActionResult prompt2() {
			return View();
		}
		#endregion

		#region index
		// GET: /Shop/ShopStock/
		public ActionResult index() {
			return View();
		}
		#endregion

		#region 私有库存列表
		/// <summary>
		/// 私有库存列表
		/// </summary>
		/// <returns></returns>
		public ActionResult search(string ProductsID) {
			DataTable dt = ShopAllocationService.GetshopAllocationDataTable( ZConvert.StrToInt(ProductsID) );				
			List<shopAllocationByShop> list = new List<shopAllocationByShop>();
			int total = dt.Rows.Count;
			for (int i = 0; i < dt.Rows.Count; i++) {
				shopAllocationByShop obj = new shopAllocationByShop();
				PlanLog.WriteLog(dt.Rows[i]["ShopID"].ToString(), "私有库存列表");
				obj.ShopID = ZConvert.StrToInt(dt.Rows[i]["ShopID"]);
				obj.xsnum = dt.Rows[i]["xsnum"].ToString();
				obj.IsSalePub = ZConvert.StrToInt(dt.Rows[i]["IsSalePub"]) == 1 ? "是" : "否";
			    PaiXie.Data.Shop  objshop= ShopService.GetSingleShop(obj.ShopID);
				if (objshop!=null)
					obj.ShopName = objshop.Name;
				list.Add(obj);
			}
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 删除  独享  根据店铺  商品
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Delete(string shopid, string ProductsID) {
			BaseResult BaseResult = new BaseResult();
		    int result=	ShopAllocationService.Del(shopid,  ProductsID);
			if (result == 0) {
				BaseResult.result = -1;
				BaseResult.message = "删除失败";
			}
			return JsonDate(BaseResult);
		}

		#endregion

		#region 添加独享
		/// <summary>
		///添加独享
		/// </summary>
		/// <returns></returns>
		public ActionResult AddDx(string ProductsID) {
			//商品sku 信息
			List<ProductsSkuKucInfo> list = ProductsManager.GetProductsSkuKucInfo(ZConvert.StrToInt(ProductsID));
			List<ShopAllocation>  ShopAllocationList=ShopAllocationService.GetQuerySingleByProductsID(ZConvert.StrToInt(ProductsID));
			int _DxNum = ShopAllocationList.Sum(p=>p.SaleInventory);
			ViewBag.dxnum = list.Sum(p => p.KyNum) - _DxNum;
			List<ProductsSkuList> obj = ProductsSkuService.GetProductsSkuList(ZConvert.StrToInt(ProductsID));		
			//可设置独享的库存 
			for (int i = 0; i < obj.Count; i++) {
				List<ShopAllocation> objShopAllocationlist = ShopAllocationService.GetQuerySingleByProductsSkuID(obj[i].ID);
				int DxNumsum = objShopAllocationlist.Sum(p => p.SaleInventory);

				int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(obj[i].ID, 1);
				obj[i].kc = kfhNum - DxNumsum;
			}


				ViewBag.psku = obj;
			ViewBag.ProductsID = ProductsID;

		


			return View();
		}

		#endregion

		#region 添加独享
		/// <summary>
		///添加独享
		/// </summary>
		/// <returns></returns>
		public ActionResult AddDxRead(int ProductsID, int shopid) {
			List<ProductsSkuKucInfo> list = ProductsManager.GetProductsSkuKucInfo(ZConvert.StrToInt(ProductsID));
			List<ShopAllocation> ShopAllocationList = ShopAllocationService.GetQuerySingleByProductsID(ZConvert.StrToInt(ProductsID));
			int _DxNum = ShopAllocationList.Sum(p => p.SaleInventory);

			ViewBag.dxnum = list.Sum(p => p.KyNum) - _DxNum;


			//店铺名称
			ViewBag.shopname = ShopService.GetSingleShop(ZConvert.StrToInt(shopid)).Name;
			//商品sku 信息
			List<ProductsSkuList> obj = ProductsSkuService.GetProductsSkuList(ZConvert.StrToInt(ProductsID));			
			for (int i = 0; i < obj.Count(); i++) {
				string sl =  ShopAllocationService. SaleInventory( obj[i].ID ,  shopid,  ProductsID);
				obj[i].dxsl = ZConvert.StrToInt(sl,0); 
			}


			for (int i = 0; i < obj.Count; i++) {
				//ProductsSkuKucInfo objProductsSkuKucInfo = list.Where(p => p.ProductsSkuCode == obj[i].Code).FirstOrDefault();
				//if (objProductsSkuKucInfo != null) {
				//	obj[i].kc = objProductsSkuKucInfo.KyNum;
				//}

				List<ShopAllocation> objShopAllocationlist = ShopAllocationService.GetQuerySingleByProductsSkuID(obj[i].ID);
				int DxNumsum = objShopAllocationlist.Sum(p => p.SaleInventory);

				int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(obj[i].ID, 1);
				obj[i].kc = kfhNum - DxNumsum;

			}


			ViewBag.psku = obj;
			ViewBag.ProductsID = ProductsID;
			//是否独享
			ViewBag.IsSalePub = ShopAllocationService.IsSalePub(shopid, ProductsID);				
			return View();
		}

		#endregion

		#region 独享 保存

		[HttpPost]
		public ActionResult DxSave(shopAllocationList obj) {
			BaseResult BaseResult = new BaseResult();
			if (obj.ShopID == 0) {
				BaseResult.result = -1;
				BaseResult.message = "请选择店铺";
				return JsonDate(BaseResult);
			}
			if (obj.ProductsID == 0) {
				BaseResult.result = -1;
				BaseResult.message = "请选择商品";
				return JsonDate(BaseResult);
			}


			int result = 1;
			try {
				    using (IDbContext context = Db.GetInstance().Context()) {
                    context.UseTransaction(true);
					ShopAllocationService.Del(obj.ShopID.ToString(), obj.ProductsID.ToString(), context);
			
						for (int i = 0; i < obj.ProductsSkuID.Length; i++) {
							if (obj.dxsl[i] == 0) {
								continue;
							}
					ShopAllocation ShopAllocation = new ShopAllocation();
					ShopAllocation.ShopID = obj.ShopID;
					ShopAllocation.ProductsID = obj.ProductsID;
					ShopAllocation.ProductsSkuID = obj.ProductsSkuID[i];

                    //验证可设置独享库存
					List<ShopAllocation> objShopAllocationlist = ShopAllocationService.GetQuerySingleByProductsSkuID(obj.ProductsSkuID[i]);
					int DxNumsum = objShopAllocationlist.Sum(p => p.SaleInventory);
					int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(obj.ProductsSkuID[i], 1);
					int  kc = kfhNum - DxNumsum;
						ProductsSku objProductsSku=	ProductsSkuService.GetSingleProductsSku(obj.ProductsSkuID[i]);
					if (obj.Isggkc > kc) {
						BaseResult.result = -1;
						BaseResult.message ="SKU码"+ objProductsSku.Code+"独享数量不能大于可分配数量";
						break;
					}


					ShopAllocation.SaleInventory = obj.dxsl[i];
					ShopAllocation.IsSalePub = obj.Isggkc;
					ShopAllocation.CreatePerson = FormsAuth.GetUserCode();
					ShopAllocation.UpdatePerson = FormsAuth.GetUserCode();
					ShopAllocation.CreateDate = System.DateTime.Now;
					ShopAllocation.UpdateDate = System.DateTime.Now;
				result=	ShopAllocationService.Add(ShopAllocation);
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
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "独享保存", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);
		}

		#endregion

		#region 设置商品库存
		public ActionResult SetStockPro(string sid) {
			int tflag = 0;
			PaiXie.Data.Products objProducts = new Data.Products();
			var obj = ProductsService.GetSingleProducts(ZConvert.StrToInt(sid));
			if (obj != null) {
				objProducts = obj;
			}
			ViewBag.objProducts = objProducts;
			shopComancationList list = new shopComancationList();
			List<PaiXie.Data.Shop> shoplist = ShopService.shoplist();			
			string[] shopname = new string[shoplist.Count()];
			int[] shopid = new int[shoplist.Count()];
			string[] range = new string[shoplist.Count()];
			int i = 0;
			foreach (var item in shoplist) {
				shopname[i] = item.Name;
				shopid[i] = item.ID;
				string ranges = ShopComancationService.Ranges(item.ID,ZConvert.StrToInt(sid) );
				if (string.IsNullOrEmpty(ranges)) {
					tflag = 1;
					ranges = "100";
				}
					
				range[i] = ranges;
				i++;
			}
			string Remarks = ShopComancationService.Remark(ZConvert.StrToInt(sid));
				if (string.IsNullOrEmpty(Remarks))
				Remarks = "0";
			list.ShopID = shopid;
			list.ShopName = shopname;
			list.Ranges = range;
			list.Remark = Remarks;
			ViewBag.list = list;
			ViewBag.IsSz = tflag;
			return View();
		}
		#endregion

		#region 设置公共库存

		public ActionResult SetStock() {
			shopComancationList list = new shopComancationList();
			List<PaiXie.Data.Shop> shoplist = ShopService.shoplist();		
			string[] shopname = new string[shoplist.Count()];
			int[] shopid = new int[shoplist.Count()];
			string[] range = new string[shoplist.Count()];
			int i = 0;
			foreach (var item in shoplist) {
				shopname[i] = item.Name;
				shopid[i] = item.ID;
				string ranges = ShopComancationService.Ranges(item.ID,0); 
				
				if (string.IsNullOrEmpty(ranges))
					ranges = "100";
				range[i] = ranges;
				i++;
			}
			string Remarks = ShopComancationService.Remark(0);			
			if (string.IsNullOrEmpty(Remarks))
				Remarks = "0";
			list.ShopID = shopid;
			list.ShopName = shopname;
			list.Ranges = range;
			list.Remark = Remarks;
			ViewBag.list = list;
			return View();
		}
		#endregion

		#region 公共库存分配保存
		[HttpPost]
		public ActionResult Save(shopComancationList obj) {
			BaseResult BaseResult = new BaseResult();
			try {
				 using (IDbContext context = Db.GetInstance().Context()) {
                    context.UseTransaction(true);
				for (int i = 0; i < obj.ShopName.Length; i++) {
					ShopComancation objShopComancation = new ShopComancation();
					objShopComancation.ShopID = obj.ShopID[i];
					objShopComancation.Ranges = obj.Ranges[i];
					objShopComancation.Remark = obj.Remark;
					objShopComancation.ProductsID = obj.ProductsID;
					ShopComancationService.Del( obj.ShopID[i] ,obj.ProductsID);
				   int result=	ShopComancationService.Add(objShopComancation);
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
				PaiXie.Api.Bll.Sys.SaveErrorLog(ex, "公共库存分配保存", FormsAuth.GetUserCode());
			}
			return JsonDate(BaseResult);
		}
		#endregion
	}
}