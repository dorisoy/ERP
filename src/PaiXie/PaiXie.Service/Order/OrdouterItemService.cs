using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrdouterItemService  : BaseService<OrdouterItem> {
    
        #region Update
        
		public static int Update(OrdouterItem entity, IDbContext context = null) {
			return OrdouterItemRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(OrdouterItem entity, IDbContext context = null) {
			return OrdouterItemRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static OrdouterItem GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrdouterItemRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return OrdouterItemRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<OrdouterItem> GetManyOrdouterItem(int ordouterID, IDbContext context = null) {
			return OrdouterItemRepository.GetInstance().GetManyOrdouterItem(ordouterID, context);
		}

		#endregion

		#region 获取订单商品是否添加完成

		/// <summary>
		/// 获取订单商品是否添加完成
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int getIsProductAddFin(int ordouterID, IDbContext context = null) {
			return OrdouterItemRepository.GetInstance().getIsProductAddFin(ordouterID, context);
		}

		#endregion

		#region 获取订单商品是否退款

		/// <summary>
		/// 获取订单商品是否退款
		/// </summary>
		/// <param name="ordouterID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual int getIsRefund(int ordouterID, IDbContext context = null) {
			return OrdouterItemRepository.GetInstance().getIsRefund(ordouterID, context);
		}

		#endregion
	}
}





