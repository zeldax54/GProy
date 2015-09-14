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
    public class InvoiceProjectDetailsController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: InvoiceProjectDetails
        public ActionResult Index()
        {
            var invoiceProjectDetails = db.InvoiceProjectDetails.Include(i => i.Invoice).Include(i => i.ProjectDetailsSpecialist);
            return View(invoiceProjectDetails.ToList());
        }

        // GET: InvoiceProjectDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceProjectDetails invoiceProjectDetails = db.InvoiceProjectDetails.Find(id);
            if (invoiceProjectDetails == null)
            {
                return HttpNotFound();
            }
            return View(invoiceProjectDetails);
        }

        // GET: InvoiceProjectDetails/Create
        public ActionResult Create()
        {
            ViewBag.invoiceId = new SelectList(db.Invoice, "invoiceId", "invoiceNum");
            ViewBag.projDetailsSpecialistId = new SelectList(db.ProjectDetailsSpecialist, "projDetailsSpecialistId", "projDetailsSpecialistId");
            return View();
        }

        // POST: InvoiceProjectDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoiceId,amount,invoiceProjectDetailsId,projDetailsSpecialistId")] InvoiceProjectDetails invoiceProjectDetails)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceProjectDetails.Add(invoiceProjectDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invoiceId = new SelectList(db.Invoice, "invoiceId", "invoiceNum", invoiceProjectDetails.invoiceId);
            ViewBag.projDetailsSpecialistId = new SelectList(db.ProjectDetailsSpecialist, "projDetailsSpecialistId", "projDetailsSpecialistId", invoiceProjectDetails.projDetailsSpecialistId);
            return View(invoiceProjectDetails);
        }

        // GET: InvoiceProjectDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceProjectDetails invoiceProjectDetails = db.InvoiceProjectDetails.Find(id);
            if (invoiceProjectDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.invoiceId = new SelectList(db.Invoice, "invoiceId", "invoiceNum", invoiceProjectDetails.invoiceId);
            ViewBag.projDetailsSpecialistId = new SelectList(db.ProjectDetailsSpecialist, "projDetailsSpecialistId", "projDetailsSpecialistId", invoiceProjectDetails.projDetailsSpecialistId);
            return View(invoiceProjectDetails);
        }

        // POST: InvoiceProjectDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoiceId,amount,invoiceProjectDetailsId,projDetailsSpecialistId")] InvoiceProjectDetails invoiceProjectDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceProjectDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invoiceId = new SelectList(db.Invoice, "invoiceId", "invoiceNum", invoiceProjectDetails.invoiceId);
            ViewBag.projDetailsSpecialistId = new SelectList(db.ProjectDetailsSpecialist, "projDetailsSpecialistId", "projDetailsSpecialistId", invoiceProjectDetails.projDetailsSpecialistId);
            return View(invoiceProjectDetails);
        }

        // GET: InvoiceProjectDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceProjectDetails invoiceProjectDetails = db.InvoiceProjectDetails.Find(id);
            if (invoiceProjectDetails == null)
            {
                return HttpNotFound();
            }
            return View(invoiceProjectDetails);
        }

        // POST: InvoiceProjectDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceProjectDetails invoiceProjectDetails = db.InvoiceProjectDetails.Find(id);
            db.InvoiceProjectDetails.Remove(invoiceProjectDetails);
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
