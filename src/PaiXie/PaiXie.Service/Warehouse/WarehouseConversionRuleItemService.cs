using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseConversionRuleItemService  : BaseService<WarehouseConversionRuleItem> {
    
		public static int Update(WarehouseConversionRuleItem entity, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseConversionRuleItem entity, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// ��ȡ��Ʒת��������Ʒʵ��
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="ruleID">����ID</param>
		/// <param name="ruleID">��ƷSKU��</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehouseConversionRuleItem GetSingleWarehouseConversionRuleItem(string warehouseCode, int ruleID, string productsSkuCode, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().GetSingleWarehouseConversionRuleItem(warehouseCode, ruleID, productsSkuCode, context);
		}

		/// <summary>
		/// ��ȡת��������Ʒ�б�
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="ruleID">����ID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static List<WarehouseConversionRuleItem> GetManyWarehouseConversionRuleItem(string warehouseCode, int ruleID, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().GetManyWarehouseConversionRuleItem(warehouseCode, ruleID, context);
		}

		/// <summary>
		/// ���ݹ�����Ʒ��IDɾ����¼
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param> 
		/// <param name="ruleItemID">������Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, int ruleItemID, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Delete(warehouseCode, ruleItemID, context);
		}

			/// <summary>
		/// ���ݹ���IDɾ����¼
		/// </summary>
		/// <param name="ruleItemID">������Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> ruleIDList, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().Delete(warehouseCode, ruleIDList, context);
		}

		/// <summary>
		/// ��ȡת��������Ʒ�б�(��Ʒת��ʱʹ��)
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="ruleID">����ID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static List<WarehouseConversionRuleItemInfo> GetManyWarehouseConversionRuleItemInfo(string warehouseCode, int ruleID, IDbContext context = null) {
			return WarehouseConversionRuleItemRepository.GetInstance().GetManyWarehouseConversionRuleItemInfo(warehouseCode, ruleID, context);
		}
	}
}





