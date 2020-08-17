using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrddiscountService  : BaseService<Orddiscount> {
    
        #region Update
        
		public static int Update(Orddiscount entity, IDbContext context = null) {
			return OrddiscountRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Orddiscount entity, IDbContext context = null) {
			return OrddiscountRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Orddiscount GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrddiscountRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return OrddiscountRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取订单优惠实体列表

		/// <summary>
		/// 获取订单优惠实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orddiscount> GetManyOrddiscount(string erpOrderCode, IDbContext context = null) {
			return OrddiscountRepository.GetInstance().GetManyOrddiscount(erpOrderCode, context);
		}

		/// <summary>
		/// 获取订单优惠实体列表
		/// </summary>
		/// <param name="ordbaseID">订单表主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orddiscount> GetManyOrddiscount(int ordbaseID, IDbContext context = null) {
			return OrddiscountRepository.GetInstance().GetManyOrddiscount(ordbaseID, context);
		}

		/// <summary>
		/// 获取订单优惠实体列表
		/// </summary>
		/// <param name="erpOrderCode">系统订单号</param>
		/// <param name="erpOrderCode">商品SKUID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Orddiscount> GetManyOrddiscount(string erpOrderCode, int productsSkuID, IDbContext context = null) {
			return OrddiscountRepository.GetInstance().GetManyOrddiscount(erpOrderCode, productsSkuID, context);
		}
		#endregion
	}
}





