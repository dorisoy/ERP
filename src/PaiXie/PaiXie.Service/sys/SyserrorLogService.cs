using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class SyserrorLogService  : BaseService<SyserrorLog> {
    
		public static int Add(SyserrorLog entity, IDbContext context = null) {
			return SyserrorLogRepository.GetInstance().Add(entity, context);
		}
	}
}





