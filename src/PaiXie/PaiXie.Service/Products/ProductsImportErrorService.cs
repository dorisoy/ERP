using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ProductsImportErrorService  : BaseService<ProductsImportError> {
    
		public static int Update(ProductsImportError entity, IDbContext context = null) {
			return ProductsImportErrorRepository.GetInstance().Update(entity, context);
		}

		public static int Add(ProductsImportError entity, IDbContext context = null) {
			return ProductsImportErrorRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// 删除整张表
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static int Delete(IDbContext context = null) {
			return ProductsImportErrorRepository.GetInstance().Delete(context);
		}

		/// <summary>
		/// 获取商品分页列表
		/// </summary>
		/// <param name="data">sql语句实体类</param>
		/// <param name="count">总条数</param>
		/// <returns></returns>
		public static List<ProductsImportError> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<ProductsImportError> obj = new BaseRepository<ProductsImportError>();
			return obj.GetQueryManyForPage(data, out  count);
		}
	}
}





