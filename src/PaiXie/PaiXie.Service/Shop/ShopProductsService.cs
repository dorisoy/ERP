using PaiXie.Data;
using System.Collections.Generic;
using FluentData;
namespace PaiXie.Service {
	public class ShopProductsService : BaseService<ShopProducts> {

		public static int Update(ShopProducts entity) {
			return ShopProductsRepository.GetInstance().Update(entity);
		}



		public static int Add(ShopProducts entity) {
			return ShopProductsRepository.GetInstance().Add(entity);
		}
		#region ���ݵ���ID����Ʒ�����ѯ������Ʒ��Ϣ
		/// <summary>
		/// ���ݵ���ID����Ʒ���Ų�ѯ��Ʒ��Ϣ
		/// </summary>
		/// <param name="shopID">����ID</param>
		/// <param name="outerId">ƽ̨��Ʒ����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ShopProducts GetSingleShopProductsByOuterId(int shopID, string outerId, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().GetSingleShopProductsByOuterId(shopID, outerId, context);
		}

		#endregion

		#region ���ݵ���ID��goodsId��ѯ������Ʒ��Ϣ
		/// <summary>
		/// ���ݵ���ID��goodsId��ѯ��Ʒ��Ϣ
		/// </summary>
		/// <param name="shopID">����ID</param>
		/// <param name="goodsId">goodsId</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ShopProducts GetSingleShopProductsByGoodsId(int shopID, int goodsId, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().GetSingleShopProductsByGoodsId(shopID, goodsId, context);
		}

		#endregion

		#region ���ݵ�����Ʒ���ʶ��ѯ������Ʒ��Ϣ
		/// <summary>
		/// ���ݵ�����Ʒ���ʶ��ѯ������Ʒ��Ϣ
		/// </summary>
		/// <param name="shopProductsID">������Ʒ���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ShopProducts GetSingleShopProducts(int shopProductsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().GetSingleShopProducts(shopProductsID, context);
		}

		#endregion

		#region ����ɹ�֮�������ƷID

		/// <summary>
		/// ����ɹ�֮�������ƷID
		/// </summary>
		/// <param name="shopProductsID">������Ʒ���ʶ</param>
		/// <param name="productsID">ϵͳ��Ʒ���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateProductsID(int shopProductsID, int productsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().UpdateProductsID(shopProductsID, productsID, context);
		}

		#endregion

		#region ����ʧ��֮����´�����ʾ��Ϣ

		/// <summary>
		/// ����ʧ��֮����´�����ʾ��Ϣ
		/// </summary>
		/// <param name="shopProductsID">������Ʒ���ʶ</param>
		/// <param name="errorMessage">������ʾ��Ϣ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateErrorMessage(int shopProductsID, string errorMessage, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().UpdateErrorMessage(shopProductsID, errorMessage, context);
		}

		#endregion

		#region ����ϵͳ��Ʒ���ʶ��ɾ����Ӧ������������Ʒ

		/// <summary>
		/// ����ϵͳ��Ʒ���ʶ��ɾ����Ӧ������������Ʒ
		/// </summary>
		/// <param name="productsID">ϵͳ��Ʒ���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int DelByProductsID(int productsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().DelByProductsID(productsID, context);
		}

		#endregion

		#region ɾ��������Ʒ

		/// <summary>
		/// ɾ��������Ʒ
		/// </summary>
		/// <param name="shopProductsID">Ҫɾ���ĵ�����Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int shopProductsID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().Del(shopProductsID, context);
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
			return ShopProductsRepository.GetInstance().shopProductscount(shopid, context);
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
		public static List<ShopProducts> getshopProductslist(int shopID, int platformType, string OuterId , IDbContext context = null) {
			return ShopProductsRepository.GetInstance().getshopProductslist(shopID, platformType,  OuterId , context);
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
		public static List<ShopProducts> getshopProductslist(int shopID, int platformType, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().getshopProductslist(shopID, platformType, context);
		}

		#endregion

		#region ɾ��

	/// <summary>
		/// ɾ��
	/// </summary>
	/// <param name="shopID"></param>
	/// <param name="platformType"></param>
	/// <param name="context"></param>
	/// <returns></returns>
		public static int Del(int shopID, int platformType, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().Del(shopID, platformType, context);
		}

		#endregion

		#region ɾ��  ����shopid
		/// <summary>
		///   ����shopid
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int DelbyshopID(int shopID, IDbContext context = null) {
			return ShopProductsRepository.GetInstance().DelbyshopID(shopID, context);
		}
		#endregion


		#region ɾ�� ���ݵ���id  ��Ʒ����
		/// <summary>
		/// ɾ�� ���ݵ���id  ��Ʒ����
		/// </summary>
		/// <param name="shopID"></param>
		/// <param name="ProductsCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  int DelshopStockUpdateSinge(int shopID, string ProductsCode, IDbContext context = null) {

			return ShopProductsRepository.GetInstance().DelshopStockUpdateSinge(shopID,ProductsCode, context);
		
		}
		#endregion


		
	}
}