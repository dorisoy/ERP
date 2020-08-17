using PaiXie.Data;
using System.Collections.Generic;
using FluentData;
namespace PaiXie.Service {
	public class ShopUpdateProductsService : BaseService<ShopUpdateProducts> {

		public static int Update(ShopUpdateProducts entity) {
			return ShopUpdateProductsRepository.GetInstance().Update(entity);
		}



		public static int Add(ShopUpdateProducts entity) {
			return ShopUpdateProductsRepository.GetInstance().Add(entity);
		}

		#region ɾ������������Ʒ
		/// <summary>
		/// ɾ������������Ʒ
		/// </summary>
		/// <param name="ShopID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int Del(int ShopID, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().Del(ShopID,context);
		}
		#endregion

		#region ��ȡʵ���б�

		/// <summary>
		/// ��ȡʵ���б�
		/// </summary>
		/// <param name="shopID">����id </param>
		/// <param name="platformType">ƽ̨</param>
		/// <param name="OuterId">�ⲿ����</param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ShopUpdateProducts> getshopProductslist(int shopID, int platformType, string OuterId, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().getshopProductslist(shopID, platformType, OuterId, context);
		}

		#endregion

		#region ��ȡʵ���б�

		/// <summary>
		/// ��ȡʵ���б�
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="platformType"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<ShopUpdateProducts> getshopProductslist(int shopID, int platformType, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().getshopProductslist(shopID, platformType, context);
		}

		#endregion

		#region ����shopid ��ȡ������

		/// <summary>
		/// ����shopid ��ȡ������
		/// </summary>
		/// <param name="shopid"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string shopProductscount(int shopid, IDbContext context = null) {
			return ShopUpdateProductsRepository.GetInstance().shopProductscount(shopid, context);
		}

		#endregion
	}
}