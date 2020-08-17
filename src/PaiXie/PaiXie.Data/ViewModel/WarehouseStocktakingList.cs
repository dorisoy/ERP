using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class WarehouseStocktakingList:WarehouseStocktaking{
		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WarehouseName { get; set; }
		/// <summary>
		/// 确认人名称
		/// </summary>
		public string UpdatePersonName { get; set; }
		/// <summary>
		/// 状态名称
		/// </summary>
		public string StatusName { get; set; }

	
	}
}
