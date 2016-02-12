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
    public class FakultesController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: Fakultes
        public ActionResult Index()
        {
            return View(db.Fakulte.ToList());
        }

        // GET: Fakultes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fakulte fakulte = db.Fakulte.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // GET: Fakultes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fakultes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fak_id,fak_name,fak_adres,fak_tel,fak_fax")] Fakulte fakulte)
        {
            if (ModelState.IsValid)
            {
                db.Fakulte.Add(fakulte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fakulte);
        }

        // GET: Fakultes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fakulte fakulte = db.Fakulte.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // POST: Fakultes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fak_id,fak_name,fak_adres,fak_tel,fak_fax")] Fakulte fakulte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakulte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fakulte);
        }

        // GET: Fakultes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fakulte fakulte = db.Fakulte.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // POST: Fakultes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fakulte fakulte = db.Fakulte.Find(id);
            db.Fakulte.Remove(fakulte);
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
