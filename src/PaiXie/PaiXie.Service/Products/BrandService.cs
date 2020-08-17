using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class BrandService : BaseService<Brand> {

		#region Update

		public static int Update(Brand entity, IDbContext context = null) {
			return BrandRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(Brand entity, IDbContext context = null) {
			return BrandRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 根据品牌名称获取品牌ID

		/// <summary>
		/// 根据品牌名称获取品牌ID
		/// </summary>
		/// <param name="brandName">品牌名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetBrandID(string brandName, IDbContext context = null) {
			return BrandRepository.GetInstance().GetBrandID(brandName, context);
		}

		#endregion

		#region 根据品牌名称获取排除指定品牌ID之外的品牌ID(修改品牌时使用)

		/// <summary>
		/// 根据品牌名称获取排除指定品牌ID之外的品牌ID(修改品牌时使用)
		/// </summary>
		/// <param name="brandName">品牌名称</param>
		/// <param name="exceptBrandID">需要排除品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetBrandID(string brandName, int exceptBrandID, IDbContext context = null) {
			return BrandRepository.GetInstance().GetBrandID(brandName, exceptBrandID, context);
		}

		#endregion

		#region 删除品牌

		/// <summary>
		/// 删除品牌
		/// </summary>
		/// <param name="brandID">要删除的品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int brandID, IDbContext context = null) {
			return BrandRepository.GetInstance().Del(brandID, context);
		}

		#endregion

		#region 根据品牌ID获取品牌信息

		/// <summary>
		/// 根据品牌ID获取品牌信息
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Brand GetSingleBrand(int brandID, IDbContext context = null) {
			return BrandRepository.GetInstance().GetSingleBrand(brandID, context);
		}

		#endregion

		#region 根据仓库编码获取授权品牌信息

		/// <summary>
		/// 根据仓库编码获取授权品牌信息
		/// </summary>
		/// <param name="brandID">品牌ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Brand> GetManyBrandByWarehouseCode(string warehouseCode, IDbContext context = null) {
			return BrandRepository.GetInstance().GetManyBrandByWarehouseCode(warehouseCode, context);
		}

		#endregion

		#region 获取品牌信息

		/// <summary>
		/// 获取品牌信息
		/// </summary>
		/// <returns></returns>
		public static List<Brand> GetBrand() {
			return BrandRepository.GetInstance().GetBrand();

		}

		#endregion
	}
}





