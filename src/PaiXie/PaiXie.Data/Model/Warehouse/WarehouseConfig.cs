using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 仓库配置信息表
	/// </summary>
	[Serializable]
	public partial class WarehouseConfig {
		public WarehouseConfig() { }
        
        
        private  int _ID;
	    /// <summary>
	    /// 主键ID
	    /// </summary>
		public  int ID {
			set { _ID = value; }
			get { return _ID; }
		}

        
        private  string _WarehouseCode;
	    /// <summary>
	    /// 仓库编码
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

        
        private  string _SendPerson;
	    /// <summary>
	    /// 寄件人
	    /// </summary>
		public  string SendPerson {
			set { _SendPerson = value; }
			get { return _SendPerson; }
		}

        
        private  string _SendTel;
	    /// <summary>
	    /// 寄件人电话或手机
	    /// </summary>
		public  string SendTel {
			set { _SendTel = value; }
			get { return _SendTel; }
		}

        
        private  int _SendProvinceID;
	    /// <summary>
	    /// 寄件人省ID
	    /// </summary>
		public  int SendProvinceID {
			set { _SendProvinceID = value; }
			get { return _SendProvinceID; }
		}

        
        private  string _SendProvince;
	    /// <summary>
	    /// 寄件人省
	    /// </summary>
		public  string SendProvince {
			set { _SendProvince = value; }
			get { return _SendProvince; }
		}

        
        private  int _SendCityID;
	    /// <summary>
	    /// 寄件人市ID
	    /// </summary>
		public  int SendCityID {
			set { _SendCityID = value; }
			get { return _SendCityID; }
		}

        
        private  string _SendCity;
	    /// <summary>
	    /// 寄件人市
	    /// </summary>
		public  string SendCity {
			set { _SendCity = value; }
			get { return _SendCity; }
		}

        
        private  int _SendDistrictID;
	    /// <summary>
	    /// 寄件人区ID
	    /// </summary>
		public  int SendDistrictID {
			set { _SendDistrictID = value; }
			get { return _SendDistrictID; }
		}

        
        private  string _SendDistrict;
	    /// <summary>
	    /// 寄件人区
	    /// </summary>
		public  string SendDistrict {
			set { _SendDistrict = value; }
			get { return _SendDistrict; }
		}

        
        private  string _SendAddressDetail;
	    /// <summary>
	    /// 寄件人详细地址（不包含省市区）
	    /// </summary>
		public  string SendAddressDetail {
			set { _SendAddressDetail = value; }
			get { return _SendAddressDetail; }
		}

        
        private  string _SendAddress;
	    /// <summary>
	    /// 寄件人地址
	    /// </summary>
		public  string SendAddress {
			set { _SendAddress = value; }
			get { return _SendAddress; }
		}

        
        private  string _SendPostCode;
	    /// <summary>
	    /// 寄件人邮政编码
	    /// </summary>
		public  string SendPostCode {
			set { _SendPostCode = value; }
			get { return _SendPostCode; }
		}

        
        private  int _IsSame;
	    /// <summary>
	    /// 售后地址与寄件地址是一致 0否 1是
	    /// </summary>
		public  int IsSame {
			set { _IsSame = value; }
			get { return _IsSame; }
		}

        
        private  string _ReceivePerson;
	    /// <summary>
	    /// 收货人
	    /// </summary>
		public  string ReceivePerson {
			set { _ReceivePerson = value; }
			get { return _ReceivePerson; }
		}

        
        private  string _ReceiveTel;
	    /// <summary>
	    /// 收货人电话或手机
	    /// </summary>
		public  string ReceiveTel {
			set { _ReceiveTel = value; }
			get { return _ReceiveTel; }
		}

        
        private  int _ReceiveProvinceID;
	    /// <summary>
	    /// 收货人省ID
	    /// </summary>
		public  int ReceiveProvinceID {
			set { _ReceiveProvinceID = value; }
			get { return _ReceiveProvinceID; }
		}

        
        private  string _ReceiveProvince;
	    /// <summary>
	    /// 收货人省
	    /// </summary>
		public  string ReceiveProvince {
			set { _ReceiveProvince = value; }
			get { return _ReceiveProvince; }
		}

        
        private  int _ReceiveCityID;
	    /// <summary>
	    /// 收货人市ID
	    /// </summary>
		public  int ReceiveCityID {
			set { _ReceiveCityID = value; }
			get { return _ReceiveCityID; }
		}

        
        private  string _ReceiveCity;
	    /// <summary>
	    /// 收货人市
	    /// </summary>
		public  string ReceiveCity {
			set { _ReceiveCity = value; }
			get { return _ReceiveCity; }
		}

        
        private  int _ReceiveDistrictID;
	    /// <summary>
	    /// 收货人区ID
	    /// </summary>
		public  int ReceiveDistrictID {
			set { _ReceiveDistrictID = value; }
			get { return _ReceiveDistrictID; }
		}

        
        private  string _ReceiveDistrict;
	    /// <summary>
	    /// 收货人区
	    /// </summary>
		public  string ReceiveDistrict {
			set { _ReceiveDistrict = value; }
			get { return _ReceiveDistrict; }
		}

        
        private  string _ReceiveAddressDetail;
	    /// <summary>
	    /// 收货人详细地址
	    /// </summary>
		public  string ReceiveAddressDetail {
			set { _ReceiveAddressDetail = value; }
			get { return _ReceiveAddressDetail; }
		}

        
        private  string _ReceiveAddress;
	    /// <summary>
	    /// 收货人地址
	    /// </summary>
		public  string ReceiveAddress {
			set { _ReceiveAddress = value; }
			get { return _ReceiveAddress; }
		}

        
        private  string _ReceivePostCode;
	    /// <summary>
	    /// 收货人邮政编码
	    /// </summary>
		public  string ReceivePostCode {
			set { _ReceivePostCode = value; }
			get { return _ReceivePostCode; }
		}

        
        private  int _IsScanDelivery;
	    /// <summary>
	    /// 是否先校验后发货 0否 1是
	    /// </summary>
		public  int IsScanDelivery {
			set { _IsScanDelivery = value; }
			get { return _IsScanDelivery; }
		}

        
        private  int _IsWeightDelivery;
	    /// <summary>
	    /// 是否先称重后发货 0否 1是
	    /// </summary>
		public  int IsWeightDelivery {
			set { _IsWeightDelivery = value; }
			get { return _IsWeightDelivery; }
		}

        
        private  int _IsOpenWeightWarn;
	    /// <summary>
	    /// 是否开启称重预警 0否 1是
	    /// </summary>
		public  int IsOpenWeightWarn {
			set { _IsOpenWeightWarn = value; }
			get { return _IsOpenWeightWarn; }
		}

        
        private  decimal _DeviationWeight;
	    /// <summary>
	    /// 称重预警误差重量
	    /// </summary>
		public  decimal DeviationWeight {
			set { _DeviationWeight = value; }
			get { return _DeviationWeight; }
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

