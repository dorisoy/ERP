using FluentData;
using PaiXie.Core;
using PaiXie.Core.Enum;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	public class OrdbaseManager {

		#region 暂存订单

		/// <summary>
		/// 暂存订单
		/// </summary>
		/// <param name="ordbase"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult ScratchOrder(Ordbase ordbase, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);

					if (string.IsNullOrEmpty(ordbase.ErpOrderCode)) {
						ordbase.ErpOrderCode = Sys.GetBillNo("");
						ordbase.CreateType = (int)OrdCreateType.手动;
						ordbase.CreateDate = DateTime.Now;
						ordbase.CreatePerson = userCode;
						resultInfo = AddOrder(ordbase, userCode, userName, context);
					}
					else {
						Ordbase oldOrdbase = OrdbaseService.GetQuerySingleByErpOrderCode(ordbase.ErpOrderCode, context);
						oldOrdbase.BuyName = ordbase.BuyName;
						oldOrdbase.BuyMtel = ordbase.BuyMtel;
						oldOrdbase.BuyPostCode = ordbase.BuyPostCode;
						oldOrdbase.Province = ordbase.Province;
						oldOrdbase.ProvinceID = ordbase.ProvinceID;
						oldOrdbase.City = ordbase.City;
						oldOrdbase.CityID = ordbase.CityID;
						oldOrdbase.District = ordbase.District;
						oldOrdbase.DistrictID = ordbase.DistrictID;
						oldOrdbase.BuyAddressDetail = ordbase.BuyAddressDetail;
						oldOrdbase.BuyAddr = ordbase.Province + ordbase.City + ordbase.District + ordbase.BuyAddressDetail;
						oldOrdbase.PaymentMethod = ordbase.PaymentMethod;
						oldOrdbase.BuyCodFee = ordbase.BuyCodFee;
						oldOrdbase.LogisticsID = ordbase.LogisticsID;
						oldOrdbase.Freight = ordbase.Freight;
						oldOrdbase.ShopID = ordbase.ShopID;
						oldOrdbase.OutOrderCode = ordbase.OutOrderCode;
						oldOrdbase.ExpectedDeliDate = ordbase.ExpectedDeliDate;
						oldOrdbase.DeliveryMethod = ordbase.DeliveryMethod;
						oldOrdbase.SinceSome = ordbase.SinceSome;
						oldOrdbase.InvoiceName = ordbase.InvoiceName;
						oldOrdbase.SellerRemark = ordbase.SellerRemark;
						oldOrdbase.IsNeedInvoice = string.IsNullOrWhiteSpace(ordbase.InvoiceName) ? 0 : 1;
						int rowsAffected = OrdbaseService.Update(oldOrdbase, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新订单失败！";
						}
					}

					//更新订单主表商品金额和数量
					if (resultInfo.result == 1) {
						int rowsAffected = OrdbaseService.UpdateOrdbaseAmount(ordbase.ErpOrderCode, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "更新订单主表商品金额和数量失败！";
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
				Sys.SaveErrorLog(ex, "暂存订单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 添加订单

		/// <summary>
		/// 添加订单
		/// </summary>
		/// <param name="ordbase"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult AddOrder(Ordbase ordbase, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					if (ordbase.PaymentMethod == (int)PaymentMethod.货到付款) {
						ordbase.IsCod = 1;
					}
					resultInfo = AddOrder(ordbase, userCode, userName, context);

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
				Sys.SaveErrorLog(ex, "添加订单", userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 添加订单
		/// </summary>
		/// <param name="ordbase"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult AddOrder(Ordbase ordbase, string userCode, string userName, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				int ordbaseID = OrdbaseService.Add(ordbase, context);
				ordbase.ID = ordbaseID;
				if (ordbaseID == 0) {
					resultInfo.result = 0;
					resultInfo.message = "添加订单失败！";
				}
				else {
					#region 订单操作日志

					string msg = string.Format("添加订单{0}", ordbase.OrdouterID == 0 ? "" : "[自动]");
					resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

					#endregion
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 生成订单

		/// <summary>
		/// 生成订单（手动添加）
		/// </summary>
		/// <param name="ordbase"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult Generate(Ordbase ordbase, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					ordbase.GenerateOrderDate = DateTime.Now;
					ordbase.OrderStatus = (int)OrdBaseStatus.待审核;
					ordbase.UpdatePerson = userCode;
					ordbase.UpdateDate = DateTime.Now;
					int rowsAffected = 0;
					if (OrdbaseService.GetQuerySingleByErpOrderCode(ordbase.ErpOrderCode, context).OrderStatus == (int)OrdBaseStatus.未生成) {
						rowsAffected = OrdbaseService.Update(ordbase, context);
					}
					if (rowsAffected > 0) {
						rowsAffected = OrdbaseService.UpdateOrdbaseAmount(ordbase.ErpOrderCode, context);
					}
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "生成订单失败！";
					}
					if (resultInfo.result == 1) {
						#region 订单操作日志

						string msg = string.Format("生成订单");
						resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

						#endregion
					}
					if (resultInfo.result == 1) {
						#region 添加收款记录

						ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(ordbase.ErpOrderCode, context);
						if (ordbase.IsCod == 0) {
							OrdaccountsBill accountsBill = new OrdaccountsBill();
							accountsBill.BillNo = PaiXie.Api.Bll.Sys.GetBillNo(BillType.SK.ToString());
							accountsBill.BillType = (int)BillType.SK;
							accountsBill.BillWay = 1;
							accountsBill.ErpOrderCode = ordbase.ErpOrderCode;
							accountsBill.AssociatedCode = ordbase.ErpOrderCode;
							accountsBill.Amount = ordbase.ReceivableAmount;
							accountsBill.PayDate = ordbase.PayDate;
							accountsBill.Remark = "订单生成时添加收款单";
							accountsBill.CreateDate = DateTime.Now;
							accountsBill.CreatePerson = userCode;
							resultInfo = OrdbaseManager.AddAccountsBill(accountsBill, FormsAuth.GetUserCode(),context);
						}

						#endregion
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
				Sys.SaveErrorLog(ex, "生成订单", userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 生成订单（外部订单）
		/// </summary>
		/// <param name="ordbase"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult Generate(int ordouterID, string userCode, string userName, bool IsAuto = false) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordouter ordouter = OrdouterService.GetQuerySingleByID(ordouterID, context);
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(ordouter.ErpOrderCode, context);

					int IsProductAddFin = OrdouterItemService.getIsProductAddFin(ordouter.ID);
					if (IsProductAddFin == 0) {
						resultInfo.result = 0;
						resultInfo.message = "订单未添加商品，不能生成！";
					}
					if (resultInfo.result == 1) {
						if (ordbase.LogisticsID == 0) {
							List<ShopExpressSet> expressSetList = ShopExpressSetService.GetManyShopExpressSet(ordouter.ShopID);
							foreach (var expressSet in expressSetList) {
								if (ordouter.ShippingType == expressSet.ShippingType) {
									ordbase.LogisticsID = expressSet.LogisticsID;
									break;
								}
							}
							if (ordbase.LogisticsID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "订单未匹配物流，不能生成！";
							}
						}
					}
					if (resultInfo.result == 1) {
						if (string.IsNullOrEmpty(ordbase.BuyName)) {
							ordbase.BuyName = ordouter.BuyName;
							ordbase.BuyTel = ordouter.BuyTel;
							ordbase.BuyMtel = ordouter.BuyMtel;
							ordbase.BuyPostCode = ordouter.BuyPostCode;
							ordbase.BuyAddressDetail = ordouter.BuyAddressDetail;
							ordbase.BuyAddr = ordouter.BuyAddr;
							PaiXie.Api.Bll.AreaManager.Area area = AreaManager.GetAreaInfo(ordouter.BuyAddr);
							ordbase.ProvinceID = area.ProvinceID;
							ordbase.Province = area.Province;
							ordbase.CityID = area.CityID;
							ordbase.City = area.City;
							ordbase.DistrictID = area.CountyID;
							ordbase.District = area.County;
						}
						ordbase.BuyNickName = ordouter.BuyNickName;
						ordbase.BuyMessage = ordouter.BuyMessage;
						ordbase.SellerRemark = ordouter.SellerRemark;
						ordbase.Freight = ordouter.Freight;
						ordbase.Created = ordouter.Created;
						ordbase.PayDate = ordouter.PayDate;
						ordbase.IsCod = ordouter.IsCod;
						ordbase.IsNeedInvoice = ordouter.IsNeedInvoice;
						ordbase.InvoiceInfo = ordouter.InvoiceInfo;
						ordbase.InvoiceName = ordouter.InvoiceName;
						ordbase.GenerateOrderDate = DateTime.Now;
						ordbase.OrderStatus = (int)OrdBaseStatus.待审核;
						ordbase.UpdatePerson = userCode;
						ordbase.UpdateDate = DateTime.Now;
						int rowsAffected = 0;
						if (OrdbaseService.GetQuerySingleByErpOrderCode(ordouter.ErpOrderCode, context).OrderStatus == (int)OrdBaseStatus.未生成) {
							rowsAffected = OrdbaseService.Update(ordbase, context);
						}
						if (rowsAffected > 0) {
							rowsAffected = OrdbaseService.UpdateOrdbaseAmount(ordbase.ErpOrderCode, context);
						}
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "生成订单失败！";
						}
						if (resultInfo.result == 1) {
							#region 订单操作日志

							string msg = string.Format("生成订单{0}", IsAuto ? "[自动]" : "");
							resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

							#endregion
						}
						if (resultInfo.result == 1) {
							ordouter.GenerateState = 1;
							rowsAffected = OrdouterService.Update(ordouter, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新外部订单失败！";
							}
						}
						if (resultInfo.result == 1) {
							#region 添加收款记录

							if (ordouter.IsCod == 0 && ordouter.RealAmount > 0) {
								OrdaccountsBill accountsBill = new OrdaccountsBill();
								accountsBill.BillNo = PaiXie.Api.Bll.Sys.GetBillNo(BillType.SK.ToString());
								accountsBill.BillType = (int)BillType.SK;
								accountsBill.BillWay = 1;
								accountsBill.ErpOrderCode = ordouter.ErpOrderCode;
								accountsBill.AssociatedCode = ordouter.ErpOrderCode;
								accountsBill.Amount = ordouter.RealAmount;
								accountsBill.PayDate = ordouter.PayDate;
								accountsBill.Status = 1;
								accountsBill.Remark = "订单生成时添加收款单";
								accountsBill.CreateDate = DateTime.Now;
								accountsBill.CreatePerson = userCode;
								resultInfo = OrdbaseManager.AddAccountsBill(accountsBill, FormsAuth.GetUserCode(),context);
							}

							#endregion
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
				Sys.SaveErrorLog(ex, "生成订单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 删除订单

		/// <summary>
		/// 删除订单
		/// </summary>
		/// <param name="id"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult Delte(int id, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordbase ordbase = OrdbaseService.GetQuerySingleByID(id, context);
					resultInfo = Delte(id, userCode, userName, context);
					if (resultInfo.result == 1) {
						resultInfo = OrdouterManager.Delte(ordbase.ErpOrderCode, context);
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
				Sys.SaveErrorLog(ex, "删除订单", userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 删除订单
		/// </summary>
		/// <param name="id"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult Delte(int id, string userCode, string userName, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				Ordbase ordbase = OrdbaseService.GetQuerySingleByID(id, context);

				//删除订单主表
				int rowsAffected = OrdbaseService.DelByID(id, context);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "删除订单失败！";
				}
				else {
					#region 订单操作日志

					string msg = string.Format("删除订单");
					resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

					#endregion
				}

				//删除订单商品
				if (resultInfo.result == 1) {
					List<Orditem> itemList = OrditemService.GetManyOrditem(id, context);
					foreach (var item in itemList) {
						resultInfo = OrditemManager.DeleteItem(item.ID, userCode, userName, false, context);
						if (resultInfo.result == 0) {
							break;
						}
					}
				}

				//商品订单优惠
				if (resultInfo.result == 1) {
					List<Orddiscount> discounList = OrddiscountService.GetManyOrddiscount(ordbase.ErpOrderCode, context);
					foreach (var discoun in discounList) {
						resultInfo = OrditemManager.DeleteDiscount(discoun.ID, userCode, userName, false, context);
						if (resultInfo.result == 0) {
							break;
						}
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 保存自动生成设置

		/// <summary>
		/// 自动生成设置
		/// </summary>
		/// <param name="autogenerationWebInfo"></param>
		/// <returns></returns>
		public static BaseResult SaveAutogeneration(ShopAutogenerationWebInfo autogenerationWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				ShopAutogeneration autogeneration = ShopAutogenerationService.GetSingleShopAutogeneration(autogenerationWebInfo.ShopID);
				if (autogeneration != null) {
					autogeneration.IsAutoDown = autogenerationWebInfo.IsAutoDown;
					autogeneration.DownInterval = autogenerationWebInfo.DownInterval;
					autogeneration.CreateInterval = autogenerationWebInfo.CreateInterval;
					autogeneration.IsAutogeneration = autogenerationWebInfo.IsAutogeneration;
					autogeneration.GenerateInterval = autogenerationWebInfo.GenerateInterval;
					autogeneration.NotGenerated = string.Join(",", autogenerationWebInfo.NotGenerated);
					autogeneration.UpdatePerson = FormsAuth.GetUserCode();
					autogeneration.UpdateDate = DateTime.Now;
					int rowsAffected = ShopAutogenerationService.Update(autogeneration);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新自动生成设置失败！";
					}
				}
				else {
					autogeneration = new ShopAutogeneration();
					autogeneration.ShopID = autogenerationWebInfo.ShopID;
					autogeneration.IsAutoDown = autogenerationWebInfo.IsAutoDown;
					autogeneration.DownInterval = autogenerationWebInfo.DownInterval;
					autogeneration.CreateInterval = autogenerationWebInfo.CreateInterval;
					autogeneration.IsAutogeneration = autogenerationWebInfo.IsAutogeneration;
					autogeneration.GenerateInterval = autogenerationWebInfo.GenerateInterval;
					autogeneration.NotGenerated = string.Join(",", autogenerationWebInfo.NotGenerated);
					autogeneration.CreatePerson = FormsAuth.GetUserCode();
					autogeneration.CreateDate = DateTime.Now;
					int ID = ShopAutogenerationService.Add(autogeneration);
					if (ID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加自动生成设置失败！";
					}

				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "自动生成设置", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		#endregion

		#region 保存匹配快递设置

		/// <summary>
		/// 保存匹配快递设置
		/// </summary>
		/// <param name="autogenerationWebInfo"></param>
		/// <returns></returns>
		public static BaseResult SaveExpressSet(ShopExpressSetWebInfo expressSetWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					ShopExpressSetService.DelByShopID(expressSetWebInfo.ShopID, context);
					for (int i = 0; i < expressSetWebInfo.LogisticsID.Length; i++) {
						ShopExpressSet expressSet = new ShopExpressSet();
						expressSet.ShopID = expressSetWebInfo.ShopID;
						expressSet.ShippingType = expressSetWebInfo.ShippingType[i];
						expressSet.LogisticsID = expressSetWebInfo.LogisticsID[i];
						expressSet.CreatePerson = FormsAuth.GetUserCode();
						expressSet.CreateDate = DateTime.Now;
						int expressSetID = ShopExpressSetService.Add(expressSet, context);
						if (expressSetID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "匹配快递设置失败！";
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
				Sys.SaveErrorLog(ex, "匹配快递设置", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		#endregion

		#region 挂起订单

		/// <summary>
		/// 挂起订单
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="hangRemark"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult HandOrder(string erpOrderCode, string hangRemark, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
					if (ordbase.OrderStatus != (int)OrdBaseStatus.已发货 && ordbase.OrderStatus != (int)OrdBaseStatus.已取消) {
						ordbase.IsHang = 1;
						ordbase.HangRemark = hangRemark;
						int rowsAffected = OrdbaseService.Update(ordbase, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "挂起订单失败！";
						}
						else {
							#region 订单操作日志

							string msg = string.Format("挂起订单({0})", ordbase.ErpOrderCode);
							resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

							#endregion
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
				Sys.SaveErrorLog(ex, "挂起订单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 取消挂起

		/// <summary>
		/// 取消挂起
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult CancelHang(string erpOrderCode, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode);
					if (ordbase.IsHang == 1) {
						ordbase.IsHang = 0;
						ordbase.HangRemark = "";
						int rowsAffected = OrdbaseService.Update(ordbase, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "取消挂起失败！";
						}
						else {
							#region 订单操作日志

							string msg = string.Format("取消挂起({0})", ordbase.ErpOrderCode);
							resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

							#endregion
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
				Sys.SaveErrorLog(ex, "挂起订单", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 添加收退款

		/// <summary>
		/// 添加收退款
		/// </summary>
		/// <param name="accountsBill"></param>
		/// <param name="userCode"></param>
		/// <returns></returns>
		public static BaseResult AddAccountsBill(OrdaccountsBill accountsBill, string userCode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					resultInfo = AddAccountsBill(accountsBill, userCode, context);

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
				Sys.SaveErrorLog(ex, "添加收退款", userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 添加收退款
		/// </summary>
		/// <param name="accountsBill"></param>
		/// <param name="userCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult AddAccountsBill(OrdaccountsBill accountsBill, string userCode, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				int billID = OrdaccountsBillService.Add(accountsBill, context);
				if (billID == 0) {
					resultInfo.result = 0;
					if (accountsBill.BillType == 1)
						resultInfo.message = "添加收款失败！";
					else
						resultInfo.message = "添加退款失败！";

				}
				if (resultInfo.result == 1) {
					int rowsAffected = OrdbaseService.UpdateRealAmount(accountsBill.ErpOrderCode, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新订单实收金额失败！";
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);
			}
			return resultInfo;
		}

		#endregion

		#region 确认付款

		/// <summary>
		/// 确认付款
		/// </summary>
		/// <param name="accountsBill"></param>
		/// <param name="userCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult SavePayInfo(OrdaccountsBill accountBillWebInfo, string userCode) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					OrdaccountsBill accountBill = OrdaccountsBillService.GetSingleByBillNo(accountBillWebInfo.BillNo);
					if (accountBill.Status != 2) {
						accountBill.PaymentMethod = accountBillWebInfo.PaymentMethod;
						accountBill.PaymentAccount = accountBillWebInfo.PaymentAccount;
						accountBill.ReceivableAccount = accountBillWebInfo.ReceivableAccount;
						accountBill.TradingNumber = accountBillWebInfo.TradingNumber;
						accountBill.PayDate = accountBillWebInfo.PayDate;
						accountBill.Remark = accountBillWebInfo.Remark;
						accountBill.Status = 1;
						int rowsAffected = OrdaccountsBillService.Update(accountBill);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "付款失败！";
						}

						if (resultInfo.result == 1) {
							rowsAffected = OrdbaseService.UpdateRealAmount(accountBill.ErpOrderCode, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新订单实收金额失败！";
							}
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
				Sys.SaveErrorLog(ex, "确认付款", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 取消订单

		/// <summary>
		/// 取消订单
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult Cancel(string erpOrderCode, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					if (IsCanCancel(erpOrderCode, context)) {
						Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
						int orderStatus = ordbase.OrderStatus;
						ordbase.OrderStatus = (int)OrdBaseStatus.已取消;
						ordbase.CancelRemark = "";
						ordbase.CancelDate = DateTime.Now;
						ordbase.CancelPort = 0;
						int rowsAffected = OrdbaseService.Update(ordbase, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "取消订单失败！";
						}
						else {
							#region 订单操作日志

							string msg = string.Format("取消订单");
							resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context);

							#endregion
						}
						if (resultInfo.result == 1) {
							if (orderStatus == (int)OrdBaseStatus.待审核) {
								OrdoccupyService.DeleteByErpOrderCode(ordbase.ErpOrderCode, context);
							}
							else {
								List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundByErpOrderCode(erpOrderCode, context);
								foreach (var outbound in outboundList) {
									outbound.Status = (int)WarehouseOutboundStatus.已取消;
									outbound.CancelDate = DateTime.Now;
									outbound.CancelRemark = "";
									rowsAffected = WarehouseOutboundService.Update(outbound, context);
									if (rowsAffected == 0) {
										resultInfo.result = 0;
										resultInfo.message = "取消出库单[" + outbound.BillNo + "]失败！";
									}
									else {
										#region 订单操作日志

										string msg = string.Format("取消出库单");
										resultInfo = OrdlogManager.Save(userCode, userName, ordbase.ErpOrderCode, ordbase.OutOrderCode, msg, context, outbound.WarehouseCode, outbound.BillNo);

										#endregion
									}
									if (resultInfo.result == 1) {
										List<WarehouseOutboundPickItem> outboundPickItemList = WarehouseOutboundPickItemService.GetQueryManyByOutboundID(outbound.ID, context);
										foreach (var pickItem in outboundPickItemList) {
											if (pickItem.LocationID > 0) {
												rowsAffected = WarehouseProductsBatchService.DeductionZyNum(userCode, pickItem.ProductsBatchID, pickItem.Num, context);
												if (rowsAffected == 0) {
													resultInfo.result = 0;
													resultInfo.message = "出库单[" + outbound.BillNo + "]扣减批次占用失败！";
													break;
												}
												rowsAffected = WarehouseLocationProductsService.DeductionZyNum(userCode, pickItem.WarehouseCode, pickItem.ProductsSkuID, pickItem.LocationID, pickItem.ProductsBatchID, pickItem.Num, context);
												if (rowsAffected == 0) {
													resultInfo.result = 0;
													resultInfo.message = "出库单[" + outbound.BillNo + "]扣减库位占用失败！";
													break;
												}
											}
											else {
												rowsAffected = WarehouseBookingProductsSkuService.DeductionZyNum(userCode, pickItem.WarehouseCode, pickItem.ProductsSkuID, pickItem.Num, context);
												if (rowsAffected == 0) {
													resultInfo.result = 0;
													resultInfo.message = "出库单[" + outbound.BillNo + "]扣减预售占用失败！";
													break;
												}
											}
										}
									}
									else {
										break;
									}
									if (resultInfo.result == 1) {
										WarehouseOutboundPickItemService.DelByOutboundID(outbound.WarehouseCode, outbound.ID, context);
									}
								}
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "订单不能取消！";
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
				Sys.SaveErrorLog(ex, "取消订单", userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 是否可以取消
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool IsCanCancel(string erpOrderCode, IDbContext context = null) {
			bool flag = true;
			Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
			if (ordbase.OrderStatus == (int)OrdBaseStatus.待审核 || ordbase.OrderStatus == (int)OrdBaseStatus.发货中) {
				if (ordbase.OrderStatus == (int)OrdBaseStatus.发货中) {
					List<WarehouseOutbound> outboundList = WarehouseOutboundService.GetWarehouseOutboundByErpOrderCode(erpOrderCode, context);
					if (outboundList.Count != outboundList.Where(r => r.Status == (int)WarehouseOutboundStatus.待拣货).ToList().Count) {
						flag = false;
					}
				}
			}
			else {
				flag = false;
			}
			return flag;
		}
		#endregion

		#region 自动匹配仓库

		/// <summary>
		/// 自动匹配仓库
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable AutoMatchingWarehouse(string erpOrderCode, string userCode, string userName, IDbContext context = null) {
			DataTable warehouseDt = OrdbaseService.GetMatchingWarehouse(erpOrderCode);
			DataTable matchingDt = warehouseDt.Clone();
			try {
				if (warehouseDt.Rows.Count > 0) {
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
					foreach (DataRow warehouseDr in warehouseDt.Rows) {
						if (WarehouseAreaMapService.IswareaMap(ordbase.CityID.ToString(), warehouseDr["ID"].ToString(), context) != 0) {
							DataRow dr = matchingDt.NewRow();
							dr["ID"] = warehouseDr["ID"];
							dr["Code"] = warehouseDr["Code"];
							dr["Name"] = warehouseDr["Name"];
							matchingDt.Rows.Add(dr);
						}
					}
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "自动匹配仓库", userCode);
			}
			return matchingDt;
		}

		#endregion

		#region 分配仓库

		/// <summary>
		/// 分配仓库
		/// </summary>
		/// <param name="id"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static BaseResult DistributionWarehouse(int id, string userCode, string userName) {
			BaseResult resultInfo = new BaseResult();
			try {
				Ordbase ordbase = OrdbaseService.GetQuerySingleByID(id);
				DataTable dt = AutoMatchingWarehouse(ordbase.ErpOrderCode, userCode, userName);
				if (dt.Rows.Count > 0) {
					resultInfo = OutboundManager.CreateOutbound(userCode, userName, ordbase.ErpOrderCode, dt.Rows[0]["Code"].ToString());
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "分配仓库", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
