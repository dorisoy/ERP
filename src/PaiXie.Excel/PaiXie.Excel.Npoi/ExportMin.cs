using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using PaiXie.Excel.Shared;
namespace PaiXie.Excel.Npoi
{
	public class ExportMin : IExportMin
	{
		public void GenerateXlsFormat(string format, string xlsUrl, DataTable dt, string reportName)
		{
			string value = Path.GetExtension(xlsUrl).Trim(new char[]
			{
				'.'
			}).ToLower();
			ExcelType excelType = (ExcelType)Enum.Parse(typeof(ExcelType), value, true);
			if (excelType != ExcelType.xlsx)
			{
				if (excelType == ExcelType.csv)
				{
					StringBuilder stringBuilder = this.ConvertExcelStringByDs(dt, format);
					File.WriteAllText(xlsUrl, stringBuilder.ToString(), Encoding.Default);
				}
				this.GenerateXls(xlsUrl, dt, format, reportName);
			}
		}
		public void GenerateXlsTemplate(string templateUrl, string xlsUrl, DataTable dt, string reportName)
		{
			string value = Path.GetExtension(xlsUrl).Trim(new char[]
			{
				'.'
			}).ToLower();
			ExcelType excelType = (ExcelType)Enum.Parse(typeof(ExcelType), value, true);
			if (excelType != ExcelType.xlsx)
			{
				if (excelType != ExcelType.csv)
				{
					this.GenerateXlsByTemplate(templateUrl, xlsUrl, dt, reportName);
				}
			}
		}
		public void GenerateXlsFormat<T>(string format, string xlsUrl, List<T> list, string reportName)
		{
			string value = Path.GetExtension(xlsUrl).Trim(new char[]
			{
				'.'
			}).ToLower();
			ExcelType excelType = (ExcelType)Enum.Parse(typeof(ExcelType), value, true);
			if (excelType != ExcelType.xlsx)
			{
				if (excelType == ExcelType.csv)
				{
					StringBuilder stringBuilder = this.ConvertEntity<T>(list, format);
					File.WriteAllText(xlsUrl, stringBuilder.ToString(), Encoding.Default);
				}
				this.GenerateXlsList<T>(xlsUrl, list, format, reportName);
			}
		}
		public void GenerateXlsTemplate<T>(string templateUrl, string xlsUrl, List<T> list, string reportName)
		{
			string value = Path.GetExtension(xlsUrl).Trim(new char[]
			{
				'.'
			}).ToLower();
			ExcelType excelType = (ExcelType)Enum.Parse(typeof(ExcelType), value, true);
			if (excelType != ExcelType.xlsx)
			{
				if (excelType != ExcelType.csv)
				{
					this.GenerateXlsListByTemplate<T>(templateUrl, xlsUrl, list, reportName);
				}
			}
		}
		private void GenerateXlsByTemplate(string templateUrl, string xlsUrl, DataTable dt, string reportName)
		{
			HSSFWorkbook hSSFWorkbook = this.CreateWorkbook(templateUrl);
			HSSFWorkbook hSSFWorkbook2 = this.CreateWorkbook("");
			ISheet sheet = hSSFWorkbook.GetSheet("Sheet1");
			ISheet sheet2 = hSSFWorkbook2.CreateSheet(reportName);
			int num = sheet.LastRowNum - 1;
			IRow row = sheet2.CreateRow(0);
			for (int i = 0; i < 100; i++)
			{
				ICell cell = sheet.GetRow(0).GetCell(i);
				if (cell == null)
				{
					row.CreateCell(i);
				}
				else
				{
					ICellStyle cellStyle = cell.CellStyle;
					ICell cell2 = row.CreateCell(i);
					cell2.SetCellValue(cell.StringCellValue);
					ICellStyle cellStyle2 = hSSFWorkbook2.CreateCellStyle();
					cellStyle2.CloneStyleFrom(cellStyle);
					cell2.CellStyle = cellStyle2;
				}
			}
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				IRow row2 = sheet2.CreateRow(i + 1);
				IRow row3 = null;
				for (int j = 0; j < 100; j++)
				{
					ICell cell = sheet.GetRow(1).GetCell(j);
					if (cell == null)
					{
						row2.CreateCell(j);
					}
					else
					{
						if (cell.CellType == CellType.Formula)
						{
							ICell cell2 = row2.CreateCell(j);
							string text = cell.CellFormula;
							text = text.Replace("2", (i + 2).ToString());
							cell2.SetCellFormula(text);
						}
						else
						{
							ICell cell2 = row2.CreateCell(j);
							string text2 = cell.StringCellValue.Replace("[$", "").Replace("]", "");
							Type dataType = dt.Columns[text2].DataType;
							if (dataType == typeof(decimal) || dataType == typeof(int) || dataType == typeof(double))
							{
								cell2.SetCellValue(Convert.ToDouble(dt.Rows[i][text2]));
							}
							else
							{
								cell2.SetCellValue(dt.Rows[i][text2].ToString());
							}
							if (i == 0)
							{
								ICellStyle cellStyle = hSSFWorkbook2.CreateCellStyle();
								cellStyle.CloneStyleFrom(cell.CellStyle);
								if (cellStyle != null)
								{
									cell2.CellStyle = cellStyle;
								}
							}
							else
							{
								cell2.CellStyle=row3.Cells[j].CellStyle;
							}
						}
					}
				}
			}
			for (int k = 0; k < num; k++)
			{
				IRow row4 = sheet2.CreateRow(dt.Rows.Count + 1 + k);
				for (int i = 0; i < 100; i++)
				{
					IRow row5 = sheet.GetRow(2 + k);
					if (row5 != null)
					{
						ICell cell = row5.GetCell(i);
						if (cell != null)
						{
							ICellStyle cellStyle = cell.CellStyle;
							ICell cell2 = row4.CreateCell(i);
							if (cell.CellType == CellType.Formula)
							{
								string text = cell.CellFormula;
								string str = text.Split(new char[]
								{
									':'
								})[1].Replace("2", (dt.Rows.Count + 1).ToString());
								text = text.Split(new char[]
								{
									':'
								})[0] + ":" + str;
								cell2.SetCellFormula(text);
							}
							else
							{
								cell2.SetCellValue(cell.StringCellValue);
							}
							ICellStyle cellStyle2 = hSSFWorkbook2.CreateCellStyle();
							cellStyle2.CloneStyleFrom(cellStyle);
							cell2.CellStyle = cellStyle2;
						}
					}
				}
			}
			for (int i = 0; i < sheet.NumMergedRegions; i++)
			{
				CellRangeAddress mergedRegion = sheet.GetMergedRegion(i);
				CellRangeAddress cellRangeAddress = mergedRegion.Copy();
				cellRangeAddress.FirstRow = cellRangeAddress.FirstRow + dt.Rows.Count - 1;
				cellRangeAddress.LastRow = cellRangeAddress.LastRow + dt.Rows.Count - 1;
				sheet2.AddMergedRegion(cellRangeAddress);
			}
			sheet2.ForceFormulaRecalculation = true;
			FileStream fileStream = new FileStream(xlsUrl, FileMode.Create);
			hSSFWorkbook2.Write(fileStream);
			fileStream.Close();
		}
		private void GenerateXls(string xlsUrl, DataTable dt, string format, string reportName)
		{
			HSSFWorkbook hSSFWorkbook = this.CreateWorkbook("");
			int num = dt.Rows.Count / 60000;
			int num2 = 60000;
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i <= num; i++)
				{
					DataTable dataTable = dt.AsEnumerable().Skip(i * num2).Take(num2).CopyToDataTable<DataRow>();
					string text = reportName + ((i == 0) ? "" : ("-" + i.ToString()));
					ISheet sheet = hSSFWorkbook.CreateSheet(text);
					IRow row = sheet.CreateRow(0);
					for (int j = 0; j < format.Split(new char[]
					{
						';'
					}).Count<string>(); j++)
					{
						string text2 = format.Split(new char[]
						{
							';'
						})[j];
						string cellValue = text2.Split(new char[]
						{
							'|'
						})[1];
						ICell cell = row.CreateCell(j);
						cell.SetCellValue(cellValue);
					}
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						DataRow dataRow = dataTable.Rows[j];
						IRow row2 = sheet.CreateRow(j + 1);
						for (int k = 0; k < format.Split(new char[]
						{
							';'
						}).Count<string>(); k++)
						{
							string text2 = format.Split(new char[]
							{
								';'
							})[k];
							string text3 = text2.Split(new char[]
							{
								'|'
							})[0];
							ICell cell = row2.CreateCell(k);
							Type dataType = dataTable.Columns[text3].DataType;
							if (dataType == typeof(decimal) || dataType == typeof(int) || dataType == typeof(double))
							{
								cell.SetCellValue(Convert.ToDouble(dataTable.Rows[j][text3]));
							}
							else
							{
								cell.SetCellValue(dataTable.Rows[j][text3].ToString());
							}
						}
					}
					sheet.ForceFormulaRecalculation = true;
				}
			}
			else
			{
				ISheet sheet = hSSFWorkbook.CreateSheet(reportName);
				IRow row = sheet.CreateRow(0);
				for (int j = 0; j < format.Split(new char[]
				{
					';'
				}).Count<string>(); j++)
				{
					string text2 = format.Split(new char[]
					{
						';'
					})[j];
					string cellValue = text2.Split(new char[]
					{
						'|'
					})[1];
					ICell cell = row.CreateCell(j);
					cell.SetCellValue(cellValue);
				}
			}
			FileStream fileStream = new FileStream(xlsUrl, FileMode.Create);
			hSSFWorkbook.Write(fileStream);
			fileStream.Close();
		}
		private HSSFWorkbook CreateWorkbook(string url)
		{
			HSSFWorkbook hSSFWorkbook;
			if (!string.IsNullOrEmpty(url))
			{
				FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
				hSSFWorkbook = new HSSFWorkbook(fileStream);
			}
			else
			{
				hSSFWorkbook = new HSSFWorkbook();
			}
			DocumentSummaryInformation documentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation();
			documentSummaryInformation.Company = "拍鞋网 PAIXIE";
			hSSFWorkbook.DocumentSummaryInformation = documentSummaryInformation;
			SummaryInformation summaryInformation = PropertySetFactory.CreateSummaryInformation();
			summaryInformation.Author="EC-DRP Team";
			hSSFWorkbook.SummaryInformation = summaryInformation;
			return hSSFWorkbook;
		}
		private void GenerateXlsList<T>(string xlsUrl, List<T> list, string format, string reportName)
		{
			HSSFWorkbook hSSFWorkbook = this.CreateWorkbook("");
			int num = list.Count / 60000;
			int num2 = 60000;
			for (int i = 0; i <= num; i++)
			{
				List<T> list2 = list.Skip(i * num2).Take(num2).ToList<T>();
				string text = reportName + ((i == 0) ? "" : ("-" + i.ToString()));
				ISheet sheet = hSSFWorkbook.CreateSheet(text);
				IRow row = sheet.CreateRow(0);
				for (int j = 0; j < format.Split(new char[]
				{
					';'
				}).Count<string>(); j++)
				{
					string text2 = format.Split(new char[]
					{
						';'
					})[j];
					string cellValue = text2.Split(new char[]
					{
						'|'
					})[1];
					ICell cell = row.CreateCell(j);
					cell.SetCellValue(cellValue);
				}
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
				for (int j = 0; j < list2.Count; j++)
				{
					IRow row2 = sheet.CreateRow(j + 1);
					for (int k = 0; k < format.Split(new char[]
					{
						';'
					}).Count<string>(); k++)
					{
						string text2 = format.Split(new char[]
						{
							';'
						})[k];
						string text3 = text2.Split(new char[]
						{
							'|'
						})[0];
						if (properties.Count <= 0)
						{
							return;
						}
						for (int l = 0; l < properties.Count; l++)
						{
							if (properties[l].Name == text3)
							{
								ICell cell = row2.CreateCell(k);
								string text4 = properties[text3].GetValue(list2[j]).ToString();
								Type propertyType = properties[text3].PropertyType;
								if (propertyType == typeof(decimal) || propertyType == typeof(int) || propertyType == typeof(double))
								{
									cell.SetCellValue(Convert.ToDouble(properties[text3].GetValue(list2[j])));
								}
								else
								{
									cell.SetCellValue(properties[text3].GetValue(list2[j]).ToString());
								}
							}
						}
					}
				}
				sheet.ForceFormulaRecalculation = true;
			}
			FileStream fileStream = new FileStream(xlsUrl, FileMode.Create);
			hSSFWorkbook.Write(fileStream);
			fileStream.Close();
		}
		private void GenerateXlsListByTemplate<T>(string templateUrl, string xlsUrl, List<T> list, string reportName)
		{
			HSSFWorkbook hSSFWorkbook = this.CreateWorkbook(templateUrl);
			HSSFWorkbook hSSFWorkbook2 = this.CreateWorkbook("");
			ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
			ISheet sheet = hSSFWorkbook2.CreateSheet(reportName);
			int num = sheetAt.LastRowNum - 1;
			int num2 = Convert.ToInt32(sheetAt.GetRow(1).LastCellNum);
			IRow row = sheet.CreateRow(0);
			for (int i = 0; i < num2; i++)
			{
				ICell cell = sheetAt.GetRow(0).GetCell(i);
				if (cell == null)
				{
					row.CreateCell(i);
				}
				else
				{
					ICellStyle cellStyle = cell.CellStyle;
					ICell cell2 = row.CreateCell(i);
					cell2.SetCellValue(cell.StringCellValue);
					ICellStyle cellStyle2 = hSSFWorkbook2.CreateCellStyle();
					cellStyle2.CloneStyleFrom(cellStyle);
					cell2.CellStyle = cellStyle2;
				}
			}
			ICellStyle cellStyle3 = hSSFWorkbook2.CreateCellStyle();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
			IRow row2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				IRow row3 = sheet.CreateRow(i + 1);
				for (int j = 0; j < num2; j++)
				{
					ICell cell = sheetAt.GetRow(1).GetCell(j);
					if (cell == null)
					{
						row3.CreateCell(j);
					}
					else
					{
						if (cell.CellType == CellType.Formula)
						{
							ICell cell2 = row3.CreateCell(j);
							string text = cell.CellFormula;
							text = text.Replace("2", (i + 2).ToString());
							cell2.SetCellFormula(text);
						}
						else
						{
							ICell cell2 = row3.CreateCell(j);
							string name = cell.StringCellValue.Replace("[$", "").Replace("]", "");
							Type propertyType = properties[name].PropertyType;
							if (propertyType == typeof(decimal) || propertyType == typeof(int) || propertyType == typeof(double))
							{
								cell2.SetCellValue(Convert.ToDouble(properties[name].GetValue(list[i])));
							}
							else
							{
								cell2.SetCellValue(properties[name].GetValue(list[i]).ToString());
							}
							if (i == 0)
							{
								ICellStyle cellStyle = hSSFWorkbook2.CreateCellStyle();
								cellStyle.CloneStyleFrom(cell.CellStyle);
								if (cellStyle != null)
								{
									cell2.CellStyle=cellStyle;
								}
							}
							else
							{
								cell2.CellStyle = row2.Cells[j].CellStyle;
							}
						}
					}
				}
				row2 = row3;
			}
			ICellStyle cellStyle4 = hSSFWorkbook2.CreateCellStyle();
			for (int k = 0; k < num; k++)
			{
				IRow row4 = sheet.CreateRow(list.Count + 1 + k);
				for (int i = 0; i < num2; i++)
				{
					IRow row5 = sheetAt.GetRow(2 + k);
					if (row5 != null)
					{
						ICell cell = row5.GetCell(i);
						if (cell != null)
						{
							ICellStyle cellStyle = cell.CellStyle;
							ICell cell2 = row4.CreateCell(i);
							if (cell.CellType == CellType.Formula)
							{
								string text = cell.CellFormula;
								string str = text.Split(new char[]
								{
									':'
								})[1].Replace("2", (list.Count + 1).ToString());
								text = text.Split(new char[]
								{
									':'
								})[0] + ":" + str;
								cell2.SetCellFormula(text);
							}
							else
							{
								cell2.SetCellValue(cell.StringCellValue);
							}
							cellStyle4.CloneStyleFrom(cellStyle);
							cell2.CellStyle = cellStyle4;
						}
					}
				}
			}
			for (int i = 0; i < sheetAt.NumMergedRegions; i++)
			{
				CellRangeAddress mergedRegion = sheetAt.GetMergedRegion(i);
				CellRangeAddress cellRangeAddress = mergedRegion.Copy();
				cellRangeAddress.FirstRow=cellRangeAddress.FirstRow + list.Count - 1;
				cellRangeAddress.LastRow=cellRangeAddress.LastRow + list.Count - 1;
				sheet.AddMergedRegion(cellRangeAddress);
			}
			sheet.ForceFormulaRecalculation = true;
			FileStream fileStream = new FileStream(xlsUrl, FileMode.Create);
			hSSFWorkbook2.Write(fileStream);
			fileStream.Close();
		}
		private PropertyDescriptorCollection FindProperties<T>()
		{
			return TypeDescriptor.GetProperties(typeof(T));
		}
		private StringBuilder ConvertEntity<T>(List<T> list, string format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AddHead<T>(stringBuilder, format);
			this.AddBody<T>(stringBuilder, list, format);
			return stringBuilder;
		}
		private void AddHead<T>(StringBuilder sb, string format)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.FindProperties<T>();
			if (propertyDescriptorCollection.Count > 0)
			{
				string[] array = format.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string str = (i == array.Length - 1) ? "\r\n" : ",";
					sb.Append(array[i].Split(new char[]
					{
						'|'
					})[1] + str);
				}
			}
		}
		private void AddBody<T>(StringBuilder sb, List<T> list, string format)
		{
			if (list != null && list.Count > 0)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = this.FindProperties<T>();
				if (propertyDescriptorCollection.Count > 0)
				{
					string[] array = format.Split(new char[]
					{
						';'
					});
					for (int i = 0; i < list.Count; i++)
					{
						for (int j = 0; j < array.Length; j++)
						{
							string text = format.Split(new char[]
							{
								';'
							})[j];
							string text2 = text.Split(new char[]
							{
								'|'
							})[0];
							for (int k = 0; k < propertyDescriptorCollection.Count; k++)
							{
								if (propertyDescriptorCollection[k].Name == text2)
								{
									string str = (j == array.Length - 1) ? "\r\n" : ",";
									string str2 = propertyDescriptorCollection[text2].GetValue(list[i]).ToString();
									sb.Append(str2 + str);
								}
							}
						}
					}
				}
			}
		}
		private StringBuilder ConvertExcelStringByDs(DataTable dt, string format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = format.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = (i == array.Length - 1) ? "\r\n" : ",";
				stringBuilder.Append(array[i].Split(new char[]
				{
					'|'
				})[1] + text);
			}
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					string text2 = format.Split(new char[]
					{
						';'
					})[j];
					string text3 = text2.Split(new char[]
					{
						'|'
					})[0];
					if (dt.Columns[j].ColumnName == text3)
					{
						string text = (j == array.Length - 1) ? "\r\n" : ",";
						stringBuilder.Append(dt.Rows[i][text3] + text);
					}
				}
			}
			return stringBuilder;
		}
	}
}
