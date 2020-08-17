using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseProductsBatchService  : BaseService<WarehouseProductsBatch> {
    
        #region Update
        
		public static int Update(WarehouseProductsBatch entity, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().Update(entity, context);
		}
        
        #endregion

        #region Add
		
        public static int Add(WarehouseProductsBatch entity, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().Add(entity, context);
		}
        
        #endregion
        
        #region 获取单个实体 通过主键ID

	    /// <summary>
	    /// 获取单个实体 通过主键ID
	    /// </summary>
	    /// <param name="id">主键ID</param>
	    /// <param name="context">数据库连接对象</param>
	    /// <returns></returns>
	    public static WarehouseProductsBatch GetQuerySingleByID(int id, IDbContext context = null) {
		    return WarehouseProductsBatchRepository.GetInstance().GetQuerySingleByID(id, context);
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
		    return WarehouseProductsBatchRepository.GetInstance().DelByID(id, context);
	    }
    
        #endregion             

		#region 根据SkuID获取最新批次实体

		/// <summary>
		/// 根据SkuID获取最新批次实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsID">商品ID</param>
		/// <param name="context">数据库连接</param>
		/// <returns></returns>
		public static WarehouseProductsBatch GetLatestWarehouseProductsBatch(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().GetLatestWarehouseProductsBatch(warehouseCode, productsSkuID, context);
		}

		#endregion

		#region 更新批次商品冻结数量和可用数量 (冻结加，可用减)

		/// <summary>
		/// 更新批次商品冻结数量和可用数量 (冻结加，可用减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="djNum">冻结数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateDjNumAndKyNum(string userCode, int productsBatchID, int djNum, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().UpdateDjNumAndKyNum(userCode, productsBatchID, djNum, context);
		}

		#endregion

		#region 更新批次商品冻结和在库 (冻结减，在库减)

		/// <summary>
		/// 更新批次商品冻结和在库 (冻结减，在库减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">出库数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateDjNumAndZkNum(string userCode, int productsBatchID, int outNum, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().UpdateDjNumAndZkNum(userCode, productsBatchID, outNum, context);
		}

		#endregion

		#region 更新批次商品占用、可用、在库 (发货时调用)

		/// <summary>
		/// 更新批次商品占用、可用、在库 (发货时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">发货数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateZyNumAndKyNumAndZkNum(string userCode, int productsBatchID, int outNum, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().UpdateZyNumAndKyNumAndZkNum(userCode, productsBatchID, outNum, context);
		}

		#endregion

		#region 根据仓库编码、商品SKUID、批次 获取单个实体

		/// <summary>
		/// 根据仓库编码、商品SKUID、批次 获取单个实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="batchCode">批次</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseProductsBatch GetSingleWarehouseProductsBatch(string warehouseCode, int productsSkuID, string batchCode, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().GetSingleWarehouseProductsBatch(warehouseCode, productsSkuID, batchCode, context);
		}

		#endregion

		#region 扣减占用(取消出库单时调用)

		/// <summary>
		/// 扣减占用(取消出库单时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeductionZyNum(string userCode, int productsBatchID, int num, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().DeductionZyNum(userCode, productsBatchID, num, context);
		}

		#endregion

		#region 增加占用(生成出库单时调用)

		/// <summary>
		/// 增加占用(生成出库单时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int IncreaseZyNum(string userCode, int productsBatchID, int num, IDbContext context = null) {
			return WarehouseProductsBatchRepository.GetInstance().IncreaseZyNum(userCode, productsBatchID, num, context);
		}

		#endregion

		/// <summary>
		/// 获取批次情况分页列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<BatchList> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<BatchList> obj = new BaseRepository<BatchList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		/// <summary>
		/// 获取批次商品情况分页列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<BatchItemList> GetQueryManyForPageBatchItemList(SelectBuilder data, out int count) {
			BaseRepository<BatchItemList> obj = new BaseRepository<BatchItemList>();
			return obj.GetQueryManyForPage(data, out  count);
		}
	}
}





