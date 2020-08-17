using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
	public class CategoryService : BaseService<Category> {

		#region Update

		public static int Update(Category entity, IDbContext context = null) {
			return CategoryRepository.GetInstance().Update(entity, context);
		}

		#endregion

		#region Add

		public static int Add(Category entity, IDbContext context = null) {
			return CategoryRepository.GetInstance().Add(entity, context);
		}

		#endregion

		#region ���ݷ������ƺ͸���ID��ȡ����ID

		/// <summary>
		/// ���ݷ������ƺ͸���ID��ȡ����ID
		/// </summary>
		/// <param name="categoryName">��������</param>
		/// <param name="parentID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetCategoryID(string categoryName, int parentID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetCategoryID(categoryName, parentID, context);
		}

		#endregion

		#region ���ݷ������ƺ͸���ID��ȡ�ų�ָ������ID֮��ķ���ID(�޸ķ���ʱʹ��)

		/// <summary>
		/// ���ݷ������ƺ͸���ID��ȡ�ų�ָ������ID֮��ķ���ID(�޸ķ���ʱʹ��)
		/// </summary>
		/// <param name="categoryName">��������</param>
		/// <param name="parentID">����ID</param>
		/// <param name="exceptCategoryID">��Ҫ�ų�����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int GetCategoryID(string categoryName, int parentID, int exceptCategoryID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetCategoryID(categoryName, parentID, exceptCategoryID, context);
		}

		#endregion

		#region ɾ������

		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="categoryID">Ҫɾ���ķ���ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static int Del(int categoryID, IDbContext context = null) {
			return CategoryRepository.GetInstance().Del(categoryID, context);
		}

		#endregion

		#region ���ݷ���ID��ȡ������Ϣ

		/// <summary>
		/// ���ݷ���ID��ȡ������Ϣ
		/// </summary>
		/// <param name="categoryID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static Category GetSingleCategory(int categoryID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetSingleCategory(categoryID, context);
		}

		#endregion

		#region ���ݸ�������ID��ȡ�Ӽ�����ID�б�

		/// <summary>
		/// ���ݸ�������ID��ȡ�Ӽ�����ID�б�
		/// </summary>
		/// <param name="parentID">��������ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static List<int> GetChildCategoryID(int parentID, IDbContext context = null) {
			return CategoryRepository.GetInstance().GetChildCategoryID(parentID, context);
		}

		#endregion
	}
}





