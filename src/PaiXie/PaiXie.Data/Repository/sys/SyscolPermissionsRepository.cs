using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SyscolPermissionsRepository:BaseRepository<SyscolPermissions> {

	 #region 构造函数
	 private static SyscolPermissionsRepository _instance;
	 public static SyscolPermissionsRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SyscolPermissionsRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SyscolPermissions entity) {
		 int id = Db.GetInstance().Context().Insert<SyscolPermissions>("sys_colPermissions", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SyscolPermissions entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SyscolPermissions>("sys_colPermissions", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





