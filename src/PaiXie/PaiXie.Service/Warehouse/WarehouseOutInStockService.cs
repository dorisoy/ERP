using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseOutInStockService  : BaseService<WarehouseOutInStock> {
    
        #region Update
        
		public static int Update(WarehouseOutInStock entity, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseOutInStock entity, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseOutInStock GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseOutInStockRepository.GetInstance().GetQuerySingleByID(id, context);
	    }
    
	    #endregion

		#region 获取单个实体 通过出入库单号

		/// <summary>
		/// 获取单个实体 通过出入库单号
	    /// </summary>
		/// <param name="billNo">出入库单号</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
		public static WarehouseOutInStock GetQuerySingleByBillNo(string billNo, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().GetQuerySingleByBillNo(billNo, context);
	    }

		#endregion

		#region 删除操作  通过ID 未提交状态才可以删除

		/// <summary>
		/// 删除操作  通过ID 未提交状态才可以删除
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库对象</param>
	    /// <returns></returns>
	    public static int DelByID(int id, IDbContext context = null) {
		    return WarehouseOutInStockRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 获取id

		public static  string GetidByBillNo(string BillNo, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().GetidByBillNo(BillNo, context);

		} 
		#endregion

		#region 将某一状态的出入库单更新为另外一个状态

		/// <summary>
		/// 将某一状态的出入库单更新为另外一个状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出入库单主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateStatus(string userCode, int id, int oldStatus, int newStatus, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().UpdateStatus(userCode, id, oldStatus, newStatus, context);
		}

		#endregion

		#region 获取实体

		public static  WarehouseOutInStock GetModelByBillNo(string BillNo, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().GetModelByBillNo(BillNo, context);

		} 
		#endregion

	    #region 获取列表 通过 ids 
		public static  List<WarehouseOutInStock>  Getlistbyids(string ids, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().Getlistbyids(ids, context);
		} 
		#endregion
	
	    #region 获取价格
		/// <summary>
		/// 获取价格
		/// </summary>
		/// <param name="skucode">skucode</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  string  PurchasePrice(string skucode, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().PurchasePrice(skucode, context);
		} 
		#endregion

	    #region 获取价格
		/// <summary>
		/// 获取价格
		/// </summary>
		/// <param name="ProductsID">ProductsID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  string  CostPrice(int  ProductsID, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().CostPrice(ProductsID, context);
		} 
		#endregion

		#region 获取价格
		/// <summary>
		/// 获取价格
		/// </summary>
		/// <param name="skucode">skucode</param>
		/// <param name="SuppliersID">SuppliersID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  string  PurchasePrice(string skucode, int SuppliersID, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().PurchasePrice(skucode,SuppliersID, context);
		} 
		#endregion

		#region 入库单状态更新
		/// <summary>
		/// 入库单状态更新
		/// </summary>
		/// <param name="id">主键id</param>
		/// <param name="context"></param>
		/// <returns></returns>

		public static  int   updatestatus( int id, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().updatestatus(id, context);
		} 
		#endregion

		#region 入库单数量
		/// <summary>
		/// 入库单数量
		/// </summary>
		/// <param name="sourceid">源id</param>
		/// <param name="context"></param>
		/// <returns></returns>

		public static  int   warehouseOutInStockCOUNT( int sourceid, IDbContext context = null) {
			return WarehouseOutInStockRepository.GetInstance().warehouseOutInStockCOUNT(sourceid, context);
		} 
		#endregion

	}
}