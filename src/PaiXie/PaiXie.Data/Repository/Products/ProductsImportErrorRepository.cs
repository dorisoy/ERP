using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;
namespace PaiXie.Data {
	public class ProductsImportErrorRepository : BaseRepository<ProductsImportError> {

		#region 构造函数
		private static ProductsImportErrorRepository _instance;
		public static ProductsImportErrorRepository GetInstance() {
			if (_instance == null) {
				_instance = new ProductsImportErrorRepository();
			}
			return _instance;
		}
		#endregion

		#region Add
		public int Add(ProductsImportError entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int Id = context.Insert<ProductsImportError>("productsImportError", entity)
						.AutoMap(x => x.ID)
						.ExecuteReturnLastId<int>();
			return Id;
		}
		#endregion

		#region Update
		public int Update(ProductsImportError entity, IDbContext context = null) {
			if (context == null) context = Db.GetInstance().Context();
			int rowsAffected = context.Update<ProductsImportError>("productsImportError", entity)
			.AutoMap(x => x.ID)
			.Where(x => x.ID)
			.Execute();
			return rowsAffected;
		}
		#endregion

		#region 删除整张表

		/// <summary>
		/// 删除整张表
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public int Delete(IDbContext context = null) {
			string sqlStr = @"Delete FROM productsImportError";
			return Del(sqlStr, context);
		}

		#endregion
	}
}





