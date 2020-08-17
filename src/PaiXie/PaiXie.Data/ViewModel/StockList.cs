using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 库存情况实体类
	/// </summary>
	public class StockList : Products{
		public StockList() { }

		/// <summary>
		/// 商品分类名称
		/// </summary>
		public string BrandName { get; set; }
       
		/// <summary>
		/// 商品分类名称
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// 期初库存
		/// </summary>
		public int InitialInventory { get; set; }

		/// <summary>
		/// 期初成本
		/// </summary>
		public decimal InitialCost { get; set; }

		/// <summary>
		/// 税率百分比转换
		/// </summary>
		public string TaxRateConvert { get; set; }

		/// <summary>
		/// 出库数量
		/// </summary>
		public int OutboundNum { get; set; }

		/// <summary>
		/// 入库数量
		/// </summary>
		public int StorageNum { get; set; }

		/// <summary>
		/// 销售数量
		/// </summary>
		public int SalesVolumes { get; set; }

		/// <summary>
		/// 销售金额
		/// </summary>
		public decimal SalesAmount { get; set; }

		/// <summary>
		/// 调整数量
		/// </summary>
		public int AdjustQuantity { get; set; }

		/// <summary>
		/// 转出数量
		/// </summary>
		public int RollOutQuantity { get; set; }

		/// <summary>
		/// 转入数量
		/// </summary>
		public int QuantityOfTransfer { get; set; }

		/// <summary>
		/// 期末数量
		/// </summary>
		public int FinalQuantity { get; set; }

		/// <summary>
		/// 期末成本
		/// </summary>
		public decimal FinalCost { get; set; }
	}
}
