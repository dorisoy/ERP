using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 同步发货信息表
	/// </summary>
	[Serializable]
	public partial class SendShop {
		public SendShop() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
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

        
        private  string _OutOrderCode;
	    /// <summary>
	    /// 外部单号
	    /// </summary>
		public  string OutOrderCode {
			set { _OutOrderCode = value; }
			get { return _OutOrderCode; }
		}

        
        private  int _ShopID;
	    /// <summary>
	    /// 店铺ID
	    /// </summary>
		public  int ShopID {
			set { _ShopID = value; }
			get { return _ShopID; }
		}

        
        private  int _OrderSource;
	    /// <summary>
	    /// 订单来源 枚举
	    /// </summary>
		public  int OrderSource {
			set { _OrderSource = value; }
			get { return _OrderSource; }
		}

        
        private  int _IsCod;
	    /// <summary>
	    /// 是否货到付款 0否 1是
	    /// </summary>
		public  int IsCod {
			set { _IsCod = value; }
			get { return _IsCod; }
		}

        
        private  int _ExpressID;
	    /// <summary>
	    /// 下单选择快递公司ID
	    /// </summary>
		public  int ExpressID {
			set { _ExpressID = value; }
			get { return _ExpressID; }
		}

        
        private  int _DeliveryExpressID;
	    /// <summary>
	    /// 发货快递公司ID
	    /// </summary>
		public  int DeliveryExpressID {
			set { _DeliveryExpressID = value; }
			get { return _DeliveryExpressID; }
		}

        
        private  string _WaybillNo;
	    /// <summary>
	    /// 运单号
	    /// </summary>
		public  string WaybillNo {
			set { _WaybillNo = value; }
			get { return _WaybillNo; }
		}

        
        private  DateTime _DeliveryDate;
	    /// <summary>
	    /// 发货时间
	    /// </summary>
		public  DateTime DeliveryDate {
			set { _DeliveryDate = value; }
			get { return _DeliveryDate; }
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

        
        private  DateTime _UpdateDate;
	    /// <summary>
	    /// 修改时间
	    /// </summary>
		public  DateTime UpdateDate {
			set { _UpdateDate = value; }
			get { return _UpdateDate; }
		}

        
        private  int _Status;
	    /// <summary>
	    /// 同步店铺状态 0未同步 1同步成功 2同步失败
	    /// </summary>
		public  int Status {
			set { _Status = value; }
			get { return _Status; }
		}

        
        private  string _Remark;
	    /// <summary>
	    /// 备注
	    /// </summary>
		public  string Remark {
			set { _Remark = value; }
			get { return _Remark; }
		}

        
        private  DateTime _TaskTime;
	    /// <summary>
	    /// 任务时间 相同表示是同一批同步店铺发货的
	    /// </summary>
		public  DateTime TaskTime {
			set { _TaskTime = value; }
			get { return _TaskTime; }
		}

	}
}

