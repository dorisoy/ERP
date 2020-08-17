using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysuserRoleMapRepository:BaseRepository<SysuserRoleMap> {

	 #region 构造函数
	 private static SysuserRoleMapRepository _instance;
	 public static SysuserRoleMapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysuserRoleMapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SysuserRoleMap entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<SysuserRoleMap>("sys_userRoleMap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(SysuserRoleMap entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<SysuserRoleMap>("sys_userRoleMap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region  删除用户角色关联
	 /// <summary>
	 /// by  uid
	 /// </summary>
	 /// <param name="rcode"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int DelsysuserRoleMap(string ucode, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = ucode;
		 string sqlStr = "delete  from sys_userRoleMap where UserCode=@0";
		 return Del(sqlStr, context, objects);
	 }
	 //by rid
	 public int DelsysuserRoleMapbyrole(string rcode, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = rcode;
		 string sqlStr = "delete  from sys_userRoleMap where RoleCode=@0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	 #region 用户角色关联计数
	 /// <summary>
	 /// 用户角色关联计数
	 /// </summary>
	 /// <param name="usercode"></param>
	 /// <param name="rolecode"></param>
	 /// <param name="context"></param>
	 /// <returns></returns>
	 public int Getsys_userRoleMapCount(string usercode, string rolecode, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = usercode;
		 objects[1] = rolecode;
		 return GetCount("SELECT  COUNT(0)    FROM  sys_userRoleMap WHERE UserCode=@0 AND  RoleCode=@1", context, objects);

	 }
	 #endregion
		

	}
}