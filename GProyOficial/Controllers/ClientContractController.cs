using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GProyOficial.Models;
using GProyOficial.Models.ViewModel;

namespace GProyOficial.Controllers
{
    public class ClientContractController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: Contracts
        public ActionResult Index(int? id)
        {
            ContractList spList = new ContractList();
            var contract = db.Contract.Include(s => s.Client).Where(s => s.clientId == id);
            spList.Contracts = contract.ToList();
            spList.ClientId = id;
            return View(spList);
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientId = contract.clientId;
            
            ViewBag.stateC = db.StateC.Where(s => s.type == "Contrato");
            ViewBag.stateContract = db.StateContract.First(s => s.contractId == id && s.state);
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create(int? id)
        {
            ViewBag.clientId = id;
            ViewBag.nombcliente = db.Client.Find(id).name;
            //ViewBag.clientId = db.Client.Where(c => c.legalPerson).ToList();
            // ViewBag.clientId = new SelectList(db.Client, "clientId", "name");//tengo que mostrar solamente los que tengan personalidad juridica
            ViewBag.stateC = db.StateC.Where(s => s.type == "Contrato");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "contractId,number,signedProvider,nom1,nom2,description,clientId,expirationDate,signedClient,comitteNumber,comitteDate")] Contract contract, int stateC, System.DateTime dateState, string descriptionState, int idClient)
        {
            if (ModelState.IsValid)
            {
                StateContract _stateContract = new StateContract
                {
                    stateCId = stateC,
                    Contract = contract,
                    date = dateState,
                    description = descriptionState,
                    state = true
                };
                db.StateContract.Add(_stateContract);
                contract.nom1 = "tempvalue";
                contract.nom2 = "tmpvalue";
                contract.clientId = idClient;
                db.Contract.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = contract.clientId });
            }

            ViewBag.clientId = contract.clientId;
            ViewBag.stateC = db.StateC.Where(s => s.type == "Contrato");
            //ViewBag.clientId = new SelectList(db.Client, "clientId", "name", contract.clientId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientId = contract.clientId;
            //ViewBag.clientId = new SelectList(db.Client, "clientId", "name", contract.clientId);
            ViewBag.stateC = db.StateC.Where(s => s.type == "Contrato");
            ViewBag.stateContract = db.StateContract.First(s => s.contractId == id && s.state);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "contractId,number,signedProvider,nom1,nom2,description,clientId,expirationDate,signedClient,comitteNumber,comitteDate")] Contract contract, int stateC, System.DateTime dateState, string descriptionState, int idClient)
        {
            if (ModelState.IsValid)
            {
                List<StateContract> stateContracts = db.StateContract.Where(s => s.contractId == contract.contractId).ToList();
                bool found = false;
                foreach (StateContract stateContract in stateContracts)
                {
                    if (stateContract.stateCId == stateC)
                    {
                        found = true;
                        stateContract.state = true;
                        stateContract.date = dateState;
                        stateContract.description = descriptionState;
                    }
                    else
                    {
                        stateContract.state = false;
                    }
                }
                if (!found)
                {
                    StateContract _stateContract = new StateContract
                    {
                        stateCId = stateC,
                        Contract = contract,
                        date = dateState,
                        description = descriptionState,
                        state = true
                    };
                    db.StateContract.Add(_stateContract);
                }
                //db.SaveChanges();

                contract.clientId = idClient;
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name", contract.clientId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.stateC = db.StateC.Where(s => s.type == "Contrato");
            ViewBag.stateContract = db.StateContract.First(s => s.contractId == id && s.state);
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contract.Find(id);
            List<StateContract> stateContracts = db.StateContract.Where(s => s.contractId == contract.contractId).ToList();
            if (stateContracts.Any())
            {
                int count = stateContracts.Count;
                for (int i = 0; i < count; i++)
                    db.StateContract.Remove(stateContracts[i]);
            }
            db.Contract.Remove(contract);
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
