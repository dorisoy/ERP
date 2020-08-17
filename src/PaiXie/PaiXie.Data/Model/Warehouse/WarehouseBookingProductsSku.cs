using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 无注释
	/// </summary>
	[Serializable]
	public partial class WarehouseBookingProductsSku {
		public WarehouseBookingProductsSku() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  int _ProductsID;
	    /// <summary>
		/// Products表主键ID
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  int _ProductsSkuID;
	    /// <summary>
		/// ProductsSku表主键ID
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}


		private int _BookingModel;
	    /// <summary>
	    /// 库存扣减模式 0：扣减 1：不扣减
	    /// </summary>
		public int BookingModel {
			set { _BookingModel = value; }
			get { return _BookingModel; }
		}

        
        private  int _BookingNum;
	    /// <summary>
	    /// 预售数量
	    /// </summary>
		public  int BookingNum {
			set { _BookingNum = value; }
			get { return _BookingNum; }
		}


		private int _ZyNum;
		/// <summary>
		/// 预售占用
		/// </summary>
		public int ZyNum {
			set { _ZyNum = value; }
			get { return _ZyNum; }
		}


		private int _CdNum;
		/// <summary>
		/// 冲抵数量
		/// </summary>
		public int CdNum {
			set { _CdNum = value; }
			get { return _CdNum; }
		}


        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    /// 修改人
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 修改时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

		
	}
}

