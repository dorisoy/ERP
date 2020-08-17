using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Purchase {
	public class PurchaseAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Purchase";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"Purchase_default",
				"Purchase/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
