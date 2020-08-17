using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class WarehouseBookingProductsSkuService  : BaseService<WarehouseBookingProductsSku> {

		public static int Update(WarehouseBookingProductsSku entity, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseBookingProductsSku entity, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// ȡ��Ԥ����Ʒ
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().Delete(warehouseCode, productsID, context);
		}

		/// <summary>
		/// ������ƷID����SKUԤ���б�
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static List<WarehouseBookingProductsSku> GetManyWarehouseBookingProductsSku(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetManyWarehouseBookingProductsSku(warehouseCode, productsID, context);
		}

		/// <summary>
		/// ��ȡԤ����Ϣ
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="productsSkuID">SkuId</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehouseBookingProductsSku GetSingleWarehouseBookingProductsSku(string warehouseCode, int productsID, int productsSkuID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetSingleWarehouseBookingProductsSku(warehouseCode, productsID, productsSkuID, context);
		}

		/// <summary>
		/// ��ȡԤ����Ʒ��ҳ�б�
		/// </summary>
		/// <param name="data">sql���ʵ����</param>
		/// <param name="count">������</param>
		/// <returns></returns>
		public static List<WarehouseBookingProductsList> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<WarehouseBookingProductsList> obj = new BaseRepository<WarehouseBookingProductsList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		/// <summary>
		/// ������ƷID����SKUԤ���б�
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<WarehouseBookingProductsSkuInfo> GetManyWarehouseBookingProductsSkuInfo(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetManyWarehouseBookingProductsSkuInfo(warehouseCode, productsID, context);
		}

		/// <summary>
		/// ��ȡ��ƷԤ����Ϣ
		/// </summary>
		/// <param name="warehouseCode"></param>
		/// <param name="productsID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static WarehouseBookingProductsList GetSingleWarehouseBookingProducts(string warehouseCode, int productsID, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().GetSingleWarehouseBookingProducts(warehouseCode, productsID, context);
		}

		#region �ۼ�Ԥ��ռ��

		/// <summary>
		/// �ۼ�Ԥ��ռ��
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="num">ռ������</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int DeductionZyNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().DeductionZyNum(userCode, warehouseCode, productsSkuID, num, context);
		}

		#endregion

		#region ���ӳ������

		/// <summary>
		/// ���ӳ������
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="num">�������</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int IncreaseCdNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().IncreaseCdNum(userCode, warehouseCode, productsSkuID, num, context);
		}

		#endregion

		#region ����Ԥ��ռ��

		/// <summary>
		/// ����Ԥ��ռ��
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="num">ռ������</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int IncreaseZyNum(string userCode, string warehouseCode, int productsSkuID, int num, IDbContext context = null) {
			return WarehouseBookingProductsSkuRepository.GetInstance().IncreaseZyNum(userCode, warehouseCode, productsSkuID, num, context);
		}

		#endregion
	}
}





