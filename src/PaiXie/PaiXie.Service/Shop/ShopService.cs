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
		/// 根据店铺ID获取店铺信息
		/// </summary>
		/// <param name="shopID">店铺ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static Shop GetSingleShop(int shopID, IDbContext context = null) {
			return ShopRepository.GetInstance().GetSingleShop(shopID, context);
		}
		/// <summary>
		/// 禁用店铺
		/// </summary>
		/// <param name="id"></param>
		/// <param name="context"></param>
		/// <returns></returns>
			public static int   DeleteShop(string  id) {
			return ShopRepository.GetInstance().DeleteShop(id);
		}
		/// <summary>
			/// 检查代码 排除自己
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="ID"></param>
		/// <param name="context"></param>
		/// <returns></returns>
			public static int   CheckCode(string Code, int ID, IDbContext context = null) {
			return ShopRepository.GetInstance().CheckCode( Code,  ID, context);
		}
		/// <summary>
			/// 检查代码 
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="context"></param>
		/// <returns></returns>
			public static int CheckCode2(string Code, IDbContext context = null) {
				return ShopRepository.GetInstance().CheckCode2(Code,  context);
			}




			/// <summary>
			/// 检查名称 排除自己
			/// </summary>
			/// <param name="Code"></param>
			/// <param name="ID"></param>
			/// <param name="context"></param>
			/// <returns></returns>
			public static int CheckName(string Name, int ID, IDbContext context = null) {
				return ShopRepository.GetInstance().CheckName(Name, ID, context);
			}
			/// <summary>
			/// 检查名称
			/// </summary>
			/// <param name="Code"></param>
			/// <param name="context"></param>
			/// <returns></returns>
			public static int CheckName2(string Name, IDbContext context = null) {
				return ShopRepository.GetInstance().CheckName2(Name, context);
			}

		/// <summary>
			/// 线上平台店铺列表
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static List<PaiXie.Data.Shop>  shoplist( IDbContext context = null) {
				return ShopRepository.GetInstance().shoplist( context);
			}


		


       
	}
}





