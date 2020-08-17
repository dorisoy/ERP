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
		/// 获取实体
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public static Logistics GetLogistics(int id) {
			return LogisticsRepository.GetInstance().GetLogistics(id);
		}

		/// <summary>
		/// 删除实体
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public static int DelLogistics(string id) {
			return LogisticsRepository.GetInstance().DelLogistics(id);
		}

		/// <summary>
		/// 检查唯一性
		/// </summary>
		/// <param name="ID">主键id</param>
		/// <param name="roleCode">代码</param>
		/// <returns></returns>
		public static int GetLogisticsCount(int ID, string roleCode) {
			return LogisticsRepository.GetInstance().GetLogisticsCount(ID, roleCode);
		}
		/// <summary>
		/// 检查唯一性
		/// </summary>
		/// <param name="roleCode">代码</param>
		/// <returns></returns>
		public static int GetLogisticsCount(string roleCode) {
			return LogisticsRepository.GetInstance().GetLogisticsCount(roleCode);

		}

		/// <summary>
		/// 禁用、启用物流公司
		/// </summary>
		/// <param name="id">物流公司id </param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int SetIsEnable(int id, IDbContext context = null) {
			return LogisticsRepository.GetInstance().SetIsEnable(id, context);

		}

		/// <summary>
		/// 获取可用物流列表
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<Logistics> GetManyLogistics(IDbContext context = null) {
			return LogisticsRepository.GetInstance().GetManyLogistics(context);
		}
	}
}





