using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace PaiXie.Utils {
	public partial class ExcelFile {

		#region Excel导入

		/// <summary>
		/// Excel导入
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static DataTable ImportExcelFile(string filePath) {
			HSSFWorkbook hssfworkbook;
			#region//初始化信息
			try {
				using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
					hssfworkbook = new HSSFWorkbook(file);
				}
			}
			catch (Exception e) {
				throw e;
			}
			#endregion

			ISheet sheet = hssfworkbook.GetSheetAt(0);

			DataTable table = new DataTable();
			IRow headerRow = sheet.GetRow(0);//第一行为标题行
			int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
			int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

			//handling header.
			for (int i = headerRow.FirstCellNum; i < cellCount; i++) {
				DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
				table.Columns.Add(column);
			}
			for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++) {
				IRow row = sheet.GetRow(i);
				DataRow dataRow = table.NewRow();

				if (row != null) {
					for (int j = row.FirstCellNum; j < cellCount; j++) {
						if (row.GetCell(j) != null)
							dataRow[j] = GetCellValue(row.GetCell(j));
					}
				}

				table.Rows.Add(dataRow);
			}
			return table;


		}

		#endregion

		#region  根据Excel列类型获取列的值

		/// <summary>
		/// 根据Excel列类型获取列的值
		/// </summary>
		/// <param name="cell">Excel列</param>
		/// <returns></returns>
		private static string GetCellValue(ICell cell) {
			if (cell == null)
				return string.Empty;
			switch (cell.CellType) {
				case CellType.Blank:
					return string.Empty;
				case CellType.Boolean:
					return cell.BooleanCellValue.ToString();
				case CellType.Error:
					return cell.ErrorCellValue.ToString();
				case CellType.Numeric:
				case CellType.Unknown:
				default:
					return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
				case CellType.String:
					return cell.StringCellValue;
				case CellType.Formula:
					try {
						HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
						e.EvaluateInCell(cell);
						return cell.ToString();
					}
					catch {
						return cell.NumericCellValue.ToString();
					}
			}
		}


		#endregion
	}
}
