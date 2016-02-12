using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.veritabani;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        public ActionResult Index()
        {
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
        public ActionResult cikis()
        {
            Session.Abandon();
            Session["LogedUserID"] = null;
            return RedirectToAction("Index");
        }


    }
    
}