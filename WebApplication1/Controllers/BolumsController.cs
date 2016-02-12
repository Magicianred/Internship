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
    public class BolumsController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: Bolums
        public ActionResult Index()
        {
            var bolum = db.Bolum.Include(b => b.Fakulte);
            return View(bolum.ToList());
        }

        // GET: Bolums/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolum.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // GET: Bolums/Create
        public ActionResult Create()
        {
            ViewBag.fak_id = new SelectList(db.Fakulte, "fak_id", "fak_name");
            return View();
        }

        // POST: Bolums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bol_id,bol_name,bol_adres,bol_tel,bol_fax,bol_kod,bol_url,fak_id")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Bolum.Add(bolum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fak_id = new SelectList(db.Fakulte, "fak_id", "fak_name", bolum.fak_id);
            return View(bolum);
        }

        // GET: Bolums/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolum.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            ViewBag.fak_id = new SelectList(db.Fakulte, "fak_id", "fak_name", bolum.fak_id);
            return View(bolum);
        }

        // POST: Bolums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bol_id,bol_name,bol_adres,bol_tel,bol_fax,bol_kod,bol_url,fak_id")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bolum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fak_id = new SelectList(db.Fakulte, "fak_id", "fak_name", bolum.fak_id);
            return View(bolum);
        }

        // GET: Bolums/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bolum bolum = db.Bolum.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: Bolums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Bolum bolum = db.Bolum.Find(id);
            db.Bolum.Remove(bolum);
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
