using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace  PaiXie.Data 
{
 public	class TreeRepository:BaseRepository<Tree> {

	 #region 构造函数
	 private static TreeRepository _instance;
	 public static TreeRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new TreeRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(Tree entity) {	
		 int id = Db.GetInstance().Context().Insert<Tree>("Tree", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(Tree entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<Tree>("Tree", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion



	}
}





