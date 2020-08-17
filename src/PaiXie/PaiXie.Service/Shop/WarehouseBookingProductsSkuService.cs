using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class WarehouseBookingProductsSkuService  : BaseService<WarehouseBookingProductsSku> {
    
		public static int Update(WarehouseBookingProductsSku entity) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Update(entity);
		}

	

		public static int Add(WarehouseBookingProductsSku entity) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Add(entity);
		}
	}
}





