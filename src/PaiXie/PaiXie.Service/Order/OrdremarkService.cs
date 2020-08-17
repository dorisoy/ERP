using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class OrdremarkService  : BaseService<Ordremark> {
    
        #region Update
        
		public static int Update(Ordremark entity, IDbContext context = null) {
			return OrdremarkRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Ordremark entity, IDbContext context = null) {
			return OrdremarkRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Ordremark GetQuerySingleByID(int id, IDbContext context = null) {
		    return OrdremarkRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return OrdremarkRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取实体列表

		/// <summary>
		/// 获取实体列表
		/// </summary>
		/// <param name="erpOrderCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<Ordremark> GetManyOrdremark(string erpOrderCode, IDbContext context = null) {
			return OrdremarkRepository.GetInstance().GetManyOrdremark(erpOrderCode, context);
		}

		#endregion
	}
}





