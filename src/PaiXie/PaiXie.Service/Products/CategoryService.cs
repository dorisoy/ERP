using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
	public class CategoryService : BaseService<Category> {

		#region Update

		public static int Update(Category entity, IDbContext context = null) {
			return CategoryRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(Category entity, IDbContext context = null) {
			return CategoryRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region 根据分类名称和父级ID获取分类ID

		/// <summary>
		/// 根据分类名称和父级ID获取分类ID
		/// </summary>
		/// <param name="categoryName">分类名称</param>
		/// <param name="parentID">父级ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetCategoryID(string categoryName, int parentID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetCategoryID(categoryName, parentID, context);
		}

		#endregion

		#region 根据分类名称和父级ID获取排除指定分类ID之外的分类ID(修改分类时使用)

		/// <summary>
		/// 根据分类名称和父级ID获取排除指定分类ID之外的分类ID(修改分类时使用)
		/// </summary>
		/// <param name="categoryName">分类名称</param>
		/// <param name="parentID">父级ID</param>
		/// <param name="exceptCategoryID">需要排除分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int GetCategoryID(string categoryName, int parentID, int exceptCategoryID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetCategoryID(categoryName, parentID, exceptCategoryID, context);
		}

		#endregion

		#region 删除分类

		/// <summary>
		/// 删除分类
		/// </summary>
		/// <param name="categoryID">要删除的分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Del(int categoryID, IDbContext context = null) {
			return CategoryRepository.GetInstance().Del(categoryID, context);
		}

		#endregion

		#region 根据分类ID获取分类信息

		/// <summary>
		/// 根据分类ID获取分类信息
		/// </summary>
		/// <param name="categoryID">分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Category GetSingleCategory(int categoryID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetSingleCategory(categoryID, context);
		}

		#endregion

		#region 根据父级分类ID获取子级分类ID列表

		/// <summary>
		/// 根据父级分类ID获取子级分类ID列表
		/// </summary>
		/// <param name="parentID">父级分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<int> GetChildCategoryID(int parentID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetChildCategoryID(parentID, context);
		}

		#endregion
	}
}





