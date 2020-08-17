using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	public class OrditemManager {

		#region 添加商品

		/// <summary>
		/// 添加商品[手动]
		/// </summary>
		/// <param name="ordProductsSkuWebInfo"></param>
		/// <returns></returns>
		public static BaseResult AddItem(OrdProductsSkuWebInfo ordProductsSkuWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					int ordbaseID = 0;
					int shopID = ordProductsSkuWebInfo.ShopID;
					string erpOrderCode = ordProductsSkuWebInfo.ErpOrderCode;
					Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(erpOrderCode, context);
					if (ordbase == null) {
						#region 添加订单

						erpOrderCode = Sys.GetBillNo("");
						ordbase = new Ordbase();
						ordbase.ErpOrderCode = erpOrderCode;
						ordbase.ShopID = shopID;
						ordbase.CreateType = (int)OrdCreateType.手动;
						ordbase.CreateDate = DateTime.Now;
						ordbase.CreatePerson = FormsAuth.GetUserCode();
						resultInfo = OrdbaseManager.AddOrder(ordbase, FormsAuth.GetUserCode(), FormsAuth.GetUserName(), context);
						ordbaseID = ordbase.ID;

						#endregion

						ordProductsSkuWebInfo.ID = ordbaseID;
						ordProductsSkuWebInfo.ErpOrderCode = erpOrderCode;
					}
					else {
						ordbaseID = ordbase.ID;
					}

					if (resultInfo.result == 1) {
						for (int i = 0; i < ordProductsSkuWebInfo.ProductsSkuID.Length; i++) {
							int num = ZConvert.StrToInt(ordProductsSkuWebInfo.Num[i]);
							int productsSkuID = ZConvert.StrToInt(ordProductsSkuWebInfo.ProductsSkuID[i]);
							int productsID = ordProductsSkuWebInfo.ProductsID;
							int addType = ordProductsSkuWebInfo.AddType;
							if (num == 0) continue;
							int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(productsSkuID, 1);
							if (num > kfhNum) {
								resultInfo.result = 0;
								resultInfo.message = "商品库存不足";
								break;
							}
							OrdouterItem outerItem = new OrdouterItem();
							Orditem orditem = OrditemService.GetSingleOrditem(erpOrderCode, productsSkuID, addType, context);
							if (orditem == null) {
								#region 添加商品

								//获取商品相关信息
								Products products = ProductsService.GetSingleProducts(productsID, context);
								ProductsSku productsSku = ProductsSkuService.GetSingleProductsSku(productsSkuID, context);
								Brand brand = BrandService.GetSingleBrand(products.BrandID, context);
								Category category = CategoryService.GetSingleCategory(products.CategoryID, context);
								Syscode sysCode = SyscodeService.GetSyscodeByCode(products.MeasurementUnitID);

								orditem = new Orditem();
								orditem.ErpOrderCode = erpOrderCode;
								orditem.OrdbaseID = ordbaseID;
								orditem.ShopID = shopID;
								if (brand != null) {
									orditem.BrandID = brand.ID;
									orditem.BrandName = brand.Name;
								}
								if (category != null) {
									orditem.CategoryID = category.ID;
									orditem.CategoryName = category.Name;
								}
								if (sysCode != null) {
									orditem.Unit = sysCode.Text;
								}
								orditem.ProductsID = products.ID;
								orditem.ProductsName = products.Name;
								orditem.ProductsCode = products.Code;
								orditem.ProductsNo = products.No;
								orditem.ProductsWeight = products.Weight;
								orditem.TaxRate = products.TaxRate;
								orditem.MeasurementUnitID = products.MeasurementUnitID;
								orditem.ProductsNum = num;
								orditem.ProductsSkuID = productsSku.ID;
								orditem.ProductsSkuCode = productsSku.Code;
								orditem.ProductsSkuSaleprop = productsSku.Saleprop;
								if (ordProductsSkuWebInfo.IsOutOrder == 0) {
									if (addType == 0) {
										orditem.SellingPrice = productsSku.SellingPrice;
										orditem.ActualSellingPrice = productsSku.SellingPrice;
									}
								}
								else {
									outerItem = OrdouterItemService.GetQuerySingleByID(ordProductsSkuWebInfo.OrdouterItemID);
									orditem.OrdouterItemID = outerItem.ID;
									orditem.OutOrderCode = outerItem.OutOrderCode;
									orditem.ShopID = outerItem.ShopID;
									orditem.SellingPrice = outerItem.Price;
									orditem.ActualSellingPrice = outerItem.Payment;
								}
								orditem.CostPrice = productsSku.CostPrice;
								orditem.AddType = addType;
								orditem.CreateDate = DateTime.Now;
								orditem.CreatePerson = FormsAuth.GetUserCode();

								int orditemID = OrditemService.Add(orditem, context);
								if (orditemID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "添加商品失败！";
									break;
								}
								else {
									#region 订单操作日志

									string msg = string.Format("添加商品（SKU码：{0}，数量：{1}）", productsSku.Code, num);
									resultInfo = OrdlogManager.Save(FormsAuth.GetUserCode(), FormsAuth.GetUserName(), orditem.ErpOrderCode, orditem.OutOrderCode, msg, context);

									#endregion
								}

								//更新订单主表商品金额和数量
								if (resultInfo.result == 1) {
									int rowsAffected = OrdbaseService.UpdateOrdbaseAmount(orditem.ErpOrderCode, context);
									if (rowsAffected == 0) {
										resultInfo.result = 0;
										resultInfo.message = "更新订单主表商品金额和数量失败！";
									}
								}

								//添加下单占用
								if (resultInfo.result == 1) {
									resultInfo = AddOccupy(orditem.ErpOrderCode, orditemID, orditem.ProductsID, orditem.ProductsSkuID, orditem.ProductsNum, FormsAuth.GetUserCode(), context);
								}

								#endregion
							}
							else {
								#region 更新商品数量

								resultInfo = UpdateProductsNum(orditem, orditem.ProductsNum + num, FormsAuth.GetUserCode(), FormsAuth.GetUserName(), context);
								if (resultInfo.result == 0) {
									break;
								}

								#endregion
							}

							//更新外部订单添加状态
							if (ordProductsSkuWebInfo.IsOutOrder == 1) {
								outerItem.IsProductAddFin = 1;
								int rowsAffected = OrdouterItemService.Update(outerItem);
								if (rowsAffected == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新外部订单添加状态失败！";
								}
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
				Sys.SaveErrorLog(ex, "订单添加商品", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 添加商品[自动]
		/// </summary>
		/// <param name="ordouter"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult AddItem(Ordouter ordouter, string userCode, string userName, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				#region 添加订单

				Ordbase ordbase = new Ordbase();
				ordbase.ErpOrderCode = Sys.GetBillNo("");
				ordbase.OutOrderCode = ordouter.OutOrderCode;
				ordbase.OrdouterID = ordouter.ID;
				ordbase.ShopID = ordouter.ShopID;
				ordbase.OrderSource = ordouter.OrderSource;
				ordbase.CreateType = (int)OrdCreateType.API下载;
				ordbase.CreateDate = DateTime.Now;
				ordbase.CreatePerson = userCode;
				resultInfo = OrdbaseManager.AddOrder(ordbase, userCode, userName, context);

				#endregion

				if (resultInfo.result == 1) {
					List<OrdouterItem> itemList = OrdouterItemService.GetManyOrdouterItem(ordouter.ID, context);
					foreach (var item in itemList) {
						ProductsSku productsSku = ProductsSkuService.GetSingleProductsSku(item.ProductsSkuCode, context);
						if (item.IsRefund == 0) {
							if (productsSku == null) {
								item.ProductAddMsg = "找不到SKU";
							}
							else {
								int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(productsSku.ID, 1);
								if (item.ProductsNum > kfhNum) {
									item.ProductAddMsg = "商品库存不足";
								}
							}
						}
						if (string.IsNullOrEmpty(item.ProductAddMsg)) {
							#region 添加商品

							Products products = ProductsService.GetSingleProducts(productsSku.ProductsID, context);
							Brand brand = BrandService.GetSingleBrand(products.BrandID, context);
							Category category = CategoryService.GetSingleCategory(products.CategoryID, context);
							Syscode sysCode = SyscodeService.GetSyscodeByCode(products.MeasurementUnitID);

							Orditem orditem = new Orditem();
							orditem.OrdbaseID = ordbase.ID;
							orditem.ErpOrderCode = ordbase.ErpOrderCode;
							orditem.OutOrderCode = item.OutOrderCode;
							orditem.ShopID = item.ShopID;
							orditem.ChildOrderCode = item.ChildOrderCode;
							orditem.OrdouterItemID = item.ID;
							if (brand != null) {
								orditem.BrandID = brand.ID;
								orditem.BrandName = brand.Name;
							}
							if (category != null) {
								orditem.CategoryID = category.ID;
								orditem.CategoryName = category.Name;
							}
							if (sysCode != null) {
								orditem.Unit = sysCode.Text;
							}
							orditem.ProductsID = products.ID;
							orditem.ProductsName = products.Name;
							orditem.ProductsCode = products.Code;
							orditem.ProductsNo = products.No;
							orditem.ProductsWeight = products.Weight;
							orditem.MeasurementUnitID = products.MeasurementUnitID;
							orditem.TaxRate = products.TaxRate;
							orditem.ProductsNum = item.ProductsNum;
							orditem.ProductsSkuID = productsSku.ID;
							orditem.ProductsSkuCode = productsSku.Code;
							orditem.ProductsSkuSaleprop = productsSku.Saleprop;
							orditem.SellingPrice = item.Price;
							orditem.ActualSellingPrice = item.Payment;
							orditem.CostPrice = productsSku.CostPrice;
							orditem.DiscountAmount = Math.Truncate(item.DiscountFee / item.ProductsNum * 1000) / 1000;
							orditem.CreateDate = DateTime.Now;
							orditem.CreatePerson = userCode;

							int orditemID = OrditemService.Add(orditem, context);
							if (orditemID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "添加商品失败！";
								break;
							}
							else {
								#region 订单操作日志

								string msg = string.Format("添加商品（SKU码：{0}，数量：{1}）[自动]", orditem.ProductsSkuCode, orditem.ProductsNum);
								resultInfo = OrdlogManager.Save(userCode, userName, orditem.ErpOrderCode, orditem.OutOrderCode, msg, context);

								#endregion
							}

							//更新订单主表商品金额和数量
							if (resultInfo.result == 1) {
								int rowsAffected = OrdbaseService.UpdateOrdbaseAmount(orditem.ErpOrderCode, context);
								if (rowsAffected == 0) {
									resultInfo.result = 0;
									resultInfo.message = "更新订单主表商品金额和数量失败！";
								}
							}

							//添加下单占用
							if (resultInfo.result == 1) {
								resultInfo = AddOccupy(orditem.ErpOrderCode, orditemID, orditem.ProductsID, orditem.ProductsSkuID, orditem.ProductsNum, userCode, context);
							}

							#endregion
						}
						if (resultInfo.result == 1) {
							item.ErpOrderCode = ordbase.ErpOrderCode;
							item.IsProductAddFin = string.IsNullOrEmpty(item.ProductAddMsg) ? 1 : 0;
							item.ProductAddMsg = string.IsNullOrEmpty(item.ProductAddMsg) ? "添加成功" : item.ProductAddMsg;
							int rowsAffected = OrdouterItemService.Update(item, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新外部订单商品失败！";
								break;
							}
						}
					}
				}
				if (resultInfo.result == 1) {
					ordouter.ErpOrderCode = ordbase.ErpOrderCode;
					int rowsAffected = OrdouterService.Update(ordouter, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新外部订单失败！";
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 添加优惠

		/// <summary>
		/// 添加优惠
		/// </summary>
		/// <param name="ordProductsSkuWebInfo"></param>
		/// <returns></returns>
		public static BaseResult AddDiscount(OrddiscountWebInfo orddiscountWebInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					Orddiscount orddiscount = new Orddiscount();
					orddiscount.ErpOrderCode = orddiscountWebInfo.ErpOrderCode;
					orddiscount.OrdbaseID = orddiscountWebInfo.OrdbaseID;
					orddiscount.Type = orddiscountWebInfo.Type;
					if (orddiscount.Type == 0) {
						orddiscount.LibProductsSkuID = string.Join(",", orddiscountWebInfo.ProductsSkuID);
						orddiscount.LibProductsSkuCode = orddiscountWebInfo.LibProductsSkuCode;
					}
					orddiscount.Remark = orddiscountWebInfo.Remark;
					orddiscount.Amount = orddiscountWebInfo.Amount;
					orddiscount.CreateDate = DateTime.Now;
					orddiscount.CreatePerson = FormsAuth.GetUserCode();

					int orddiscountID = OrddiscountService.Add(orddiscount, context);
					if (orddiscountID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加优惠失败！";
					}
					else {
						#region 订单操作日志

						string msg = "";
						if (orddiscount.Type == 0) {
							msg = string.Format("添加优惠（关联商品：{0}，优惠金额：{1}）", orddiscount.LibProductsSkuCode, orddiscount.Amount);
						}
						else {
							msg = string.Format("添加优惠（订单包邮：{0}，邮费金额：{1}）", orddiscount.LibProductsSkuCode, orddiscount.Amount);
						}
						resultInfo = OrdlogManager.Save(FormsAuth.GetUserCode(), FormsAuth.GetUserName(), orddiscount.ErpOrderCode, "", msg, context);

						#endregion
					}

					if (orddiscount.Type == 0) {
						//更新订单商品优惠金额
						if (resultInfo.result == 1) {
							resultInfo = UpdateDiscount(orddiscount.ErpOrderCode, context);
						}

						//更新订单主表商品金额和数量
						if (resultInfo.result == 1) {
							int rowsAffected = OrdbaseService.UpdateOrdbaseAmount(orddiscount.ErpOrderCode, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新订单主表商品金额和数量失败！";
							}
						}
					}
					else {
						Ordbase ordbase = OrdbaseService.GetQuerySingleByErpOrderCode(orddiscountWebInfo.ErpOrderCode, context);
						if (ordbase.Freight == 0) {
							resultInfo.result = 0;
							resultInfo.message = "运费为0不需要包邮！";
						}
						else {
							ordbase.ReceivableAmount = ordbase.ReceivableAmount - ordbase.Freight;
							ordbase.Freight = 0;
							int rowsAffected = OrdbaseService.Update(ordbase, context);
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
				Sys.SaveErrorLog(ex, "订单添加优惠", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		#endregion

		#region 删除优惠

		/// <summary>
		/// 删除优惠
		/// </summary>
		/// <param name="discountID">优惠ID</param>
		/// <param name="IsUpdateProducts">是否更新商品优惠金额</param>
		/// <returns></returns>
		public static BaseResult DeleteDiscount(int discountID, bool IsUpdateProducts = true) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					resultInfo = DeleteDiscount(discountID, FormsAuth.GetUserCode(), FormsAuth.GetUserName(), IsUpdateProducts, context);

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
				Sys.SaveErrorLog(ex, "删除订单商品", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 删除优惠
		/// </summary>
		/// <param name="discountID"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="IsUpdateProducts"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult DeleteDiscount(int discountID, string userCode, string userName, bool IsUpdateProducts = true, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				Orddiscount orddiscount = OrddiscountService.GetQuerySingleByID(discountID, context);
				int rowsAffected = OrddiscountService.DelByID(discountID, context);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "删除优惠失败！";
				}
				else {
					#region 订单操作日志

					string msg = string.Format("删除优惠（关联商品：{0}，优惠金额：{1}）", orddiscount.LibProductsSkuCode, orddiscount.Amount);
					resultInfo = OrdlogManager.Save(userCode, userName, orddiscount.ErpOrderCode, "", msg, context);

					#endregion
				}

				//更新订单商品优惠金额
				if (resultInfo.result == 1 && IsUpdateProducts) {
					resultInfo = UpdateDiscount(orddiscount.ErpOrderCode, context);
				}

				//更新订单主表商品金额和数量
				if (resultInfo.result == 1 && IsUpdateProducts) {
					rowsAffected = OrdbaseService.UpdateOrdbaseAmount(orddiscount.ErpOrderCode, context);
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
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "删除优惠", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 更新订单商品优惠金额

		/// <summary>
		/// 更新订单商品优惠金额
		/// </summary>
		/// <param name="orddiscount"></param>
		/// <returns></returns>
		public static BaseResult UpdateDiscount(string erpOrderCode, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				List<Orditem> itemList = OrditemService.GetManyOrditem(erpOrderCode, context);
				List<Orddiscount> discounList = OrddiscountService.GetManyOrddiscount(erpOrderCode, context);

				//赠品不参与订单优惠
				itemList = (from i in itemList where i.AddType == 0 orderby i.ProductsNum descending, i.SellingPrice ascending select i).ToList<Orditem>();
				decimal totalPrice = itemList.Sum(i => i.SellingPrice * i.ProductsNum);

				#region 更新订单商品优惠金额

				foreach (var orditem in itemList) {
					orditem.DiscountAmount = 0;
				}

				foreach (var discoun in discounList) {
					if (discoun.Type == 0) {
						int i = 1;
						decimal discountAmount = 0;
						decimal tempDiscountAmount = 0;
						foreach (var orditem in itemList) {
							if (System.Text.RegularExpressions.Regex.IsMatch(discoun.LibProductsSkuID, "\\b" + orditem.ProductsSkuID.ToString() + "\\b")) {
								if (i == itemList.Count) {
									discountAmount = Math.Truncate((discoun.Amount - tempDiscountAmount) / orditem.ProductsNum * 1000) / 1000;
								}
								else {
									discountAmount = Math.Truncate(discoun.Amount * (orditem.SellingPrice * orditem.ProductsNum / totalPrice) / orditem.ProductsNum * 1000) / 1000;
								}
								orditem.DiscountAmount += discountAmount;
								tempDiscountAmount += discountAmount * orditem.ProductsNum;
								i++;
							}
						}
					}
				}

				int rowsAffected = 0;
				foreach (var item in itemList) {
					item.UpdateDate = DateTime.Now;
					item.UpdatePerson = FormsAuth.GetUserCode();
					rowsAffected = OrditemService.Update(item, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新订单商品优惠金额失败！";
						break;
					}
				}

				#endregion
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 更新订单商品数量

		/// <summary>
		/// 更新订单商品数量
		/// </summary>
		/// <param name="orditemID"></param>
		/// <param name="productsNum"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static BaseResult UpdateProductsNum(int orditemID, int productsNum,int type) {
			BaseResult resultInfo = new BaseResult();
			try {
				Orditem orditem = OrditemService.GetQuerySingleByID(orditemID);
				int kfhNum = ProductsSkuService.GetKfhNumByProductsSkuID(orditem.ProductsSkuID, 1);
				if (type == 1 && kfhNum < 1) {
					resultInfo.result = 0;
					resultInfo.message = "库存数量不足！";
				}
				if (resultInfo.result == 1 && orditem.ProductsNum != productsNum) {
					resultInfo = UpdateProductsNum(orditem, productsNum, FormsAuth.GetUserCode(), FormsAuth.GetUserName());
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "更新订单商品数量", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 更新订单商品数量
		/// </summary>
		/// <param name="orditem"></param>
		/// <param name="productsNum"></param>
		/// <param name="userCode"></param>
		/// <param name="userName"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult UpdateProductsNum(Orditem orditem, int productsNum, string userCode, string userName, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				orditem.ProductsNum = productsNum;
				orditem.UpdateDate = DateTime.Now;
				orditem.UpdatePerson = FormsAuth.GetUserCode();

				int rowsAffected = OrditemService.Update(orditem, context);
				if (rowsAffected == 0) {
					resultInfo.result = 0;
					resultInfo.message = "更新订单商品数量失败！";
				}
				else {
					#region 订单操作日志

					string msg = string.Format("更新商品（SKU码：{0}，数量：{1}）", orditem.ProductsSkuCode, orditem.ProductsNum);
					resultInfo = OrdlogManager.Save(userCode, userName, orditem.ErpOrderCode, orditem.OutOrderCode, msg, context);

					#endregion
				}

				//添加下单占用
				if (resultInfo.result == 1) {
					resultInfo = AddOccupy(orditem.ErpOrderCode, orditem.ID, orditem.ProductsID, orditem.ProductsSkuID, orditem.ProductsNum, userCode, context);
				}

				//更新订单优惠金额
				if (resultInfo.result == 1) {
					if (orditem.AddType == 0) {
						resultInfo = UpdateDiscount(orditem.ErpOrderCode, context);
					}
				}

				//更新订单主表商品金额和数量
				if (resultInfo.result == 1) {
					rowsAffected = OrdbaseService.UpdateOrdbaseAmount(orditem.ErpOrderCode, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新订单主表商品金额和数量失败！";
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 删除订单商品

		/// <summary>
		/// 删除订单商品
		/// </summary>
		/// <param name="orditemID"></param>
		/// <returns></returns>
		public static BaseResult DeleteItem(int orditemID, bool IsCheckDiscount = true) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					resultInfo = DeleteItem(orditemID, FormsAuth.GetUserCode(), FormsAuth.GetUserName(), IsCheckDiscount, context);

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
				Sys.SaveErrorLog(ex, "删除订单商品", FormsAuth.GetUserCode());
			}
			return resultInfo;
		}

		/// <summary>
		/// 删除订单商品
		/// </summary>
		/// <param name="orditemID"></param>
		/// <param name="IsCheckDiscount"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult DeleteItem(int orditemID, string userCode, string userName, bool IsCheckDiscount = true, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				Orditem orditem = OrditemService.GetQuerySingleByID(orditemID, context);
				if (IsCheckDiscount && OrdbaseService.GetQuerySingleByErpOrderCode(orditem.ErpOrderCode, context).OrderStatus != (int)OrdBaseStatus.未生成 && OrditemService.GetCount(orditem.ErpOrderCode, context) == 1) {
					resultInfo.result = 0;
					resultInfo.message = "最后一件商品部能删除！";
				
				}
				if (resultInfo.result == 1) {
					if (orditem.AddType == 0 && IsCheckDiscount) {
						if (OrddiscountService.GetManyOrddiscount(orditem.ErpOrderCode, orditem.ProductsSkuID, context).Count > 0) {
							resultInfo.result = 0;
							resultInfo.message = "请先删除关联商品的优惠信息！";
						}
					}
				}
				if (resultInfo.result == 1) {
					int rowsAffected = OrditemService.DelByID(orditemID, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "删除订单商品失败！";
					}
					else {
						#region 订单操作日志

						string msg = string.Format("删除商品（SKU码：{0}）", orditem.ProductsSkuCode);
						resultInfo = OrdlogManager.Save(userCode, userName, orditem.ErpOrderCode, orditem.OutOrderCode, msg, context);

						#endregion
					}
				}
				if (resultInfo.result == 1) {
					if (orditem.OrdouterItemID > 0) {
						OrdouterItem ordouterItem = OrdouterItemService.GetQuerySingleByID(orditem.OrdouterItemID, context);
						if (ordouterItem != null) {
							ordouterItem.IsProductAddFin = 0;
							int rowsAffected = OrdouterItemService.Update(ordouterItem, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "更新外部订单商品失败！";
							}
						}
					}
				}

				//更新订单主表商品金额和数量
				if (resultInfo.result == 1 && IsCheckDiscount) {
					int rowsAffected = OrdbaseService.UpdateOrdbaseAmount(orditem.ErpOrderCode, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新订单主表商品金额和数量失败！";
					}
				}

				//删除下单占用
				if (resultInfo.result == 1) {
					resultInfo = DeleteOccupy(orditemID, context);
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);
			}
			return resultInfo;
		}

		#endregion

		#region 添加下单占用

		/// <summary>
		/// 添加下单占用
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="orditemID"></param>
		/// <param name="productsID"></param>
		/// <param name="productsSkuID"></param>
		/// <param name="num"></param>
		/// <param name="userCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult AddOccupy(string erpOrderCode, int orditemID, int productsID, int productsSkuID, int num, string userCode, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				Ordoccupy ordoccupy = OrdoccupyService.GetSingleOrdoccupy(orditemID, context);
				if (ordoccupy == null) {
					ordoccupy = new Ordoccupy();
					ordoccupy.ErpOrderCode = erpOrderCode;
					ordoccupy.OrditemID = orditemID;
					ordoccupy.ProductsID = productsID;
					ordoccupy.ProductsSkuID = productsSkuID;
					ordoccupy.Num = num;
					ordoccupy.Remark = "下单占用";
					ordoccupy.CreateDate = DateTime.Now;
					ordoccupy.CreatePerson = userCode;

					int ordoccupyID = OrdoccupyService.Add(ordoccupy, context);
					if (ordoccupyID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加下单占用失败！";
					}
				}
				else {
					ordoccupy.Num = num;
					ordoccupy.UpdateDate = DateTime.Now;
					ordoccupy.UpdatePerson = userCode;

					int rowsAffected = OrdoccupyService.Update(ordoccupy, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "更新下单占用失败！";
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion

		#region 删除下单占用

		/// <summary>
		/// 删除下单占用
		/// </summary>
		/// <param name="orditemID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static BaseResult DeleteOccupy(int orditemID, IDbContext context = null) {
			BaseResult resultInfo = new BaseResult();
			try {
				Ordoccupy ordoccupy = OrdoccupyService.GetQuerySingleByID(orditemID, context);
				if (ordoccupy != null) {
					int rowsAffected = OrdoccupyService.Delete(orditemID, context);
					if (rowsAffected == 0) {
						resultInfo.result = 0;
						resultInfo.message = "删除下单占用失败！";
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message + ex.StackTrace);;
			}
			return resultInfo;
		}

		#endregion
	}
}
