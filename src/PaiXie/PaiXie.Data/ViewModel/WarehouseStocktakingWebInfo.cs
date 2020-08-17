using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 盘点记录实体类 前端交互使用
	/// </summary>
	public class WarehouseStocktakingWebInfo {
		/// <summary>
		/// 盘点备注
		/// </summary>
		public string Remark { get; set; }
		
		/// <summary>
		/// 盘点库区ID
		/// </summary>
		public int[] LocationID { get; set; }

		/// <summary>
		/// 盘点库区名称
		/// </summary>
		public string[] LocationName { get; set; }
	}
}
