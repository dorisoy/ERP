using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
using System.Collections;
using Newtonsoft.Json;
using PaiXie.Utils;
using System.Data;
using Newtonsoft.Json.Converters;
namespace PaiXie.Api.Bll {
	
	/// <summary>
	/// 库位管理
	/// </summary>
	public class LocationManager {

		#region 删除库区

		/// <summary>
		///删除库区
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="locationIDs">库区ID字符串，多个以半角逗号分隔</param>
		/// <returns></returns>
		public static BaseResult DelTopLocation(string userCode, string warehouseCode, string position, string target, string buttonName, string locationIDs) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					string[] arrLocationID = locationIDs.Split(',');
					foreach (var item in arrLocationID) {
						int topLocationID = ZConvert.StrToInt(item);
						WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(topLocationID, context);
						int productsNum = WarehouseLocationProductsService.GetProductsNum(topLocationID, context);
						bool calDel = productsNum == 0;
						if (calDel) {
							bool tempFlag = WarehouseLocationService.DelByID(topLocationID, context) > 0;
							if (tempFlag) {
								string oldMessage = JsonConvert.SerializeObject(warehouseLocation, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
								Sys.WriteSyslog(position, target, buttonName, oldMessage, string.Empty, (int)SyslogList.库位, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
								WarehouseLocationService.DelLocationByTopLocationID(topLocationID, context);
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = warehouseLocation.Name + " 删除失败，可能已经被删除！";
								break;
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = warehouseLocation.Name + " 中还有商品，不能删除！";
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
				Sys.SaveErrorLog(ex, "删除库区", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存库区

		/// <summary>
		/// 保存库区
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="obj">库区信息实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string userCode, string warehouseCode, string position, string target, string buttonName, WarehouseLocationInfo obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					string oldMessage = string.Empty;
					string newMessage = string.Empty;
					if (obj.ID == 0) {

						#region 添加库区

						WarehouseLocation objWarehouseLocation = new WarehouseLocation();
						objWarehouseLocation.Name = obj.Name;
						objWarehouseLocation.Code = obj.Code;
						objWarehouseLocation.StructName = string.Empty;
						objWarehouseLocation.StructCode = string.Empty;
						objWarehouseLocation.TypeID = obj.TypeID;
						objWarehouseLocation.IsEnable = (int)IsEnable.是;
						objWarehouseLocation.WarehouseCode = warehouseCode;
						objWarehouseLocation.CreatePerson = userCode;
						objWarehouseLocation.CreateDate = DateTime.Now;
						int id = WarehouseLocationService.Add(objWarehouseLocation, context);

						#endregion
						if (id > 0) {
							newMessage = JsonConvert.SerializeObject(objWarehouseLocation, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
							Sys.WriteSyslog(position, target, buttonName, string.Empty, newMessage, (int)SyslogList.库位, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
							if (obj.StructCount != null) {
								//根据库区结构创建库位
								int[] arrStructCount = obj.StructCount;
								string[] arrStructName = obj.StructName;
								string[] arrStructCode = obj.StructCode;
								int index = 0;
								Save(arrStructCount, arrStructName, arrStructCode, warehouseCode, userCode, obj.TypeID, index, id, obj.Name, obj.Code, context);
							}
							else {
								//只添加库区，库位后面手动添加
								//resultInfo.result = 0;
								//resultInfo.message = "添加库区信息失败，必须选择库区结构！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "添加库区信息失败！";
						}
					}
					else {
						WarehouseLocation objWarehouseLocation = new WarehouseLocation();
						objWarehouseLocation.ID = obj.ID;
						objWarehouseLocation.Code = obj.Code;
						objWarehouseLocation.Name = obj.Name;
						objWarehouseLocation.TypeID = obj.TypeID;
						objWarehouseLocation.UpdatePerson = userCode;
						objWarehouseLocation.UpdateDate = DateTime.Now;
						bool tempFlag = WarehouseLocationService.Update(objWarehouseLocation, out oldMessage, out newMessage, context) > 0;
						if (!tempFlag) {
							resultInfo.result = 0;
							resultInfo.message = "修改库区信息失败！";
						}
						else {
							Sys.WriteSyslog(position, target, buttonName, string.Empty, newMessage, (int)SyslogList.库位, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
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
				Sys.SaveErrorLog(ex, "保存库区信息", userCode);
			}
			return resultInfo;
		}

		/// <summary>
		/// 递归保存库位
		/// </summary>
		/// <param name="arrStructCount">结构数量数组</param>
		/// <param name="arrStructName">结构名称数组</param>
		/// <param name="arrStructCode">结构代码数组</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="userCode">创建人</param>
		/// <param name="typeID">库区类型</param>
		/// <param name="index">当前循环下标</param>
		/// <param name="parentID">父级ID</param>
		/// <param name="parentName">父级名称</param>
		/// <param name="parentCode">父级代码</param>
		/// <param name="context">数据库连接</param>
		private static void Save(int[] arrStructCount, string[] arrStructName, string[] arrStructCode, string warehouseCode, string userCode, int typeID, int index, int parentID, string parentName, string parentCode, IDbContext context) {
			int structCount = arrStructCount[index];
			index++;
			for (int i = 0; i < structCount; i++) {
				string currentName = parentName + (i + 1) + arrStructName[index - 1];
				string currentCode = parentCode + (i + 1) + arrStructCode[index - 1];
				if (index < arrStructCount.Length) {
					Save(arrStructCount, arrStructName, arrStructCode, warehouseCode, userCode, typeID, index, parentID, currentName, currentCode, context);
				}
				else {
					WarehouseLocation objWarehouseLocation = new WarehouseLocation();
					objWarehouseLocation.Name = currentName;
					objWarehouseLocation.Code = currentCode;
					objWarehouseLocation.StructName = arrStructName[index - 1];
					objWarehouseLocation.StructCode = arrStructCode[index - 1];
					objWarehouseLocation.TypeID = typeID;
					objWarehouseLocation.IsEnable = (int)IsEnable.是;
					objWarehouseLocation.ParentID = parentID;
					objWarehouseLocation.WarehouseCode = warehouseCode;
					objWarehouseLocation.CreatePerson = userCode;
					objWarehouseLocation.CreateDate = DateTime.Now;
					WarehouseLocationService.Add(objWarehouseLocation, context);
				}
			}
		}
		#endregion

		#region 删除库位

		/// <summary>
		///删除库位
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="position">位置 例： api/mms/send</param>
		/// <param name="target">名称 例： 菜单管理</param>
		/// <param name="buttonName">事件名称 例：修改</param>
		/// <param name="locationIDs">库位ID字符串，多个以半角逗号分隔</param>
		/// <returns></returns>
		public static BaseResult DeleteChild(string userCode, string warehouseCode, string position, string target, string buttonName, string locationIDs) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					string[] arrLocationID = locationIDs.Split(',');
					foreach (var item in arrLocationID) {
						int locationID = ZConvert.StrToInt(item);
						List<int> idList = new List<int>();
						idList.Add(locationID);
						int productsNum = WarehouseLocationProductsService.GetProductsNum(idList, context);
						bool calDel = productsNum == 0;
						WarehouseLocation warehouseLocation = WarehouseLocationService.GetQuerySingleByID(locationID, context);
						if (calDel) {
							bool tempFlag = WarehouseLocationService.DelByID(locationID, context) > 0;
							if (tempFlag) {
								WarehouseLocationProductsService.DelByLocationID(locationID, context);
								string oldMessage = JsonConvert.SerializeObject(warehouseLocation, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
								Sys.WriteSyslog(position, target, buttonName, oldMessage, string.Empty, (int)SyslogList.库位, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
							}
							else {
								resultInfo.result = 0;
								resultInfo.message = warehouseLocation.Name + " 删除失败，可能已经被删除！";
								break;

							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = warehouseLocation.Name + " 中还有商品，不能删除！";
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
				Sys.SaveErrorLog(ex, "删除库区", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存库位信息

		public static BaseResult Save(string userCode, string warehouseCode, string position, string target, string buttonName, WarehouseLocation obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				if (obj.ID == 0) {
					obj.WarehouseCode = warehouseCode;
					obj.IsEnable = (int)IsEnable.是;
					obj.CreatePerson = userCode;
					obj.CreateDate = DateTime.Now;
					bool tempFlag = WarehouseLocationService.Add(obj) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "添加库位失败！";
					}
				}
				else {
					using (IDbContext context = Db.GetInstance().Context()) {
						context.UseTransaction(true);
						string oldMessage = string.Empty;
						string newMessage = string.Empty;
						WarehouseLocation objWarehouseLocation = WarehouseLocationService.GetQuerySingleByID(obj.ID, context);
						objWarehouseLocation.Code = obj.Code;
						objWarehouseLocation.Name = obj.Name;
						objWarehouseLocation.UpdatePerson = userCode;
						objWarehouseLocation.UpdateDate = DateTime.Now;
						bool tempFlag = WarehouseLocationService.Update(objWarehouseLocation, out oldMessage, out newMessage, context) > 0;
						if (!tempFlag) {
							resultInfo.result = 0;
							resultInfo.message = "修改库位失败！";
						}
						else {
							Sys.WriteSyslog(position, target, buttonName, string.Empty, newMessage, (int)SyslogList.库位, string.Empty, string.Empty, string.Empty, (int)ProjectType.仓库端, warehouseCode, context);
						}
						if (resultInfo.result == 1) {
							context.Commit();
						}
						else {
							context.Rollback();
						}
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存库位信息", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 查看库位商品库存信息

		/// <summary>
		/// 查看库位商品库存信息
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <returns></returns>
		public static List<LocationProductsKucInfo> GetLocationProductsKucInfo(int locationID) {
			List<LocationProductsKucInfo> skuInfoList = new List<LocationProductsKucInfo>(); ;
			try {
				DataTable dt = WarehouseLocationService.GetLocationProductsKucInfo(locationID);
				foreach (DataRow dr in dt.Rows) {
					LocationProductsKucInfo skuInfo = new LocationProductsKucInfo();
					skuInfo.ProductsCode = dr["ProductsCode"].ToString();
					skuInfo.ProductsName = dr["ProductsName"].ToString();
					skuInfo.Saleprop = dr["Saleprop"].ToString();
					skuInfo.ProductsSkuCode = dr["ProductsSkuCode"].ToString();
					skuInfo.ProductsBatchCode = dr["ProductsBatchCode"].ToString();
					skuInfo.ZkNum = ZConvert.StrToInt(dr["ZkNum"]);
					skuInfo.ZyNum = ZConvert.StrToInt(dr["ZyNum"]);
					skuInfo.DjNum = ZConvert.StrToInt(dr["DjNum"]);
					skuInfoList.Add(skuInfo);
				}
			}
			catch (Exception ex) {
				Sys.SaveErrorLog(ex, "查看商品库存信息", FormsAuth.GetUserCode());
			}
			return skuInfoList;
		}

		#endregion

		#region 库区初始化

		/// <summary>
		/// 库区初始化
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static BaseResult InitLocation(string userCode, string warehouseCode, IDbContext context) {
			BaseResult resultInfo = new BaseResult();
			try {
				int[] arrLocationType = new[] { (int)LocationType.发货区, (int)LocationType.中转区, (int)LocationType.废品区 };
				foreach (var locationType in arrLocationType) {
					WarehouseLocation topLocation = new WarehouseLocation();
					switch (locationType) {
						case (int)LocationType.发货区:
							topLocation.Code = "FH";
							topLocation.Name = "发货区";
							break;
						case (int)LocationType.中转区:
							topLocation.Code = "ZZ";
							topLocation.Name = LocationType.中转区.ToString();
							break;
						case (int)LocationType.废品区:
							topLocation.Code = "FP";
							topLocation.Name = LocationType.废品区.ToString();
							break;
					}
					topLocation.WarehouseCode = warehouseCode;
					topLocation.TypeID = locationType;
					topLocation.ParentID = 0;
					topLocation.IsEnable = (int)IsEnable.是;
					topLocation.Seq = 0;
					topLocation.CreateDate = DateTime.Now;
					topLocation.CreatePerson = userCode;
					int topID = WarehouseLocationService.Add(topLocation, context);
					if (topID > 0) {
						WarehouseLocation subLocation = new WarehouseLocation();
						subLocation.WarehouseCode = warehouseCode;
						subLocation.Code = topLocation.Code + "0001";
						subLocation.Name = topLocation.Name + "第一位";
						subLocation.TypeID = locationType;
						subLocation.ParentID = topID;
						subLocation.IsEnable = (int)IsEnable.是;
						subLocation.Seq = 0;
						subLocation.CreateDate = DateTime.Now;
						subLocation.CreatePerson = userCode;
						int subID = WarehouseLocationService.Add(subLocation, context);
						if (subID == 0) {
							resultInfo.result = 0;
							resultInfo.message = subLocation.Name + "添加失败！";
							break;
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = topLocation.Name + "添加失败！";
						break;
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "库区初始化出现异常！";
				Sys.SaveErrorLog(ex, "库区初始化", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
