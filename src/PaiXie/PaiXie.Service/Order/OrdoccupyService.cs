using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrdoccupyService  : BaseService<Ordoccupy> {
    
        #region Update
        
		public static int Update(Ordoccupy entity, IDbContext context = null) {
			return OrdoccupyRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Ordoccupy entity, IDbContext context = null) {
			return OrdoccupyRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Ordoccupy GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrdoccupyRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return OrdoccupyRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取单个实体

		/// <summary>
		/// 获取单个实体
		/// </summary>
		/// <param name="id">系统订单明细表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Ordoccupy GetSingleOrdoccupy(int ordItemID, IDbContext context = null) {
			return OrdoccupyRepository.GetInstance().GetSingleOrdoccupy(ordItemID, context); 
		}

		#endregion

		#region 删除占用

		/// <summary>
		///删除占用
		/// </summary>
		/// <param name="id">系统订单明细表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(int orditemID, IDbContext context = null) {
			return OrdoccupyRepository.GetInstance().Delete(orditemID, context);
		}

		/// <summary>
		/// 根据订单号删除占用
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int DeleteByErpOrderCode(string erpOrderCode, IDbContext context = null) {
			return OrdoccupyRepository.GetInstance().DeleteByErpOrderCode(erpOrderCode, context);
		}

		#endregion

		#region 根据订单明细主键ID更新订单占用数量

		/// <summary>
		/// 根据订单明细主键ID更新订单占用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="ordItemID">订单明细主键ID</param>
		/// <param name="num">数量 正数增加，负数扣减</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateNum(string userCode, int ordItemID, int num, IDbContext context = null) {
			return OrdoccupyRepository.GetInstance().UpdateNum(userCode, ordItemID, num, context);
		}

		#endregion
	}
}





