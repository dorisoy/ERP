using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 库存情况实体类
	/// </summary>
	public class BatchList{
		public BatchList() { }

		/// <summary>
		/// 批次号
		/// </summary>
		public string BatchCode { get; set; }

		/// <summary>
		/// 批次来源
		/// </summary>
		public int BatchSource { get; set; }

		/// <summary>
		/// 商品数量
		/// </summary>
		public int ProductsCount { get; set; }

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
		/// 当前库存
		/// </summary>
		public int Inventory { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
	}
}
