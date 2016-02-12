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
    public class AkademisyensController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: Akademisyens
        public ActionResult Index()
        {
            var akademisyen = db.Akademisyen.Include(a => a.Bolum);
            return View(akademisyen.ToList());
        }

        // GET: Akademisyens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Akademisyen akademisyen = db.Akademisyen.Find(id);
            if (akademisyen == null)
            {
                return HttpNotFound();
            }
            return View(akademisyen);
        }

        // GET: Akademisyens/Create
        public ActionResult Create()
        {
            ViewBag.bol_kod = new SelectList(db.Bolum, "bol_kod", "bol_name");
            return View();
        }

        // POST: Akademisyens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aka_id,aka_ad,aka_soyad,aka_tc,aka_sorumlumu,bol_kod,aka_parola")] Akademisyen akademisyen)
        {
            if (!ModelState.IsValid)
            {
                db.Akademisyen.Add(akademisyen);
                db.SaveChanges();
                return RedirectToAction("../AkademisyenIslem/AGiris");
            }

            ViewBag.bol_kod = new SelectList(db.Bolum, "bol_kod", "bol_name", akademisyen.bol_kod);
            return View(akademisyen);
        }

        // GET: Akademisyens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Akademisyen akademisyen = db.Akademisyen.Find(id);
            if (akademisyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.bol_kod = new SelectList(db.Bolum, "bol_kod", "bol_name", akademisyen.bol_kod);
            return View(akademisyen);
        }

        // POST: Akademisyens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aka_id,aka_ad,aka_soyad,aka_tc,aka_sorumlumu,bol_kod,aka_parola")] Akademisyen akademisyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(akademisyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bol_kod = new SelectList(db.Bolum, "bol_kod", "bol_name", akademisyen.bol_kod);
            return View(akademisyen);
        }

        // GET: Akademisyens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Akademisyen akademisyen = db.Akademisyen.Find(id);
            if (akademisyen == null)
            {
                return HttpNotFound();
            }
            return View(akademisyen);
        }

        // POST: Akademisyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Akademisyen akademisyen = db.Akademisyen.Find(id);
            db.Akademisyen.Remove(akademisyen);
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
