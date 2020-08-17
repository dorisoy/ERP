using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class WarehouseInventoryWarnRepository:BaseRepository<WarehouseInventoryWarn> {

	 #region 构造函数
	 private static WarehouseInventoryWarnRepository _instance;
	 public static WarehouseInventoryWarnRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new WarehouseInventoryWarnRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(WarehouseInventoryWarn entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<WarehouseInventoryWarn>("warehouseInventoryWarn", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(WarehouseInventoryWarn entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<WarehouseInventoryWarn>("warehouseInventoryWarn", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





