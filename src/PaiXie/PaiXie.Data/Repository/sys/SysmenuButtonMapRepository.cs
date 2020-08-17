using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class SysmenuButtonMapRepository:BaseRepository<SysmenuButtonMap> {

	 #region 构造函数
	 private static SysmenuButtonMapRepository _instance;
	 public static SysmenuButtonMapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SysmenuButtonMapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SysmenuButtonMap entity) {
		 int id = Db.GetInstance().Context().Insert<SysmenuButtonMap>("sys_menuButtonMap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(SysmenuButtonMap entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<SysmenuButtonMap>("sys_menuButtonMap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





