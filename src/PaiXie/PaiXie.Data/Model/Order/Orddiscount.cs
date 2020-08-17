using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 订单优惠表
	/// </summary>
	[Serializable]
	public partial class Orddiscount {
		public Orddiscount() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 无注释
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _ErpOrderCode;
	    /// <summary>
	    /// 系统订单号
	    /// </summary>
		public  string ErpOrderCode {
			set { _ErpOrderCode = value; }
			get { return _ErpOrderCode; }
		}

        
        private  int _OrdbaseID;
	    /// <summary>
	    /// 订单表主键ID
	    /// </summary>
		public  int OrdbaseID {
			set { _OrdbaseID = value; }
			get { return _OrdbaseID; }
		}

        
        private  int _Type;
	    /// <summary>
		/// 优惠类型 0：直减金额 1：订单包邮
	    /// </summary>
		public  int Type {
			set { _Type = value; }
			get { return _Type; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 优惠备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  decimal _Amount;
	    /// <summary>
	    /// 优惠金额
	    /// </summary>
		public  decimal Amount {
			set { _Amount = value; }
			get { return _Amount; }
		}

        
        private  string _LibProductsSkuID;
	    /// <summary>
	    /// 关联商品SKUID
	    /// </summary>
		public  string LibProductsSkuID {
			set { _LibProductsSkuID = value; }
			get { return _LibProductsSkuID; }
		}

        
        private  string _LibProductsSkuCode;
	    /// <summary>
	    /// 关联商品SKU码
	    /// </summary>
		public  string LibProductsSkuCode {
			set { _LibProductsSkuCode = value; }
			get { return _LibProductsSkuCode; }
		}

        
        private  DateTime _CreateDate;
	    /// <summary>
	    /// 创建时间
	    /// </summary>
		public  DateTime CreateDate {
			set { _CreateDate = value; }
			get { return _CreateDate; }
		}

        
        private  string _CreatePerson;
	    /// <summary>
	    /// 创建人
	    /// </summary>
		public  string CreatePerson {
			set { _CreatePerson = value; }
			get { return _CreatePerson; }
		}

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 修改时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

        
        private  string _UpdatePerson;
	    /// <summary>
	    /// 修改人
	    /// </summary>
		public  string UpdatePerson {
			set { _UpdatePerson = value; }
			get { return _UpdatePerson; }
		}

	}
}

