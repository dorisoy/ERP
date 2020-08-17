using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysloginHistoryRepository:BaseRepository<SysloginHistory> {

	 #region 构造函数
	 private static SysloginHistoryRepository _instance;
	 public static SysloginHistoryRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysloginHistoryRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SysloginHistory entity) {
		 int id = Db.GetInstance().Context().Insert<SysloginHistory>("sys_loginHistory", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SysloginHistory entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SysloginHistory>("sys_loginHistory", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





