using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SysroleColumnmapService  : BaseService<SysroleColumnmap> {
    
		public static int Update(SysroleColumnmap entity) {
			return SysroleColumnmapRepository.GetInstance().Update(entity);
		}

	

		public static int Add(SysroleColumnmap entity) {
			return SysroleColumnmapRepository.GetInstance().Add(entity);
		}
	}
}





