using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopAutogenerationService  : BaseService<ShopAutogeneration> {
    
        #region Update
        
		public static int Update(ShopAutogeneration entity, IDbContext context = null) {
			return ShopAutogenerationRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(ShopAutogeneration entity, IDbContext context = null) {
			return ShopAutogenerationRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static ShopAutogeneration GetQuerySingleByID(int id, IDbContext context = null) {
		    return ShopAutogenerationRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return ShopAutogenerationRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<ShopAutogeneration> GetManyShopAutogeneration(IDbContext context = null) {
			return ShopAutogenerationRepository.GetInstance().GetManyShopAutogeneration(context);
		}

		#endregion

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID">网店ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ShopAutogeneration GetSingleShopAutogeneration(int shopID, IDbContext context = null) {
			return ShopAutogenerationRepository.GetInstance().GetSingleShopAutogeneration(shopID, context);
		}

		#endregion
	}
}





