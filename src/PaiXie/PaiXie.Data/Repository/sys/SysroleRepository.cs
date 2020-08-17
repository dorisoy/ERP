using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysroleRepository:BaseRepository<Sysrole> {

	 #region 构造函数
	 private static SysroleRepository _instance;
	 public static SysroleRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysroleRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(Sysrole entity) {
		 int id = Db.GetInstance().Context().Insert<Sysrole>("sys_role", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(Sysrole entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<Sysrole>("sys_role", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 获取角色
	 /// <summary>
	 /// 获取角色
	 /// </summary>
	 /// <param name="rcode">角色id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public Sysrole GetSysrole(string roleid, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = roleid;
		 string sqlStr = "SELECT 	* FROM sys_role WHERE ID=@0";
		 return GetQuerySingle(sqlStr, context, objects);
	 }
	 #endregion

	 #region 删除角色
	 /// <summary>
	 /// 删除角色
	 /// </summary>
	 /// <param name="rcode">角色id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int DelSysrole(string id, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = id;
		 string sqlStr = "delete  from sys_role where ID=@0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	 #region 唯一性检查
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <param name="rcode"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int GetsysroleCount(int ID, string roleCode, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = ID;
		 objects[1] = roleCode;
		 string sqlStr = "select count(0)  from sys_role where ID!=@0 and Code=@1";
		 return GetCount(sqlStr, context, objects);
	 }
	 public int GetsysroleCount(string roleCode, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = roleCode;
		 string sqlStr = "select count(0)  from sys_role where  Code=@0";
		 return GetCount(sqlStr, context, objects);
	 }
	 #endregion
	
	 #region 获取角色
	 /// <summary>
	 /// 获取角色
	 /// </summary>
	 /// <param name="rcode">角色id</param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public List<Sysrole>  GetSysrolelist( IDbContext context = null) {		
		 string sqlStr = "SELECT 	* FROM sys_role WHERE IsEnable=1 ORDER BY  seq ASC";
		 return GetQueryMany(sqlStr, context);
	 }
	 #endregion
	}
}