using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new MyExceptionFilterAttribute());  //错误处理	
		    filters.Add(new System.Web.Mvc.AuthorizeAttribute());	//登录控制
			filters.Add(new CompressFilter());	//文件压缩
			filters.Add(new MvcMenuFilter());//权限控制
			
		}
	}
}