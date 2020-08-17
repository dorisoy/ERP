using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysroleColumnmapRepository:BaseRepository<SysroleColumnmap> {

	 #region 构造函数
	 private static SysroleColumnmapRepository _instance;
	 public static SysroleColumnmapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysroleColumnmapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SysroleColumnmap entity) {
		 int id = Db.GetInstance().Context().Insert<SysroleColumnmap>("sys_roleColumnmap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SysroleColumnmap entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SysroleColumnmap>("sys_roleColumnmap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





