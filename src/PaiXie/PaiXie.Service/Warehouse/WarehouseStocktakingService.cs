using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseStocktakingService  : BaseService<WarehouseStocktaking> {
    
        #region Update
        
		public static int Update(WarehouseStocktaking entity, IDbContext context = null) {
			return WarehouseStocktakingRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseStocktaking entity, IDbContext context = null) {
			return WarehouseStocktakingRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseStocktaking GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseStocktakingRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehouseStocktakingRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取未确认盘点记录数量

		/// <summary>
		/// 获取未确认盘点记录数量
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int GetUnconfirmedCount(string warehouseCode, IDbContext context = null) {
			return WarehouseStocktakingRepository.GetInstance().GetUnconfirmedCount(warehouseCode, context);
		}

		#endregion
	}
}





