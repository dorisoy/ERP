using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace PaiXie.Service 
{
 	public class WarehouseAreaMapService  : BaseService<WarehouseAreaMap> {
    
		public static int Update(WarehouseAreaMap entity, IDbContext context = null) {
			return WarehouseAreaMapRepository.GetInstance().Update(entity, context);
		}

		public static int Add(WarehouseAreaMap entity, IDbContext context = null) {
			return WarehouseAreaMapRepository.GetInstance().Add(entity, context);
		}

		public static int IswareaMap(string areaid, string wcode, IDbContext context = null) {
			return WarehouseAreaMapRepository.GetInstance().IswareaMap(areaid, wcode, context);
		}

		public static int DeleteWarehouseAreaMap(string wid, IDbContext context = null) {
			return WarehouseAreaMapRepository.GetInstance().DeleteWarehouseAreaMap(wid, context);
		}

		#region 根据仓库id 获取仓库设置区域
		/// <summary>
		/// 根据仓库id 获取仓库设置区域
		/// </summary>
		/// <param name="wid"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static  List<WarehouseAreaMap> GetWarehouseAreaMapList(int wid, IDbContext context = null) {
			return WarehouseAreaMapRepository.GetInstance().GetWarehouseAreaMapList(wid, context);
	
		}
		#endregion


	}
}





