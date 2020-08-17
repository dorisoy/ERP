using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class LogisticsAreaMapService  : BaseService<LogisticsAreaMap> {
    
		public static int Update(LogisticsAreaMap entity) {
			return LogisticsAreaMapRepository.GetInstance().Update(entity);
		}

	

		public static int Add(LogisticsAreaMap entity) {
			return LogisticsAreaMapRepository.GetInstance().Add(entity);
		}
	}
}





