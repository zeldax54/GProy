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
    public class CurrencyTypesController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: CurrencyTypes
        public ActionResult Index()
        {
            return View(db.CurrencyType.ToList());
        }

        // GET: CurrencyTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = db.CurrencyType.Find(id);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            return View(currencyType);
        }

        // GET: CurrencyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrencyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "currencyTypeId,type")] CurrencyType currencyType)
        {
            if (ModelState.IsValid)
            {
                db.CurrencyType.Add(currencyType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencyType);
        }

        // GET: CurrencyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = db.CurrencyType.Find(id);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            return View(currencyType);
        }

        // POST: CurrencyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "currencyTypeId,type")] CurrencyType currencyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencyType);
        }

        // GET: CurrencyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = db.CurrencyType.Find(id);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            return View(currencyType);
        }

        // POST: CurrencyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyType currencyType = db.CurrencyType.Find(id);
            db.CurrencyType.Remove(currencyType);
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
