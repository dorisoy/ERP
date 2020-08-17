using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysbuttonRepository:BaseRepository<Sysbutton> {

	 #region 构造函数
	 private static SysbuttonRepository _instance;
	 public static SysbuttonRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysbuttonRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(Sysbutton entity) {
		 int id = Db.GetInstance().Context().Insert<Sysbutton>("sys_button", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(Sysbutton entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<Sysbutton>("sys_button", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 获取菜单事件
	/// <summary>
	 /// 获取菜单事件
	/// </summary>
	/// <returns></returns>
	 public List<Sysbutton> GetSysbuttonlist() {
		 string sqlStr = "SELECT  *  FROM sys_button";
		 return GetQueryMany(sqlStr);
	 }
	 public Sysbutton GetSysbutton(string url) {
		 Object[] objects = new Object[1];
		 objects[0] = url;
		 string sqlStr = "SELECT  *  FROM sys_button WHERE Url =@0 ";
		 return GetQuerySingle(sqlStr, null, objects);
	 }
	 public Sysbutton GetSysbuttonUrl(string Code) {
		 Object[] objects = new Object[1];
		 objects[0] = Code;
		 string sqlStr = "SELECT  *  FROM sys_button WHERE Code =@0 ";
		 return GetQuerySingle(sqlStr, null, objects);
	 }
	 #endregion

	 #region 获取菜单事件
	 /// <summary>
	 /// 获取菜单事件
	 /// </summary>
	 /// <param name="rcode"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public List<Sysbutton> GetSysbuttonlist(string UserCode, string code) {
		 Object[] objects = new Object[2];
		 objects[0] = UserCode;
		 objects[1] = code;
		 string sqlStr = @" SELECT  *  FROM  sys_button WHERE   CODE IN (SELECT   ButtonCode  FROM sys_roleMenuButtonMap WHERE RoleCode IN (SELECT  RoleCode  FROM  sys_userRoleMap WHERE UserCode=@0))  and  CODE =@1 ";
		 return GetQueryMany(sqlStr, null, objects);
	 }
	 #endregion

	 #region 获取权限
	/// <summary>
	 /// 获取权限
	/// </summary>
	/// <param name="UserCode">用户代码</param>
	/// <param name="url">url地址</param>
	/// <returns></returns>
	 public int GetPower(string UserCode, string url) {
		
		 Object[] objects = new Object[2];
		 objects[0] = UserCode;
		 objects[1] = url;
		 string sqlStr = "	  ";
sqlStr += "	SELECT COUNT(0)  FROM  (  ";
sqlStr += "	SELECT  CODE,NAME,url  FROM  ";
sqlStr += "	sys_menu  WHERE IsEnable=1 AND  CODE IN   ";
sqlStr += "	(  ";
sqlStr += "	SELECT   MenuCode  FROM sys_rolemenumap WHERE RoleCode IN (SELECT  RoleCode  FROM  sys_userRoleMap WHERE UserCode=@0)  ";
sqlStr += "	)  ";
sqlStr += "	UNION ALL  ";

sqlStr += "	SELECT  CODE,NAME,url  FROM  ";
sqlStr += "	sys_button  WHERE CODE IN   ";
sqlStr += "	(  ";
sqlStr += "	SELECT   ButtonCode  FROM sys_roleMenuButtonMap WHERE RoleCode IN (SELECT  RoleCode  FROM  sys_userRoleMap WHERE UserCode=@0)  ";
sqlStr += "	)) A WHERE url=@1";
return GetCount(sqlStr, null, objects);
	 }
	 #endregion
	}
}