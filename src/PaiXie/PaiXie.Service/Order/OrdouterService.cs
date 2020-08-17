using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class OrdouterService : BaseService<Ordouter> {

		#region Update

		public static int Update(Ordouter entity, IDbContext context = null) {
			return OrdouterRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(Ordouter entity, IDbContext context = null) {
			return OrdouterRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Ordouter GetQuerySingleByID(int id, IDbContext context = null) {
			return OrdouterRepository.GetInstance().GetQuerySingleByID(id, context);
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static int DelByID(int id, IDbContext context = null) {
			return OrdouterRepository.GetInstance().DelByID(id, context);
		}

		#endregion

		#region 获取订单数量

		/// <summary>
		/// 获取订单数量
		/// </summary>
		/// <param name="outOrderCode">外部订单号</param>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetCount(string outOrderCode, int shopID, IDbContext context = null) {
			return OrdouterRepository.GetInstance().GetCount(outOrderCode, shopID, context);
		}

		#endregion

		#region 获取TOP数量的实体列表

		/// <summary>
		/// 获取TOP数量的实体列表（自动生成时用）
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="topNum"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<Ordouter> GetManyOrdouterByTop(int shopID, int topNum, IDbContext context = null) {
			return OrdouterRepository.GetInstance().GetManyOrdouterByTop(shopID, topNum, context);
		}

		#endregion

		#region 根据系统订单号获取单个实体

		/// <summary>
		/// 根据系统订单号获取单个实体
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static Ordouter GetQuerySingleByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			return OrdouterRepository.GetInstance().GetQuerySingleByErpOrderCode(erpOrderCode, context);
		}

		#endregion
	}
}





