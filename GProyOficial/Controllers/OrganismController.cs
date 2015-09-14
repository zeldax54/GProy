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
    public class OrganismController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: Organism
        public ActionResult Index()
        {
            return View(db.Organism.ToList());
        }

        // GET: Organism/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organism organism = db.Organism.Find(id);
            if (organism == null)
            {
                return HttpNotFound();
            }
            return View(organism);
        }

        // GET: Organism/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organism/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "organismId,name")] Organism organism)
        {
            if (ModelState.IsValid)
            {
                db.Organism.Add(organism);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organism);
        }

        // GET: Organism/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organism organism = db.Organism.Find(id);
            if (organism == null)
            {
                return HttpNotFound();
            }
            return View(organism);
        }

        // POST: Organism/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "organismId,name")] Organism organism)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organism).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organism);
        }

        // GET: Organism/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organism organism = db.Organism.Find(id);
            if (organism == null)
            {
                return HttpNotFound();
            }
            return View(organism);
        }

        // POST: Organism/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organism organism = db.Organism.Find(id);
            db.Organism.Remove(organism);
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
