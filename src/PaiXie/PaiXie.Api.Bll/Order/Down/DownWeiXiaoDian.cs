using FluentData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaiXie.Core;
using PaiXie.Core.Enum;
using PaiXie.Data;
using PaiXie.Data.PXResponseOrder;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	/// <summary>
	/// 下载微小店订单
	/// </summary>
	public class DownWeiXiaoDian : DownOrder {
		public override void Download() {
			try {
				Shop shop = ShopService.GetSingleShop(downParam.ShopID);
				if (shop == null) {
					return;
				}
				if (shop.AppKey == "" || shop.AppSecret == "" || shop.AppSession == "") {
					return;
				}

				bool hasNext = true;
				string appKey = shop.AppKey;
				string appSecret = shop.AppSecret;
				string appSession = shop.AppSession;
				downParam.Url = ZConfig.GetConfigString("WeiXiaoDian_Url");
				downParam.PageSize = ZConfig.GetConfigInt("WeiXiaoDian_PageSize");
				int totalCount = 0;
				int finshCount = 0;
				while (hasNext) {
                    #region 下载订单列表

					IDictionary<string, string> dic = new Dictionary<string, string>();
					dic.Add("api_key", appKey);
					dic.Add("api_secret", appSecret);
					dic.Add("api_signkey", appSession);
					dic.Add("method", "GetOrderList");
					dic.Add("page_no", downParam.PageNo.ToString());
					dic.Add("page_size", downParam.PageSize.ToString());
					dic.Add("datetype", downParam.DateType.ToString());
					dic.Add("start_date", downParam.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
					dic.Add("end_date", downParam.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
					dic.Add("orderstatus", "2");
					string rspStr = PXinterface.GetPost(downParam.Url, dic);
					OrderRespone orderRespone = JsonConvert.DeserializeObject<OrderRespone>(rspStr);

					#endregion

					//更新任务总条数
					string reqStr = "url:" + downParam.Url + " param:" + JsonConvert.SerializeObject(dic, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
					try {
						totalCount = ZConvert.StrToInt(orderRespone.pageinfo.records);
					}
					catch { }
					ShopTaskService.UpdateTotalCount(downParam.TaskID, totalCount, reqStr, rspStr);

					if (orderRespone.status == "200") {
						foreach (var orderInfo in orderRespone.recordset) {
							int orderCount = OrdouterService.GetCount(orderInfo.order_id, downParam.ShopID);
							if (orderCount == 0) {
								try {
									#region 下载订单详情

									dic.Clear();
									dic.Add("api_key", appKey);
									dic.Add("api_secret", appSecret);
									dic.Add("api_signkey", appSession);
									dic.Add("method", "GetOrderInfo");
									dic.Add("order_id", orderInfo.order_id);
									rspStr = PaiXie.Core.PXinterface.GetPost(downParam.Url, dic);
									OrderRespone orderInfoRespone = JsonConvert.DeserializeObject<OrderRespone>(rspStr);
									if (orderInfoRespone.status == "200") {
										orderInfo.receiver_name = orderInfoRespone.record.receiver_name;
										orderInfo.receiver_state = orderInfoRespone.record.receiver_state;
										orderInfo.receiver_city = orderInfoRespone.record.receiver_city;
										orderInfo.receiver_district = orderInfoRespone.record.receiver_district;
										orderInfo.receiver_address = orderInfoRespone.record.receiver_address;
										orderInfo.receiver_mobile = orderInfoRespone.record.receiver_mobile;
										orderInfo.receiver_zip = orderInfoRespone.record.receiver_zip;
										orderInfo.itemlist = orderInfoRespone.record.itemlist;

										//添加外部订单
										BaseResult resultInfo = AddOrdouter(orderInfo);

										if (resultInfo.result == 1) {
											finshCount++;
										}
									}

									#endregion
								}
								catch (Exception ex) {
									Sys.SaveErrorLog(ex, "下载微小店订单[" + (downParam.IsAuto == 0 ? "手动" : "自动") + "]", downParam.UserCode);
								}
							}
							
							//更新进度
							ShopTaskService.UpdateFinshCount(downParam.TaskID);
						}
						if (orderRespone.pageinfo.page == orderRespone.pageinfo.pages) {
							hasNext = false;
						}
						else {
							downParam.PageNo++;
						}
					}
					else {
						hasNext = false;
					}
				}
				if (finshCount != totalCount) {
					ShopTaskService.UpdateFinshCount(downParam.TaskID, finshCount, (int)ShopTaskStatus.已结束);
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "下载微小店订单[" + (downParam.IsAuto == 0 ? "手动" : "自动") + "]", downParam.UserCode);
				ShopTaskService.UpdateStatus(downParam.TaskID, (int)ShopTaskStatus.已结束);
			}
		}

		/// <summary>
		/// 添加外部订单
		/// </summary>
		/// <param name="orderInfo"></param>
		private BaseResult AddOrdouter(OrderInfo orderInfo) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					//添加外部订单
					Ordouter ordouter = new Ordouter();
					ordouter.OutOrderCode = orderInfo.order_id;
					ordouter.OrderSource = (int)ThirdApi.微小店;
					ordouter.ShopID = downParam.ShopID;
					ordouter.BuyName = orderInfo.receiver_name;
					ordouter.BuyMtel = orderInfo.receiver_mobile;
					ordouter.BuyPostCode = orderInfo.receiver_zip;
					ordouter.BuyMessage = orderInfo.buyer_memo;
					ordouter.SellerRemark = orderInfo.seller_memo;
					ordouter.BuyProvince = orderInfo.receiver_state;
					ordouter.BuyCity = orderInfo.receiver_city;
					ordouter.BuyDistrict = orderInfo.receiver_district;
					ordouter.BuyAddressDetail = orderInfo.receiver_address;
					ordouter.BuyAddr = ordouter.BuyProvince + ordouter.BuyCity + ordouter.BuyDistrict + ordouter.BuyAddressDetail;
					ordouter.Created = ZConvert.StrToDateTime(orderInfo.created, DateTime.Now);
					ordouter.Modified = ZConvert.StrToDateTime(orderInfo.modified, ordouter.Created);
					ordouter.PayDate = ZConvert.StrToDateTime(orderInfo.pay_time, DateTime.Now);
					ordouter.ShippingType = (int)OrdShippingType.其他;
					ordouter.OrderDiscount = Math.Abs(ZConvert.StrToDecimal(orderInfo.discount_fee));
					ordouter.ReceivableAmount = ZConvert.StrToDecimal(orderInfo.total_amount);
					ordouter.RealAmount = ZConvert.StrToDecimal(orderInfo.payment);
					ordouter.UncollectedeAmount = ordouter.ReceivableAmount - ordouter.RealAmount;
					ordouter.Freight = ZConvert.StrToDecimal(orderInfo.post_fee);
					ordouter.PlatformFee = ZConvert.StrToDecimal(orderInfo.platform_fee);
					ordouter.ProductsAmount = ZConvert.StrToDecimal(orderInfo.item_fee) - Math.Abs(ZConvert.StrToDecimal(orderInfo.discount_fee));
					ordouter.CreateDate = DateTime.Now;
					ordouter.CreatePerson = downParam.UserCode;
					int ordouterID = OrdouterService.Add(ordouter, context);
					ordouter.ID = ordouterID;
					if (ordouterID == 0) {
						resultInfo.result = 0;
						resultInfo.message = "添加外部订单[" + ordouter.OutOrderCode + "]失败！";
					}
					else {
						foreach (var item in orderInfo.itemlist) {
							//添加外部订单商品
							OrdouterItem ordouterItem = new OrdouterItem();
							ordouterItem.OutOrderCode = orderInfo.order_id;
							ordouterItem.OrdouterID = ordouterID;
							ordouterItem.ShopID = downParam.ShopID;
							ordouterItem.ProductsName = item.title;
							ordouterItem.ProductsCode = item.outer_id;
							ordouterItem.ProductsSkuCode = item.outer_sku_id;
							ordouterItem.ProductsNum = ZConvert.StrToInt(item.nums);
							ordouterItem.Price = ZConvert.StrToDecimal(item.price);
							ordouterItem.DiscountFee = ZConvert.StrToDecimal(item.discount_fee);
							ordouterItem.Payment = ZConvert.StrToDecimal(item.money);
							ordouterItem.RefundStatus = item.aftersalesstatus;
							if (ordouterItem.RefundStatus == "2" || ordouterItem.RefundStatus == "3" || ordouterItem.RefundStatus == "5" || ordouterItem.RefundStatus == "6") {
								ordouterItem.ProductAddMsg = "申请退款";
								ordouterItem.IsRefund = 1;
							}
							ordouterItem.CreateDate = DateTime.Now;
							ordouterItem.CreatePerson = downParam.UserCode;
							int outerItemID = OrdouterItemService.Add(ordouterItem, context);
							ordouterItem.ID = outerItemID;
							if (outerItemID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "添加外部订单[" + ordouterItem.OutOrderCode + "]商品SKU[" + ordouterItem.ProductsSkuCode + "]失败！";
								break;
							}
						}
					}
					if (resultInfo.result == 1) {
						#region 订单操作日志

						string msg = string.Format("API下载订单{0}", (downParam.IsAuto == 0 ? "" : "[自动]"));
						resultInfo = OrdlogManager.Save(downParam.UserCode, "", "", ordouter.OutOrderCode, msg, context);

						#endregion
					}
					if (resultInfo.result == 1) {
						resultInfo = OrditemManager.AddItem(ordouter, downParam.UserCode, "", context);
					}

					if (resultInfo.result == 1) {
						context.Commit();
					}
					else {
						context.Rollback();
						Sys.SaveErrorLog(new Exception(resultInfo.message), "下载微小店订单" + orderInfo.order_id + "[" + (downParam.IsAuto == 0 ? "手动" : "自动") + "]", downParam.UserCode);
					}
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "下载微小店订单" + orderInfo.order_id + "[" + (downParam.IsAuto == 0 ? "手动" : "自动") + "]", downParam.UserCode);
			}

			return resultInfo;
		}
	}
}
