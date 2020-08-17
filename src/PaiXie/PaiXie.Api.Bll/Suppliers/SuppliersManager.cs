using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Api.Bll {

	/// <summary>
	/// 供应商管理
	/// </summary>
	public class SuppliersManager {

		#region 根据商品ID查询关联供应商

		/// <summary>
		/// 根据商品ID查询关联供应商
		/// </summary>
		/// <param name="productsID">商品ID</param>
		/// <returns></returns>
		public static List<SuppliersItemInfo> GetSuppliersByProductsID(int productsID) {
			List<SuppliersItemInfo> suppliersItemInfoList = new List<SuppliersItemInfo>();

			DataTable dt = SuppliersItemService.GetQueryManyByProductsID(productsID);
			foreach (DataRow dr in dt.Rows) {
				SuppliersItemInfo suppliersItemInfo = new SuppliersItemInfo();
				suppliersItemInfo.ID = ZConvert.StrToInt(dr["ID"]);
				suppliersItemInfo.SuppliersID = ZConvert.StrToInt(dr["SuppliersID"]);
				suppliersItemInfo.AliasName = ZConvert.ToString(dr["AliasName"]);
				suppliersItemInfo.ProductsID = ZConvert.StrToInt(dr["ProductsID"]);
				suppliersItemInfo.ProductsCode = ZConvert.ToString(dr["ProductsCode"]);
				suppliersItemInfo.ProductsName = ZConvert.ToString(dr["ProductsName"]);
				suppliersItemInfo.ProductsNo = ZConvert.ToString(dr["ProductsNo"]);
				suppliersItemInfo.ProductsSkuID = ZConvert.StrToInt(dr["ProductsSkuID"]);
				suppliersItemInfo.ProductsSkuCode = ZConvert.ToString(dr["ProductsSkuCode"]);
				suppliersItemInfo.ProductsSkuSaleprop = ZConvert.ToString(dr["ProductsSkuSaleprop"]);
				suppliersItemInfo.PurchasePrice = ZConvert.StrToDecimal(dr["PurchasePrice"]);
				suppliersItemInfo.ArrivalCycle = ZConvert.StrToInt(dr["ArrivalCycle"]);
				suppliersItemInfo.CreateDate = ZConvert.StrToDateTime(dr["CreateDate"], DateTime.Now);
				suppliersItemInfo.CreatePerson = ZConvert.ToString(dr["CreatePerson"]);
				suppliersItemInfo.UpdateDate = ZConvert.StrToDateTime(dr["UpdateDate"], DateTime.Now);
				suppliersItemInfo.UpdatePerson = ZConvert.ToString(dr["UpdatePerson"]);
				suppliersItemInfoList.Add(suppliersItemInfo);
			}

			return suppliersItemInfoList;
		}

		#endregion

		#region 根据商品SKUID查询关联供应商

		/// <summary>
		/// 根据商品SKUID查询关联供应商
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <returns></returns>
		public static List<SuppliersItemInfo> GetSuppliersByProductsSkuID(int productsSkuID) {
			List<SuppliersItemInfo> suppliersItemInfoList = new List<SuppliersItemInfo>();
			DataTable dt = SuppliersItemService.GetQueryManyByProductsSkuID(productsSkuID);
			foreach (DataRow dr in dt.Rows) {
				SuppliersItemInfo suppliersItemInfo = new SuppliersItemInfo();
				suppliersItemInfo.ID = ZConvert.StrToInt(dr["ID"]);
				suppliersItemInfo.SuppliersID = ZConvert.StrToInt(dr["SuppliersID"]);
				suppliersItemInfo.AliasName = ZConvert.ToString(dr["AliasName"]);
				suppliersItemInfo.ProductsID = ZConvert.StrToInt(dr["ProductsID"]);
				suppliersItemInfo.ProductsCode = ZConvert.ToString(dr["ProductsCode"]);
				suppliersItemInfo.ProductsName = ZConvert.ToString(dr["ProductsName"]);
				suppliersItemInfo.ProductsNo = ZConvert.ToString(dr["ProductsNo"]);
				suppliersItemInfo.ProductsSkuID = ZConvert.StrToInt(dr["ProductsSkuID"]);
				suppliersItemInfo.ProductsSkuCode = ZConvert.ToString(dr["ProductsSkuCode"]);
				suppliersItemInfo.ProductsSkuSaleprop = ZConvert.ToString(dr["ProductsSkuSaleprop"]);
				suppliersItemInfo.PurchasePrice = ZConvert.StrToDecimal(dr["PurchasePrice"]);
				suppliersItemInfo.ArrivalCycle = ZConvert.StrToInt(dr["ArrivalCycle"]);
				suppliersItemInfo.IsDefault = ZConvert.StrToInt(dr["IsDefault"]);
				suppliersItemInfo.CreateDate = ZConvert.StrToDateTime(dr["CreateDate"], DateTime.Now);
				suppliersItemInfo.CreatePerson = ZConvert.ToString(dr["CreatePerson"]);
				suppliersItemInfo.UpdateDate = ZConvert.StrToDateTime(dr["UpdateDate"], DateTime.Now);
				suppliersItemInfo.UpdatePerson = ZConvert.ToString(dr["UpdatePerson"]);
				suppliersItemInfoList.Add(suppliersItemInfo);
			}
			return suppliersItemInfoList;
		}

		#endregion

		#region 根据商品SKUID和供应商ID获取采购单价

		/// <summary>
		/// 根据商品SKUID和供应商ID获取采购单价
		/// </summary>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="suppliersID">供应商ID</param>
		/// <returns></returns>
		public static decimal GetPurchasePrice(int productsSkuID, int suppliersID) {
			SuppliersItem suppliersItem = SuppliersItemService.GetSingleSuppliersItem(productsSkuID, suppliersID);
			return suppliersItem.PurchasePrice;
		}

		#endregion

		#region 保存供应商信息

		public static BaseResult AddSuppliers(string userCode, Suppliers obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				if (obj.ID == 0) {
					obj.CreatePerson = userCode;
					obj.CreateDate = DateTime.Now;
					obj.IsEnable = (int)IsEnable.是;
					bool tempFlag = SuppliersService.Add(obj) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "添加供应商失败！";
					}
				}
				else {
					Suppliers objSuppliers = SuppliersService.GetQuerySingleByID(obj.ID);
					objSuppliers.Name = obj.Name;
					objSuppliers.AliasName = obj.AliasName;
					objSuppliers.ContactPerson = obj.ContactPerson;
					objSuppliers.Email = obj.Email;
					objSuppliers.Fax = obj.Fax;
					objSuppliers.IsEnable = (int)IsEnable.是;
					objSuppliers.UpdatePerson = userCode;
					objSuppliers.UpdateDate = DateTime.Now;
					bool tempFlag = SuppliersService.Update(objSuppliers) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "修改供应商失败！";
					}

				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存供应商信息", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存供应商商品信息

		/// <summary>
		/// 保存供应商商品信息
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="objWebInfo">供应商商品实体类</param>
		/// <returns></returns>
		public static BaseResult AddSuppliersItem(string userCode, SuppliersItemWebInfo objWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					for (int i = 0; i < objWebInfo.ProductsSkuID.Length; i++) {
						int productsSkuID = objWebInfo.ProductsSkuID[i];
						string productsSkuCode = objWebInfo.ProductsSkuCode[i];
						string productsSkuSaleprop = objWebInfo.ProductsSkuSaleprop[i];
						decimal purchasePrice = objWebInfo.PurchasePrice[i];
						ProductsSku productsSkuInfo = ProductsSkuService.GetSingleProductsSku(productsSkuID, context);
						if (productsSkuInfo != null) {
							SuppliersItem suppliersItem = SuppliersItemService.GetSingleSuppliersItem(productsSkuID, objWebInfo.SuppliersID, context);
							if (suppliersItem == null) {
								suppliersItem = new SuppliersItem();
								suppliersItem.SuppliersID = objWebInfo.SuppliersID;
								suppliersItem.ProductsCode = objWebInfo.ProductsCode;
								suppliersItem.ProductsID = objWebInfo.ProductsID;
								suppliersItem.ProductsName = objWebInfo.ProductsName;
								suppliersItem.ProductsNo = objWebInfo.ProductsNo;
								suppliersItem.ProductsSkuCode = productsSkuCode;
								suppliersItem.ProductsSkuID = productsSkuID;
								suppliersItem.ProductsSkuSaleprop = productsSkuSaleprop;
								suppliersItem.PurchasePrice = purchasePrice;
								suppliersItem.ArrivalCycle = objWebInfo.ArrivalCycle;
								suppliersItem.CreatePerson = userCode;
								suppliersItem.CreateDate = DateTime.Now;
								int purchaseItemID = SuppliersItemService.Add(suppliersItem, context);
								if (purchaseItemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 添加失败！";
									break;
								}
							}
							else {
								suppliersItem.ArrivalCycle = objWebInfo.ArrivalCycle;
								suppliersItem.PurchasePrice = objWebInfo.PurchasePrice[i];
								int count = SuppliersItemService.Update(suppliersItem, context);
								if (count == 0) {
									resultInfo.result = 0;
									resultInfo.message = "商品SKU码 " + productsSkuCode + " 修改失败！";
									break;
								}
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码 " + productsSkuCode + " 不存在！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存供应商商品信息", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除供应商

		/// <summary>
		/// 删除供应商
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="idList">供应商ID列表</param>
		/// <returns></returns>
		public static BaseResult DeleteSuppliers(string userCode, List<int> idList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var id in idList) {
						Suppliers suppliers = SuppliersService.GetQuerySingleByID(id, context);
						suppliers.Name += "_已删除";
						suppliers.AliasName +="_已删除";
						suppliers.IsEnable = (int)IsEnable.否;
						suppliers.UpdatePerson = userCode;
						suppliers.UpdateDate = DateTime.Now;
						int count = SuppliersService.Update(suppliers, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "供应商 " + suppliers.Name.Replace("_已删除", "") + " 删除失败！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除供应商", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除供应商商品

		/// <summary>
		/// 删除供应商商品
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult DeleteSuppliersItem(string userCode, List<int> productsIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in productsIDList) {
						int count = SuppliersItemService.DelByProductsID(productsID, context);
						if (count == 0) {
							Products products = ProductsService.GetSingleProducts(productsID, context);
							resultInfo.result = 0;
							resultInfo.message = "商品编码 " + products.Code + " 删除失败！";
							break;
						}
					}
					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除供应商商品", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
