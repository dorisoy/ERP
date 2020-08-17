using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseMoveLocationService  : BaseService<WarehouseMoveLocation> {
    
        #region Update
        
		public static int Update(WarehouseMoveLocation entity, IDbContext context = null) {
			return WarehouseMoveLocationRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseMoveLocation entity, IDbContext context = null) {
			return WarehouseMoveLocationRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseMoveLocation GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseMoveLocationRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehouseMoveLocationRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 修改移位单状态

		/// <summary>
		/// 修改移位单状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">移位单主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateStatus(string userCode, int id, int oldStatus, int newStatus, IDbContext context = null) {
			return WarehouseMoveLocationRepository.GetInstance().UpdateStatus(userCode, id, oldStatus, newStatus, context);
		}

		#endregion
	}
}





