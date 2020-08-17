using System;
using System.Text.RegularExpressions;
namespace PaiXie.Utils {
	public partial class ZConvert {
		/// <summary>
		/// 金额转大写
		/// </summary>
		/// <remarks>只支持三位小数转换,多余位数四舍五入</remarks>
		/// <param name="LowerMoney">输入金额</param>
		/// <returns>中文大写金额</returns>
		public static string ToChinese(string input) {
			if (string.IsNullOrEmpty(input))
				throw new ArgumentOutOfRangeException("input");
			string temp = string.Empty;

			bool IsNegative = false; // 是否是负数

			if (input.Trim().Substring(0, 1) == "-") {
				// 是负数则先转为正数
				input = input.Trim().Remove(0, 1);
				IsNegative = true;
			}

			input.Replace(",", "");//移除美式金额写法的逗号
			if (input.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length > 3)
				throw new ArgumentException("输入金额不合法！", "input");

			string strLower = null;
			string strUpart = null;
			string strUpper = null;

			int iTemp = 0;
			// 保留三位小数 123.4888→123.489　　123.4→123.4
			input = Math.Round(double.Parse(input), 3).ToString();
			if (input.IndexOf(".") > 0) {
				if (input.IndexOf(".") == input.Length - 3) {
					input = input + "00";
				}
			}
			else {
				input = input + ".000";
			}
			strLower = input;
			iTemp = 1;
			strUpper = "";
			while (iTemp <= strLower.Length) {
				switch (strLower.Substring(strLower.Length - iTemp, 1)) {
					case ".":
						strUpart = "圆";
						break;
					case "0":
						strUpart = "零";
						break;
					case "1":
						strUpart = "壹";
						break;
					case "2":
						strUpart = "贰";
						break;
					case "3":
						strUpart = "叁";
						break;
					case "4":
						strUpart = "肆";
						break;
					case "5":
						strUpart = "伍";
						break;
					case "6":
						strUpart = "陆";
						break;
					case "7":
						strUpart = "柒";
						break;
					case "8":
						strUpart = "捌";
						break;
					case "9":
						strUpart = "玖";
						break;
				}

				switch (iTemp) {
					case 1:
						strUpart = strUpart + "厘";
						break;
					case 2:
						strUpart = strUpart + "分";
						break;
					case 3:
						strUpart = strUpart + "角";
						break;
					case 4:
						strUpart = strUpart + "";
						break;
					case 5:
						strUpart = strUpart + "";
						break;
					case 6:
						strUpart = strUpart + "拾";
						break;
					case 7:
						strUpart = strUpart + "佰";
						break;
					case 8:
						strUpart = strUpart + "仟";
						break;
					case 9:
						strUpart = strUpart + "万";
						break;
					case 10:
						strUpart = strUpart + "拾";
						break;
					case 11:
						strUpart = strUpart + "佰";
						break;
					case 12:
						strUpart = strUpart + "仟";
						break;
					case 13:
						strUpart = strUpart + "亿";
						break;
					case 14:
						strUpart = strUpart + "拾";
						break;
					case 15:
						strUpart = strUpart + "佰";
						break;
					case 16:
						strUpart = strUpart + "仟";
						break;
					case 17:
						strUpart = strUpart + "万";
						break;
					default:
						strUpart = strUpart + "";
						break;
				}

				strUpper = strUpart + strUpper;
				iTemp = iTemp + 1;
			}

			strUpper = strUpper.Replace("零拾", "零");
			strUpper = strUpper.Replace("零佰", "零");
			strUpper = strUpper.Replace("零仟", "零");
			strUpper = strUpper.Replace("零零零", "零");
			strUpper = strUpper.Replace("零零", "零");
			strUpper = strUpper.Replace("零角零分零厘", "整");
			//strUpper = strUpper.Replace("角零分", "角");
			//strUpper = strUpper.Replace("零角", "零");
			strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
			strUpper = strUpper.Replace("亿零万零圆", "亿圆");
			strUpper = strUpper.Replace("零亿零万", "亿");
			strUpper = strUpper.Replace("零万零圆", "万圆");
			strUpper = strUpper.Replace("零亿", "亿");
			strUpper = strUpper.Replace("零万", "万");
			strUpper = strUpper.Replace("零圆", "圆");
			strUpper = strUpper.Replace("零零", "零");

			// 对壹圆以下的金额的处理
			if (strUpper.Substring(0, 1) == "圆") {
				strUpper = strUpper.Substring(1, strUpper.Length - 1);
			}
			if (strUpper.Substring(0, 1) == "零") {
				strUpper = strUpper.Substring(1, strUpper.Length - 1);
			}
			if (strUpper.Substring(0, 1) == "角") {
				strUpper = strUpper.Substring(1, strUpper.Length - 1);
			}
			if (strUpper.Substring(0, 1) == "分") {
				strUpper = strUpper.Substring(1, strUpper.Length - 1);
			}
			if (strUpper.Substring(0, 1) == "厘") {
				strUpper = strUpper.Substring(1, strUpper.Length - 1);
			}
			if (strUpper.Substring(0, 1) == "整") {
				strUpper = "零圆整";
			}
			temp = strUpper;

			if (IsNegative == true) {
				return "负" + temp;
			}
			else {
				return temp;
			}
		}
		/// <summary>
		/// 金额转大写
		/// </summary>
		/// <param name="input">输入金额</param>
		/// <returns>中文大写金额</returns>
		public static string ToChinese(decimal input) { return ToChinese(input.ToString()); }
	}
}