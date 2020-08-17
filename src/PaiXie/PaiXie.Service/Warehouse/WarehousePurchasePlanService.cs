using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehousePurchasePlanService  : BaseService<WarehousePurchasePlan> {

		#region Update

		public static int Update(WarehousePurchasePlan entity, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehousePurchasePlan entity, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Add(entity, context);
		}

		#endregion
		
		#region ���ݲɹ��ƻ���IDɾ����¼

		/// <summary>
		/// ���ݲɹ��ƻ���IDɾ����¼
		/// </summary>
		/// <param name="projectType">1:����� 2:�ֿ�� ʹ��ö��</param>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Delete(int projectType, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Delete(projectType, planID, context);
		}

		#endregion

		#region ���ݲɹ��ƻ���ID������¼

		/// <summary>
		/// ���ݲɹ��ƻ���ID������¼
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int End(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().End(userCode, planID, context);
		}

		#endregion

		#region ���ݲɹ��ƻ���ID���ѽ���״̬��ԭΪ���ύ

		/// <summary>
		/// ���ݲɹ��ƻ���ID���ѽ���״̬��ԭΪ���ύ
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Restore(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().Restore(userCode, planID, context);
		}

		#endregion

		#region  ��ȡ�ɹ��ƻ���ʵ��

		/// <summary>
		/// ��ȡ�ɹ��ƻ���ʵ��
		/// </summary>
		/// <param name="planID">�ɹ��ƻ���ID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehousePurchasePlan GetSingleWarehousePurchasePlan(int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().GetSingleWarehousePurchasePlan(planID, context);
		}

		#endregion

		#region ���²ɹ��ƻ����ļƻ�����

		/// <summary>
		/// ���²ɹ��ƻ����ļƻ�����
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planID">�ƻ�������ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateNum(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().UpdateNum(userCode, planID, context);
		}

		#endregion

		#region ���²ɹ��ƻ������Ѳɹ�����

		/// <summary>
		/// ���²ɹ��ƻ������Ѳɹ�����
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planID">�ƻ�������ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdatePurchasedNum(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().UpdatePurchasedNum(userCode, planID, context);
		}

		#endregion

		#region ���²ɹ��ƻ����Ĳɹ�������

		/// <summary>
		/// ���²ɹ��ƻ����Ĳɹ�������
		/// </summary>
		/// <param name="userCode">�û��ʺ�</param>
		/// <param name="planID">�ƻ�������ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdatePurchaseOrderCount(string userCode, int planID, IDbContext context = null) {
			return WarehousePurchasePlanRepository.GetInstance().UpdatePurchaseOrderCount(userCode, planID, context);
		}

		#endregion
	}
}





