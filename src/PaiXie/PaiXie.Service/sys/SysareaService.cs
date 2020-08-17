using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
	public class SysareaService : BaseService<Sysarea> {

		public static int Update(Sysarea entity) {
			return SysareaRepository.GetInstance().Update(entity);
		}



		public static int Add(Sysarea entity) {
			return SysareaRepository.GetInstance().Add(entity);
		}
		/// <summary>
		/// ��ȡ����
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			return SysareaRepository.GetInstance().GetDataTable();
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		/// <returns></returns>
		public static DataTable GetareaDataTable() {
			return SysareaRepository.GetInstance().GetareaDataTable();
		}
		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="id">����id</param>
		/// <param name="level">�ȼ�  0  ʡ��  1  �м�  2  ����</param>
		/// <returns></returns>
		public static int DelArea(int id, int level) {
			return SysareaRepository.GetInstance().DelArea(id, level);
		}
		/// <summary>
		/// ��ȡ����
		/// </summary>
		/// <param name="id">����id</param>
		/// <returns></returns>
		public static Sysarea GetArea(int id) {
			return SysareaRepository.GetInstance().GetArea(id);
		}

		/// <summary>
		/// �ָ���ʼ����������
		/// </summary>
		/// <returns></returns>
		public static int initArea() {
			return SysareaRepository.GetInstance().initArea();
		}

		/// <summary>
		/// �༭����
		/// </summary>
		/// <param name="name">����</param>
		/// <param name="id">����id</param>
		/// <returns></returns>
		public static int EditArea(string name, int id) {
			return SysareaRepository.GetInstance().EditArea(name, id);
		}

		#region ��ȡ�����б�

		/// <summary>
		/// ��ȡ�����б�
		/// </summary>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<Sysarea> GetLargeAreaList(IDbContext context = null) {
			return SysareaRepository.GetInstance().GetLargeAreaList(context);
		}

		#endregion

		#region ���ݴ������ƻ�ȡʡ���б�

		/// <summary>
		/// ���ݴ������ƻ�ȡʡ���б�
		/// </summary>
		/// <param name="largeAreaName">��������</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<Sysarea> GetProvinceList(string largeAreaName, IDbContext context = null) {
			return SysareaRepository.GetInstance().GetProvinceList(largeAreaName, context);
		}

		#endregion

		#region ����ʡ��ID��ȡ�����б�

		/// <summary>
		/// ����ʡ��ID��ȡ�����б�
		/// </summary>
		/// <param name="provinceID">ʡ��ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<Sysarea> GetCityList(int provinceID, IDbContext context = null) {
			return SysareaRepository.GetInstance().GetCityList(provinceID, context);
		}

		#endregion

		#region ��ȡ����ʡ�����ֵ�

		/// <summary>
		/// ��ȡ����ʡ�����ֵ�
		/// </summary>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static DataTable GetManySysarea(IDbContext context = null) {
			return SysareaRepository.GetInstance().GetManySysarea(context);
		}

		#endregion
	}
}