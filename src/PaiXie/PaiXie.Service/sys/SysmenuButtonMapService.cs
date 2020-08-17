using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SysmenuButtonMapService  : BaseService<SysmenuButtonMap> {
    
		public static int Update(SysmenuButtonMap entity) {
			return SysmenuButtonMapRepository.GetInstance().Update(entity);
		}

	

		public static int Add(SysmenuButtonMap entity) {
			return SysmenuButtonMapRepository.GetInstance().Add(entity);
		}
	}
}





