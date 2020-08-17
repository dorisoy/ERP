using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysroleMenuButtonMapRepository:BaseRepository<SysroleMenuButtonMap> {

	 #region 构造函数
	 private static SysroleMenuButtonMapRepository _instance;
	 public static SysroleMenuButtonMapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysroleMenuButtonMapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int Add(SysroleMenuButtonMap entity, IDbContext context = null) {
		 if (context == null)
			 context = Db.GetInstance().Context();
		 int id = context.Insert<SysroleMenuButtonMap>("sys_roleMenuButtonMap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SysroleMenuButtonMap entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SysroleMenuButtonMap>("sys_roleMenuButtonMap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 删除角色菜单事件
	 /// <summary>
	 /// 删除角色菜单事件
	 /// </summary>
	 /// <param name="rcode">角色代码</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int DeleteroleMenuButtonMap(string rcode, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = rcode;
		 string sqlStr = "delete  from sys_roleMenuButtonMap where RoleCode=@0";
		 return Del(sqlStr, context, objects);
	 } 
	 #endregion

	 #region 是否有角色菜单事件权限
	/// <summary>
	 /// 是否有角色菜单事件权限
	/// </summary>
	/// <param name="rolecode"></param>
	/// <param name="buttoncode"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	 public int IsroleMenuButtonMap(string rolecode,string buttoncode, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = rolecode;
		 objects[1] = buttoncode;
		 string sqlStr = "SELECT count(0)  FROM sys_roleMenuButtonMap WHERE RoleCode=@0 AND ButtonCode=@1";
		 return  GetCount(sqlStr, context, objects);
	 } 
	 #endregion
	 
	}
}