using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
 public	class testtableRepository:BaseRepository<testtable> {

	 #region 构造函数
	 private static testtableRepository _instance;
	 public static testtableRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new testtableRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(testtable entity) {
		 int id = Db.GetInstance().Context().Insert<testtable>("testtable", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(testtable entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<testtable>("testtable", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}