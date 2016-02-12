using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.veritabani;

namespace WebApplication1.Controllers
{
    public class StajBilgisController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: StajBilgis
        [HttpGet]
        public ActionResult Index(string searchit,string sec)
        {
            if (Session["LogedUserID"] == null)
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                List<StajBilgi> stajlistesi = db.StajBilgi.ToList();
                List<Bolum> bolumlistesi = db.Bolum.ToList();
                
                ViewBag.stajlar = stajlistesi;
                ViewBag.bolumler = bolumlistesi;
                var stajs = from f in db.StajBilgi
                              select f;
                 
                if (!String.IsNullOrEmpty(sec))
                {

                   stajs = from f in db.StajBilgi
                           where f.Ogrenci.ogr_bolumkod.Equals(sec)
                           select f;
                   if (!String.IsNullOrEmpty(searchit))
                   {
                        stajs = stajs.Where(a => a.kullanilan_teknolojiler.Contains(searchit) || a.calisma_alani.Contains(searchit) || a.firma_adi.Contains(searchit) || a.departman.Contains(searchit));                      
                   }
                    stajlistesi = stajs.ToList();
                    ViewBag.stajlar = stajlistesi;
                }

                if (String.IsNullOrEmpty(sec))
                {
                    if (!String.IsNullOrEmpty(searchit))
                    {
                        stajs = stajs.Where(a => a.kullanilan_teknolojiler.Contains(searchit) || a.calisma_alani.Contains(searchit) || a.firma_adi.Contains(searchit) || a.departman.Contains(searchit));
                    }

                    stajlistesi = stajs.ToList();
                    ViewBag.stajlar = stajlistesi;
                }
                return View(stajs);
             }
            //var stajBilgi = db.StajBilgi.Include(s => s.Ogrenci);
            //return View(stajBilgi.ToList());

            
         }

        // GET: StajBilgis/Details/5
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

        // GET: StajBilgis/Create
        public ActionResult Create()
        {
            if (Session["LogedUserID"] == null)
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                // ViewBag.ogrenci_tc = new SelectList(db.Ogrenci, "ogr_tc", "ogr_ad");
                ViewBag.ogrenci_tc = @Session["LogedUserID"].ToString();
                return View();
            }
        }

        // POST: StajBilgis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        [ValidateAntiForgeryToken, HttpPost]   
       // [Authorize]
        public ActionResult Create([Bind(Include = "staj_id,staj_bas,staj_bit,calisma_alani,kullanilan_teknolojiler,yetkili_yorumu,staj_onaylandimi,ogrenci_tc,staj_defteri,firma_adi,firma_tel,firma_adres,firma_fax,firma_mail,departman")] StajBilgi stajBilgi,HttpPostedFileBase yuklenecekdosya)
        {
            int a = 10;
            if (Session["LogedUserID"] == null)
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (yuklenecekdosya != null)
                    {
                        var yuklemeyeri = "";
                        string extension = System.IO.Path.GetExtension(yuklenecekdosya.FileName);
                        if (extension == ".doc")
                        {
                            string dosyayolu = Path.GetFileName(yuklenecekdosya.FileName);
                            yuklemeyeri = Path.Combine(Server.MapPath("~/Defterler"), dosyayolu);
                            yuklenecekdosya.SaveAs(yuklemeyeri);
                            stajBilgi.staj_defteri = yuklemeyeri;
                            stajBilgi.staj_onaylandimi = false;
                            stajBilgi.yetkili_yorumu = "Bir Yorum Giriniz";
                            stajBilgi.ogrenci_tc = @Session["LogedUserID"].ToString();

                            db.StajBilgi.Add(stajBilgi);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else {
                            return RedirectToAction("Create");

                        }

                    }
                    else {
                        return RedirectToAction("Create");
                    }


                }
                return RedirectToAction("Index");
            }
        }
        // GET: StajBilgis/Edit/5
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
                return RedirectToAction("Index");
            }
            ViewBag.ogrenci_tc = new SelectList(db.Ogrenci, "ogr_tc", "ogr_ad", stajBilgi.ogrenci_tc);
            return View(stajBilgi);
        }

        // GET: StajBilgis/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: StajBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            db.StajBilgi.Remove(stajBilgi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
         
    }
}
