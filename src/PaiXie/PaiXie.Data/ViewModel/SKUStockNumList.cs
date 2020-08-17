using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 入库单 添加商品  sku 库存列表
	/// </summary>
	public class SKUStockNumList {

	   /// <summary>
	   ///库位编码
	   /// </summary>
		public string LibraryCode { get; set; }
		/// <summary>
		/// 当前库存
		/// </summary>
		public int Inventory { get; set; }
		/// <summary>
		/// 入库数量
		/// </summary>
		public int StorageNum { get; set; }

		public int ID { get; set; }

		/// <summary>
		/// 是否可编辑
		/// </summary>
		public int IsEdit { get; set; }

	
	}

	/// <summary>
	/// 入库单  添加商品   提交实体
	/// </summary>
	public class PutSKUStock {

		/// <summary>
		///库位编码
		/// </summary>
		public string[] LibraryCode { get; set; }
	
		/// <summary>
		/// 入库数量
		/// </summary>
		public int[] StorageNum { get; set; }

		/// <summary>
		/// 采购价
		/// </summary>
		public string PurchasePrice { get; set; }
		/// <summary>
		/// 生产日期
		/// </summary>
		public string ProductionDate { get; set; }
		/// <summary>
		/// 库区id
		/// </summary>
		public int ReservoirArea { get; set; }
		/// <summary>
		/// sku code
		/// </summary>
		public string  ffcode { get; set; }

		/// <summary>
		/// 入库单号
		/// </summary>
		public string BillNo { get; set; }
		
		
	}
}
