using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class CategoryRepository : BaseRepository<Category> {

		#region 构造函数
		private static CategoryRepository _instance;
		public static CategoryRepository GetInstance() {
			if (_instance == null) {
				_instance = new CategoryRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(Category entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int id = context.Insert<Category>("category", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return id;
		}
		#endregion

		#region Update
		public int Update(Category entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<Category>("category", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
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
		public int GetCategoryID(string categoryName, int parentID, IDbContext context = null) {
			Object[] objects = new Object[2];
			objects[0] = categoryName;
			objects[1] = parentID;
			string sqlStr = "SELECT ID FROM category WHERE Name=@0 and ParentID=@1";
			Category category = GetQuerySingle(sqlStr, context, objects);
			int categoryID = 0;
			if (category != null) {
				categoryID = category.ID;
			}
			return categoryID;
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
		public int GetCategoryID(string categoryName, int parentID, int exceptCategoryID, IDbContext context = null) {
			Object[] objects = new Object[3];
			objects[0] = categoryName;
			objects[1] = parentID;
			objects[2] = exceptCategoryID;
			string sqlStr = "SELECT ID FROM category WHERE Name=@0 and ParentID=@1 and ID<>@2";
			Category category = GetQuerySingle(sqlStr, context, objects);
			int categoryID = 0;
			if (category != null) {
				categoryID = category.ID;
			}
			return categoryID;
		}

		#endregion

		#region 删除分类

		/// <summary>
		/// 删除分类
		/// </summary>
		/// <param name="categoryID">分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Del(int categoryID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = categoryID;
			string sqlStr = "DELETE FROM category Where ID=@0";
			return Del(sqlStr, context, objects);
		}

		#endregion

		#region 根据分类ID获取分类信息

		/// <summary>
		/// 根据分类ID获取分类信息
		/// </summary>
		/// <param name="categoryID">分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public Category GetSingleCategory(int categoryID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = categoryID;
			string sqlStr = "SELECT * FROM category Where ID=@0";
			return GetQuerySingle(sqlStr, context, objects);
		}

		#endregion

		#region 根据父级分类ID获取子级分类ID列表

		/// <summary>
		/// 根据父级分类ID获取子级分类ID列表
		/// </summary>
		/// <param name="parentID">父级分类ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public List<int> GetChildCategoryID(int parentID, IDbContext context = null) {
			Object[] objects = new Object[1];
			objects[0] = parentID;
			string sqlStr = "SELECT ID FROM category WHERE ParentID=@0";
			List<Category> categoryList = GetQueryMany(sqlStr, context, objects);
			List<int> categoryIDList = new List<int>();
			foreach (var item in categoryList) {
				categoryIDList.Add(item.ID);
			}
			return categoryIDList;
		}

		#endregion
	}
}





