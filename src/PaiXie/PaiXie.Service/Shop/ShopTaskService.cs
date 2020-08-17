using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class ShopTaskService  : BaseService<ShopTask> {
    
		public static int Add(ShopTask entity, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// ��������״̬
		/// </summary>
		/// <param name="taskID">�����ʶ</param>
		/// <param name="taskStatus">����״̬ ö��ֵ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateStatus(string taskID, int taskStatus, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateStatus(taskID, taskStatus, context);
		}

		#region �����������(ָ������)

		/// <summary>
		/// �����������(ָ������)
		/// </summary>
		/// <param name="taskID"></param>
		/// <param name="finshCount"></param>
		/// <param name="taskStatus"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int UpdateFinshCount(string taskID, int finshCount, int taskStatus, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateFinshCount(taskID, finshCount, taskStatus, context);
		}

		#endregion

		/// <summary>
		/// �����������(�Զ���1)
		/// </summary>
		/// <param name="taskID">�����ʶ</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateFinshCount(string taskID, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateFinshCount(taskID, context);
		}

		/// <summary>
		/// ��������������
		/// </summary>
		/// <param name="taskID">�����ʶ</param>
		/// <param name="TotalCount">����������</param>
		/// <param name="requestMessage">������</param>
		/// <param name="responseMessage">��Ӧ����</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int UpdateTotalCount(string taskID, int totalCount, string requestMessage, string responseMessage, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().UpdateTotalCount(taskID, totalCount, requestMessage, responseMessage, context);
		}

		/// <summary>
		/// ��������ID��ȡ������Ϣ
		/// </summary>
		/// <param name="taskID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ShopTask GetSingleShopTask(string taskID, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().GetSingleShopTask(taskID, context);
		}

		/// <summary>
		/// ���ݵ���ID���������͡���ȡ���һ��������Ϣ
		/// </summary>
		/// <param name="shopID">����ID</param>
		/// <param name="taskType">�������� ö��</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static ShopTask GetSingleShopTask(int shopID, int taskType, IDbContext context = null) {
			return ShopTaskRepository.GetInstance().GetSingleShopTask(shopID, taskType, context);
		}
	}
}





