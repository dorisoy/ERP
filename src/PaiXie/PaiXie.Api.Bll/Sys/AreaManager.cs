using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PaiXie.Api.Bll {
	/// <summary>
	/// 省市区街道地址
	/// </summary>
	public class AreaManager {
		#region AreaAnalysis

		/// <summary>
		/// 分析地址，返回匹配的省市县名称和ID
		/// 未匹配到县级地区情况下,如果县级地区包含有其它区、其他区或辖区，则匹配为该区。
		/// </summary>
		/// <param name="address">要分析的地址</param>
		/// <returns>返回匹配的地区结构体</returns>
		public static Area GetAreaInfo(string address) {
			address = address.Trim();
			Area area = new Area();
			if (string.IsNullOrEmpty(address))
				return area;
			area.Address = address;
			string filter = "";
			DataTable dt = SysareaService.GetManySysarea();

			//匹配省份地区ID
			filter = " ParentID = 0 ";
			DataRow[] drProvince = dt.Select(filter);
			string temAddress = address;
			int provinceId = GetAreaId(drProvince, ref temAddress);
			if (provinceId > 0)//找到省份ID
            {
				area.ProvinceID = provinceId;
				filter = " ID = " + provinceId;
				DataRow[] dr1 = dt.Select(filter);
				if (dr1.Length > 0) {
					area.Province = Convert.ToString(dr1[0]["Name"]);
				}

			}
			//匹配市级地区ID
			if (area.ProvinceID > 0)//找到省份ID
            {
				filter = " ParentID = " + area.ProvinceID;
				DataRow[] drCity = dt.Select(filter);
				int cityId = GetAreaId(drCity, ref temAddress);
				if (cityId > 0)//找到市级地区ID
                {
					area.CityID = cityId;
					filter = " ID = " + cityId;
					DataRow[] dr2 = dt.Select(filter);
					if (dr2.Length > 0) {
						area.City = Convert.ToString(dr2[0]["Name"]);
					}
				}
				if (area.CityID < 1)//未找到市级地区ID,有可能是直辖市或只写县级市，从县级地区匹配
                {
					for (int i = 0; i < drCity.Length; i++) {
						filter = " Name <> '其它区' and Name <> '辖区' and Name <> '其他区' And  ParentID = " + drCity[i]["ID"].ToString();//不要用其它区、辖区、其他区匹配，否则会出现错误。因为这些区的关键字是镇、县、区，可能会匹配到。
						DataRow[] drCounty = dt.Select(filter);
						int countyId = GetAreaId(drCounty, ref temAddress);
						if (countyId > 0)//找到市级地区ID
                        {
							area.CountyID = countyId;
							filter = " ID = " + countyId;
							DataRow[] dr3 = dt.Select(filter);
							if (dr3.Length > 0) {
								area.County = Convert.ToString(dr3[0]["Name"]);
								area.CityID = ZConvert.StrToInt(dr3[0]["ParentID"]);
								filter = " ID = " + area.CityID;
								DataRow[] dr2 = dt.Select(filter);
								if (dr2.Length > 0) {
									area.City = Convert.ToString(dr2[0]["Name"]);
								}
							}
							break;//只匹配第一个
						}
					}

				}
			}

			//匹配县级地区ID
			if (area.CityID > 0 && area.CountyID < 1)//找到市级地区ID
            {
				filter = " ParentID = " + area.CityID;
				DataRow[] drCounty = dt.Select(filter);
				if (drCounty.Length > 0) {
					int countyId = GetAreaId(drCounty, ref temAddress);
					if (countyId > 0)//找到县级地区ID
                    {
						area.CountyID = countyId;
						filter = " ID = " + countyId;
						DataRow[] dr3 = dt.Select(filter);
						if (dr3.Length > 0) {
							area.County = Convert.ToString(dr3[0]["Name"]);
						}
					}
					else//未匹配到县级地区情况下
                    {
						//如果县级地区包含有其它区、其他区或辖区，则匹配为该区。
						for (int i = 0; i < drCounty.Length; i++) {
							string county = Convert.ToString(drCounty[i]["Name"]);
							if (county == "其它区" || county == "其他区" || county == "辖区") {
								countyId = ZConvert.StrToInt(drCounty[i]["AreaID"]);
								area.CountyID = countyId;
								area.County = county;
								break;
							}
						}
					}
				}
			}
			return area;
		}


		/// <summary>
		/// 获取地区ID
		/// </summary>
		/// <param name="areas"></param>
		/// <param name="address">返回过滤匹配关键字后的地址，以免上一级地区名称和下一级名称关键字相同时又重新匹配到。</param>
		/// <returns>失败返回-1</returns>
		private static int GetAreaId(DataRow[] areas, ref string address) {
			int areaId = -1;
			int firstIndex = -1;//最匹配字符串索引位置
			string firstKey = "";//最匹配关键字
			int rowIndex = -1;
			for (int i = 0; i < areas.Length; i++) {
				string[] areaKeys = Convert.ToString(areas[i]["AreaKeys"]).Trim().Replace("，", ",").Split(new char[] { ',' });
				foreach (string areaKey in areaKeys) {
					if (string.IsNullOrEmpty(areaKey)) {
						continue;
					}
					int foundIndex = address.IndexOf(areaKey);
					if (foundIndex == -1) {
						continue;
					}
					else {
						if ((firstIndex == -1)//第一次找到
							|| (foundIndex < firstIndex //取最小者
							|| ((foundIndex == firstIndex) && areaKey.Length > firstKey.Length))//索引位置相同时取key长度大的；如设置北京为北京、北京市两个关键字，取北京市。
							) {
							firstIndex = foundIndex;
							rowIndex = i;
							firstKey = areaKey;
						}
					}
				}
			}

			if (rowIndex > -1) {
				areaId = ZConvert.StrToInt(areas[rowIndex]["ID"]);
				address = address.Remove(firstIndex, firstKey.Length);
			}
			return areaId;
		}

		/// <summary>
		/// 地区结构体
		/// </summary>
		public class Area {
			/// <summary>
			/// 省级地区名称
			/// </summary>
			public string Province = string.Empty;
			/// <summary>
			/// 市级地区名称
			/// </summary>
			public string City = string.Empty;
			/// <summary>
			/// 县级地区名称
			/// </summary>
			public string County = string.Empty;
			/// <summary>
			/// 省级地区ID
			/// </summary>
			public int ProvinceID = 0;
			/// <summary>
			/// 市级地区ID
			/// </summary>
			public int CityID = 0;
			/// <summary>
			/// 县级地区Id
			/// </summary>
			public int CountyID = 0;
			/// <summary>
			/// 原来用来分析的地址
			/// </summary>
			public string Address = string.Empty;
		}

		#endregion
	}
}
