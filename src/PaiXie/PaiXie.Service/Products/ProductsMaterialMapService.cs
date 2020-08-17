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

		#region ��������IDɾ��

		/// <summary>
		/// ��������IDɾ��
		/// </summary>
		/// <param name="productsMaterialMapID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int productsMaterialMapID, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().Del(productsMaterialMapID, context);
		}

		#endregion

		#region �������Ϲ������ʶ��ȡ���Ϲ�����Ϣ

		/// <summary>
		/// �������Ϲ������ʶ��ȡ���Ϲ�����Ϣ
		/// </summary>
		/// <param name="productsMaterialMapID">���Ϲ������ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsMaterialMap GetSingleProductsMaterialMap(int productsMaterialMapID, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().GetSingleProductsMaterialMap(productsMaterialMapID, context);
		}

		#endregion

		#region �ж��Ƿ����

		/// <summary>
		/// �ж��Ƿ����
		/// </summary>
		/// <param name="sourceProductsSkuID">Ҫ�������ϵ�SKUID</param>
		/// <param name="fromProductsSkuID">����SKUID</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool IsExists(int sourceProductsSkuID, int fromProductsSkuID, IDbContext context = null) {
			return ProductsMaterialMapRepository.GetInstance().IsExists(sourceProductsSkuID, fromProductsSkuID, context);
		}

		#endregion
	}
}





