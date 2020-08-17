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
	public class WarehouseProductsManager {

		#region 从商品库导入商品列表
		/// <summary>
		/// 添加商品信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult AddProductsInfo(string warehouseCode, List<int> productsIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime createDate = DateTime.Now;
					foreach (var _productsID in productsIDList) {
						//判断是否添加
						if (WarehouseProductsService.GetCount(warehouseCode, new List<int> { _productsID }, 0, context) > 0) {
							continue;
						}
						//添加商品信息
						Products products = ProductsService.GetSingleProducts(_productsID, context);
						WarehouseProducts warehouseProducts = new WarehouseProducts();
						warehouseProducts.WarehouseCode = warehouseCode;
						warehouseProducts.ProductsID = _productsID;
						warehouseProducts.ProductsStatus = products.Status;
						warehouseProducts.CreateDate = createDate;
						warehouseProducts.CreatePerson = FormsAuth.GetUserName();

						int warehouseProductsID = WarehouseProductsService.Add(warehouseProducts, context);
						if (warehouseProductsID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品编码[" + products.Code + "]导入仓库失败！";
							break;
						}
						else {
							List<ProductsSku> skuList = ProductsSkuService.GetManyProductsSku(_productsID, context);
							foreach (ProductsSku skuItem in skuList) {
								//添加商品SKU信息
								int _productsSkuID = skuItem.ID;
								WarehouseProductsSku warehouseProductsSku = new WarehouseProductsSku();
								warehouseProductsSku.WarehouseCode = warehouseCode;
								warehouseProductsSku.ProductsID = _productsID;
								warehouseProductsSku.ProductsSkuID = _productsSkuID;
								warehouseProductsSku.CreateDate = createDate;
								warehouseProductsSku.CreatePerson = FormsAuth.GetUserName();

								int warehouseProductsSkuID = WarehouseProductsSkuService.Add(warehouseProductsSku, context);
								if (warehouseProductsSkuID == 0) {
									resultInfo.result = 0;
									resultInfo.message = "SKU编码[" + skuItem.Code + "]导入仓库失败！";
									break;
								}
							}
						}

						if (resultInfo.result == 0) break;
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "仓库端商品库导入仓库", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 从商品列表删除商品
		/// <summary>
		/// 删除商品信息
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult DelProductsInfo(string warehouseCode, List<int> productsIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in productsIDList) {
						if (ProductsService.GetTotalNum(productsID, warehouseCode, context) > 0) {
								resultInfo.result = 0;
								resultInfo.message = "有库存的商品不能删除！";
								break;
						}
						int rowsAffected = WarehouseProductsService.Delete(warehouseCode, new List<int> { productsID }, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "删除商品失败！";
							break;
						}
						else {
							rowsAffected = WarehouseProductsSkuService.Delete(warehouseCode, new List<int> { productsID }, context);
							if (rowsAffected == 0) {
								resultInfo.result = 0;
								resultInfo.message = "删除商品SKU失败！";
								break;
							}

							//删除预售
							WarehouseBookingProductsSkuService.Delete(warehouseCode, productsID, context);
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "仓库端删除商品", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 添加预售商品
		/// <summary>
		/// 添加预售
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="inventoryModel">库存扣减模式 0：扣减 1：不扣减</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="skuList">SKU预售数量信息 </param>
		/// <returns></returns>
		public static BaseResult AddBookingProductsSku(string warehouseCode, int bookingModel, int productsID, IDictionary<int, int> skuList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime createDate = DateTime.Now;
					DateTime updateDate = createDate;
					WarehouseProducts warehouseProducts = WarehouseProductsService.GetSingleWarehouseProducts(warehouseCode, productsID, context);
					int rowsAffected = 0;
					if (warehouseProducts != null) {
						warehouseProducts.IsBooking = 1;
						warehouseProducts.BookingModel = bookingModel;
						warehouseProducts.UpdatePerson = FormsAuth.GetUserName();
						warehouseProducts.UpdateDate = updateDate;
						rowsAffected = WarehouseProductsService.Update(warehouseProducts, context);
					}
					if (rowsAffected > 0) {
						foreach (var skuID in skuList.Keys) {
							WarehouseBookingProductsSku warehouseBookingProductsSku = new WarehouseBookingProductsSku();
							warehouseBookingProductsSku.WarehouseCode = warehouseCode;
							warehouseBookingProductsSku.ProductsID = productsID;
							warehouseBookingProductsSku.BookingModel = bookingModel;
							warehouseBookingProductsSku.ProductsSkuID = skuID;
							warehouseBookingProductsSku.BookingNum = skuList[skuID];
							warehouseBookingProductsSku.CreateDate = createDate;
							warehouseBookingProductsSku.CreatePerson = FormsAuth.GetUserName();
							int warehouseBookingProductsSkuID = WarehouseBookingProductsSkuService.Add(warehouseBookingProductsSku, context);

							if (warehouseBookingProductsSkuID == 0) {
								resultInfo.result = 0;
								ProductsSku productsSku = ProductsSkuService.GetSingleProductsSku(skuID, context);
								resultInfo.message = "添加预售商品SKU码[" + productsSku.Code + "]失败！";
								break;
							}
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "添加预售商品失败！";
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "添加预售商品", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 取消预售商品
		/// <summary>
		/// 取消预售
		/// </summary>
		/// <param name="WarehouseCode">仓库编码</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <returns></returns>
		public static BaseResult CancelBookingProducts(string warehouseCode, List<int> productsIDList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in productsIDList) {
						List<WarehouseBookingProductsSku> bookingList = WarehouseBookingProductsSkuService.GetManyWarehouseBookingProductsSku(warehouseCode, productsID, context);
						foreach (var item in bookingList) {
							if (item.ZyNum > 0) {
								resultInfo.result = 0;
								resultInfo.message = "预售占用为0的商品才可以取消！";
							}
						}
						if (resultInfo.result == 1) {
							WarehouseProducts warehouseProducts = WarehouseProductsService.GetSingleWarehouseProducts(warehouseCode, productsID, context);
							warehouseProducts.IsBooking = 0;
							warehouseProducts.BookingModel = 0;
							warehouseProducts.UpdatePerson = FormsAuth.GetUserName();
							warehouseProducts.UpdateDate = DateTime.Now;
							int rowsAffected = WarehouseProductsService.Update(warehouseProducts, context);
							if (rowsAffected > 0) {
								rowsAffected = WarehouseBookingProductsSkuService.Delete(warehouseCode, productsID, context);
							}
							else {
								resultInfo.result = 0;
								Products products = ProductsService.GetSingleProducts(productsID, context);
								resultInfo.message = "商品编码[" + products.Code + "]取消预售失败！";
								break;
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "取消预售商品", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 修改预售数量
		/// <summary>
		/// 修改预售数量
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="skuList">SKU预售数量信息 </param>
		/// <returns></returns>
		public static BaseResult UpdateBookingNum(string warehouseCode, int productsID, IDictionary<int, int> skuList) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					DateTime updateDate = DateTime.Now;
					foreach (var skuID in skuList.Keys) {
						WarehouseBookingProductsSku warehouseBookingProductsSku = WarehouseBookingProductsSkuService.GetSingleWarehouseBookingProductsSku(warehouseCode, productsID, skuID);
						warehouseBookingProductsSku.BookingNum = skuList[skuID];
						warehouseBookingProductsSku.UpdatePerson = FormsAuth.GetUserName();
						warehouseBookingProductsSku.UpdateDate = updateDate;
						int rowsAffected = WarehouseBookingProductsSkuService.Update(warehouseBookingProductsSku, context);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = "修改预售数量失败！";
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "修改预售数量", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion

		#region 根据商品SKUID将预售占用转为库位占用

		/// <summary>
		/// 根据商品SKUID将预售占用转为库位占用
		/// </summary>
		/// <param name="userCode">操作帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <param name="ordItemID">出库单明细表主键ID 如果有传且大于0，则只将该ID的预售占用转为库位占用</param>
		/// <returns></returns>
		public static BaseResult BookingZyToLocationZy(string userCode, string warehouseCode, int productsSkuID, IDbContext context, int ordItemID = 0) {
			BaseResult resultInfo = new BaseResult();
			try {
				List<WarehouseOutboundPickItem> pickItemList = WarehouseOutboundPickItemService.GetBookingPickItemList(warehouseCode, productsSkuID, context, ordItemID);
				List<WarehouseLocationProducts> locationProductsList = WarehouseLocationProductsService.GetManyLocationProducts(warehouseCode, productsSkuID, context);
				foreach (var pickItem in pickItemList) {					
					foreach (var locationProducts in locationProductsList) {
						int kfhNum = locationProducts.KyNum - locationProducts.ZyNum;
						int currentPickNum = kfhNum < pickItem.Num ? kfhNum : pickItem.Num;
						#region 增加库位拣货明细
						WarehouseOutboundPickItem warehouseOutboundPickItem = new WarehouseOutboundPickItem();
						warehouseOutboundPickItem.WarehouseCode = warehouseCode;
						warehouseOutboundPickItem.ErpOrderCode = pickItem.ErpOrderCode;
						warehouseOutboundPickItem.OrditemID = pickItem.OrditemID;
						warehouseOutboundPickItem.OutboundID = pickItem.OutboundID;
						warehouseOutboundPickItem.OutboundBillNo = pickItem.OutboundBillNo;
						warehouseOutboundPickItem.ProductsID = pickItem.ProductsID;
						warehouseOutboundPickItem.ProductsCode = pickItem.ProductsCode;
						warehouseOutboundPickItem.ProductsName = pickItem.ProductsName;
						warehouseOutboundPickItem.ProductsNo = pickItem.ProductsNo;
						warehouseOutboundPickItem.ProductsSkuID = pickItem.ProductsSkuID;
						warehouseOutboundPickItem.ProductsSkuCode = pickItem.ProductsSkuCode;
						warehouseOutboundPickItem.ProductsSkuSaleprop = pickItem.ProductsSkuSaleprop;
						warehouseOutboundPickItem.LocationID = locationProducts.LocationID;
						WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(locationProducts.LocationID, context);
						warehouseOutboundPickItem.LocationCode = warehouseLocation.Code;
						warehouseOutboundPickItem.LocationName = warehouseLocation.Name;
						warehouseOutboundPickItem.ProductsBatchID = locationProducts.ProductsBatchID;
						warehouseOutboundPickItem.ProductsBatchCode = locationProducts.ProductsBatchCode;
						warehouseOutboundPickItem.ActualSellingPrice = pickItem.ActualSellingPrice;
						warehouseOutboundPickItem.Num = currentPickNum;
						warehouseOutboundPickItem.CreatePerson = userCode;
						warehouseOutboundPickItem.CreateDate = DateTime.Now;
						int pickItemID = WarehouseOutboundPickItemService.Add(warehouseOutboundPickItem);
						if (pickItemID == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]增加库位拣货明细失败！";
							break;
						}
						#endregion
						#region 增加库位和批次占用
						int count = WarehouseLocationProductsService.IncreaseZyNum(userCode, warehouseCode, productsSkuID, locationProducts.LocationID, locationProducts.ProductsBatchID, currentPickNum, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]增加库位占用失败！";
							break;
						}
						count = WarehouseProductsBatchService.IncreaseZyNum(userCode, locationProducts.ProductsBatchID, currentPickNum, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]增加批次占用失败！";
							break;
						}
						#endregion
						#region 更新预售拣货明细数量
						pickItem.Num -= currentPickNum;
						pickItem.UpdatePerson = userCode;
						pickItem.UpdateDate = DateTime.Now;
						count = WarehouseOutboundPickItemService.Update(pickItem, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]更新预售拣货明细数量失败！";
							break;
						}
						#endregion
						#region 扣减预售占用
						count = WarehouseBookingProductsSkuService.DeductionZyNum(userCode, warehouseCode, productsSkuID, currentPickNum, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]扣减预售占用失败！";
							break;
						}
						#endregion
						#region 增加预售冲抵
						count = WarehouseBookingProductsSkuService.IncreaseCdNum(userCode, warehouseCode, productsSkuID, currentPickNum, context);
						if (count == 0) {
							resultInfo.result = 0;
							resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]增加预售冲抵失败！";
							break;
						}
						#endregion
						#region 预售拣货明细数量为0则删除
						if (pickItem.Num == 0) {
							count = WarehouseOutboundPickItemService.DelByID(pickItem.ID, context);
							if (count == 0) {
								resultInfo.result = 0;
								resultInfo.message = "商品SKU码[" + pickItem.ProductsSkuCode + "]删除预售拣货明细失败！";
							}
							//当前拣货明细转换完成，继续转换下一条
							break;
						}
						#endregion
					}
					if (resultInfo.result != 1) {
						break;
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "商品SKUID[" + productsSkuID + "]预售占用转库位占用", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 商品上下架

		/// <summary>
		/// 商品上下架
		/// </summary>
		/// <param name="warehouseCode">仓库编码 如果传空值，则更新所有仓库</param>
		/// <param name="productsIDList">商品ID列表</param>
		/// <param name="productsStatus">商品销售状态 销售中=1 仓库中=2 </param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static BaseResult UpdateProductsStatus(string warehouseCode, List<int> productsIDList, int productsStatus) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					foreach (var productsID in productsIDList) {
						int rowsAffected = WarehouseProductsService.UpdateProductsStatus(FormsAuth.GetWarehouseCode(), new List<int> { productsID }, productsStatus);
						if (rowsAffected == 0) {
							resultInfo.result = 0;
							resultInfo.message = productsStatus == (int)ProductsStatus.销售中 ? "上架失败！" : "下架失败！";
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
				resultInfo.result = 0;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "仓库端商品上下架", FormsAuth.GetUserCode());
			}

			return resultInfo;
		}

		#endregion
	}
}
