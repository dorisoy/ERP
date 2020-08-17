using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {

	/// <summary>
	/// 仓库地址管理信息实体类 前端交互使用
	/// </summary>
	public class WarehouseAddressWebInfo {

		/// <summary>
		/// 发货人
		/// </summary>
		public string SendPerson { get; set; }

		/// <summary>
		/// 发货人手机/电话
		/// </summary>
		public string SendTel { get; set; }

		/// <summary>
		/// 发货人省ID
		/// </summary>
		public int SendProvinceID { get; set; }

		/// <summary>
		/// 发货人市ID
		/// </summary>
		public int SendCityID { get; set; }

		/// <summary>
		/// 发货人区ID
		/// </summary>
		public int SendDistrictID { get; set; }

		/// <summary>
		/// 发货人省
		/// </summary>
		public string SendProvince { get; set; }

		/// <summary>
		/// 发货人市
		/// </summary>
		public string SendCity { get; set; }

		/// <summary>
		/// 发货人区
		/// </summary>
		public string SendDistrict { get; set; }

		/// <summary>
		/// 发货人街道地址
		/// </summary>
		public string SendAddressDetail { get; set; }

		/// <summary>
		/// 发货人邮编
		/// </summary>
		public string SendPostCode { get; set; }

		/// <summary>
		/// 售后地址与寄件地址是一致 0否 1是
		/// </summary>
		public int IsSame { get; set; }

		/// <summary>
		/// 收货人
		/// </summary>
		public string ReceivePerson { get; set; }

		/// <summary>
		/// 收货人手机/电话
		/// </summary>
		public string ReceiveTel { get; set; }

		/// <summary>
		/// 收货人省ID
		/// </summary>
		public int ReceiveProvinceID { get; set; }

		/// <summary>
		/// 收货人市ID
		/// </summary>
		public int ReceiveCityID { get; set; }

		/// <summary>
		/// 收货人区ID
		/// </summary>
		public int ReceiveDistrictID { get; set; }

		/// <summary>
		/// 收货人省
		/// </summary>
		public string ReceiveProvince { get; set; }

		/// <summary>
		/// 收货人市
		/// </summary>
		public string ReceiveCity { get; set; }

		/// <summary>
		/// 收货人区
		/// </summary>
		public string ReceiveDistrict { get; set; }

		/// <summary>
		/// 收货人街道地址
		/// </summary>
		public string ReceiveAddressDetail { get; set; }

		/// <summary>
		/// 收货人邮编
		/// </summary>
		public string ReceivePostCode { get; set; }
	}
}
