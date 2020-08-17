using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class WarehouseConversionRuleService : BaseService<WarehouseConversionRule> {

		public static int Update(WarehouseConversionRule entity, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseConversionRule entity, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().Add(entity, context);
		}

		/// <summary>
		/// ��ȡ��Ʒת������ʵ��
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="ruleID">����ID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static WarehouseConversionRule GetSingleWarehouseConversionRule(string warehouseCode, int ruleID, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().GetSingleWarehouseConversionRule(warehouseCode, ruleID, context);
		}

		/// <summary>
		/// ɾ��ת������
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsIDList">����ID�б�</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static int Delete(string warehouseCode, List<int> ruleIDList, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().Delete(warehouseCode, ruleIDList, context);
		}

		/// <summary>
		/// ��ȡ��Ʒת������ʵ���б�
		/// </summary>
		/// <param name="warehouseCode">�ֿ����</param>
		/// <param name="productsSkuID">��ƷSkuID</param>
		/// <param name="context">���ݿ�����</param>
		/// <returns></returns>
		public static List<WarehouseConversionRule> GetManyWarehouseConversionRule(string warehouseCode, int productsSkuID, IDbContext context = null) {
			return WarehouseConversionRuleRepository.GetInstance().GetManyWarehouseConversionRule(warehouseCode, productsSkuID, context);
		}
	}
}





