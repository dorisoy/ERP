using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class SelectAreaWebInfo {
		public List<LargeArea> LargeAreaList { get; set; }
	}

	public class LargeArea {

		/// <summary>
		/// 大区名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 省份列表
		/// </summary>
		public List<Province> ProvinceList { get; set; }
	}

	public class Province {

		/// <summary>
		/// 省份ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 省份简称
		/// </summary>
		public string AliasName { get; set; }

		/// <summary>
		/// 是否选中
		/// </summary>
		public bool IsChecked { get; set; }

		/// <summary>
		/// 城市列表
		/// </summary>
		public List<City> CityList { get; set; }
	}

	public class City {

		/// <summary>
		/// 城市ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 城市名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 是否选中
		/// </summary>
		public bool IsChecked { get; set; }
	}
}
