using System;
using System.Collections.Generic;
using System.Data;
namespace PaiXie.Excel.Shared
{
	public interface IExportMin
	{
		void GenerateXlsFormat(string format, string xlsUrl, DataTable dt, string reportName);
		void GenerateXlsFormat<T>(string format, string xlsUrl, List<T> list, string reportName);
		void GenerateXlsTemplate(string templateUrl, string xlsUrl, DataTable dt, string reportName);
		void GenerateXlsTemplate<T>(string templateUrl, string xlsUrl, List<T> list, string reportName);
	}
}
