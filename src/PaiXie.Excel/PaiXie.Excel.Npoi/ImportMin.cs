using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;
using PaiXie.Excel.Shared;
using NPOI.XSSF.UserModel;
namespace PaiXie.Excel.Npoi
{
	public class ImportMin : IImportMin
	{
		public DataTable Import(string xlsUrl, int headerRowIndex)
		{
			return ImportMin.ImportDataTableFromExcel(xlsUrl, headerRowIndex);
		}
		public static DataTable ImportDataTableFromExcel(string url, int headerRowIndex)
		{
			FileStream fileStream = null;

			ISheet sheetAt;
			
			try {
				fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
				HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
				sheetAt = hSSFWorkbook.GetSheetAt(0);
			}
			catch {
				fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
				XSSFWorkbook xSSFWorkbook = new XSSFWorkbook(fileStream);
				sheetAt = xSSFWorkbook.GetSheetAt(0);
			}
			DataTable dataTable = new DataTable();
			IRow row = sheetAt.GetRow(headerRowIndex);
			int lastCellNum = (int)row.LastCellNum;
			for (int i = (int)row.FirstCellNum; i < lastCellNum; i++)
			{
				DataColumn column = new DataColumn(row.GetCell(i).StringCellValue);
				dataTable.Columns.Add(column);
			}
			for (int i = headerRowIndex + 1; i <= sheetAt.LastRowNum; i++)
			{
				IRow row2 = sheetAt.GetRow(i);
				DataRow dataRow = dataTable.NewRow();
				for (int j = (int)row2.FirstCellNum; j < lastCellNum; j++)
				{
					dataRow[j] = ((row2.GetCell(j) == null) ? string.Empty : row2.GetCell(j).ToString());
				}
				dataTable.Rows.Add(dataRow);
			}
			fileStream.Dispose();
			return dataTable;
		}
	}
}
