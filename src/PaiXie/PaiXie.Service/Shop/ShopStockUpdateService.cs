using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopStockUpdateService  : BaseService<ShopStockUpdate> {
    
        #region Update
        
		public static int Update(ShopStockUpdate entity, IDbContext context = null) {
			return ShopStockUpdateRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(ShopStockUpdate entity, IDbContext context = null) {
			return ShopStockUpdateRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static ShopStockUpdate GetQuerySingleByID(int id, IDbContext context = null) {
		    return ShopStockUpdateRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return ShopStockUpdateRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 店铺库存更新状态

		/// <summary>
		/// 店铺库存更新状态
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="PlatformType"></param>
		/// <param name="context"></param>
		/// <returns></returns>
	    public static List<ShopStockUpdate> ShopStockUpdatelist(int shopid,int PlatformType,  IDbContext context = null) {
		    return ShopStockUpdateRepository.GetInstance().ShopStockUpdatelist( shopid, PlatformType,  context);
	    }
    
        #endregion             

		#region 店铺库存更新状态  完成数

		/// <summary>
		/// 店铺库存更新状态  完成数
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="PlatformType"></param>
		/// <param name="context"></param>
		/// <returns></returns>
	    public static string shopStockUpdatecount(int shopid,  IDbContext context = null) {
		    return ShopStockUpdateRepository.GetInstance().shopStockUpdatecount( shopid,  context);
	    }
    
        #endregion    
        
 
		#region 店铺库存更新状态  最后更新时间
		/// <summary>
		/// 店铺库存更新状态  最后更新时间
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static object LastTimeshopStockUpdate(int shopid, IDbContext context = null) {
			return ShopStockUpdateRepository.GetInstance().LastTimeshopStockUpdate(shopid, context);
		}
		#endregion	


	}
}