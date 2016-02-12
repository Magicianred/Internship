using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.veritabani;

namespace WebApplication1.Controllers
{
    public class AkademisyenIslemController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: AkademisyenIslem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AGiris()
        {
            if (Session["LogedUserID"] != null)
            {
                return RedirectToAction("../StajBilgis/Index");
            }


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AGiris(Akademisyen k)
        {
            if (ModelState.IsValid)
            {
                using (InternshipEntities dc = new InternshipEntities())
                {
                    var v = dc.Akademisyen.Where(a => a.aka_mail.Equals(k.aka_mail) &&
                        a.aka_parola.Equals(k.aka_parola)).FirstOrDefault();
                    //List<Akademisyen> akaliste = db.Akademisyen.ToList();
                    //ViewBag.akaliste = akaliste;
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.aka_tc.ToString();
                        Session["LogedUserName"] = v.aka_ad.ToString();
                        List<Ogrenci> ogrliste = db.Ogrenci.ToList();
                        ViewBag.ogrliste = ogrliste;
                        var ogrenciler = from f in db.Ogrenci

                                         select f;
                        ogrliste = ogrenciler.ToList();
                        ViewBag.ogrliste = ogrliste;





                        List<Akademisyen> akaliste = db.Akademisyen.ToList();
                        ViewBag.akaliste = akaliste;
                        var sorumlular = from f in db.Akademisyen
                                         where f.aka_sorumlumu == true
                                         select f;
                        akaliste = sorumlular.ToList();
                        ViewBag.akaliste = akaliste;

                        return RedirectToAction("../AkademisyenIslem/OnayVeYorum");
                    }
                    else
                    {
                        return RedirectToAction("../AkademisyenIslem/AGiris");
                    }

                }
               
            }
            return View(k.ToString());
        }
        public ActionResult OnayVeYorum()
        {
            if (Session["LogedUserID"] == null)
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                List<Akademisyen> akaliste = db.Akademisyen.ToList();
                ViewBag.akaliste = akaliste;
                var sorumlular = from f in db.Akademisyen
                                 where f.aka_sorumlumu == true
                                 select f;
                akaliste = sorumlular.ToList();
                ViewBag.akaliste = akaliste;
                List<StajBilgi> stajlistesi = db.StajBilgi.ToList();
                ViewBag.stajlar = stajlistesi;
                var stajs = from f in db.StajBilgi
                            select f;

                stajlistesi = stajs.ToList();
                ViewBag.stajlar = stajlistesi;
              
                    
                

                return View();
            }
            
            




        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            if (stajBilgi == null)
            {
                return HttpNotFound();
            }

            
            return View(stajBilgi);
        }

         public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            if (stajBilgi == null)
            {
                return HttpNotFound();
            }
            ViewBag.ogrenci_tc = new SelectList(db.Ogrenci, "ogr_tc", "ogr_ad", stajBilgi.ogrenci_tc);
            return View(stajBilgi);
        }

        // POST: StajBilgis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staj_id,staj_bas,staj_bit,calisma_alani,kullanilan_teknolojiler,yetkili_yorumu,staj_onaylandimi,ogrenci_tc,staj_defteri,firma_adi,firma_tel,firma_adres,firma_fax,firma_mail,departman")] StajBilgi stajBilgi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stajBilgi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OnayVeYorum");
            }
            ViewBag.ogrenci_tc = new SelectList(db.Ogrenci, "ogr_tc", "ogr_ad", stajBilgi.ogrenci_tc);
            return View(stajBilgi);
        }

    }

}