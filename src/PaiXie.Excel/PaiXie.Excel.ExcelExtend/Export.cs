using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace PaiXie.ExcelExtend
{
	public static class Export
	{
		private static HSSFWorkbook _hssfworkbook;
		public static void ExportTo(string fileName, string name, string[] format, List<TitleStyle> titleStyles, DataTable data)
		{
			if (format != null)
			{
				Export.InitializeWorkbook();
				ISheet sheet = Export._hssfworkbook.CreateSheet(name);
				int num = 0;
				if (titleStyles != null)
				{
					for (int i = 0; i < titleStyles.Count; i++)
					{
						try
						{
							IRow row = sheet.CreateRow(i);
							ICell cell = row.CreateCell(0);
							cell.SetCellValue(titleStyles[i].Content);
							ICellStyle cellStyle = Export._hssfworkbook.CreateCellStyle();
							cellStyle.Alignment = HorizontalAlignment.Center;
							IFont font = Export._hssfworkbook.CreateFont();
							font.FontHeight = 256;
							row.HeightInPoints = 20f;
							cellStyle.SetFont(font);
							cell.CellStyle = cellStyle;
							sheet.AddMergedRegion(new CellRangeAddress(num, titleStyles[i].LastRow, 0, titleStyles[i].LastCol));
							num++;
						}
						catch (Exception)
						{
						}
					}
				}
				if (format != null)
				{
					IRow row2 = sheet.CreateRow(num);
					for (int i = 0; i < format.Length; i++)
					{
						try
						{
							string cellValue = format[i].Split(new char[]
							{
								'|'
							})[1];
							row2.CreateCell(i).SetCellValue(cellValue);
						}
						catch (Exception)
						{
						}
					}
					num++;
				}
				for (int j = 0; j < data.Rows.Count; j++)
				{
					IRow row2 = sheet.CreateRow(num);
					for (int k = 0; k < format.Length; k++)
					{
						try
						{
							string columnName = format[k].Split(new char[]
							{
								'|'
							})[0];
							string cellValue = (data.Rows[j][columnName] == DBNull.Value) ? "" : Convert.ToString(data.Rows[j][columnName]);
							row2.CreateCell(k).SetCellValue(cellValue);
						}
						catch (Exception)
						{
						}
					}
					num++;
				}
				Export.WriteToFile(fileName);
			}
		}
		public static void ExportTo(string fileName, string name, string[] format, DataTable data)
		{
			Export.ExportTo(fileName, name, format, null, data);
		}
		private static void WriteToFile(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Create);
			Export._hssfworkbook.Write(fileStream);
			fileStream.Close();
		}
		private static void InitializeWorkbook()
		{
			Export._hssfworkbook = new HSSFWorkbook();
			DocumentSummaryInformation documentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation();
			documentSummaryInformation.Company = "拍鞋网 PAIXIE";
			Export._hssfworkbook.DocumentSummaryInformation = documentSummaryInformation;
			SummaryInformation summaryInformation = PropertySetFactory.CreateSummaryInformation();
			summaryInformation.Subject = "ERP-NET TEAM";
			Export._hssfworkbook.SummaryInformation = summaryInformation;
		}
	}
}
