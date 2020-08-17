using System.Web.Mvc;

namespace PaiXie.Erp.Areas.SysWarehouse {
	public class SysWarehouseAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "SysWarehouse";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"SysWarehouse_default",
				"SysWarehouse/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
