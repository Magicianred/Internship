using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.veritabani;

namespace WebApplication1.Controllers
{
    public class OgrenciIslemController : Controller
    {
        // GET: OgrenciIslem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OGiris()
        {

            if (Session["LogedUserID"] != null)
            {
                return RedirectToAction("../StajBilgis/Index");
            }


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OGiris(Ogrenci k)
        {
            if (!ModelState.IsValid)
            {
                using (InternshipEntities dc = new InternshipEntities())
                {
                    var v = dc.Ogrenci.Where(a => a.ogr_mail.Equals(k.ogr_mail) &&
                        a.ogr_parola.Equals(k.ogr_parola)).FirstOrDefault();

                    if (v != null)
                    {
                        Session["LogedUserID"] = v.ogr_tc.ToString();
                        Session["LogedUserName"] = v.ogr_ad.ToString();
                        return RedirectToAction("../StajBilgis/Index");
                    }
                    else
                    {
                        return RedirectToAction("../OgrenciIslem/OGiris");
                    }

                }
            }
            return View(k);
        }

    }
}