using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace  PaiXie.Data 
{
 public	class WarehouseBookingProductsSkuRepository:BaseRepository<WarehouseBookingProductsSku> {

	 #region 构造函数
	 private static WarehouseBookingProductsSkuRepository _instance;
	 public static WarehouseBookingProductsSkuRepository GetInstance() {
		 if (_instance == null) {
			 _instance = new WarehouseBookingProductsSkuRepository();
		 }
		 return _instance;
	 }
	 #endregion

	 #region Add
	 public int  Add(WarehouseBookingProductsSku entity, IDbContext context = null) {
        if (context == null) context = Db.GetInstance().Context();
		 int Id = context.Insert<WarehouseBookingProductsSku>("warehouseBookingProductsSku", entity)
					 .AutoMap(x => x.ID)
					 .ExecuteReturnLastId<int>();
		 return Id;
	 }
	 #endregion

	 #region Update
	 public int Update(WarehouseBookingProductsSku entity, IDbContext context = null) {
         if (context == null) context = Db.GetInstance().Context();
		 int rowsAffected = context.Update<WarehouseBookingProductsSku>("warehouseBookingProductsSku", entity)
		 .AutoMap(x => x.ID)
		 .Where(x => x.ID)
		 .Execute();
		 return rowsAffected;
	 }
	 #endregion

	}
}





