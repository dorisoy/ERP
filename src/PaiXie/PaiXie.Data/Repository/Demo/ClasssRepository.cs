using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace  PaiXie.Data 
{
 public	class classsRepository:BaseRepository<classs> {

	 #region 构造函数
	 private static classsRepository _instance;
	 public static classsRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new classsRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(classs entity) {
		 int id = Db.GetInstance().Context().Insert<classs>("classs", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(classs entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<classs>("classs", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





