using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web.Mvc;
using GProyOficial.Models;
using GProyOficial.Models.ViewModel;

namespace GProyOficial.Controllers
{
    public class ClientsController : Controller
    {
        private GProyEntities db = new GProyEntities();
        
        // GET: Clients
        public ActionResult Index()
        {
            var client = db.Client.Include(c => c.Organism);
            return View(client.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.fatherId = db.Client.Where(c => c.isSubject == false);
            return View(client);
        }

        public ICollection<AccountBank> SetJson(string jsonsList)
        {
            ICollection<AccountBank> listAccountBanks = new List<AccountBank>();
            int ini = 0;
            int end = 0;
            int posini = 0;
            int posfin = 0;
            bool foundIni = false;
            bool foundEnd = false;
            foreach (var objectList in jsonsList)
            {
                string trama = objectList.ToString();
                string res = "";

                //para calcular la position inicial y final
                ini += 1;
                if (trama == "[")
                {
                    foundIni = true;
                    posini = ini;
                }
                end += 1;
                if (trama == "]")
                {
                    foundEnd = true;
                    posfin = end;
                    ini = posfin + 1;
                }

                if (posini < posfin && foundIni && foundEnd)
                {
                    foundIni = false;
                    foundEnd = false;
                    res = jsonsList.Substring(posini, posfin - posini);
                    var lista = new List<object>();
                    string temp = "";
                    for (var i = 0; i < res.Count(); i++)
                    {
                        if (res[i] != '"' && res[i] != '\r' && res[i] != '\n' && res[i] != ',' && res[i] != ' ' && res[i] != ']')
                            temp += res[i];
                        if (res[i] == ',' || res[i] == ']')
                        {
                            i = i + 1;
                            lista.Add(temp);
                            temp = "";
                        }
                    }
                    AccountBank accounBank = new AccountBank
                    {
                        accountNumber = long.Parse(lista[0].ToString()),
                        bankId = int.Parse(lista[1].ToString()),
                        currencyTypeId = int.Parse(lista[2].ToString()),
                        titular = lista[3].ToString()
                    };
                   
                    listAccountBanks.Add(accounBank);
                }
            } 
            return listAccountBanks;
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.organismId = new SelectList(db.Organism, "organismId", "name");
            ViewBag.fatherId = db.Client.Where(c => c.isSubject == false);
            //ViewBag.fatherId = new SelectList(db.Client, "clientId", "name");
            
            ViewBag.bankId = new SelectList(db.Bank, "bankId", "name");
            ViewBag.currencyTypeId = new SelectList(db.CurrencyType, "currencyTypeId", "type");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientId,dateCreation,name,identifAbrev,reup,street,city,province,telephone,email,agent,agentPosition,organismId,country,isSubject,fatherId,legalPerson")] Client client, int clientFather, string jsonData)
        {
           if (ModelState.IsValid)
           {
               
               ICollection<AccountBank> accountBanks = SetJson(jsonData);
               if (accountBanks.Count > 0)
               {
                   client.AccountBank = accountBanks;
                   foreach (AccountBank accountBank in accountBanks)
                       db.AccountBank.Add(accountBank);
               }
               if (!client.isSubject)
               {
                   client.fatherId = null;
               }
               else
               {
                   client.fatherId = clientFather;
               }

               db.Client.Add(client);
               db.SaveChanges();
              
               return RedirectToAction("Index");
           }
           
            ViewBag.organismId = new SelectList(db.Organism, "organismId", "name", client.organismId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.organismId = new SelectList(db.Organism, "organismId", "name", client.organismId);
            //ViewBag.fatherId = new SelectList(db.Client, "fatherId", "name", client.fatherId);
            ViewBag.fatherId = db.Client.Where(c => c.isSubject == false);
            ViewBag.bankId = new SelectList(db.Bank, "bankId", "name");
            ViewBag.currencyTypeId = new SelectList(db.CurrencyType, "currencyTypeId", "type");
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientId,dateCreation,name,identifAbrev,reup,street,city,province,telephone,email,agent,agentPosition,organismId,country,isSubject,fatherId,legalPerson")] Client client, int? clientFather, string jsonData)
        {
            if (ModelState.IsValid)
            {
                if (jsonData != "")
                {
                    List<AccountBank> accountBankList = db.AccountBank.Where(a => a.clientId == client.clientId).ToList();
                    if (accountBankList.Any())
                    {
                        int count = accountBankList.Count;
                        for (int i = 0; i < count; i++)
                            db.AccountBank.Remove(accountBankList[i]);
                        
                        db.SaveChanges();
                    }
                
                    ICollection<AccountBank> accountBanks = SetJson(jsonData);
                    if (accountBanks.Count > 0)
                    {
                        foreach (AccountBank accountBank in accountBanks)
                        {
                            accountBank.clientId = client.clientId;
                            db.AccountBank.Add(accountBank);
                        }
                    }
                }
                if (client.isSubject)
                {
                    client.fatherId = clientFather;
                }
                //if (client.fatherId != clientFather)
                  //  client.fatherId = clientFather;

                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.organismId = new SelectList(db.Organism, "organismId", "name", client.organismId);
            ViewBag.fatherId = new SelectList(db.Client, "fatherId", "name", client.fatherId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.fatherId = db.Client.Where(c => c.isSubject == false);
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            List<AccountBank> accountBankList = db.AccountBank.Where(a => a.clientId == client.clientId).ToList();
            if (accountBankList.Any())
            {
                int count = accountBankList.Count;
                for (int i = 0; i < count; i++)
                    db.AccountBank.Remove(accountBankList[i]);
            }
            db.Client.Remove(client);
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
