using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
	public class WarehouseOutInStockItemService : BaseService<WarehouseOutInStockItem> {

		#region Update

		public static int Update(WarehouseOutInStockItem entity, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseOutInStockItem entity, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="id">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseOutInStockItem GetQuerySingleByID(int id, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetQuerySingleByID(id, context);
		}

		#endregion

		#region 获取出入库商品分页列表

		/// <summary>
		/// 获取出入库商品分页列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<WarehouseOutStockItemList> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<WarehouseOutStockItemList> obj = new BaseRepository<WarehouseOutStockItemList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		#endregion

		#region 获取出库库位商品列表

		public static List<OutStockLocationProductsList> GetQueryManyForOutStockLocationProductsList(SelectBuilder data, out int count) {
			BaseRepository<OutStockLocationProductsList> obj = new BaseRepository<OutStockLocationProductsList>();
			return obj.GetQueryManyForPage(data, out  count);
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
			return WarehouseOutInStockItemRepository.GetInstance().DelByID(id, context);
		}

		#endregion

		#region 根据出入库单ID删除所有商品

		/// <summary>
		/// 根据出入库单ID删除所有商品
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeleteByOutInStockID(int outInStockID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().DeleteByOutInStockID(outInStockID, context);
		}

		#endregion

		#region 根据出入库单ID获取所有商品

		/// <summary>
		/// 根据出入库单ID获取所有商品
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseOutInStockItem> GetWarehouseOutInStockItemList(int outInStockID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetWarehouseOutInStockItemList(outInStockID, context);
		}

		#endregion

		#region 根据出入库单商品表主键ID删除商品

		/// <summary>
		/// 根据出入库单商品表主键ID删除商品
		/// </summary>
		/// <param name="outInStockItemIDList">出入库单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(List<int> outInStockItemIDList, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().Delete(outInStockItemIDList, context);
		}

		#endregion

		#region 根据出入库单商品表主键ID获取商品

		/// <summary>
		/// 根据出入库单商品表主键ID获取商品
		/// </summary>
		/// <param name="outInStockItemIDList">出入库单商品表主键ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<WarehouseOutInStockItem> GetWarehouseOutInStockItemList(List<int> outInStockItemIDList, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetWarehouseOutInStockItemList(outInStockItemIDList, context);
		}

		#endregion

		#region 根据出入库单ID获取商品数量

		/// <summary>
		/// 根据出入库单ID获取商品数量
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsNumByOutInStockID(int outInStockID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetProductsNumByOutInStockID(outInStockID, context);
		}

		#endregion

		#region 根据出入库单ID、商品SKUID、批次ID 获取单个实体

		/// <summary>
		/// 根据出入库单ID、商品SKUID、批次ID 获取单个实体
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseOutInStockItem GetSingleWarehouseOutInStockItem(int outInStockID, int productsSkuID, int productsBatchID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetSingleWarehouseOutInStockItem(outInStockID, productsSkuID, productsBatchID, context);
		}

		#endregion

		#region 根据出入库单ID、商品SKUID、库位ID、批次ID 获取单个实体

		/// <summary>
		/// 根据出入库单ID、商品SKUID、库位ID、批次ID 获取单个实体
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseOutInStockItem GetSingleWarehouseOutInStockItem(int outInStockID, int productsSkuID, int locationID, int productsBatchID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetSingleWarehouseOutInStockItem(outInStockID, productsSkuID, locationID, productsBatchID, context);
		}

		#endregion

		#region 根据出入库单ID、商品SKUID 获取单个实体

		/// <summary>
		/// 根据出入库单ID、商品SKUID 获取单个实体
		/// </summary>
		/// <param name="outInStockID">出入库单ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseOutInStockItem GetSingleWarehouseOutInStockItem(int outInStockID, int productsSkuID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetSingleWarehouseOutInStockItem(outInStockID, productsSkuID, context);
		}

		#endregion

		#region  根据出入库单明细ID更新出入库数量

		/// <summary>
		/// 根据出入库单明细ID更新出入库数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出入库单明细ID</param>
		/// <param name="diffNum">要更新数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateProductsNum(string userCode, int id, int diffNum, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().UpdateProductsNum(userCode, id, diffNum, context);
		}

		#endregion

		#region 将某一状态的出入库单明细更新为另外一个状态

		/// <summary>
		/// 将某一状态的出入库单明细更新为另外一个状态
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="id">出入库单明细主键ID</param>
		/// <param name="oldStatus">旧状态</param>
		/// <param name="newStatus">新状态</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateStatus(string userCode, int id, int oldStatus, int newStatus, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().UpdateStatus(userCode, id, oldStatus, newStatus, context);
		}

		#endregion

		#region 根据单号获取 入库单 入库数
		public static string GetSumProductsNum(string BillNo, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetSumProductsNum(BillNo, context);
		}
		#endregion

		#region 根据单号获取入库单 总价格
		public static string GetSumPrice(string BillNo, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetSumPrice(BillNo, context);
		}
		#endregion

		#region 更新财务审核状态
		public static int UpdatecwshenheStatus(int newstatus, int oldstatus, int outinstockid, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().UpdatecwshenheStatus(newstatus, oldstatus, outinstockid, context);
		}
		#endregion

		#region 已经入库数量  其他库位的入库数量
		public static string GetOtherProductsNum(int ProductsSkuID, int OutInStockID, int LocationID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetOtherProductsNum(ProductsSkuID, OutInStockID, LocationID, context);
		}
		#endregion

		#region 根据 sku吗  库位id   出入库单号id  获取实体
		/// <summary>
		/// 根据 sku吗  库位id   出入库单号id  获取实体
		/// </summary>
		/// <param name="productsSkuCode"></param>
		/// <param name="LocationID"></param>
		/// <param name="OutInStockID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseOutInStockItem GetWarehouseOutInStockItem(string productsSkuCode, int LocationID, int OutInStockID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().GetWarehouseOutInStockItem(productsSkuCode, LocationID, OutInStockID, context);
		}
		#endregion

		#region 更新时间
		/// <summary>
		/// 更新时间
		/// </summary>
		/// <param name="scdate">时间</param>
		/// <param name="ids">id 列表</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int updateProductionDate(string scdate, string ids, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().updateProductionDate(scdate, ids, context);
		}
		#endregion

		#region 更新库位id
		/// <summary>
		/// 更新库位id
		/// </summary>
		/// <param name="Locationid">库位id</param>
		/// <param name="ids">id 列表</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int updateLocationID(int Locationid, string ids, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().updateLocationID(Locationid, ids, context);
		}
		#endregion

		#region 更新批次号  确认状态
		/// <summary>
		/// 更新批次号  确认状态
		/// </summary>
		/// <param name="productsBatchID"></param>
		/// <param name="billNo"></param>
		/// <param name="id"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int updateproductsBatch(int productsBatchID, string billNo, int id, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().updateproductsBatch(productsBatchID, billNo, id, context);
		}
		#endregion

		#region  获取总数量
		/// <summary>
		/// 获取总数量
		/// </summary>
		/// <param name="SourceItemID">来源明细id </param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string ProductsNum(int SourceItemID, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().ProductsNum(SourceItemID, context);
		}
		#endregion

		#region  更新采购商品数量
		/// <summary>
		/// 更新采购商品数量
		/// </summary>
		/// <param name="SourceItemID">来源明细id </param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int updateInStockNum(int InStockNum, int id, IDbContext context = null) {
			return WarehouseOutInStockItemRepository.GetInstance().updateInStockNum(InStockNum, id, context);
		}
		#endregion
	}
}