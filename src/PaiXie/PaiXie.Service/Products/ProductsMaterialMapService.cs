using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
	public class ProductsMaterialMapService : BaseService<ProductsMaterialMap> {

		#region Update

		public static int Update(ProductsMaterialMap entity, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add
		
		public static int Add(ProductsMaterialMap entity, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 根据主键ID删除

		/// <summary>
		/// 根据主键ID删除
		/// </summary>
		/// <param name="productsMaterialMapID">主键ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int productsMaterialMapID, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().Del(productsMaterialMapID, context);
		}

		#endregion

		#region 根据物料关联表标识获取物料关联信息

		/// <summary>
		/// 根据物料关联表标识获取物料关联信息
		/// </summary>
		/// <param name="productsMaterialMapID">物料关联表标识</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static ProductsMaterialMap GetSingleProductsMaterialMap(int productsMaterialMapID, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().GetSingleProductsMaterialMap(productsMaterialMapID, context);
		}

		#endregion

		#region 判断是否存在

		/// <summary>
		/// 判断是否存在
		/// </summary>
		/// <param name="sourceProductsSkuID">要关联物料的SKUID</param>
		/// <param name="fromProductsSkuID">物料SKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool IsExists(int sourceProductsSkuID, int fromProductsSkuID, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().IsExists(sourceProductsSkuID, fromProductsSkuID, context);
		}

		#endregion
	}
}





