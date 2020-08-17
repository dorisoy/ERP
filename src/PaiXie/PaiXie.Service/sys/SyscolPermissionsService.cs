using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SyscolPermissionsService  : BaseService<SyscolPermissions> {
    
		public static int Update(SyscolPermissions entity) {
			return SyscolPermissionsRepository.GetInstance().Update(entity);
		}

	

		public static int Add(SyscolPermissions entity) {
			return SyscolPermissionsRepository.GetInstance().Add(entity);
		}
	}
}





