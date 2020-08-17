using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class SysloginHistoryService  : BaseService<SysloginHistory> {
    
		public static int Update(SysloginHistory entity) {
			return SysloginHistoryRepository.GetInstance().Update(entity);
		}

	

		public static int Add(SysloginHistory entity) {
			return SysloginHistoryRepository.GetInstance().Add(entity);
		}
	}
}





