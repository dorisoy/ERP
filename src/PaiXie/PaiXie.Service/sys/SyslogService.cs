using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class SyslogService : BaseService<Syslog> {

		public static int Update(Syslog entity, IDbContext context = null) {
			return SyslogRepository.GetInstance().Update(entity, context);
		}

		public static int Add(Syslog entity, IDbContext context = null) {
			return SyslogRepository.GetInstance().Add(entity, context);
		}


		/// <summary>
		/// ��ȡ����ʵ�� ͨ������ID
		/// </summary>
		/// <param name="ID">����id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static Syslog GetQuerySingleByID(int ID, IDbContext context = null) {
			return SyslogRepository.GetInstance().GetQuerySingleByID(ID, context);
		}

		/// <summary>
		/// ɾ������  ͨ��ID
		/// </summary>
		/// <param name="ID">����id</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int DelByID(int ID, IDbContext context = null) {
			return SyslogRepository.GetInstance().DelByID(ID, context = null);
		}




	}
}





