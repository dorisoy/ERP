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
		/// ɾ�����ű�
		/// </summary>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Delete(IDbContext context = null) {
			return ProductsImportErrorRepository.GetInstance().Delete(context);
		}

		/// <summary>
		/// ��ȡ��Ʒ��ҳ�б�
		/// </summary>
		/// <param name="data">sql���ʵ����</param>
		/// <param name="count">������</param>
		/// <returns></returns>
		public static List<ProductsImportError> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<ProductsImportError> obj = new BaseRepository<ProductsImportError>();
			return obj.GetQueryManyForPage(data, out  count);
		}
	}
}





