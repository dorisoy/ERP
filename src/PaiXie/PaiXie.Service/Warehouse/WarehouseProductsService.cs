using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using PaiXie.Core;
using PaiXie.Utils;
using FluentData;
namespace  PaiXie.Service 
{
	public class WarehouseProductsService : BaseService<WarehouseProducts> {

		public static int Update(WarehouseProducts entity, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseProducts entity, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// ɾ���ֿ���Ʒ
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsIDList">��ƷID�б�</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> productsIDList, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().Delete(warehouseCode, productsIDList, context);
		}

		/// <summary>
		/// ������ƷIDɾ���ֿ���Ʒ
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int DeleteByProductsID(int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().DeleteByProductsID(productsID, context);
		}


		/// <summary>
		/// ��Ʒ���¼�
		/// </summary>
		/// <param name="warehouseCode">�ֿ���� �������ֵ����������вֿ�</param>
		/// <param name="productsIDList">��ƷID�б�</param>
		/// <param name="productsStatus">��Ʒ����״̬ ������=1 �ֿ���=2 </param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateProductsStatus(string warehouseCode, List<int> productsIDList, int productsStatus, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().UpdateProductsStatus(warehouseCode, productsIDList, productsStatus, context);
		}

		/// <summary>
		/// ��ȡ����
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsIDList">��ƷID�б�</param>
		/// <param name="productsStatus">��Ʒ����״̬ ������=1 �ֿ���=2 </param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetCount(string warehouseCode, List<int> productsIDList, int productsStatus, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetCount(warehouseCode, productsIDList, productsStatus, context);
		}

		/// <summary>
		/// ��ȡ��Ʒʵ��
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehouseProducts GetSingleWarehouseProducts(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetSingleWarehouseProducts(warehouseCode, productsID, context);
		}

		/// <summary>
		/// ��ȡ��Ʒʵ��
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsCode">��Ʒ����</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehouseProductsInfo GetSingleWarehouseProductsInfo(string warehouseCode, string productsCode, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetSingleWarehouseProductsInfo(warehouseCode, productsCode, context);
		}

		/// <summary>
		/// ��ȡ��Ʒʵ��
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehouseProductsInfo GetSingleWarehouseProductsInfo(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetSingleWarehouseProductsInfo(warehouseCode, productsID, context);
		}

		#region ������ƷID��ȡ�ֿ���Ʒ�б�

		/// <summary>
		/// ������ƷID��ȡ�ֿ���Ʒ�б�
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<WarehouseProducts> GetWarehouseProductsList(int productsID, IDbContext context = null) {
			return WarehouseProductsRepository.GetInstance().GetWarehouseProductsList(productsID, context);
		}

		#endregion
	}

}





