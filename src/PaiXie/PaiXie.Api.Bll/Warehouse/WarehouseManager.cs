#region using
using FluentData;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Utils;
using System.Data;
#endregion
namespace PaiXie.Api.Bll {
	/// <summary>
	/// 仓库管理
	/// </summary>
	public class WarehouseManager {
	
		#region 仓库保存

		public static BaseResult WarehouseSave(PaiXie.Data.Warehouse obj,string pwd) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult BaseResult = new BaseResult();
			try {

				if (obj.ID == 0) {
					using (IDbContext context = Db.GetInstance().Context()) {
						context.UseTransaction(true);
						string code = Sys.GetBillNo("WA");
						if (!string.IsNullOrEmpty(obj.Librand))
							obj.Librand = obj.Librand.Substring(0, obj.Librand.Length - 1);
						obj.CreatePerson = userCode;
						obj.CreateDate = System.DateTime.Now;
						obj.Code = code;
						int ID = WarehouseService.Add(obj, context);
						if (ID < 1) {
							BaseResult.result = 0;
							BaseResult.message = "仓库添加失败";
						}
						else {

							#region 添加仓库管理员，初始化库区结构和库区(发货、中转、废品)
							//	Data.Warehouse objs = WarehouseService.Getwarehouse(ID.ToString(),context);
							Sysuser objSysuser = new Sysuser();
							objSysuser.Code = obj.Name.Trim();// "ck" + code;
							objSysuser.Name = obj.Name + "管理员";
							objSysuser.Password = ZEncypt.MD5(pwd);
							objSysuser.CreatePerson = userCode;
							objSysuser.CreateDate = System.DateTime.Now;
							objSysuser.IsEnable = (int)IsEnable.是;
							objSysuser.ModeType = (int)ProjectType.仓库端;
							objSysuser.WarehouseCode = code;
							objSysuser.IsSupper = (int)IsEnable.是; //仓库  超级管理员
							ID = SysuserService.Add(objSysuser, context);
							if (ID < 1) {
								BaseResult.result = 0;
								BaseResult.message = "仓库管理员添加失败";
							}
							else {
								BaseResult = AreaStructManager.InitAreaStruct(userCode, obj.Code, context);
								if (BaseResult.result == 1) {
									BaseResult = LocationManager.InitLocation(userCode, obj.Code, context);
								}
							}
							#endregion
						}
						if (BaseResult.result == 1) {
							context.Commit();
						}
						else {
							context.Rollback();
						}
					}
				}
				else {
					using (IDbContext context = Db.GetInstance().Context()) {
						context.UseTransaction(true);
						PaiXie.Data.Warehouse objSysuser = WarehouseService.Getwarehouse(ZConvert.ToString(obj.ID), context);
						objSysuser.Code = obj.Code;
						objSysuser.Name = obj.Name;
						objSysuser.IsEnable = obj.IsEnable;
						objSysuser.Remark = obj.Remark;
						objSysuser.Address = obj.Address;
						objSysuser.UpdatePerson = userCode;
						objSysuser.UpdateDate = System.DateTime.Now;
						objSysuser.Longitude = obj.Longitude;
						objSysuser.Latitude = obj.Latitude;
						if (!string.IsNullOrEmpty(obj.Librand))
							objSysuser.Librand = obj.Librand.Substring(0, obj.Librand.Length - 1);
						else
							objSysuser.Librand = obj.Librand;

						int ID = WarehouseService.Update(objSysuser, context);
						if (ID < 1) {
							
							BaseResult.result = 0;
							BaseResult.message = "仓库编辑失败";
						}
						//修改仓库管理员密码
						if (pwd.ToString().Trim() != "") {
						ID=	SysuserService.UpdatePwdByCode(ZEncypt.MD5(pwd), obj.Name, context);
						if (ID < 1) {

							BaseResult.result = 0;
							BaseResult.message = "仓库管理员密码修改失败";
						}
						}
						if (BaseResult.result==0) {
							context.Rollback();
						//	BaseResult.result = 0;
						//	BaseResult.message = "仓库编辑失败";
						}
						else {
							context.Commit();
						}
					}
				}
			}
			catch (Exception ex) {
				BaseResult.result = 0;
				BaseResult.message = "操作失败！";
				Sys.SaveErrorLog(ex, "仓库添加编辑", FormsAuth.GetUserCode());
			}
			return BaseResult;
		}

		#endregion
	}
}