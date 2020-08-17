using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PaiXie.Excel;
namespace PaiXie.Test {
	class Program {
		static void Main(string[] args) {
			DateTime beignTime = DateTime.Now;
			string format = "SortID|序号;ProductsTitle|商品名称;ProductsSkuCode|Sku码";
			string reportName = "商品Sku";
			DataTable dt = new DataTable();
			DataColumn column1 = new DataColumn();
			column1.ColumnName = "SortID";
			column1.DataType = typeof(int);
			dt.Columns.Add(column1);

			DataColumn column2 = new DataColumn();
			column2.ColumnName = "ProductsTitle";
			column2.DataType = typeof(string);
			dt.Columns.Add(column2);

			DataColumn column3 = new DataColumn();
			column3.ColumnName = "ProductsSkuCode";
			column3.DataType = typeof(string);
			dt.Columns.Add(column3);
			for (int i = 0; i < 10000; i++) {
				DataRow newDr = dt.NewRow();
				newDr["SortID"] = i + 1;
				newDr["ProductsTitle"] = "商品名称" + (i + 1);
				newDr["ProductsSkuCode"] = "条码" + (i + 1);
				dt.Rows.Add(newDr);
			}
			ExcelHelp.exportMin.GenerateXlsFormat(format, @"D:\BaiduYunDownload\erp(3)\src\PaiXie.Excel\PaiXie.Test\" + Guid.NewGuid() + ".xls", dt, reportName);
			DateTime endTime = DateTime.Now;
			string useTime = DateDiff(beignTime, endTime);
			Console.WriteLine("导出成功用时：" + useTime);
			Console.ReadLine(); 
			//DataTable dt = ExcelHelp.importMin.Import(@"C:\Users\Administrator\Desktop\111111111.xlsx", 0);
		}

		private static string DateDiff(DateTime DateTime1, DateTime DateTime2) {
			string dateDiff = null;
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new
			TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
			return dateDiff;
		}
	}
}
