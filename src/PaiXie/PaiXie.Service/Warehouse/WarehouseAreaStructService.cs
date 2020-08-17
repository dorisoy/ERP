using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseAreaStructService  : BaseService<WarehouseAreaStruct> {

		#region Update

		public static int Update(WarehouseAreaStruct entity, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(WarehouseAreaStruct entity, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region ���ݽṹ���ƺ͸���ID��ȡ�ṹID

		/// <summary>
		/// ���ݽṹ���ƺ͸���ID��ȡ�ṹID
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="areaStructName">�ṹ����</param>
		/// <param name="parentID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetWarehouseAreaStructID(string warehouseCode, string areaStructName, int parentID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetWarehouseAreaStructID(warehouseCode, areaStructName, parentID, context);
		}

		#endregion

		#region ���ݽṹ���ƺ͸���ID��ȡ�ų�ָ���ṹID֮��ĽṹID(�޸Ľṹʱʹ��)

		/// <summary>
		/// ���ݽṹ���ƺ͸���ID��ȡ�ų�ָ���ṹID֮��ĽṹID(�޸Ľṹʱʹ��)
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="areaStructName">�ṹ����</param>
		/// <param name="parentID">����ID</param>
		/// <param name="exceptWarehouseAreaStructID">��Ҫ�ų��ṹID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetWarehouseAreaStructID(string warehouseCode, string areaStructName, int parentID, int exceptWarehouseAreaStructID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetWarehouseAreaStructID(warehouseCode, areaStructName, parentID, exceptWarehouseAreaStructID, context);
		}

		#endregion

		#region ɾ�������ṹ

		/// <summary>
		/// ɾ�������ṹ
		/// </summary>
		/// <param name="warehouseAreaStructID">�����ṹID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int warehouseAreaStructID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().Del(warehouseAreaStructID, context);
		}

		#endregion

		#region ���ݿ����ṹID��ȡ�����ṹ��Ϣ

		/// <summary>
		/// ���ݿ����ṹID��ȡ�����ṹ��Ϣ
		/// </summary>
		/// <param name="warehouseAreaStructID">�����ṹID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static WarehouseAreaStruct GetSingleWarehouseAreaStruct(int warehouseAreaStructID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetSingleWarehouseAreaStruct(warehouseAreaStructID, context);
		}

		#endregion

		#region ���ݸ��������ṹID��ȡֱ���Ӽ������ṹID�б�

		/// <summary>
		/// ���ݸ��������ṹID��ȡֱ���Ӽ������ṹID�б�
		/// </summary>
		/// <param name="parentID">���������ṹID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<int> GetChildWarehouseAreaStructID(int parentID, IDbContext context = null) {
			return WarehouseAreaStructRepository.GetInstance().GetChildWarehouseAreaStructID(parentID, context);
		}

		#endregion

		#region ���ݸ����ṹID�ݹ��ȡ�����ӽṹ ������ϵ���

		/// <summary>
		/// ���ݸ����ṹID�ݹ��ȡ�����ӽṹ ������ϵ���
		/// </summary>
		/// <param name="parentID">�����ṹID</param>
		/// <returns></returns>
		public static void GetChildWarehouseAreaStruct(int parentID, ref List<WarehouseAreaStruct> warehouseAreaStructList, IDbContext context = null) {
			WarehouseAreaStructRepository.GetInstance().GetChildWarehouseAreaStruct(parentID, ref warehouseAreaStructList, context);
		}

		#endregion
	}
}





