using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class WarehouseInventoryWarnService  : BaseService<WarehouseInventoryWarn> {
    
		public static int Update(WarehouseInventoryWarn entity) {
			return WarehouseInventoryWarnRepository.GetInstance().Update(entity);
		}

		public static int Add(WarehouseInventoryWarn entity) {
			return WarehouseInventoryWarnRepository.GetInstance().Add(entity);
		}
	}
}





