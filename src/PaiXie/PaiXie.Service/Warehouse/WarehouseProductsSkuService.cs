using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class WarehouseProductsSkuService  : BaseService<WarehouseProductsSku> {

		public static int Update(WarehouseProductsSku entity, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseProductsSku entity, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// ɾ���ֿ�SKU
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsIDList">��ƷID�б�</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> productsIDList, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().Delete(warehouseCode, productsIDList, context);
		}

		/// <summary>
		/// ������ƷIDɾ���ֿ�SKU
		/// </summary>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int DeleteByProductsID(int productsID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().DeleteByProductsID(productsID, context);
		}
		/// <summary>
		/// ������ƷSKUIDɾ���ֿ�SKU
		/// </summary>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int DeleteByProductsSkuID(int productsSkuID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().DeleteByProductsSkuID(productsSkuID, context);
		}

		/// <summary>
		/// ����SKU���ȡ��ƷSKU��Ϣ
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuCode">SKU��</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static WarehouseProductsSkuInfo GetSingleWarehouseProductsSkuInfo(string warehouseCode, string productsSkuCode, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuCode, context);
		}

		/// <summary>
		/// ����SkuID��ȡ��ƷSKU��Ϣ
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSkuID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static WarehouseProductsSkuInfo GetSingleWarehouseProductsSkuInfo(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().GetSingleWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
		}

		/// <summary>
		/// ����SkuID��ȡ��ƷSKU�б�
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSkuID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<WarehouseProductsSkuInfo> GetManyWarehouseProductsSkuInfo(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseProductsSkuRepository.GetInstance().GetManyWarehouseProductsSkuInfo(warehouseCode, productsSkuID, context);
		}
	}
}





