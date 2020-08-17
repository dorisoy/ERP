using System.Web.Mvc;

namespace PaiXie.Erp.Areas.OrderRefund {
    public class OrderRefundAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "OrderRefund";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "OrderRefund_default",
                "OrderRefund/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
