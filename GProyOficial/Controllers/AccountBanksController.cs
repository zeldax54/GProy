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
    public class AccountBanksController : Controller
    {
        private GProyEntities db = new GProyEntities();
        
       // GET: AccountBanks
        public ActionResult Index()
        {
       
            var accountBank = db.AccountBank.Include(a => a.Bank).Include(a => a.Client).Include(a => a.CurrencyType);
            return View(accountBank.ToList()); 
        }

        // GET: AccountBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountBank accountBank = db.AccountBank.Find(id);
            if (accountBank == null)
            {
                return HttpNotFound();
            }
            return View(accountBank);
        }

        // GET: AccountBanks/Create
        public ActionResult Create()
        {
            ViewBag.bankId = new SelectList(db.Bank, "bankId", "name");
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name");
            ViewBag.currencyTypeId = new SelectList(db.CurrencyType, "currencyTypeId", "type");
            return View();
        }

        // POST: AccountBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientId,bankId,currencyTypeId,accountNumber,titular")] AccountBank accountBank)
        {
            if (ModelState.IsValid)
            {
                db.AccountBank.Add(accountBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bankId = new SelectList(db.Bank, "bankId", "name", accountBank.bankId);
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name", accountBank.clientId);
            ViewBag.currencyTypeId = new SelectList(db.CurrencyType, "currencyTypeId", "type", accountBank.currencyTypeId);
            return View(accountBank);
        }

        // GET: AccountBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountBank accountBank = db.AccountBank.Find(id);
            if (accountBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.bankId = new SelectList(db.Bank, "bankId", "name", accountBank.bankId);
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name", accountBank.clientId);
            ViewBag.currencyTypeId = new SelectList(db.CurrencyType, "currencyTypeId", "type", accountBank.currencyTypeId);
            return View(accountBank);
        }

        // POST: AccountBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientId,bankId,currencyTypeId,accountBank1,titular")] AccountBank accountBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bankId = new SelectList(db.Bank, "bankId", "name", accountBank.bankId);
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name", accountBank.clientId);
            ViewBag.currencyTypeId = new SelectList(db.CurrencyType, "currencyTypeId", "type", accountBank.currencyTypeId);
            return View(accountBank);
        }

        // GET: AccountBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountBank accountBank = db.AccountBank.Find(id);
            if (accountBank == null)
            {
                return HttpNotFound();
            }
            return View(accountBank);
        }

        // POST: AccountBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountBank accountBank = db.AccountBank.Find(id);
            db.AccountBank.Remove(accountBank);
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
