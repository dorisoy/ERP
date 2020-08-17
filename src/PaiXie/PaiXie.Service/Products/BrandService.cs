using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service {
	public class BrandService : BaseService<Brand> {

		#region Update

		public static int Update(Brand entity, IDbContext context = null) {
			return BrandRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(Brand entity, IDbContext context = null) {
			return BrandRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region ����Ʒ�����ƻ�ȡƷ��ID

		/// <summary>
		/// ����Ʒ�����ƻ�ȡƷ��ID
		/// </summary>
		/// <param name="brandName">Ʒ������</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetBrandID(string brandName, IDbContext context = null) {
			return BrandRepository.GetInstance().GetBrandID(brandName, context);
		}

		#endregion

		#region ����Ʒ�����ƻ�ȡ�ų�ָ��Ʒ��ID֮���Ʒ��ID(�޸�Ʒ��ʱʹ��)

		/// <summary>
		/// ����Ʒ�����ƻ�ȡ�ų�ָ��Ʒ��ID֮���Ʒ��ID(�޸�Ʒ��ʱʹ��)
		/// </summary>
		/// <param name="brandName">Ʒ������</param>
		/// <param name="exceptBrandID">��Ҫ�ų�Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetBrandID(string brandName, int exceptBrandID, IDbContext context = null) {
			return BrandRepository.GetInstance().GetBrandID(brandName, exceptBrandID, context);
		}

		#endregion

		#region ɾ��Ʒ��

		/// <summary>
		/// ɾ��Ʒ��
		/// </summary>
		/// <param name="brandID">Ҫɾ����Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int brandID, IDbContext context = null) {
			return BrandRepository.GetInstance().Del(brandID, context);
		}

		#endregion

		#region ����Ʒ��ID��ȡƷ����Ϣ

		/// <summary>
		/// ����Ʒ��ID��ȡƷ����Ϣ
		/// </summary>
		/// <param name="brandID">Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static Brand GetSingleBrand(int brandID, IDbContext context = null) {
			return BrandRepository.GetInstance().GetSingleBrand(brandID, context);
		}

		#endregion

		#region ���ݲֿ�����ȡ��ȨƷ����Ϣ

		/// <summary>
		/// ���ݲֿ�����ȡ��ȨƷ����Ϣ
		/// </summary>
		/// <param name="brandID">Ʒ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<Brand> GetManyBrandByWarehouseCode(string warehouseCode, IDbContext context = null) {
			return BrandRepository.GetInstance().GetManyBrandByWarehouseCode(warehouseCode, context);
		}

		#endregion

		#region ��ȡƷ����Ϣ

		/// <summary>
		/// ��ȡƷ����Ϣ
		/// </summary>
		/// <returns></returns>
		public static List<Brand> GetBrand() {
			return BrandRepository.GetInstance().GetBrand();

		}

		#endregion
	}
}





