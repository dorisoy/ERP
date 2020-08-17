using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Utils {
	public class DateTimeUtility {
		/// <summary>
		/// 日期转换为时间戳（时间戳单位秒）
		/// </summary>
		/// <param name="TimeStamp"></param>
		/// <returns></returns>
		public static string ConvertToTimeStamp(DateTime time) {

			System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
			return ((int)(time - startTime).TotalSeconds).ToString();

		}

		/// <summary>
		/// 时间戳转换为日期（时间戳单位秒）
		/// </summary>
		/// <param name="TimeStamp"></param>
		/// <returns></returns>
		public static DateTime ConvertToDateTime(string timeStamp) {

			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long lTime = long.Parse(timeStamp + "0000000");
			TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);

		}

	}
}
