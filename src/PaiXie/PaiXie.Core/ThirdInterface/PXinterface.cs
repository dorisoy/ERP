using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using PaiXie.Utils;
namespace PaiXie.Core {
	public class PXinterface {

		/// <summary>
		/// 拍鞋网或微小店post提交
		/// </summary>
		/// <param name="url">接口地址</param>
		/// <param name="paramDictionary">参数字典</param>
		/// <returns></returns>
		public static string GetPost(string url, IDictionary<string, string> paramDictionary) {
			string api_signkey = paramDictionary["api_signkey"].ToString();
			paramDictionary.Add("v", "1.11");//V2.0
			paramDictionary.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			paramDictionary.Remove("api_signkey");
			string sign = getSign(paramDictionary, paramDictionary["api_key"].ToString(), api_signkey);
			paramDictionary.Add("sign", sign);
			string OrdersStr = ZHttp.WebRequestPost(url, paramDictionary);
			return GetGBString(OrdersStr);
		}

		/// <summary> 
		/// 获取签名
		/// </summary> 
		/// <param name="parameters"></param> 
		/// <param name="api_key"></param> 
		/// <param name="api_signkey"></param> 
		/// <returns></returns> 
		public static String getSign(IDictionary<string, string> parameters, string api_key, string api_signkey) {
			// 第一步：把字典按Key的字母顺序排序 
			IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
			IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

			// 第二步：把所有参数名和参数值串在一起 
			StringBuilder query = new StringBuilder("");
			while (dem.MoveNext()) {
				string key = dem.Current.Key;
				string value = dem.Current.Value;
				if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value)) {
					query.Append(key).Append(value);
				}
			}
			string source = query.ToString();
			source = api_key + source + api_signkey;
			return ZEncypt.MD5(source).ToUpper();

		}

		#region 十六进制转换
		/// <summary> 
		/// 十六进制转换 
		/// </summary> 
		/// <param name="content"></param> 
		/// <returns></returns> 
		public static string GetGBString(string content) {
			string strreg = @"\\u([0-9a-fA-F]{4})";
			System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(strreg,
			System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			System.Text.RegularExpressions.MatchEvaluator evaluator = new System.Text.RegularExpressions.MatchEvaluator(ReplaceMatchEvaluator);
			string result = reg.Replace(content, ReplaceMatchEvaluator);
			return result;


		}
		private static string ReplaceMatchEvaluator(System.Text.RegularExpressions.Match m) {
			string reult = ToGB2312(m.Value);
			return reult;

		}

		/// <summary> 
		/// 16进制字符串转为中文 
		/// </summary> 
		/// <param name="str"></param> 
		/// <returns></returns> 
		private static string ToGB2312(string str) {
			string r = "";
			System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			byte[] bts = new byte[2];
			foreach (System.Text.RegularExpressions.Match m in mc) {
				bts[0] = (byte)int.Parse(m.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
				bts[1] = (byte)int.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber);
				r += Encoding.Unicode.GetString(bts);
			}
			return r;
		}
		#endregion
	}
}
