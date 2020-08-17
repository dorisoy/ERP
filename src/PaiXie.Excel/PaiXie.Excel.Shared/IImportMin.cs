using System;
using System.Data;
namespace PaiXie.Excel.Shared
{
	public interface IImportMin
	{
		DataTable Import(string xlsUrl, int headerRowIndex);
	}
}
