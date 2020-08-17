using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PaiXie.Data
{
    /// <summary>
	/// 仓库库位绑定商品信息表
	/// </summary>
	[Serializable]
	public partial class WarehouseLocationProducts {
		public WarehouseLocationProducts() { }
        
        
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
	    /// 仓库编号
	    /// </summary>
		public  string WarehouseCode {
			set { _WarehouseCode = value; }
			get { return _WarehouseCode; }
		}

		
        private  int _TopLocationID;
	    /// <summary>
		/// 库位所属库区ID
	    /// </summary>
		public int TopLocationID {
			set { _TopLocationID = value; }
			get { return _TopLocationID; }
		}

        private  int _LocationID;
	    /// <summary>
	    /// 库位ID(库位信息表主键)
	    /// </summary>
		public  int LocationID {
			set { _LocationID = value; }
			get { return _LocationID; }
		}

        
        private  int _LocationTypeID;
	    /// <summary>
	    /// 库位类型ID（包括中转区=1、废品区=2、发货区=3、备用区=4 单选）
	    /// </summary>
		public  int LocationTypeID {
			set { _LocationTypeID = value; }
			get { return _LocationTypeID; }
		}

        
        private  int _ProductsID;
	    /// <summary>
	    /// 商品表标识
	    /// </summary>
		public  int ProductsID {
			set { _ProductsID = value; }
			get { return _ProductsID; }
		}

        
        private  int _ProductsSkuID;
	    /// <summary>
	    /// 商品Sku表标识
	    /// </summary>
		public  int ProductsSkuID {
			set { _ProductsSkuID = value; }
			get { return _ProductsSkuID; }
		}

        
        private  int _ProductsBatchID;
	    /// <summary>
	    /// 商品批次表标识
	    /// </summary>
		public  int ProductsBatchID {
			set { _ProductsBatchID = value; }
			get { return _ProductsBatchID; }
		}

        
        private  string _ProductsBatchCode;
	    /// <summary>
	    /// 商品批次号
	    /// </summary>
		public  string ProductsBatchCode {
			set { _ProductsBatchCode = value; }
			get { return _ProductsBatchCode; }
		}

        
        private  DateTime _ProductionDate;
	    /// <summary>
	    /// 生产日期
	    /// </summary>
		public  DateTime ProductionDate {
			set { _ProductionDate = value; }
			get { return _ProductionDate; }
		}

        
        private  int _ShelfLife;
	    /// <summary>
	    /// 保质期（天）
	    /// </summary>
		public  int ShelfLife {
			set { _ShelfLife = value; }
			get { return _ShelfLife; }
		}

        
        private  int _KyNum;
	    /// <summary>
	    /// 可用数量
	    /// </summary>
		public  int KyNum {
			set { _KyNum = value; }
			get { return _KyNum; }
		}

        
        private  int _ZyNum;
	    /// <summary>
	    /// 占用数量
	    /// </summary>
		public  int ZyNum {
			set { _ZyNum = value; }
			get { return _ZyNum; }
		}

        
        private  int _DjNum;
	    /// <summary>
	    /// 冻结数量
	    /// </summary>
		public  int DjNum {
			set { _DjNum = value; }
			get { return _DjNum; }
		}

        
        private  int _SdNum;
	    /// <summary>
	    /// 手动冻结
	    /// </summary>
		public  int SdNum {
			set { _SdNum = value; }
			get { return _SdNum; }
		}

        
        private  int _ZkNum;
	    /// <summary>
	    /// 在库数量
	    /// </summary>
		public  int ZkNum {
			set { _ZkNum = value; }
			get { return _ZkNum; }
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

