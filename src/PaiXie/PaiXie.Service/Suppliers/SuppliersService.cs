using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class SuppliersService  : BaseService<Suppliers> {
    
        #region Update
        
		public static int Update(Suppliers entity, IDbContext context = null) {
			return SuppliersRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(Suppliers entity, IDbContext context = null) {
			return SuppliersRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static Suppliers GetQuerySingleByID(int id, IDbContext context = null) {
		    return SuppliersRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return SuppliersRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据供应商名称获取单个实体

		/// <summary>
		/// 根据供应商名称获取单个实体
		/// </summary>
		/// <param name="suppliersName">供应商名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Suppliers GetQuerySingleByName(string suppliersName, IDbContext context = null) {
			return SuppliersRepository.GetInstance().GetQuerySingleByName(suppliersName, context);
		}

		#endregion

		#region 根据供应商简称获取单个实体

		/// <summary>
		/// 根据供应商简称获取单个实体
		/// </summary>
		/// <param name="aliasName">供应商简称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Suppliers GetQuerySingleByAliasName(string aliasName, IDbContext context = null) {
			return SuppliersRepository.GetInstance().GetQuerySingleByAliasName(aliasName, context);
		}

		#endregion

		#region 根据供应商名称获取供应商ID

		/// <summary>
		/// 根据供应商名称获取供应商ID
		/// </summary>
		/// <param name="name">供应商名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetIDByName(string name, IDbContext context = null) {
			return SuppliersRepository.GetInstance().GetIDByName(name, context);
		}

		#endregion

		#region 根据供应商名称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)

		/// <summary>
		/// 根据供应商名称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)
		/// </summary>
		/// <param name="name">供应商名称</param>
		/// <param name="exceptSuppliersID">需要排除供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetIDByName(string name, int exceptSuppliersID, IDbContext context = null) {
			return SuppliersRepository.GetInstance().GetIDByName(name, exceptSuppliersID, context);
		}

		#endregion

		#region 根据供应商简称获取供应商ID

		/// <summary>
		/// 根据供应商简称获取供应商ID
		/// </summary>
		/// <param name="aliasName">供应商简称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetIDByAliasName(string aliasName, IDbContext context = null) {
			return SuppliersRepository.GetInstance().GetIDByAliasName(aliasName, context);
		}

		#endregion

		#region 根据供应商简称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)

		/// <summary>
		/// 根据供应商简称获取排除指定供应商ID之外的供应商ID(修改供应商时使用)
		/// </summary>
		/// <param name="aliasName">供应商简称</param>
		/// <param name="exceptSuppliersID">需要排除供应商ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetIDByAliasName(string aliasName, int exceptSuppliersID, IDbContext context = null) {
			return SuppliersRepository.GetInstance().GetIDByAliasName(aliasName, exceptSuppliersID, context);
		}

		#endregion
	}
}





