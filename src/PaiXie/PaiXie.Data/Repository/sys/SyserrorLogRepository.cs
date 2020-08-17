using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data 
{
 public	class SyserrorLogRepository:BaseRepository<SyserrorLog> {

	 #region 构造函数
	 private static SyserrorLogRepository _instance;
	 public static SyserrorLogRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new SyserrorLogRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(SyserrorLog entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<SyserrorLog>("sys_errorLog", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion
	}
}





