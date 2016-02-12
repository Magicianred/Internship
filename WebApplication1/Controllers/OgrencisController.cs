using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.veritabani;

namespace WebApplication1.Controllers
{
    public class OgrencisController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: Ogrencis
        public ActionResult Index()
        {
            var ogrenci = db.Ogrenci.Include(o => o.Bolum);
            return View(ogrenci.ToList());
        }

        // GET: Ogrencis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Create
        public ActionResult Create()
        {
            ViewBag.ogr_bolumkod = new SelectList(db.Bolum, "bol_kod", "bol_name");
            return View();
        }

        // POST: Ogrencis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ogr_id,ogr_tc,ogr_ad,ogr_soyad,ogr_no,ogr_sigorta,ogr_adres,ogr_tel,ogr_mail,ogr_dogum,ogr_bolumkod,ogr_parola")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                db.Ogrenci.Add(ogrenci);
                db.SaveChanges();
                return RedirectToAction("../OgrenciIslem/OGiris");
            }

            ViewBag.ogr_bolumkod = new SelectList(db.Bolum, "bol_kod", "bol_name", ogrenci.ogr_bolumkod);
            return View(ogrenci);
        }

        // GET: Ogrencis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            ViewBag.ogr_bolumkod = new SelectList(db.Bolum, "bol_kod", "bol_name", ogrenci.ogr_bolumkod);
            return View(ogrenci);
        }

        // POST: Ogrencis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ogr_id,ogr_tc,ogr_ad,ogr_soyad,ogr_no,ogr_sigorta,ogr_adres,ogr_tel,ogr_mail,ogr_dogum,ogr_bolumkod,ogr_parola")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ogr_bolumkod = new SelectList(db.Bolum, "bol_kod", "bol_name", ogrenci.ogr_bolumkod);
            return View(ogrenci);
        }

        // GET: Ogrencis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci);
        }

        // POST: Ogrencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            db.Ogrenci.Remove(ogrenci);
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
