using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class DownOrderParam {
		/// <summary>
		/// 任务ID
		/// </summary>
		public string TaskID { get; set; }

		/// <summary>
		/// 店铺ID
		/// </summary>
		public int ShopID { get; set; }
		
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndDate { get; set; }
		
		/// <summary>
		/// 页数
		/// </summary>
		public int PageNo { get; set; }

		/// <summary>
		/// 条数
		/// </summary>
		public int PageSize { get; set; }

		///<summary>
		/// 是否自动 0:否 1：是
		/// </summary>
		public int IsAuto { get; set; }

		/// <summary>
		/// 时间类型 1：下单时间 2：付款时间
		/// </summary>
		public int DateType { get; set; }

		/// <summary>
		/// 下载地址
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 订单顺序 0:升序 1：降序
		/// </summary>
		public int OrderDesc { get; set; }

		/// <summary>
		/// 操作用户
		/// </summary>
		public string UserCode { get; set; }
	}
}
