using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseConfigService  : BaseService<WarehouseConfig> {
    
        #region Update
        
		public static int Update(WarehouseConfig entity, IDbContext context = null) {
			return WarehouseConfigRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseConfig entity, IDbContext context = null) {
			return WarehouseConfigRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseConfig GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseConfigRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 获取单个实体 通过仓库编码

	    /// <summary>
	    /// 获取单个实体 通过仓库编码
	    /// </summary>
		/// <param name="warehouseCode">仓库编码</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public static WarehouseConfig GetQuerySingleByWarehouseCode(string warehouseCode, IDbContext context = null) {
			return WarehouseConfigRepository.GetInstance().GetQuerySingleByWarehouseCode(warehouseCode, context);
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
		    return WarehouseConfigRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             
	}
}





