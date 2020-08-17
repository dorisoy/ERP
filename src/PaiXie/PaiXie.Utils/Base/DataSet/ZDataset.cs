using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace PaiXie.Utils
{
    /// <summary> 
    /// DataSet助手 
    /// </summary> 
    public class ZDataSet
    {
        private class FieldInfo
        {
            public string RelationName;
            public string FieldName;
            public string FieldAlias;
            public string Aggregate;
        }

        private DataSet ds;
        private ArrayList m_FieldInfo;
        private string m_FieldList;
        private ArrayList GroupByFieldInfo;
        private string GroupByFieldList;

        public DataSet DataSet
        {
            get { return ds; }
        }

        #region Construction

        public ZDataSet()
        {
            ds = null;
        }

        public ZDataSet(ref DataSet dataSet)
        {
            ds = dataSet;
        }

        #endregion

        #region Private Methods

        private bool ColumnEqual(object objectA, object objectB)
        {
            if (objectA == DBNull.Value && objectB == DBNull.Value)
            {
                return true;
            }
            if (objectA == DBNull.Value || objectB == DBNull.Value)
            {
                return false;
            }
            return (objectA.Equals(objectB));
        }

        private bool RowEqual(DataRow rowA, DataRow rowB, DataColumnCollection columns)
        {
            bool result = true;
            for (int i = 0; i < columns.Count; i++)
            {
                result &= ColumnEqual(rowA[columns[i].ColumnName], rowB[columns[i].ColumnName]);
            }
            return result;
        }

        private void ParseFieldList(string fieldList, bool allowRelation)
        {
            if (m_FieldList == fieldList)
            {
                return;
            }
            m_FieldInfo = new ArrayList();
            m_FieldList = fieldList;
            FieldInfo Field;
            string[] FieldParts;
            string[] Fields = fieldList.Split(',');
            for (int i = 0; i <= Fields.Length - 1; i++)
            {
                Field = new FieldInfo();
                FieldParts = Fields[i].Trim().Split(' ');
                switch (FieldParts.Length)
                {
                    case 1:
                        //to be set at the end of the loop 
                        break;
                    case 2:
                        Field.FieldAlias = FieldParts[1];
                        break;
                    default:
                        return;
                }
                FieldParts = FieldParts[0].Split('.');
                switch (FieldParts.Length)
                {
                    case 1:
                        Field.FieldName = FieldParts[0];
                        break;
                    case 2:
                        if (allowRelation == false)
                        {
                            return;
                        }
                        Field.RelationName = FieldParts[0].Trim();
                        Field.FieldName = FieldParts[1].Trim();
                        break;
                    default:
                        return;
                }
                if (Field.FieldAlias == null)
                {
                    Field.FieldAlias = Field.FieldName;
                }
                m_FieldInfo.Add(Field);
            }
        }

        private DataTable CreateTable(string tableName, DataTable sourceTable, string fieldList)
        {
            DataTable dt;
            if (fieldList.Trim() == "")
            {
                dt = sourceTable.Clone();
                dt.TableName = tableName;
            }
            else
            {
                dt = new DataTable(tableName);
                ParseFieldList(fieldList, false);
                DataColumn dc;
                foreach (FieldInfo Field in m_FieldInfo)
                {
                    dc = sourceTable.Columns[Field.FieldName];
                    DataColumn column = new DataColumn();
                    column.ColumnName = Field.FieldAlias;
                    column.DataType = dc.DataType;
                    column.MaxLength = dc.MaxLength;
                    column.Expression = dc.Expression;
                    dt.Columns.Add(column);
                }
            }
            if (ds != null)
            {
                ds.Tables.Add(dt);
            }
            return dt;
        }

        private void InsertInto(DataTable destTable, DataTable sourceTable,
                                string fieldList, string rowFilter, string sort)
        {
            ParseFieldList(fieldList, false);
            DataRow[] rows = sourceTable.Select(rowFilter, sort);
            DataRow destRow;
            foreach (DataRow sourceRow in rows)
            {
                destRow = destTable.NewRow();
                if (fieldList == "")
                {
                    foreach (DataColumn dc in destRow.Table.Columns)
                    {
                        if (dc.Expression == "")
                        {
                            destRow[dc] = sourceRow[dc.ColumnName];
                        }
                    }
                }
                else
                {
                    foreach (FieldInfo field in m_FieldInfo)
                    {
                        destRow[field.FieldAlias] = sourceRow[field.FieldName];
                    }
                }
                destTable.Rows.Add(destRow);
            }
        }

        private void ParseGroupByFieldList(string FieldList)
        {
            if (GroupByFieldList == FieldList)
            {
                return;
            }
            GroupByFieldInfo = new ArrayList();
            FieldInfo Field;
            string[] FieldParts;
            string[] Fields = FieldList.Split(',');
            for (int i = 0; i <= Fields.Length - 1; i++)
            {
                Field = new FieldInfo();
                FieldParts = Fields[i].Trim().Split(' ');
                switch (FieldParts.Length)
                {
                    case 1:
                        //to be set at the end of the loop 
                        break;
                    case 2:
                        Field.FieldAlias = FieldParts[1];
                        break;
                    default:
                        return;
                }

                FieldParts = FieldParts[0].Split('(');
                switch (FieldParts.Length)
                {
                    case 1:
                        Field.FieldName = FieldParts[0];
                        break;
                    case 2:
                        Field.Aggregate = FieldParts[0].Trim().ToLower();
                        Field.FieldName = FieldParts[1].Trim(' ', ')');
                        break;
                    default:
                        return;
                }
                if (Field.FieldAlias == null)
                {
                    if (Field.Aggregate == null)
                    {
                        Field.FieldAlias = Field.FieldName;
                    }
                    else
                    {
                        Field.FieldAlias = Field.Aggregate + "of" + Field.FieldName;
                    }
                }
                GroupByFieldInfo.Add(Field);
            }
            GroupByFieldList = FieldList;
        }

        private DataTable CreateGroupByTable(string tableName, DataTable sourceTable, string fieldList)
        {
            if (fieldList == null || fieldList.Length == 0)
            {
                return sourceTable.Clone();
            }
            else
            {
                DataTable dt = new DataTable(tableName);
                ParseGroupByFieldList(fieldList);
                foreach (FieldInfo Field in GroupByFieldInfo)
                {
                    DataColumn dc = sourceTable.Columns[Field.FieldName];
                    if (Field.Aggregate == null)
                    {
                        dt.Columns.Add(Field.FieldAlias, dc.DataType, dc.Expression);
                    }
                    else
                    {
                        dt.Columns.Add(Field.FieldAlias, dc.DataType);
                    }
                }
                if (ds != null)
                {
                    ds.Tables.Add(dt);
                }
                return dt;
            }
        }

        private void InsertGroupByInto(DataTable destTable, DataTable sourceTable, string fieldList,
                                       string rowFilter, string groupBy)
        {
            if (fieldList == null || fieldList.Length == 0)
            {
                return;
            }
            ParseGroupByFieldList(fieldList);
            ParseFieldList(groupBy, false);
            DataRow[] rows = sourceTable.Select(rowFilter, groupBy);
            DataRow lastSourceRow = null, destRow = null;
            bool sameRow;
            int rowCount = 0;
            foreach (DataRow sourceRow in rows)
            {
                sameRow = false;
                if (lastSourceRow != null)
                {
                    sameRow = true;
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        if (!ColumnEqual(lastSourceRow[Field.FieldName], sourceRow[Field.FieldName]))
                        {
                            sameRow = false;
                            break;
                        }
                    }
                    if (!sameRow)
                    {
                        destTable.Rows.Add(destRow);
                    }
                }
                if (!sameRow)
                {
                    destRow = destTable.NewRow();
                    rowCount = 0;
                }
                rowCount += 1;
                foreach (FieldInfo field in GroupByFieldInfo)
                {
                    if (field.Aggregate == null)
                    {
                        destRow[field.FieldAlias] = sourceRow[field.FieldName];
                        continue;
                    } 

                    switch (field.Aggregate.ToLower())
                    {
                        case null:
                        case "":
                        case "last":
                            destRow[field.FieldAlias] = sourceRow[field.FieldName];
                            break;
                        case "first":
                            if (rowCount == 1)
                            {
                                destRow[field.FieldAlias] = sourceRow[field.FieldName];
                            }
                            break;
                        case "count":
                            destRow[field.FieldAlias] = rowCount;
                            break;
                        case "sum":
                            destRow[field.FieldAlias] = Add(destRow[field.FieldAlias], sourceRow[field.FieldName]);
                            break;
                        case "max":
                            destRow[field.FieldAlias] = Max(destRow[field.FieldAlias], sourceRow[field.FieldName]);
                            break;
                        case "min":
                            if (rowCount == 1)
                            {
                                destRow[field.FieldAlias] = sourceRow[field.FieldName];
                            }
                            else
                            {
                                destRow[field.FieldAlias] = Min(destRow[field.FieldAlias], sourceRow[field.FieldName]);
                            }
                            break;
                    }
                }
                lastSourceRow = sourceRow;
            }
            if (destRow != null)
            {
                destTable.Rows.Add(destRow);
            }
        }

        private object Min(object a, object b)
        {
            if ((a is DBNull) || (b is DBNull))
            {
                return DBNull.Value;
            }
            if (((IComparable)a).CompareTo(b) == -1)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        private object Max(object a, object b)
        {
            if (a is DBNull)
            {
                return b;
            }
            if (b is DBNull)
            {
                return a;
            }
            if (((IComparable)a).CompareTo(b) == 1)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        private object Add(object a, object b)
        {
            if (a is DBNull)
            {
                return b;
            }
            if (b is DBNull)
            {
                return a;
            }
            return ((decimal)a + (decimal)b);
        }

        private DataTable CreateJoinTable(string tableName, DataTable sourceTable, string fieldList)
        {
            if (fieldList == null)
            {
                return sourceTable.Clone();
            }
            else
            {
                DataTable dt = new DataTable(tableName);
                ParseFieldList(fieldList, true);
                foreach (FieldInfo field in m_FieldInfo)
                {
                    if (field.RelationName == null)
                    {
                        DataColumn dc = sourceTable.Columns[field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                    else
                    {
                        DataColumn dc = sourceTable.ParentRelations[field.RelationName].ParentTable.Columns[field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                }
                if (ds != null)
                {
                    ds.Tables.Add(dt);
                }
                return dt;
            }
        }

        private void InsertJoinInto(DataTable destTable, DataTable sourceTable,
                                    string fieldList, string rowFilter, string sort)
        {
            if (fieldList == null)
            {
                return;
            }
            else
            {
                ParseFieldList(fieldList, true);
                DataRow[] Rows = sourceTable.Select(rowFilter, sort);
                foreach (DataRow SourceRow in Rows)
                {
                    DataRow DestRow = destTable.NewRow();
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        if (Field.RelationName == null)
                        {
                            DestRow[Field.FieldName] = SourceRow[Field.FieldName];
                        }
                        else
                        {
                            DataRow ParentRow = SourceRow.GetParentRow(Field.RelationName);
                            DestRow[Field.FieldName] = ParentRow[Field.FieldName];
                        }
                    }
                    destTable.Rows.Add(DestRow);
                }
            }
        }

        #endregion

        #region SelectDistinct / Distinct

        /**/
        /**/
        /**/
        /// <summary> 
        /// 按照fieldName从sourceTable中选择出不重复的行， 
        /// 相当于select distinct fieldName from sourceTable 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="sourceTable">源DataTable</param> 
        /// <param name="fieldName">列名</param> 
        /// <returns>一个新的不含重复行的DataTable，列只包括fieldName指明的列</returns> 
        public DataTable SelectDistinct(string tableName, DataTable sourceTable, string fieldName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add(fieldName, sourceTable.Columns[fieldName].DataType);

            object lastValue = null;
            foreach (DataRow dr in sourceTable.Select("", fieldName))
            {
                if (lastValue == null || !(ColumnEqual(lastValue, dr[fieldName])))
                {
                    lastValue = dr[fieldName];
                    dt.Rows.Add(new object[] { lastValue });
                }
            }
            if (ds != null && !ds.Tables.Contains(tableName))
            {
                ds.Tables.Add(dt);
            }
            return dt;
        }

        /**/
        /**/
        /**/
        /// <summary> 
        /// 按照fieldName从sourceTable中选择出不重复的行， 
        /// 相当于select distinct fieldName1,fieldName2,,fieldNamen from sourceTable 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="sourceTable">源DataTable</param> 
        /// <param name="fieldNames">列名数组</param> 
        /// <returns>一个新的不含重复行的DataTable，列只包括fieldNames中指明的列</returns> 
        public DataTable SelectDistinct(string tableName, DataTable sourceTable, string[] fieldNames)
        {
            DataTable dt = new DataTable(tableName);
            object[] values = new object[fieldNames.Length];
            string fields = "";
            for (int i = 0; i < fieldNames.Length; i++)
            {
                dt.Columns.Add(fieldNames[i], sourceTable.Columns[fieldNames[i]].DataType);
                fields += fieldNames[i] + ",";
            }
            fields = fields.Remove(fields.Length - 1, 1);
            DataRow lastRow = null;
            foreach (DataRow dr in sourceTable.Select("", fields))
            {
                if (lastRow == null || !(RowEqual(lastRow, dr, dt.Columns)))
                {
                    lastRow = dr;
                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        values[i] = dr[fieldNames[i]];
                    }
                    dt.Rows.Add(values);
                }
            }
            if (ds != null && !ds.Tables.Contains(tableName))
            {
                ds.Tables.Add(dt);
            }
            return dt;
        }

        /**/
        /**/
        /**/
        /// <summary> 
        /// 按照fieldName从sourceTable中选择出不重复的行， 
        /// 并且包含sourceTable中所有的列。 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="sourceTable">源表</param> 
        /// <param name="fieldName">字段</param> 
        /// <returns>一个新的不含重复行的DataTable</returns> 
        public DataTable Distinct(string tableName, DataTable sourceTable, string fieldName)
        {
            DataTable dt = sourceTable.Clone();
            dt.TableName = tableName;

            object lastValue = null;
            foreach (DataRow dr in sourceTable.Select("", fieldName))
            {
                if (lastValue == null || !(ColumnEqual(lastValue, dr[fieldName])))
                {
                    lastValue = dr[fieldName];
                    dt.Rows.Add(dr.ItemArray);
                }
            }
            if (ds != null && !ds.Tables.Contains(tableName))
            {
                ds.Tables.Add(dt);
            }
            return dt;
        }

        /**/
        /**/
        /**/
        /// <summary> 
        /// 按照fieldNames从sourceTable中选择出不重复的行， 
        /// 并且包含sourceTable中所有的列。 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="sourceTable">源表</param> 
        /// <param name="fieldNames">字段</param> 
        /// <returns>一个新的不含重复行的DataTable</returns> 
        public DataTable Distinct(string tableName, DataTable sourceTable, string[] fieldNames)
        {
            DataTable dt = sourceTable.Clone();
            dt.TableName = tableName;
            string fields = "";
            for (int i = 0; i < fieldNames.Length; i++)
            {
                fields += fieldNames[i] + ",";
            }
            fields = fields.Remove(fields.Length - 1, 1);
            DataRow lastRow = null;
            foreach (DataRow dr in sourceTable.Select("", fields))
            {
                if (lastRow == null || !(RowEqual(lastRow, dr, dt.Columns)))
                {
                    lastRow = dr;
                    dt.Rows.Add(dr.ItemArray);
                }
            }
            if (ds != null && !ds.Tables.Contains(tableName))
            {
                ds.Tables.Add(dt);
            }
            return dt;
        }

        #endregion

        #region Select Table Into

        /**/
        /**/
        /**/
        /// <summary> 
        /// 按sort排序，按rowFilter过滤sourceTable， 
        /// 复制fieldList中指明的字段的数据到新DataTable，并返回之 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="sourceTable">源表</param> 
        /// <param name="fieldList">字段列表</param> 
        /// <param name="rowFilter">过滤条件</param> 
        /// <param name="sort">排序</param> 
        /// <returns>新DataTable</returns> 
        public DataTable SelectInto(string tableName, DataTable sourceTable,
                                    string fieldList, string rowFilter, string sort)
        {
            DataTable dt = CreateTable(tableName, sourceTable, fieldList);
            InsertInto(dt, sourceTable, fieldList, rowFilter, sort);
            return dt;
        }

        #endregion

        #region Group By Table

        public DataTable SelectGroupByInto(string tableName, DataTable sourceTable, string fieldList,
                                           string rowFilter, string groupBy)
        {
            DataTable dt = CreateGroupByTable(tableName, sourceTable, fieldList);
            InsertGroupByInto(dt, sourceTable, fieldList, rowFilter, groupBy);
            return dt;
        }

        #endregion

        #region Join Tables

        public DataTable SelectJoinInto(string tableName, DataTable sourceTable, string fieldList, string rowFilter, string sort)
        {
            DataTable dt = CreateJoinTable(tableName, sourceTable, fieldList);
            InsertJoinInto(dt, sourceTable, fieldList, rowFilter, sort);
            return dt;
        }

        #endregion

        #region Create Table

        public DataTable CreateTable(string tableName, string fieldList)
        {
            DataTable dt = new DataTable(tableName);
            DataColumn dc;
            string[] Fields = fieldList.Split(',');
            string[] FieldsParts;
            string Expression;
            foreach (string Field in Fields)
            {
                FieldsParts = Field.Trim().Split(" ".ToCharArray(), 3); // allow for spaces in the expression 
                // add fieldname and datatype 
                if (FieldsParts.Length == 2)
                {
                    dc = dt.Columns.Add(FieldsParts[0].Trim(), Type.GetType("System." + FieldsParts[1].Trim(), true, true));
                    dc.AllowDBNull = true;
                }
                else if (FieldsParts.Length == 3) // add fieldname, datatype, and expression 
                {
                    Expression = FieldsParts[2].Trim();
                    if (Expression.ToUpper() == "REQUIRED")
                    {
                        dc = dt.Columns.Add(FieldsParts[0].Trim(), Type.GetType("System." + FieldsParts[1].Trim(), true, true));
                        dc.AllowDBNull = false;
                    }
                    else
                    {
                        dc = dt.Columns.Add(FieldsParts[0].Trim(), Type.GetType("System." + FieldsParts[1].Trim(), true, true), Expression);
                    }
                }
                else
                {
                    return null;
                }
            }
            if (ds != null)
            {
                ds.Tables.Add(dt);
            }
            return dt;
        }

        public DataTable CreateTable(string tableName, string fieldList, string keyFieldList)
        {
            DataTable dt = CreateTable(tableName, fieldList);
            string[] KeyFields = keyFieldList.Split(',');
            if (KeyFields.Length > 0)
            {
                DataColumn[] KeyFieldColumns = new DataColumn[KeyFields.Length];
                int i;
                for (i = 1; i == KeyFields.Length - 1; ++i)
                {
                    KeyFieldColumns[i] = dt.Columns[KeyFields[i].Trim()];
                }
                dt.PrimaryKey = KeyFieldColumns;
            }
            return dt;
        }

        #endregion

		
		/// <summary>
		/// 循环去除datatable中的空行
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static  DataTable  removeEmpty(DataTable dt) {
			List<DataRow> removelist = new List<DataRow>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				bool rowdataisnull = true;
				for (int j = 0; j < dt.Columns.Count; j++) {

					if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim())) {

						rowdataisnull = false;
					}

				}
				if (rowdataisnull) {
					removelist.Add(dt.Rows[i]);
				}

			}
			for (int i = 0; i < removelist.Count; i++) {
				dt.Rows.Remove(removelist[i]);
			}

			return dt;
		}
    }
}