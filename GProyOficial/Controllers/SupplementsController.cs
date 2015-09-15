using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GProyOficial.Models;
using GProyOficial.Models.ViewModel;

namespace GProyOficial.Controllers
{
    public class SupplementsController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: Supplements
        public ActionResult Index(int? id, bool idclient)
        {
            SupplementList spList = new SupplementList();  
            var supplement = db.Supplement.Include(s => s.Contract).Include(s => s.Product).Include(s => s.Service).Where(s => s.contractId == id);
            spList.Supplements = supplement.ToList();
            spList.ContractId = id;
            Contract contract = db.Contract.Find(id);
            ViewBag.nombc = contract.Client.name;
            ViewBag.clieteid = contract.Client.clientId;
            ViewBag.contrato = contract.number;
            if (contract != null && idclient)
            {
                spList.IsClient = true;
                spList.ClientId = contract.clientId;
            }
            else
            {
                spList.IsClient = false;
            }
            
            return View(spList);
        }

        // GET: Supplements/Details/5
        public ActionResult Details(int? id, bool idclient)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplement supplement = db.Supplement.Find(id);
            if (supplement == null)
            {
                return HttpNotFound();
            }
            ViewBag.contractId = supplement.contractId;
            ViewBag.stateC = db.StateC.Where(s => s.type == "Suplemento");
            ViewBag.stateSupplement = db.StateCSupplement.First(s => s.supplementId == id && s.state);
            ViewBag.clientSup = db.ClientSupplement.Where(cs => cs.supplementId == id).ToList();
            ViewBag.clientFather = db.Client.First(fa => fa.clientId == supplement.Contract.clientId);
            ViewBag.clients = db.Client.Where(cl => cl.fatherId == supplement.Contract.clientId).ToList();
            ViewBag.idclientValue = idclient; 
            return View(supplement);
        }

        public List<string> SetJson(string jsonsList)
        {
            List<string> stringList = new List<string>();
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
                    //var lista = new List<object>();
                    string temp = "";
                    for (var i = 0; i < res.Count(); i++)
                    {
                        if (res[i] != '"' && res[i] != '\r' && res[i] != '\n' && res[i] != ',' && res[i] != ' ' && res[i] != ']')
                            temp += res[i];
                        if (res[i] == ',' || res[i] == ']')
                        {
                            i = i + 1;
                            stringList.Add(temp);
                            temp = "";
                        }
                    }
                }
            }
            return stringList;
        }


        // GET: Supplements/Create
        public ActionResult Create(int? id, bool idclient)
        {
            ViewBag.contractId = id;//new SelectList(db.Contract, "contractId", "number");
            ViewBag.productId = new SelectList(db.Product, "productId", "name");
            ViewBag.serviceId = new SelectList(db.Service, "serviceId", "name");
            ViewBag.stateC = db.StateC.Where(s => s.type == "Suplemento");
            Contract contract = db.Contract.First(c => c.contractId == id);
            ViewBag.clientFather = db.Client.First(fa => fa.clientId == contract.clientId);
            ViewBag.clients = db.Client.Where(cl => cl.fatherId == contract.clientId).ToList();
            ViewBag.idclientValue = idclient;    
            return View();
        }

        // POST: Supplements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "supplementId,number,name,contractId,amount,nom1,nom2,signedClient,productId,serviceId,signedProvider,comitteNumber,comitteDate,expirationDate")] Supplement supplement, int stateC, System.DateTime dateState, string descriptionState, string jsonClient, int contractId, bool idclient)
        {
            if (ModelState.IsValid)
            {
                supplement.nom1 = "tmpvalue";
                supplement.nom2 = "tmpvalue";
                StateCSupplement _stateCSupplement = new StateCSupplement
                {
                    stateCId = stateC,
                    Supplement = supplement,
                    date = dateState,
                    description = descriptionState,
                    state = true
                };
                db.StateCSupplement.Add(_stateCSupplement);

                List<string> stringData = SetJson(jsonClient);
                decimal amountValue = 0;
                if (stringData.Any())
                {
                    for (int i = 0; i < stringData.Count(); i += 3)
                    {
                        int id = int.Parse(stringData.ElementAt(i));
                       
                        string newAmount = stringData.ElementAt(i + 1);
                        string[] newValue = newAmount.Split('.');
                        decimal endValue = 0;
                        if (newValue.Count() == 2)
                        {
                            decimal decens = decimal.Parse(newValue[0]);
                            decimal centens = decimal.Parse(newValue[1]);
                            endValue = decens + (centens / 100);
                        }
                        else
                        {
                            endValue = decimal.Parse(newAmount);
                        }
                        string idproy = stringData.ElementAt(i + 2);
                       
                       
                       
                        ClientSupplement clientSupplement = new ClientSupplement
                        {
                            clientId = id,
                            Supplement = supplement,
                            amount = endValue
                        };
                        amountValue += endValue;
                        if (clientSupplement != null)
                        {
                            db.ClientSupplement.Add(clientSupplement);
                        }
                        //creando los proyectos
                        Client client = db.Client.First(c => c.clientId == id);
                        if (client != null)
                        {
                            if (idproy != "*")
                            {
                                Project p = db.Project.Find(int.Parse(idproy));
                                ProjSup projSup = new ProjSup
                                {
                                    Project = p,
                                    Supplement = supplement,
                                    amount = endValue


                                };
                                p.totalContracted += endValue;
                                p.toInvoiced += endValue;
                                db.ProjSup.Add(projSup);
                            }
                            else
                            {
                                State c = db.State.Find(3);
                                Project project = new Project
                                {
                                    name = supplement.number + " - " + client.name,
                                    clientId = id,
                                    productId = supplement.productId,
                                    advancePercent = 0,
                                    totalContracted = endValue,
                                    toInvoiced = endValue
                                };

                                db.Project.Add(project);
                                ProjectState ps = new ProjectState
                                {
                                    State1 = c,
                                    date = DateTime.Now,
                                    description = "Proyecto creado automaticamente al crear Sumpelento",
                                    state = true,
                                    Project = project

                                };

                                db.ProjectState.Add(ps);
                                ProjSup projSup = new ProjSup
                                {
                                    Project = project,
                                    Supplement = supplement,
                                    amount = endValue

                                };
                                db.ProjSup.Add(projSup);
                            }
                      
                            
                        }
                        
                    }    
                }

                supplement.amount = amountValue;
                supplement.contractId = contractId;

                db.Supplement.Add(supplement);
                db.SaveChanges();
                return RedirectToAction("Index", new { id=supplement.contractId, idclient=idclient});
            }

            ViewBag.contractId = contractId;
            ViewBag.productId = new SelectList(db.Product, "productId", "name", supplement.productId);
            ViewBag.serviceId = new SelectList(db.Service, "serviceId", "name", supplement.serviceId);
			ViewBag.stateC = db.StateC.Where(s => s.type == "Suplemento");
            Contract contract = db.Contract.First(c => c.contractId == contractId);
            ViewBag.clientFather = db.Client.First(fa => fa.clientId == contract.clientId);
            ViewBag.clients = db.Client.Where(cl => cl.fatherId == contract.clientId).ToList();
            ViewBag.idclientValue = idclient;   
            return View(supplement);
        }

        // GET: Supplements/Edit/5
        public ActionResult Edit(int? id, bool idclient)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplement supplement = db.Supplement.Find(id);
            if (supplement == null)
            {
                return HttpNotFound();
            }
            ViewBag.contractId = supplement.contractId;
            ViewBag.productId = supplement.Product;
            ViewBag.serviceId = new SelectList(db.Service, "serviceId", "name", supplement.serviceId);
            ViewBag.stateC = db.StateC.Where(s => s.type == "Suplemento");
            ViewBag.stateSupplement = db.StateCSupplement.First(s => s.supplementId == id && s.state);
            ViewBag.clientSup = db.ClientSupplement.Where(cs => cs.supplementId == id).ToList();
            ViewBag.clientFather = db.Client.First(fa => fa.clientId == supplement.Contract.clientId);
            ViewBag.clients = db.Client.Where(cl => cl.fatherId == supplement.Contract.clientId).ToList();
            ViewBag.idclientValue = idclient;  
            return View(supplement);
        }

        // POST: Supplements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "supplementId,number,name,contractId,amount,nom1,nom2,signedClient,productId,serviceId,signedProvider,comitteNumber,comitteDate,expirationDate")] Supplement supplement, int stateC, System.DateTime dateState, string descriptionState, string jsonClient, bool idclient)
        {
            if (ModelState.IsValid)
            {
               
                
				List<StateCSupplement> stateCSupplement =
                    db.StateCSupplement.Where(st => st.supplementId == supplement.supplementId).ToList();
                bool found = false;
                foreach (StateCSupplement stateCs in stateCSupplement)
                {
                    if (stateCs.stateCId == stateC)
                    {
                        found = true;
                        stateCs.state = true;
                        stateCs.date = dateState;
                        stateCs.description = descriptionState;
                    }
                    else
                    {
                        stateCs.state = false;
                    }
                }
                if (!found)
                {
                    StateCSupplement _stateCSupplement = new StateCSupplement
                    {
                        stateCId = stateC,
                        Supplement = supplement,
                        date = dateState,
                        description = descriptionState,
                        state = true
                    };
                    db.StateCSupplement.Add(_stateCSupplement);
                }

                db.Entry(supplement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = supplement.contractId, idclient = idclient });
            }
            ViewBag.contractId = new SelectList(db.Contract, "contractId", "number", supplement.contractId);
            ViewBag.productId = new SelectList(db.Product, "productId", "name", supplement.productId);
            ViewBag.serviceId = new SelectList(db.Service, "serviceId", "name", supplement.serviceId);
            ViewBag.idclientValue = idclient;  
            return View(supplement);
        }




        // GET: Supplements/Delete/5
        public ActionResult Delete(int? id, bool idclient)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplement supplement = db.Supplement.Find(id);
            if (supplement == null)
            {
                return HttpNotFound();
            }
            ViewBag.contractId = supplement.contractId;
            ViewBag.stateC = db.StateC.Where(s => s.type == "Suplemento");
            ViewBag.stateSupplement = db.StateCSupplement.First(s => s.supplementId == id && s.state);
            ViewBag.clientSup = db.ClientSupplement.Where(cs => cs.supplementId == id).ToList();
            ViewBag.clientFather = db.Client.First(fa => fa.clientId == supplement.Contract.clientId);
            ViewBag.clients = db.Client.Where(cl => cl.fatherId == supplement.Contract.clientId).ToList();
            ViewBag.idclientValue = idclient;
            return View(supplement);
        }

        // POST: Supplements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, bool idclient, int stateC, DateTime dateState, string descriptionState)
        {
            Supplement supplement = db.Supplement.Find(id);
            //int contractId = supplement.contractId;
            //List<ClientSupplement> listClientS =
            //                db.ClientSupplement.Where(c => c.supplementId == supplement.supplementId).ToList();
            //if (listClientS.Any())
            //{
            //    int count = listClientS.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        db.ClientSupplement.Remove(listClientS[i]);
            //    }
            //}
            //List<StateCSupplement> listStateCSupplements =
            //                db.StateCSupplement.Where(s => s.supplementId == supplement.supplementId).ToList();
            //if (listStateCSupplements.Any())
            //{
            //    int count = listStateCSupplements.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        db.StateCSupplement.Remove(listStateCSupplements[i]);
            //    }
            //}
            List<StateCSupplement> stateCSupplement =
                    db.StateCSupplement.Where(st => st.supplementId == supplement.supplementId).ToList();
            bool found = false;
            foreach (StateCSupplement stateCs in stateCSupplement)
            {
                if (stateCs.stateCId == stateC)
                {
                    found = true;
                    stateCs.state = true;
                    stateCs.date = dateState;
                    stateCs.description = descriptionState;
                }
                else
                {
                    stateCs.state = false;
                }
            }
            if (!found)
            {
                StateCSupplement _stateCSupplement = new StateCSupplement
                {
                    stateCId = stateC,
                    Supplement = supplement,
                    date = dateState,
                    description = descriptionState,
                    state = true
                };
                db.StateCSupplement.Add(_stateCSupplement);
            }

          
            db.SaveChanges();
            return RedirectToAction("Index", new { id = supplement.contractId, idclient = idclient });
        }

        //Proyectos x Clientes
        public JsonResult GetProyectos(Int32 idc)
        {
           
            ICollection<Project> proys = db.Project.Where(p=>p.clientId==idc).ToList();
            ICollection<Project> proysfin = new List<Project>();
            foreach (var p in proys)
            {
                List<ProjectState> ps = p.ProjectState.ToList();
                for (int i = 0; i < ps.Count; i++)
                {
                    if(ps[i].state)
                        if (ps[i].State1.stateId == 2)
                        {
                            proysfin.Add(p);
                            break;
                        }
                    
                }
            }
            if (proysfin.Count == 0)
                return Json("-1");
            string fin = "{" + '"' + "Proyectos" + '"' + ":[";

            foreach (Project proy in proysfin)
            {
                fin += "{" + '"' + "id" + '"' + ':' + '"' + proy.projectId + '"' + "," + '"' + "nombre" + '"' + ':' + '"' + proy.name + '"' + ","
                    + '"' + "producto" + '"' + ':' + '"' + proy.Product.productId + '"' + "," + '"' + "productonomb" + '"' + ':' + '"' + proy.Product.name + '"' + "},";

            }
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";

            return Json(fin);

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
