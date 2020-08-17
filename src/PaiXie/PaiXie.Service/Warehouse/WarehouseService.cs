using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class WarehouseService : BaseService<Warehouse> {

		public static int Update(Warehouse entity, IDbContext context = null) {
			return WarehouseRepository.GetInstance().Update(entity, context);
		}

		public static int Add(Warehouse entity, IDbContext context = null) {
			return WarehouseRepository.GetInstance().Add(entity, context);
		}
		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Code"></param>
		/// <returns></returns>
		public static int Getwarehousecount(int ID, string Code) {
			return WarehouseRepository.GetInstance().Getwarehousecount(ID, Code);
		}
		/// <summary>
		/// ������Ψһ��
		/// </summary>
		/// <param name="Code"></param>
		/// <returns></returns>
		public static int Getwarehousecount(string Code) {
			return WarehouseRepository.GetInstance().Getwarehousecount(Code);

		}

		/// <summary>
		/// ɾ�� 
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int Deletewarehouse(string ID, IDbContext context = null) {
			return WarehouseRepository.GetInstance().Deletewarehouse(ID);

		}
		/// <summary>
		/// ��ȡʵ��  �������䡡
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static Warehouse Getwarehouse(string ID, IDbContext context = null) {
			return WarehouseRepository.GetInstance().Getwarehouse(ID);

		}
		/// <summary>
		/// �ֿ� ���� ���� 
		/// </summary>
		/// <param name="wid"></param>
		/// <param name="IsEnable"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static int IsEnablewarehouse(int wid, int IsEnable, IDbContext context = null) {
			return WarehouseRepository.GetInstance().IsEnablewarehouse(wid, IsEnable, context);

		}

		/// <summary>
		/// ��ȡ�ֿ�  by  �ֿ����
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static Warehouse GetwarehousebyCode(string Code, IDbContext context = null) {
			return WarehouseRepository.GetInstance().GetwarehousebyCode(Code, context);

		}

		/// <summary>
		/// ��ȡ���òֿ��б�
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<Warehouse> GetAvailableWarehouse(IDbContext context = null) {
			return WarehouseRepository.GetInstance().GetAvailableWarehouse(context);
		}
	}
}
