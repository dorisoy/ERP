using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class SysbillnoService  : BaseService<Sysbillno> {

		#region Update

		public static int Update(Sysbillno entity, IDbContext context = null) {
			return SysbillnoRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(Sysbillno entity, IDbContext context = null) {
			return SysbillnoRepository.GetInstance().Add(entity, context);
		}

		#endregion
	}
}





