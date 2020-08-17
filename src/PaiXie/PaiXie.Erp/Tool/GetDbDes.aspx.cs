using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PaiXie.Data;
using FluentData;
using System.Data;
namespace PaiXie.Erp {
	public partial class GetDbDes : System.Web.UI.Page {
		#region MyRegion

		protected void Page_Load(object sender, EventArgs e) {
			string str = "   <table border=1>";
			IDbContext context = Db.GetInstance().Context();
			DataTable dt = context.Sql(@"SELECT table_name  表名,TABLE_COMMENT 表注释 FROM INFORMATION_SCHEMA.TABLES 
 WHERE table_schema = 'erpnet'").QuerySingle<DataTable>();
			#region MyRegion

			for (int z = 0; z < dt.Rows.Count; z++) {
				str += "<tr>";
				str += "<td bgcolor=silver class='medium'>表名</td><td bgcolor=silver class='medium'></td><td bgcolor=silver class='medium'>注释</td></tr>";
				str += "<tr>";
				str += "<td class='normal' valign='top' style ='background-color:yellow;'>" + dt.Rows[z]["表名"].ToString() + "</td>";
				str += "<td class='normal' valign='top'>&nbsp;</td>";
				str += "<td class='normal' valign='top'>" + dt.Rows[z]["表注释"].ToString() + "</td>";
				str += "</tr>";





			}



			for (int z = 0; z < dt.Rows.Count; z++) {
				str += "<tr>";
				str += "<td bgcolor=silver class='medium'>名称</td><td bgcolor=silver class='medium'>类型</td><td bgcolor=silver class='medium'>注释</td></tr>";
				str += "<tr>";
				str += "<td class='normal' valign='top' style ='background-color:yellow;'> <h1>" + dt.Rows[z]["表名"].ToString() + "</h1></td>";
				str += "<td class='normal' valign='top'>&nbsp;</td>";
				str += "<td class='normal' valign='top'> <h1>" + dt.Rows[z]["表注释"].ToString() + "</h1></td>";
				str += "</tr>";
				DataTable dt2 = context.Sql(@"SELECT COLUMN_NAME 列名, DATA_TYPE 字段类型, COLUMN_COMMENT 字段注释  FROM INFORMATION_SCHEMA.COLUMNS  WHERE table_name = '" + dt.Rows[z]["表名"].ToString() + "' AND table_schema = 'erpnet'").QuerySingle<DataTable>();
				#region MyRegion
				for (int z2 = 0; z2 < dt2.Rows.Count; z2++) {

					str += "<tr>";
					str += "<td class='normal' valign='top'>" + dt2.Rows[z2]["列名"].ToString() + "</td>";
					str += "<td class='normal' valign='top'>" + dt2.Rows[z2]["字段类型"].ToString() + "</td>";
					str += "<td class='normal' valign='top'>" + dt2.Rows[z2]["字段注释"].ToString() + "</td>";
					str += "</tr>";

				}

				#endregion





			}
			#endregion


			str += "</table>";
			Response.Write(str);
		}
		
		#endregion
	}
}