using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using PaiXie.Core;
using FluentData;
namespace PaiXie.Service {
	public class ProductsService : BaseService<Products> {

		#region Update

		public static int Update(Products entity, out string oldMessage, out string newMessage, IDbContext context = null) {
			return ProductsRepository.GetInstance().Update(entity, out oldMessage, out newMessage, context);
		}

		#endregion

		#region Add

		public static int Add(Products entity, IDbContext context = null) {
			return ProductsRepository.GetInstance().Add(entity, context);
		}
		
		#endregion

		#region ������Ʒ�����ȡ��ƷID

		/// <summary>
		/// ������Ʒ�����ȡ��ƷID
		/// </summary>
		/// <param name="productsCode">��Ʒ����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetProductsID(string productsCode, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsID(productsCode, context);
		}

		#endregion

		#region ������Ʒ�����ȡ�ų�ָ����ƷID֮�����ƷID

		/// <summary>
		/// ������Ʒ�����ȡ�ų�ָ����ƷID֮�����ƷID
		/// </summary>
		/// <param name="productsCode">��Ʒ����</param>
		/// <param name="exceptProductsID">�ų���ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetProductsID(string productsCode, int exceptProductsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsID(productsCode, exceptProductsID, context);
		}

		#endregion

		#region ������ƷID��ȡ��Ʒ��Ϣ

		/// <summary>
		/// ������ƷID��ȡ��Ʒ��Ϣ
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static Products GetSingleProducts(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetSingleProducts(productsID,context);
		}

		#endregion

		#region ������Ʒ�����ȡ��Ʒ��Ϣ

		/// <summary>
		/// ������Ʒ�����ȡ��Ʒ��Ϣ
		/// </summary>
		/// <param name="productsID">��Ʒ����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static Products GetSingleProducts(string productsCode, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetSingleProducts(productsCode, context);
		}

		#endregion

		#region ������Ʒ����Ʒ��

		/// <summary>
		/// ������Ʒ����Ʒ��
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="productsIDList">��ƷID�б�</param>
		/// <param name="brandID">Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateProductsBrand(string userCode, List<int> productsIDList, int brandID, IDbContext context = null) {
			return ProductsRepository.GetInstance().UpdateProductsBrand(userCode, productsIDList, brandID, context);
		}

		#endregion

		#region ������Ʒ��������

		/// <summary>
		/// ������Ʒ��������
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="productsIDList">��ƷID�б�</param>
		/// <param name="categoryID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateProductsCategory(string userCode, List<int> productsIDList, int categoryID, IDbContext context = null) {
			return ProductsRepository.GetInstance().UpdateProductsCategory(userCode, productsIDList, categoryID, context);
		}

		#endregion

		#region ��Ʒ���¼�

		/// <summary>
		/// ��Ʒ���¼�
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="status">������=1   �ֿ���=2 ö������</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateProductsStatus(string userCode, int productsID, int status, IDbContext context = null) {
			return ProductsRepository.GetInstance().UpdateProductsStatus(userCode, productsID, status, context);
		}

		#endregion

		#region ɾ����Ʒ

		/// <summary>
		/// ɾ����Ʒ
		/// </summary>
		/// <param name="productsID">Ҫɾ������ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().Del(productsID, context);
		}

		#endregion

		#region ��ȡָ��Ʒ�Ƶ���ƷID�б�

		/// <summary>
		/// ��ȡָ��Ʒ�Ƶ���ƷID�б�
		/// </summary>
		/// <param name="brandID">Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<int> GetProductsIDListByBrandID(int brandID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsIDListByBrandID(brandID, context);
		}

		#endregion

		#region ��ȡָ���������ƷID�б�

		/// <summary>
		/// ��ȡָ���������ƷID�б�
		/// </summary>
		/// <param name="categoryID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<int> GetProductsIDListByCategoryID(int categoryID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsIDListByCategoryID(categoryID, context);
		}
		
		#endregion

		#region ����ҳ��������ȡ��ƷID�б�

		/// <summary>
		/// ����ҳ��������ȡ��ƷID�б�
		/// </summary>
		/// <param name="data">sql���ʵ����</param>
		/// <param name="count">������</param>
		/// <returns></returns>
		public static List<int> GetProductsIDListForPage(SelectBuilder data, out int count) {
			BaseRepository<Products> obj = new BaseRepository<Products>();
			List<Products> list = obj.GetQueryManyForPage(data, out  count);
			List<int> productsIDList = new List<int>();
			productsIDList.AddRange(list.Select(row => row.ID));
			return productsIDList;
		}

		#endregion

		#region ��ȡָ����ƷID�Ŀ����Ϣ

		/// <summary>
		/// ��ȡָ����ƷID�Ŀ����Ϣ
		/// </summary>
		/// <param name="productsID">��Ʒ���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static DataTable GetProductsSkuKucInfo(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsKucInfo(productsID, context);
		}

		#endregion

		#region ��ȡָ����ƷID���ֿ�����Ϣ

		/// <summary>
		/// ��ȡָ����ƷID���ֿ�����Ϣ
		/// </summary>
		/// <param name="productsID">��Ʒ���ʶ</param>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static DataTable GetWarehouseProductsSkuKucInfo(string warehouseCode,int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetWarehouseProductsKucInfo(warehouseCode, productsID, context);
		}

		#endregion

		#region ��ȡָ����ƷID���Ϲ�����Ϣ ����ͳ��

		/// <summary>
		/// ��ȡָ����ƷID���Ϲ�����Ϣ
		/// </summary>
		/// <param name="productsID">��Ʒ���ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static DataTable GetProductsMaterialMapInfo(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsMaterialMapInfo(productsID, context);
		}

		#endregion

		#region ��ȡָ����ƷSKUID���Ϲ�����Ϣ

		/// <summary>
		/// ��ȡָ����ƷSKUID���Ϲ�����Ϣ
		/// </summary>
		/// <param name="sourceProductsSkuID">��ƷSKU��ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static DataTable GetProductsSkuMaterialMapInfo(int sourceProductsSkuID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsSkuMaterialMapInfo(sourceProductsSkuID, context);
		}

		#endregion

		#region ������ƷID��ȡSKU���Ϲ����б�

		/// <summary>
		/// ������ƷID��ȡSKU���Ϲ����б�
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<ProductsSkuMaterialMapInfo> GetManyProductsSkuMaterialMapInfo(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetManyProductsSkuMaterialMapInfo(productsID, context);
		}

		#endregion

		#region ��ȡ�ɷ������
		/// <summary>
		/// ��ȡ�ɷ������
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <returns></returns>
		public static int GetKfhNumByProductsID(int productsID, IDbContext context = null) {
			int KfhNum = 0;
			ProductsInventory inventory = GetProductsInventory(productsID, context);
			if (inventory != null) {
				KfhNum = inventory.KyNum - inventory.ZyNum - inventory.OrdZyNum + inventory.BookingKyNum;
			}
			return KfhNum;
		}

		#endregion

		#region ������ƷID��ȡ�ɷ��������Ϣ

		/// <summary>
		/// ������ƷID��ȡ�ɷ��������Ϣ
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ProductsInventory GetProductsInventory(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetProductsInventory(productsID, context);
		}

		#endregion

		#region ��ȡ��Ʒ�����������ж���Ʒ�Ƿ��ɾ��
		/// <summary>
		/// ��ȡ��Ʒ�����������ж���Ʒ�Ƿ��ɾ��
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="warehouseCode">�ֿ���</param> 
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetTotalNum(int productsID,string warehouseCode, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetTotalNum(productsID, warehouseCode, context);
		}

		#endregion

		#region ��ȡ��Ʒ�ɹ���

		/// <summary>
		/// ��ȡ��Ʒ�ɹ���
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����</param>
		/// <returns></returns>
		public static decimal GetCostPrice(int productsID, IDbContext context = null) {
			return ProductsRepository.GetInstance().GetCostPrice(productsID, context);
		}

		#endregion
	}
}





