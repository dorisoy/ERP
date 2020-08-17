using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 商品转换明细记录
	/// </summary>
	[Serializable]
	public partial class WarehouseConversionItemLog {
		public WarehouseConversionItemLog() { }
        
        
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

        
        private  string _BillNo;
	    /// <summary>
	    /// 转换记录单号
	    /// </summary>
		public  string BillNo {
			set { _BillNo = value; }
			get { return _BillNo; }
		}

        
        private  int _ClID;
	    /// <summary>
	    /// 转换记录ID
	    /// </summary>
		public  int ClID {
			set { _ClID = value; }
			get { return _ClID; }
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

        
        private  int _ProductsBatchID;
	    /// <summary>
	    /// warehouseProductsBatch表主键ID
	    /// </summary>
		public  int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}

        
        private  int _Num;
	    /// <summary>
	    /// 转换数量
	    /// </summary>
		public  int Num {
			set { _Num = value; }
			get { return _Num; }
		}

        
        private  int _ConversionWay;
	    /// <summary>
	    /// 转换方向 0：转入 1：转出
	    /// </summary>
		public  int ConversionWay {
			set { _ConversionWay = value; }
			get { return _ConversionWay; }
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

		
	}
}

