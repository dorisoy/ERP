using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SyscolListRepository:BaseRepository<SyscolList> {

	 #region 构造函数
	 private static SyscolListRepository _instance;
	 public static SyscolListRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SyscolListRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SyscolList entity) {
		 int id = Db.GetInstance().Context().Insert<SyscolList>("sys_colList", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SyscolList entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SyscolList>("sys_colList", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





