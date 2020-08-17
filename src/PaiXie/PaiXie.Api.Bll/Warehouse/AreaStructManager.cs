using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Utils;
namespace PaiXie.Api.Bll {
	/// <summary>
	/// 库区结构管理
	/// </summary>
	public class AreaStructManager {

		#region 删除库区结构

		/// <summary>
		///删除库区结构
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseAreaStructIDs">库区结构ID字符串，多个以半角逗号分隔</param>
		/// <returns></returns>
		public static BaseResult Del(string userCode, string warehouseAreaStructIDs) {
			BaseResult resultInfo = new BaseResult();
			try {
				using (IDbContext context = Db.GetInstance().Context()) {
					context.UseTransaction(true);
					string[] arrWarehouseAreaStructID = warehouseAreaStructIDs.Split(',');
					foreach (var item in arrWarehouseAreaStructID) {
						int warehouseAreaStructID = ZConvert.StrToInt(item);
						WarehouseAreaStruct warehouseAreaStruct = WarehouseAreaStructService.GetSingleWarehouseAreaStruct(warehouseAreaStructID, context);
						bool tempFlag = WarehouseAreaStructService.Del(warehouseAreaStructID, context) > 0;
						if (!tempFlag) {
							resultInfo.result = 0;
							resultInfo.message = "结构名称：" + warehouseAreaStruct.Name + " 删除失败！";
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
				Sys.SaveErrorLog(ex, "删除库区结构", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 保存库区结构信息

		/// <summary>
		/// 保存库区结构信息
		/// </summary>
		/// <param name="obj">库区结构信息实体类</param>
		/// <returns></returns>
		public static BaseResult Save(string warehouseCode, string userCode, WarehouseAreaStruct obj) {
			BaseResult resultInfo = new BaseResult();
			try {
				if (obj.ID == 0) {
					obj.WarehouseCode = warehouseCode;
					obj.CreatePerson = userCode;
					obj.CreateDate = DateTime.Now;
					obj.ParentID = obj.ParentID == -1 ? 0 : obj.ParentID;
					bool tempFlag = WarehouseAreaStructService.Add(obj) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "添加库区结构失败！";
					}
				}
				else {
					WarehouseAreaStruct objWarehouseAreaStruct = WarehouseAreaStructService.GetSingleWarehouseAreaStruct(obj.ID);
					objWarehouseAreaStruct.Code = obj.Code;
					objWarehouseAreaStruct.ParentID = obj.ParentID == -1 ? 0 : obj.ParentID;
					objWarehouseAreaStruct.Name = obj.Name;
					objWarehouseAreaStruct.UpdatePerson = userCode;
					objWarehouseAreaStruct.UpdateDate = DateTime.Now;
					bool tempFlag = WarehouseAreaStructService.Update(objWarehouseAreaStruct) > 0;
					if (!tempFlag) {
						resultInfo.result = 0;
						resultInfo.message = "修改库区结构失败！";
					}
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "程序出现异常！";
				Sys.SaveErrorLog(ex, "保存库区结构信息", userCode);
			}
			return resultInfo;
		}

		#endregion

		#region 初始化库区结构 排层位

		/// <summary>
		/// 初始化库区结构 排层位
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static BaseResult InitAreaStruct(string userCode, string warehouseCode, IDbContext context) {
			BaseResult resultInfo = new BaseResult();
			try {
				WarehouseAreaStruct qWarehouseAreaStruct = new WarehouseAreaStruct();
				qWarehouseAreaStruct.WarehouseCode = warehouseCode;
				qWarehouseAreaStruct.Code = "";
				qWarehouseAreaStruct.Name = "排层位";
				qWarehouseAreaStruct.ParentID = 0;
				qWarehouseAreaStruct.Seq = 0;
				qWarehouseAreaStruct.CreateDate = DateTime.Now;
				qWarehouseAreaStruct.CreatePerson = userCode;
				int qID = WarehouseAreaStructService.Add(qWarehouseAreaStruct, context);
				if (qID > 0) {
					WarehouseAreaStruct pWarehouseAreaStruct = new WarehouseAreaStruct();
					pWarehouseAreaStruct.WarehouseCode = warehouseCode;
					pWarehouseAreaStruct.Code = "P";
					pWarehouseAreaStruct.Name = "排";
					pWarehouseAreaStruct.ParentID = qID;
					pWarehouseAreaStruct.Seq = 0;
					pWarehouseAreaStruct.CreateDate = DateTime.Now;
					pWarehouseAreaStruct.CreatePerson = userCode;
					int pID = WarehouseAreaStructService.Add(pWarehouseAreaStruct, context);
					if (pID > 0) {
						WarehouseAreaStruct cWarehouseAreaStruct = new WarehouseAreaStruct();
						cWarehouseAreaStruct.WarehouseCode = warehouseCode;
						cWarehouseAreaStruct.Code = "C";
						cWarehouseAreaStruct.Name = "层";
						cWarehouseAreaStruct.ParentID = pID;
						cWarehouseAreaStruct.Seq = 0;
						cWarehouseAreaStruct.CreateDate = DateTime.Now;
						cWarehouseAreaStruct.CreatePerson = userCode;
						int cID = WarehouseAreaStructService.Add(cWarehouseAreaStruct, context);
						if (cID > 0) {
							WarehouseAreaStruct wWarehouseAreaStruct = new WarehouseAreaStruct();
							wWarehouseAreaStruct.WarehouseCode = warehouseCode;
							wWarehouseAreaStruct.Code = "W";
							wWarehouseAreaStruct.Name = "位";
							wWarehouseAreaStruct.ParentID = cID;
							wWarehouseAreaStruct.Seq = 0;
							wWarehouseAreaStruct.CreateDate = DateTime.Now;
							wWarehouseAreaStruct.CreatePerson = userCode;
							int wID = WarehouseAreaStructService.Add(wWarehouseAreaStruct, context);
							if (wID == 0) {
								resultInfo.result = 0;
								resultInfo.message = "库区结构位添加失败！";
							}
						}
						else {
							resultInfo.result = 0;
							resultInfo.message = "库区结构层添加失败！";
						}
					}
					else {
						resultInfo.result = 0;
						resultInfo.message = "库区结构排添加失败！";
					}
				}
				else {
					resultInfo.result = 0;
					resultInfo.message = "库区结构区添加失败！";
				}
			}
			catch (Exception ex) {
				resultInfo.result = -1;
				resultInfo.message = "库区结构初始化出现异常！";
				Sys.SaveErrorLog(ex, "库区结构初始化", userCode);
			}
			return resultInfo;
		}

		#endregion
	}
}
