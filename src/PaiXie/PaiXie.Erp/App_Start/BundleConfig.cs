using System.Web;
using System.Web.Optimization;

namespace PaiXie.Erp {
	public class BundleConfig {
		// 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
			// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css"));
		    StyleBundle mystyle = new StyleBundle("~/FlyElephant/Style");
			mystyle.Include("~/Content/jquery-easyui/themes/gray/easyui.css", "~/Content/jquery-easyui/themes/icon.css");	
			bundles.Add(mystyle);


			//登录
			bundles.Add(new ScriptBundle("~/Scripts/ViewJs/js").Include("~/Scripts/ViewJs/Login.js"));


			//主页
			bundles.Add(new ScriptBundle("~/Scripts/hjs").Include("~/Scripts/jquery-ui-jqLoding.js",
				"~/Scripts/dictType.js",
				"~/Scripts/easyuiExt.js",
				"~/Scripts/showTip.js"
				));
			bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/icon").Include("~/Content/jquery-easyui/themes/icon.css"
				));
			bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/gray/easyui").Include("~/Content/jquery-easyui/themes/gray/easyui.css"
				));
			bundles.Add(new ScriptBundle("~/Content/jquery-easyui/locale/easyui-lang-zh_CN").Include(			
				"~/Content/jquery-easyui/locale/easyui-lang-zh_CN.js"
			));
			bundles.Add(new StyleBundle("~/Content/css/default").Include("~/Content/css/default.css"
				));
			// layout 
				bundles.Add(new ScriptBundle("~/Content/jquery-easyui/extvalidator").Include("~/Content/jquery-easyui/extvalidator.js"));
			bundles.Add(new StyleBundle("~/Content/jquery-easyui/demo/demo").Include("~/Content/jquery-easyui/demo/demo.css"
				));
				bundles.Add(new StyleBundle("~/Content/css/list").Include("~/Content/css/list.css"
				));


				bundles.Add(new ScriptBundle("~/Scripts/layout").Include("~/Scripts/AjaxExport.js",
					"~/Scripts/jquery.msgbox.js"
					));
		

		
			

		}
	}
}