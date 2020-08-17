using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data {
	/// <summary>
	/// 仓库商品表
	/// </summary>
	[Serializable]
	public partial class WarehouseProducts {
		public WarehouseProducts() { }


		private int _ID;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int ID {
			set { _ID = value; }
			get { return _ID; }
		}


		private string _WarehouseCode;
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}


		private int _ProductsID;
		/// <summary>
		/// 管理端商品表标识
		/// </summary>
		public int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}


		private int _ProductsStatus;
		/// <summary>
		/// 商品销售状态 销售中=1 仓库中=2 
		/// </summary>
		public int ProductsStatus {
			set { _ProductsStatus = value; }
			get { return _ProductsStatus; }
		}


		private int _IsBooking;
		/// <summary>
		/// 是否添加预售 0：否 1：是
		/// </summary>
		public int IsBooking {
			set { _IsBooking = value; }
			get { return _IsBooking; }
		}


		private int _BookingModel;
		/// <summary>
		/// 预售商品的库存扣减模式：0：扣减 1：不扣减
		/// </summary>
		public int BookingModel {
			set { _BookingModel = value; }
			get { return _BookingModel; }
		}


		private string _CreatePerson;
		/// <summary>
		/// 创建人
		/// </summary>
		public string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}


		private DateTime _CreateDate;
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}


		private string _UpdatePerson;
		/// <summary>
		/// 修改人
		/// </summary>
		public string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}


		private DateTime _UpdateDate;
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}


	}
}

