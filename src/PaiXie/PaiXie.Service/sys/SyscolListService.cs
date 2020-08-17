using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SyscolListService  : BaseService<SyscolList> {
    
		public static int Update(SyscolList entity) {
			return SyscolListRepository.GetInstance().Update(entity);
		}

	

		public static int Add(SyscolList entity) {
			return SyscolListRepository.GetInstance().Add(entity);
		}
	}
}





