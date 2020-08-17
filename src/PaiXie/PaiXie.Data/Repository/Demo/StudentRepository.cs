using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace  PaiXie.Data 
{
 public	class studentRepository:BaseRepository<student> {

	 #region 构造函数
	 private static studentRepository _instance;
	 public static studentRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new studentRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(student entity) {
		 int id = Db.GetInstance().Context().Insert<student>("student", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return id;
	 }
	 #endregion

	 #region Update
	 public int Update(student entity) {
		 int rowsAffected = Db.GetInstance().Context().Update<student>("student", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	 #region 外键 关联

	 public List<studentList> GetQueryManyList(string sqlStr) {
		 List<studentList> objlist = Db.GetInstance().Context().Sql(sqlStr).QueryMany<studentList>();
		 return objlist;
	 } 
	 #endregion

	}
}





