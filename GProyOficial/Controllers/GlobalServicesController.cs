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
    public class GlobalServicesController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: GlobalServices
        public ActionResult Index()
        {
            return View(db.GlobalService.ToList());
        }

        // GET: GlobalServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlobalService globalService = db.GlobalService.Find(id);
            if (globalService == null)
            {
                return HttpNotFound();
            }
            return View(globalService);
        }

        // GET: GlobalServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GlobalServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "globalServiceId,name")] GlobalService globalService)
        {
            if (ModelState.IsValid)
            {
                db.GlobalService.Add(globalService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(globalService);
        }

        // GET: GlobalServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlobalService globalService = db.GlobalService.Find(id);
            if (globalService == null)
            {
                return HttpNotFound();
            }
            return View(globalService);
        }

        // POST: GlobalServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "globalServiceId,name")] GlobalService globalService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(globalService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(globalService);
        }

        // GET: GlobalServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlobalService globalService = db.GlobalService.Find(id);
            if (globalService == null)
            {
                return HttpNotFound();
            }
            return View(globalService);
        }

        // POST: GlobalServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GlobalService globalService = db.GlobalService.Find(id);
            db.GlobalService.Remove(globalService);
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
