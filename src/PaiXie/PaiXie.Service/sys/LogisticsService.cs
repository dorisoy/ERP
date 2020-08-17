using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
	public class LogisticsService : BaseService<Logistics> {

		public static int Update(Logistics entity) {
			return LogisticsRepository.GetInstance().Update(entity);
		}


		public static int Add(Logistics entity) {
			return LogisticsRepository.GetInstance().Add(entity);
		}

		/// <summary>
		/// ��ȡʵ��
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public static Logistics GetLogistics(int id) {
			return LogisticsRepository.GetInstance().GetLogistics(id);
		}

		/// <summary>
		/// ɾ��ʵ��
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public static int DelLogistics(string id) {
			return LogisticsRepository.GetInstance().DelLogistics(id);
		}

		/// <summary>
		/// ���Ψһ��
		/// </summary>
		/// <param name="ID">����id</param>
		/// <param name="roleCode">����</param>
		/// <returns></returns>
		public static int GetLogisticsCount(int ID, string roleCode) {
			return LogisticsRepository.GetInstance().GetLogisticsCount(ID, roleCode);
		}
		/// <summary>
		/// ���Ψһ��
		/// </summary>
		/// <param name="roleCode">����</param>
		/// <returns></returns>
		public static int GetLogisticsCount(string roleCode) {
			return LogisticsRepository.GetInstance().GetLogisticsCount(roleCode);

		}

		/// <summary>
		/// ���á�����������˾
		/// </summary>
		/// <param name="id">������˾id </param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int SetIsEnable(int id, IDbContext context = null) {
			return LogisticsRepository.GetInstance().SetIsEnable(id, context);

		}

		/// <summary>
		/// ��ȡ���������б�
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<Logistics> GetManyLogistics(IDbContext context = null) {
			return LogisticsRepository.GetInstance().GetManyLogistics(context);
		}
	}
}





