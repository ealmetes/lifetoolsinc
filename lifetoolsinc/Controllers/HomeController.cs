using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
           if(Request.IsAuthenticated==false){
           
                return RedirectToAction("Index", "Account");
            }
           return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}