using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class LogisticsAreaMapRepository:BaseRepository<LogisticsAreaMap> {

	 #region 构造函数
	 private static LogisticsAreaMapRepository _instance;
	 public static LogisticsAreaMapRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new LogisticsAreaMapRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(LogisticsAreaMap entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<LogisticsAreaMap>("logisticsAreaMap", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(LogisticsAreaMap entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<LogisticsAreaMap>("logisticsAreaMap", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





