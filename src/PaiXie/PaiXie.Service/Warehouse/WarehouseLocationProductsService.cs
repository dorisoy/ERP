using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
	public class WarehouseLocationProductsService : BaseService<WarehouseLocationProducts> {

		#region Update

		public static int Update(WarehouseLocationProducts entity, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseLocationProducts entity, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 获取单个实体 通过主键ID

		/// <summary>
		/// 获取单个实体 通过主键ID
		/// </summary>
		/// <param name="ID">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseLocationProducts GetQuerySingleByID(int ID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetQuerySingleByID(ID, context);
		}

		#endregion

		#region 删除操作  通过ID

		/// <summary>
		/// 删除操作  通过ID
		/// </summary>
		/// <param name="ID">主键ID</param>
		/// <param name="context">数据库对象</param>
		/// <returns></returns>
		public static int DelByID(int ID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().DelByID(ID, context);
		}

		#endregion

		#region 删除操作 通过库位ID

		/// <summary>
		/// 删除操作 通过库位ID
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DelByLocationID(int locationID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().DelByLocationID(locationID, context);
		}
		#endregion

		#region 获取库位商品数量

		/// <summary>
		/// 获取库位商品数量
		/// </summary>
		/// <param name="locationIDList">库位ID列表</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsNum(List<int> locationIDList, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetProductsNum(locationIDList, context);
		}

		#endregion

		#region 获取库位商品数量实体

		/// <summary>
		/// 获取库位商品数量实体
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="produtcsID">商品ID</param>
		/// <param name="LocationID">库位ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseLocationProducts GetSingleWarehouseLocationProducts(string warehouseCode, int produtcsID, int LocationID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetSingleWarehouseLocationProducts(warehouseCode, produtcsID, LocationID, context);
		}

		#endregion

		#region 获取库区商品数量

		/// <summary>
		/// 获取库区商品数量
		/// </summary>
		/// <param name="topLocationID">库区ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetProductsNum(int topLocationID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetProductsNum(topLocationID, context);
		}

		#endregion

		#region 根据SkuID获取库位商品

		/// <summary>
		/// 根据SkuID获取库位商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="LocationID">库位ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<WarehouseLocationProducts> GetManyWarehouseLocationProducts(string warehouseCode, int productsSkuID, int LocationID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetManyWarehouseLocationProducts(warehouseCode, productsSkuID, LocationID, context);
		}

		/// <summary>
		/// 根据SkuID获取库位商品
		/// </summary>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SkuID</param>
		/// <param name="LocationID">库位ID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<LocationProductsKucInfo> GetManyLocationProductsKucInfo(string warehouseCode, int productsSkuID, int LocationID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetManyLocationProductsKucInfo(warehouseCode, productsSkuID, LocationID, context);
		}

		#endregion

		#region 更新库位商品冻结数量和可用数量(冻结加，可用减)

		/// <summary>
		/// 更新库位商品冻结数量和可用数量(冻结加，可用减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="djNum">冻结数量 差量更新可正可负</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateDjNumAndKyNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int djNum, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().UpdateDjNumAndKyNum(userCode, productsSkuID, locationID, productsBatchID, djNum, context);
		}

		#endregion

		#region 更新库位商品冻结和在库 (冻结减，在库减)

		/// <summary>
		/// 更新库位商品冻结和在库 (冻结减，在库减)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">出库数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateDjNumAndZkNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int outNum, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().UpdateDjNumAndZkNum(userCode, productsSkuID, locationID, productsBatchID, outNum, context);
		}

		#endregion

		#region 更新库位商品可用和在库 (可用加，在库加)

		/// <summary>
		/// 更新库位商品可用和在库 (可用加，在库加)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="inNum">入库数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateKyNumAndZkNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int inNum, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().UpdateKyNumAndZkNum(userCode, productsSkuID, locationID, productsBatchID, inNum, context);
		}

		#endregion

		#region 更新库位商品占用、可用、在库 (发货时调用)

		/// <summary>
		/// 更新库位商品占用、可用、在库 (发货时调用)
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="outNum">发货数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int UpdateZyNumAndKyNumAndZkNum(string userCode, int productsSkuID, int locationID, int productsBatchID, int outNum, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().UpdateZyNumAndKyNumAndZkNum(userCode, productsSkuID, locationID, productsBatchID, outNum, context);
		}

		#endregion

		#region 根据库位ID、商品SKUID、批次ID获取库位可发货库存

		/// <summary>
		/// 根据库位ID、商品SKUID、批次ID获取库位可发货库存
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetKfhNum(int locationID, int productsSkuID, int productsBatchID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetKfhNum(locationID, productsSkuID, productsBatchID, context);
		}

		#endregion

		#region 根据库位ID、商品SKUID、批次ID获取单个实体

		/// <summary>
		/// 根据库位ID、商品SKUID、批次ID获取单个实体
		/// </summary>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="productsBatchID">批次ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static WarehouseLocationProducts GetSingleWarehouseLocationProducts(int locationID, int productsSkuID, int productsBatchID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetSingleWarehouseLocationProducts(locationID, productsSkuID, productsBatchID, context);
		}

		#endregion

		#region 获取 DataTable
		/// <summary>
		/// 
		/// </summary>
		/// <param name="kqid">库区id</param>
		/// <param name="skucode">sku 代码</param>
		/// <param name="WarehouseCode">仓库代码</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static DataTable GetDataTable(int kqid, string skucode, string WarehouseCode, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetDataTable(kqid, skucode, WarehouseCode, context);
		}

		#endregion

		#region 扣减库位占用数量

		/// <summary>
		/// 扣减库位占用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeductionZyNum(string userCode, string warehouseCode, int productsSkuID, int locationID, int productsBatchID, int num, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().DeductionZyNum(userCode, warehouseCode, productsSkuID, locationID, productsBatchID, num, context);
		}

		#endregion

		#region 增加库位占用数量

		/// <summary>
		/// 增加库位占用数量
		/// </summary>
		/// <param name="userCode">用户帐号</param>
		/// <param name="warehouseCode">仓库编码</param>
		/// <param name="productsSkuID">商品SKUID</param>
		/// <param name="locationID">库位ID</param>
		/// <param name="productsBatchID">商品批次ID</param>
		/// <param name="num">占用数量</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int IncreaseZyNum(string userCode, string warehouseCode, int productsSkuID, int locationID, int productsBatchID, int num, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().IncreaseZyNum(userCode, warehouseCode, productsSkuID, locationID, productsBatchID, num, context);
		}

		#endregion

		#region 获取可发货数量大于0的库位商品列表(根据生产日期排序)

		/// <summary>
		/// 获取可发货数量大于0的库位商品列表(根据生产日期排序)
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="produtcsSkuID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<WarehouseLocationProducts> GetManyLocationProducts(string warehouseCode, int produtcsSkuID, IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().GetManyLocationProducts(warehouseCode, produtcsSkuID, context);
		}

		#endregion

		#region 删除在库数量为0的记录

		/// <summary>
		/// 删除在库数量为0的记录
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int DeleteZeroRecord(IDbContext context = null) {
			return WarehouseLocationProductsRepository.GetInstance().DeleteZeroRecord(context);
		}

		#endregion
	}
}





