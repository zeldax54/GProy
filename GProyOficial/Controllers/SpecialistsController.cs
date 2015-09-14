using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GProyOficial.Models;

namespace GProyOficial.Controllers
{
    public class SpecialistsController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: Specialists
        public ActionResult Index()
        {
            var specialist = db.Specialist.Include(s => s.Area);
            return View(specialist.ToList());
        }

        // GET: Specialists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialist specialist = db.Specialist.Find(id);
            if (specialist == null)
            {
                return HttpNotFound();
            }
            return View(specialist);
        }

        // GET: Specialists/Create
        public ActionResult Create()
        {
            ViewBag.areaId = new SelectList(db.Area, "areaId", "name");
            return View();
        }

        // POST: Specialists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "specialistId,name,areaId,ci")] Specialist specialist)
        {
            if (ModelState.IsValid)
            {
                db.Specialist.Add(specialist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.areaId = new SelectList(db.Area, "areaId", "name", specialist.areaId);
            return View(specialist);
        }

        // GET: Specialists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialist specialist = db.Specialist.Find(id);
            if (specialist == null)
            {
                return HttpNotFound();
            }
            ViewBag.areaId = new SelectList(db.Area, "areaId", "name", specialist.areaId);
            return View(specialist);
        }

        // POST: Specialists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "specialistId,name,areaId,ci")] Specialist specialist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.areaId = new SelectList(db.Area, "areaId", "name", specialist.areaId);
            return View(specialist);
        }

        // GET: Specialists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialist specialist = db.Specialist.Find(id);
            if (specialist == null)
            {
                return HttpNotFound();
            }
            return View(specialist);
        }

        // POST: Specialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialist specialist = db.Specialist.Find(id);
            db.Specialist.Remove(specialist);
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
