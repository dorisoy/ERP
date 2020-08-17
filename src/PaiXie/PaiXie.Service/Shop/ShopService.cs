using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
 	public class ShopService  : BaseService<Shop> {
    
		public static int Update(Shop entity) {
			return ShopRepository.GetInstance().Update(entity);
		}

		public static int Add(Shop entity) {
			return ShopRepository.GetInstance().Add(entity);
		}

		/// <summary>
		/// ���ݵ���ID��ȡ������Ϣ
		/// </summary>
		/// <param name="shopID">����ID</param>
		/// <param name="context">���ݿ����Ӷ���</param>
		/// <returns></returns>
		public static Shop GetSingleShop(int shopID, IDbContext context = null) {
			return ShopRepository.GetInstance().GetSingleShop(shopID, context);
		}
		/// <summary>
		/// ���õ���
		/// </summary>
		/// <param name="id"></param>
		/// <param name="context"></param>
		/// <returns></returns>
			public static int   DeleteShop(string  id) {
			return ShopRepository.GetInstance().DeleteShop(id);
		}
		/// <summary>
			/// ������ �ų��Լ�
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
			public static int   CheckCode(string Code, int ID, IDbContext context = null) {
			return ShopRepository.GetInstance().CheckCode( Code,  ID, context);
		}
		/// <summary>
			/// ������ 
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="context"></param>
		/// <returns></returns>
			public static int CheckCode2(string Code, IDbContext context = null) {
				return ShopRepository.GetInstance().CheckCode2(Code,  context);
			}




			/// <summary>
			/// ������� �ų��Լ�
			/// </summary>
			/// <param name="Code"></param>
			/// <param name="ID"></param>
			/// <param name="context"></param>
			/// <returns></returns>
			public static int CheckName(string Name, int ID, IDbContext context = null) {
				return ShopRepository.GetInstance().CheckName(Name, ID, context);
			}
			/// <summary>
			/// �������
			/// </summary>
			/// <param name="Code"></param>
			/// <param name="context"></param>
			/// <returns></returns>
			public static int CheckName2(string Name, IDbContext context = null) {
				return ShopRepository.GetInstance().CheckName2(Name, context);
			}

		/// <summary>
			/// ����ƽ̨�����б�
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<PaiXie.Data.Shop>  shoplist( IDbContext context = null) {
				return ShopRepository.GetInstance().shoplist( context);
			}


		


       
	}
}





