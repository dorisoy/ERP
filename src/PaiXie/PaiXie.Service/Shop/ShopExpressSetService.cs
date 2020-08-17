using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopExpressSetService  : BaseService<ShopExpressSet> {
    
        #region Update
        
		public static int Update(ShopExpressSet entity, IDbContext context = null) {
			return ShopExpressSetRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(ShopExpressSet entity, IDbContext context = null) {
			return ShopExpressSetRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static ShopExpressSet GetQuerySingleByID(int id, IDbContext context = null) {
		    return ShopExpressSetRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return ShopExpressSetRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<ShopExpressSet> GetManyShopExpressSet(int shopID, IDbContext context = null) {
			return ShopExpressSetRepository.GetInstance().GetManyShopExpressSet(shopID, context);
		}

		#endregion

		#region 根据店铺ID删除

		/// <summary>
		/// 根据店铺ID删除
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DelByShopID(int shopID, IDbContext context = null) {
			return ShopExpressSetRepository.GetInstance().DelByShopID(shopID, context);
		}

		#endregion
	}
}





