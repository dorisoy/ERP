using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SchemaExplorer;
using CodeSmith.Engine;

namespace CodeSmith.MyTemplates
{
    public class TableObjectTemplate : CodeTemplate
    {
        public string GetCamelCaseName(string value)
        {
			value = value.Replace(" ", "").Replace("_", "").Replace("-", "");
            return value.Substring(0, 1).ToLower() + value.Substring(1).Replace(" ", "");
        }

        public string GetPascalCaseName(string value)
        {
			value = value.Replace(" ", "").Replace("_", "").Replace("-", "");
            return value.Substring(0, 1).ToUpper() + value.Substring(1).Replace(" ", "");
        }
        
        public string ConvertCSHarpNullableVariableType(ColumnSchema column)
        {
            if (column.Name.EndsWith("TypeCode")) return column.Name;

            switch (column.DataType)
            {
                case DbType.AnsiString: return "AppConst.StringNull";
                case DbType.AnsiStringFixedLength: return "AppConst.StringNull";
                case DbType.Binary: return "byte[]";
                case DbType.Boolean: return "AppConst.BoolNull";
                case DbType.Byte: return "AppConst.IntNull";
                case DbType.Currency: return "AppConst.DecimalNull";
                case DbType.Date: return "AppConst.DateTimeNull";
                case DbType.DateTime: return "AppConst.DateTimeNull";
                case DbType.Decimal: return "AppConst.DecimalNull";
                case DbType.Double: return "AppConst.DoubleNull";
                case DbType.Guid: return "AppConst.GuidNull";
                case DbType.Int16: return "AppConst.ShortNull";
                case DbType.Int32: return "AppConst.IntNull";
                case DbType.Int64: return "AppConst.DoubleNull";
                case DbType.Object: return "object";
                case DbType.SByte: return "sbyte";
                case DbType.Single: return "AppConst.DoubleNull";
                case DbType.String: return "AppConst.StringNull";
                case DbType.StringFixedLength: return "string";
                case DbType.Time: return "TimeSpan";
                case DbType.UInt16: return "ushort";
                case DbType.UInt32: return "uint";
                case DbType.UInt64: return "ulong";
                case DbType.VarNumeric: return "AppConst.DecimalNull";
                default:
                    {
                        return "__UNKNOWN__" + column.NativeType;
                    }
            }
        }

        public string GetCSharpNullableVariableType(ColumnSchema column)
        {
            if (column.Name.EndsWith("TypeCode")) return column.Name;

            switch (column.DataType)
            {
                case DbType.AnsiString: return "string";
                case DbType.AnsiStringFixedLength: return "string";
                case DbType.Binary: return "byte[]";
                case DbType.Boolean: return "bool";
                case DbType.Byte: return "int";
                case DbType.Currency: return "decimal";
                case DbType.Date: return "DateTime";
                case DbType.DateTime: return "DateTime";
                case DbType.Decimal: return "decimal";
                case DbType.Double: return "double";
                case DbType.Guid: return "Guid";
                case DbType.Int16: return "short";
                case DbType.Int32: return "int";
                case DbType.Int64: return "long";
                case DbType.Object: return "object";
                case DbType.SByte: return "sbyte";
                case DbType.Single: return "float";
                case DbType.String: return "string";
                case DbType.StringFixedLength: return "string";
                case DbType.Time: return "TimeSpan";
                case DbType.UInt16: return "ushort";
                case DbType.UInt32: return "int";
                case DbType.UInt64: return "long";
                case DbType.VarNumeric: return "decimal";
                default:
                    {
                        return "__UNKNOWN__" + column.NativeType;
                    }
            }
        }

        public string GetCSharpConvertFill(DbType type, string fillVal)
        {
            switch (type)
            {
                case DbType.AnsiString: return "Convert.ToString(" + fillVal + ")";
                case DbType.AnsiStringFixedLength: return "Convert.ToString(" + fillVal + ")";
                case DbType.Binary: return "(byte[])" + fillVal;
                case DbType.Boolean: return "Convert.ToBoolean(" + fillVal + ")";
                case DbType.Byte: return "Convert.ToInt32(" + fillVal + ")";
                case DbType.Currency: return "Convert.ToDecimal(" + fillVal + ")";
                case DbType.Date: return "Convert.ToDateTime(" + fillVal + ")";
                case DbType.DateTime: return "Convert.ToDateTime(" + fillVal + ")";
                case DbType.Decimal: return "Convert.ToDecimal(" + fillVal + ")";
                case DbType.Double: return "Convert.ToDouble(" + fillVal + ")";
                case DbType.Guid: return "new Guid(" + fillVal + ".ToString())";
                case DbType.Int16: return "Convert.ToInt16(" + fillVal + ")";
                case DbType.Int32: return "Convert.ToInt32(" + fillVal + ")";
                case DbType.Int64: return "Convert.ToInt64(" + fillVal + ")";
                case DbType.Object: return fillVal;
                case DbType.SByte: return "Convert.ToSByte(" + fillVal + ")";
                case DbType.Single: return "Convert.ToSingle(" + fillVal + ")";
                case DbType.String: return fillVal + ".ToString()";
                case DbType.StringFixedLength: return "Convert.ToString(" + fillVal + ")";
                case DbType.Time: return "Convert.ToDateTime(" + fillVal + ")";
                case DbType.UInt16: return "Convert.ToUInt16(" + fillVal + ")";
                case DbType.UInt32: return "Convert.ToUInt32(" + fillVal + ")";
                case DbType.UInt64: return "Convert.ToUInt64(" + fillVal + ")";
                case DbType.VarNumeric: return "Convert.ToDecimal(" + fillVal + ")";
                default:
                    {
                        return "__UNKNOWN__";
                    }
            }
        }

        public string GetClassName(TableSchema table, bool isComplex)
        {
            string ret = GetPascalCaseName(table.Name);
            if (!isComplex)
                return ret;
            int i = ret.IndexOf("_") + 1;
            if (ret.EndsWith("ies"))
            {
                return ret.Substring(i, ret.Length - 3) + "y";
            }
            else if (ret.EndsWith("s"))
            {
                return ret.Substring(i, ret.Length - 1);
            }
            else
            {
                return ret.Substring(i, ret.Length - i);
            }
        }

        public bool HasParent(TableSchema table)
        {
            return table.ForeignKeyColumns.Count > 0;
        }

        public bool HasChildren(TableSchema table)
        {
            return GetChildTables(table).Count > 0;
        }

        public List<TableSchema> GetChildTables(TableSchema table)
        {
            List<TableSchema> tables = new List<TableSchema>();
            for (int i = 0; i < table.Database.Tables.Count; i++)
            {
                TableSchema otbl = table.Database.Tables[i];
                for (int j = 0; j < otbl.ForeignKeys.Count; j++)
                {
                    if (otbl.ForeignKeys[j].PrimaryKeyTable.Name == table.Name)
                    {
                        tables.Add(otbl);
                        break;
                    }
                }
            }
            return tables;
        }
    }
}