using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	/// <summary>
	/// 仓库商品信息
	/// </summary>
	public class WarehouseProductsList : Products{
		public WarehouseProductsList() { }
        
		private string _BrandName;
		/// <summary>
		/// 商品分类名称
		/// </summary>
		public string BrandName {
			set { _BrandName = value; }
			get { return _BrandName; }
		}
       
		private string _CategoryName;
		/// <summary>
		/// 商品分类名称
		/// </summary>
		public string CategoryName {
			set { _CategoryName = value; }
			get { return _CategoryName; }
		}

        private  int _Num;
	    /// <summary>
	    /// 库存数量
	    /// </summary>
		public int Num {
			set { _Num = value; }
			get { return _Num; }
		}
	}
}
