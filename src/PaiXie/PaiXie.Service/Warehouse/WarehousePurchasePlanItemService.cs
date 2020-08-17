using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePurchasePlanItemService  : BaseService<WarehousePurchasePlanItem> {

		#region Update

		public static int Update(WarehousePurchasePlanItem entity, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehousePurchasePlanItem entity, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region ���ݼƻ�������ID����ƷSKUID��ȡ����ʵ��

		/// <summary>
		/// ���ݼƻ�������ID����ƷSKUID��ȡ����ʵ��
		/// </summary>
		/// <param name="planID">�ƻ�������ID</param>
		/// <param name="productsSkuID">��ƷSKUID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static WarehousePurchasePlanItem GetSingleWarehousePurchasePlanItem(int planID, int productsSkuID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetSingleWarehousePurchasePlanItem(planID, productsSkuID, context);
		}

		#endregion

		#region �����Ʒʱ��ȡSKU�б�

		/// <summary>
		/// �����Ʒʱ��ȡSKU�б�
		/// </summary>
		/// <param name="data">sql���ʵ����</param>
		/// <param name="count">������</param>
		/// <returns></returns>
		public static List<WarehousePurchasePlanSkuList> GetQueryManyForSkuList(SelectBuilder data, out int count) {
			BaseRepository<WarehousePurchasePlanSkuList> obj = new BaseRepository<WarehousePurchasePlanSkuList>();
			return obj.GetQueryManyForPage(data, out  count);
		}

		#endregion

		#region ��ȡ�ƻ����������Ѳɹ���������ϸ�б�(���ɲɹ���)

		/// <summary>
		/// ��ȡ�ƻ����������Ѳɹ���������ϸ�б�(���ɲɹ���)
		/// </summary>
		/// <param name="planItemIDList">�ɹ��ƻ�����Ʒ������ID�б�</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<WarehousePurchasePlanItem> GetWarehousePurchasePlanItemList(List<int> planItemIDList, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetWarehousePurchasePlanItemList(planItemIDList, context);
		}

		#endregion

		#region ��ȡ����

		/// <summary>
		/// ��ȡ����
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="productsID">��ƷID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetCount(string warehouseCode, int planID, int productsID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetCount(warehouseCode, planID, productsID, context);
		}

		#endregion

		#region ���ݼƻ�����Ʒ��IDɾ����¼

		/// <summary>
		/// ���ݼƻ�����Ʒ��IDɾ����¼
		/// </summary>
		/// <param name="projectType">1:����� 2:�ֿ�� ʹ��ö��</param>
		/// <param name="ruleItemID">�ƻ�����Ʒ��ID�б�</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Delete(int projectType, List<int> planItemIDList, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().Delete(projectType, planItemIDList, context);
		}

		#endregion

		#region ���ݲɹ��ƻ���IDɾ����¼

		/// <summary>
		/// ���ݲɹ��ƻ���IDɾ����¼
		/// </summary>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int DeleteByPlanID(int planID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().DeleteByPlanID(planID, context);
		}

		#endregion

		#region ���üƻ��ɹ���Ʒ��Ӧ��

		/// <summary>
		/// ���üƻ��ɹ���Ʒ��Ӧ��
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planItemID">�ƻ��ɹ���Ʒ������ID</param>
		/// <param name="suppliersID">��Ӧ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateSuppliersID(string userCode, int planItemID, int suppliersID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().UpdateSuppliersID(userCode, planItemID, suppliersID, context);
		}

		#endregion

		#region ���¼ƻ��ɹ���Ʒ�Ѳɹ�����

		/// <summary>
		/// ���¼ƻ��ɹ���Ʒ�Ѳɹ�����
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planItemID">�ƻ��ɹ���Ʒ������ID</param>
		/// <param name="diffNum">�Ѳɹ����� �������¿����ɸ�</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdatePurchasedNum(string userCode, int planItemID, int diffNum, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().UpdatePurchasedNum(userCode, planItemID, diffNum, context);
		}

		#endregion

		#region ���ݲɹ��ƻ���ID��ȡδ�ɹ���ɵ���ϸ����

		/// <summary>
		/// ���ݲɹ��ƻ���ID��ȡδ�ɹ���ɵ���ϸ����
		/// </summary>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetNotFinCount(int planID, IDbContext context = null) {
			return WarehousePurchasePlanItemRepository.GetInstance().GetNotFinCount(planID, context);
		}

		#endregion
	}
}





