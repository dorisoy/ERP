using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PaiXie.Erp
{
	[AllowAnonymous]
	
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Error() {
			return View();
		}
    }
}