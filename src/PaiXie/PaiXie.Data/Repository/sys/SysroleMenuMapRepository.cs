using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysroleMenuMapRepository:BaseRepository<SysroleMenuMap> {

	 #region 构造函数
	 private static SysroleMenuMapRepository _instance;
	 public static SysroleMenuMapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysroleMenuMapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int Add(SysroleMenuMap entity, IDbContext context = null) {
		 if (context == null)
			 context = Db.GetInstance().Context();
		 int id = context.Insert<SysroleMenuMap>("sys_roleMenuMap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SysroleMenuMap entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SysroleMenuMap>("sys_roleMenuMap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 删除
	 public int DeleteroleMenuMap(string rcode, IDbContext context = null) {
		 Object[] objects = new Object[1];
		 objects[0] = rcode;
		 string sqlStr = "delete  from sys_roleMenuMap where RoleCode= @0";
		 return Del(sqlStr, context, objects);
	 }
	 #endregion

	 #region 是否有角色菜单权限

	 public int IsroleMenuMap(string menucode, string rolecode, IDbContext context = null) {
		 Object[] objects = new Object[2];
		 objects[0] = menucode;
		 objects[1] = rolecode;
		 string sqlStr = "SELECT count(0)  FROM sys_roleMenuMap WHERE MenuCode=@0 AND RoleCode=@1";
		 return GetCount(sqlStr, context, objects);
	 } 
	 #endregion
	
	}
}