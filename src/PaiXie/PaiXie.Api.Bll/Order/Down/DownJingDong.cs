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
	/// 下载京东订单
	/// </summary>
	public class DownJingDong : DownOrder {
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
				downParam.Url = ZConfig.GetConfigString("JingDong_Url");
				downParam.PageSize = ZConfig.GetConfigInt("JingDong_PageSize");
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
										//BaseResult resultInfo = AddOrdouter(orderInfo);

										//if (resultInfo.result == 1) {
										//	finshCount++;
										//}
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
	}
}
