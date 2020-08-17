using System;
using System.Collections.Generic;
using System.Linq;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Api.Bll;
using PaiXie.Utils;
using PaiXie.Core;
using PaiXie.Cache;
using System.Data;
using PaiXie.Excel;
using Newtonsoft.Json;
using System.Threading;
namespace PaiXie.Erp {
	public partial class mytest : System.Web.UI.Page {

		//private void ss(string sql, params Object[] objects) {
		//	List<student> products = Db.GetInstance().Context().Sql(sql, objects)
		//	.QueryMany<student>();
		//}

		//private string  sss() {
		//	for (int i = 0; i < N[0]; i++) {


		//		M[i].ToString();
		//	}
		//}
			private void thread(object obj) {
			DownProductsParam param = obj as DownProductsParam;
			ShopProductsManager.DownLoad(param);
		}
		protected void Page_Load(object sender, EventArgs e) {
			Response.Write( CacheHelper.Get<string>("a"));
			return;
			//	public int DownProducts(int shopID, int productsStatus) {
			DownProductsParam param = new DownProductsParam();
			param.TaskID = Guid.NewGuid().ToString();
			param.ShopID = 1;
			param.ProductsStatus = 0;
			param.UserCode = "111"; //FormsAuth.GetUserCode();
			ThreadPool.QueueUserWorkItem(new WaitCallback(thread), param);
			//return 1;
		//}


			return;


			//ShopStockUpdate aa = new ShopStockUpdate();
			//aa.TaskID = Guid.NewGuid().ToString();
			//aa.ShopID = 1;
			//aa.UserCode = "admin";
			//ShopProductsManager.ShopStockUpdate(aa);

			return;
			
			PlanLog.WriteLog("aa","aa");

			return;
			Object[] objects1 = new Object[1];
			objects1[0] = "admin";
			Response.Write(SysuserService.Getobject("select Name from sys_user where Code =@0", null, objects1).ToString());
			return;


			////string [] M = { "a", "b", "c"};
			////int[] N = { 5, 2, 5 };
			////for (int i = 0; i < N.Length; i++) {

			////	for (int j = 0; j < N[i]; j++) {
			////		M[0]
					

			////	}
				
			////}



			////	return;
		//	Response.Write(SyslogService.DelByID(11).ToString());
		//	return;
		//	Syslog Syslog = new Syslog();
		//	Syslog.UserCode = FormsAuth.GetUserCode();
		//	Syslog.UserName = FormsAuth.GetUserName();
		//	Syslog.Position = "api/mms/send";
		//	Syslog.Target = "菜单管理";
		//	Syslog.ButtonName = "修改";
		//	Syslog.OldMessage = JsonConvert.SerializeObject(new Sysuser(), Formatting.Indented);
		//	Syslog.Message = JsonConvert.SerializeObject(new Sysuser(), Formatting.Indented);
		//	Syslog.Type = (int)BillType.CGC;
		//	Syslog.Part1 = "";
		//	Syslog.Part2 = "";
		//	Syslog.Part3 = "";
		//	Syslog.Date = System.DateTime.Now;
		//	Syslog.ModeType =(int)ProjectType.管理端;
		//	Syslog.WarehouseCode = "0";
		//	Response.Write(SyslogService.Add(Syslog).ToString()); 
		////	Response.Write( new Users().IsAuthJs(SysbuttonList.b002.ToString()).ToString());
			Object[] objects = new Object[1];
			objects[0] = "admin";
			Response.Write(SysuserService.GetQuerySingle("select * from sys_user where Code =@0", null, objects).Name);

			//objects[1] = "wqwqwqqqqqqqqqqqqqqqqa";
			//objects[1] = 11;
			//ss("select * from student where SstuNmae =  @ProductId1", objects);



			////DateTime beignTime = DateTime.Now;
			////string format = "SortID|序号;ProductsTitle|商品名称;ProductsCode|商品货号;ProductsSkuCode|Sku码;Color|颜色;Sizes|规格;Num|数量";
			////string reportName = "商品Sku";
			////DataTable dt = new DataTable();
			////DataColumn column1 = new DataColumn();
			////column1.ColumnName = "SortID";
			////column1.DataType = typeof(int);
			////dt.Columns.Add(column1);

			////DataColumn column2 = new DataColumn();
			////column2.ColumnName = "ProductsTitle";
			////column2.DataType = typeof(string);
			////dt.Columns.Add(column2);

			////DataColumn column3 = new DataColumn();
			////column3.ColumnName = "ProductsSkuCode";
			////column3.DataType = typeof(string);
			////dt.Columns.Add(column3);

			////DataColumn column4 = new DataColumn();
			////column4.ColumnName = "ProductsCode";
			////column4.DataType = typeof(string);
			////dt.Columns.Add(column4);

			////DataColumn column5 = new DataColumn();
			////column5.ColumnName = "Color";
			////column5.DataType = typeof(string);
			////dt.Columns.Add(column5);

			////DataColumn column6 = new DataColumn();
			////column6.ColumnName = "Sizes";
			////column6.DataType = typeof(string);
			////dt.Columns.Add(column6);

			////DataColumn column7 = new DataColumn();
			////column7.ColumnName = "Num";
			////column7.DataType = typeof(string);
			////dt.Columns.Add(column7);

			////for (int i = 0; i < 200000; i++) {
			////	DataRow newDr = dt.NewRow();
			////	int index=i + 1;
			////	newDr["SortID"] = index;
			////	newDr["ProductsTitle"] = "商品名称" + index;
			////	newDr["ProductsCode"] = "商品货号" + index;
			////	newDr["ProductsSkuCode"] = "条码" + index;
			////	newDr["Color"] = "颜色" + index;
			////	newDr["Sizes"] = "尺码" + index;
			////	newDr["Num"] = "数量" + index;
			////	dt.Rows.Add(newDr);
			////}
			////ExcelHelp.exportMin.GenerateXlsFormat(format, @"C:\Users\Administrator\Desktop\" + Guid.NewGuid() + ".xls", dt, reportName);
			////DateTime endTime = DateTime.Now;
			////string useTime = DateDiff(beignTime, endTime);
			////Response.Write("导出成功用时：" + useTime + "<br/><br/><br/>");
			//CacheHelper.Add("aaa","aaa");
			//	PlanLog.WriteLog("ddsds当时的第三方v", LogType.Error.ToString());
			//PlanLog.WriteLog("ddsds当时的第三方v", LogType.General.ToString());

			//			int numberOfProducts = Db.GetInstance().Context().Sql(@"select count(*)
			//						from tt").QuerySingle<int>();
			//			Response.Write(numberOfProducts.ToString());
			//select * from orders order by orderid limit 10,20
			//Response.Write(DateTime.Now.ToString());
			//for (int i = 0; i < 1000; i++) {
				//Sys.GetBillNo("XSC");
			//	Sys.GetBillNo("XSC");
				//Sys.GetBillNo(string.Empty);
			//	Sys.GetBillNo(string.Empty);
			//}
			//Response.Write(DateTime.Now.ToString());
			//bool IsSingleWarehouse = SysConfig.IsSingleWarehouse;
			//DateTime InstallTime = SysConfig.InstallTime;
			//string SystemTitle = SysConfig.SystemTitle;
			//string SystemVersion = SysConfig.SystemVersion;
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



		protected void del_Click(object sender, EventArgs e) {
			string strsql = string.Format("delete  from testtable where id={0}", 3);
			//Response.Write(testtableService.Del(strsql).ToString());


		}
		protected void update_Click(object sender, EventArgs e) {
			//实体方式（建议）
			string strsql = string.Format("SELECT 	* FROM testtable WHERE id={0}", 2);
		//	testtable obj = testtableService.GetQuerySingle(strsql);
			obj.name = "fdddddddddddddddddddd";
		//	Response.Write(testtableService.Update(obj).ToString());


			//sql 方式
			// string strsql = string.Format("update   testtable set name='{0}' where id={1}","47484554",1);
			//Response.Write(	testtableService.Update(strsql).ToString());


		}
		protected void getmodel_Click(object sender, EventArgs e) {
			string strsql = string.Format("SELECT 	* FROM testtable WHERE id={0}", 10);
		//	Response.Write(testtableService.GetQuerySingle(strsql).name);


		}
		protected void getlist_Click(object sender, EventArgs e) {


			//string strsql = string.Format("SELECT 	* FROM testtable WHERE id>10");
			//Response.Write(testtableService.GetQueryMany(strsql).Count().ToString());
			//外键
			string strsql = string.Format("SELECT a.*,b.ClassName  FROM student a LEFT JOIN classs b ON a.ClassId=b.ID ");
			//Response.Write(studentService.GetQueryManyList(strsql)[0].ClassName);

		}

		protected void getcount_Click(object sender, EventArgs e) {
			string strsql = string.Format("SELECT 	count(*) FROM testtable ");
		//	Response.Write(testtableService.GetCount(strsql).ToString());


		}




		protected void add_Click(object sender, EventArgs e) {
			//testtable testtable = new testtable();
			testtable.name = "001";
			testtable.creTime = System.DateTime.Now;

			//Response.Write(testtableService.Add(testtable).ToString());

		}
		/// <summary>
		/// 分页
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Button2_Click(object sender, EventArgs e) {
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "";
			data.From = "testtable";
			data.Select = "*";
			data.WhereSql = "";
			data.PagingCurrentPage = Convert.ToInt32(TextBox1.Text);
			data.PagingItemsPerPage = ZConfig.GetConfigInt("pagesize");
			int total = 0;
		//	List<testtable> list = testtableService.GetQueryManyForPage(data, out total);

			Response.Write(total.ToString());
			//Response.Write(list.Count().ToString());

		}


		protected void Button8_Click(object sender, EventArgs e) {
			string App_Key = "3104908";
			string App_Secret = "9d542e08daca4a63fffb8dd4c783fb59";
			string api_signkey = "b77cf45c442fad5fcdac947305db7831";
			Dictionary<string, string> paramDictionary = new Dictionary<string, string>();
			paramDictionary.Add("method", "GetSystemTime");
			paramDictionary.Add("api_key", App_Key);
			paramDictionary.Add("api_secret", App_Secret);
			paramDictionary.Add("api_signkey", api_signkey);
			//paramDictionary.Add("start_date", sel_Time);
			//微小店测试地址
			string url = ZConfig.GetConfigString("WeiXiaoDian_Url");
			string strdata = PXinterface.GetPost(url, paramDictionary);
			Response.Write(strdata);
		}

	
	}
}