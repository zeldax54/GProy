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
    public class InvoicesController : Controller
    {
        private GProyEntities db = new GProyEntities();

        // GET: Invoices


        public ActionResult Index(Int32 iddet)
        {
            List<ProjectDetailsSpecialist> spec =
                db.ProjectDetailsSpecialist.Include(a => a.InvoiceProjectDetails)
                    .Where(p => p.projectDetailsId == iddet)
                    .ToList();


            ICollection<InvoiceProjectDetails> param = new List<InvoiceProjectDetails>();
            foreach (var s in spec)
            {
                foreach (var d in s.InvoiceProjectDetails)
                {
                    param.Add(d);
                }
            }
            List<Invoice> invoice = new List<Invoice>();
            foreach (var i in param)
            {
                invoice.Add(i.Invoice);
            }
            IEnumerable<Invoice> ie = invoice;
            ie = ie.Distinct();
            ViewBag.iddet = iddet;

            return View(ie);


        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create(Int32 iddet)
        {
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name");
            ViewBag.productId = new SelectList(db.Product, "productId", "name");
            return View();
        }

        public ActionResult CreateAlter(Int32 iddet, Int32 idclien, Int32 idprod)
        {

            ViewBag.clientId = db.Client.Find(idclien);
            ViewBag.productId = db.Product.Find(idprod);
            ProjectDetails pd = db.ProjectDetails.Find(iddet);
            ViewBag.ammount = pd.toInvoice;
            ViewBag.state = db.State.Where(p => p.type == "f" && p.name == "Propuesta").ToList()[0];
            ICollection<ProjectDetailsSpecialist> spec =
                db.ProjectDetailsSpecialist.Include(a => a.ProjSpecialist.Specialist)
                    .Where(p => p.projectDetailsId == iddet)
                    .ToList();


            StateCSupplement stateCSupplement = db.StateCSupplement.Where(st => st.supplementId == pd.ProjSup.supplementId && st.state).ToList().First();
            if (stateCSupplement.stateCId == 6 || stateCSupplement.stateCId == 7)
            {
                ViewBag.error += "Esta fase esta en un suplemento cancelado o detenido no se pueden realizar cambios.";
            }
           
            if (pd.toInvoice == 0)
            {
                ViewBag.error += " Esta fase ya se ha facturado por completo.";
            }
            if (spec.Count == 0)
            {
                ViewBag.error += "Esta fase no puede ser facturada. No tiene especialistas asignados";
            }


            else
            {
                ViewBag.spec = spec;
            }


            return View();
        }

        public void Crear_Factura(int iddetalle, int idcliente, int idproducto, int idestado, string fecha,
            string numero, string projDetailsSpecialistId, string montos)
        {
            List<int> ids = Convert(projDetailsSpecialistId);
            List<double> montosv = ConvertMontos(montos);


            DateTime f = DateTime.Parse(fecha);
            double montof = montosv.Sum();
            Invoice i = new Invoice();

            i.Client = db.Client.Find(idcliente);
            i.Product = db.Product.Find(idproducto);
            i.invoiceNum = numero;
            i.amount = montof;
            i.date = f;
            db.Invoice.Add(i);


            InvoiceStateSet issSet = new InvoiceStateSet
            {
                Invoice = i,
                State1 = db.State.Find(idestado),
                date = fecha,
                description = db.State.Find(idestado).name
            };
            db.InvoiceStateSet.Add(issSet);


            for (int j = 0; j < ids.Count; j++)
            {
                InvoiceProjectDetails ipd = new InvoiceProjectDetails();
                ipd.projDetailsSpecialistId = ids[j];
                ipd.amount = montosv[j];
                ipd.Invoice = i;
                db.InvoiceProjectDetails.Add(ipd);

            }
            //Restando del Detalle
            ProjectDetails pd = db.ProjectDetails.Find(iddetalle);
            pd.totalInvoiced = pd.totalInvoiced + montof;
            pd.toInvoice = pd.totalContracted - pd.totalInvoiced;
            //Atualizando proyecto
            pd.ProjSup.Project.totalnvoiced = decimal.Parse((pd.totalInvoiced + montof).ToString());
            pd.ProjSup.Project.toInvoiced = decimal.Parse((pd.totalContracted - pd.totalInvoiced).ToString());
            db.SaveChanges();
        }


        private List<double> ConvertMontos(string x)
        {
            x = x.Replace(",", ".");
            List<double> final = new List<double>();
            string num = "";
            for (int i = 0; i < x.Count(); i++)
            {

                if (x[i] != '*')
                {
                    num += x[i];
                }
                if (num != "" && x[i] == '*')
                {
                    double n = double.Parse(num);
                    final.Add(n);
                    num = "";
                }
            }
            return final;
        }

        private List<int> Convert(string x)
        {

            List<int> final = new List<int>();
            string num = "";
            for (int i = 0; i < x.Count(); i++)
            {

                if (x[i] != '*')
                {
                    num += x[i];
                }
                if (num != "" && x[i] == '*')
                {
                    int n = int.Parse(num);
                    final.Add(n);
                    num = "";
                }
            }
            return final;
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "invoiceId,clientId,amount,productId,date,state,invoiceNum")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoice.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clientId = new SelectList(db.Client, "clientId", "name", invoice.clientId);
            ViewBag.productId = new SelectList(db.Product, "productId", "name", invoice.productId);
            return View(invoice);
        }

        //EditarGet
       



        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id, int iddetalle)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }


           

            ViewBag.clientId = db.Client.Find(invoice.clientId);
            ViewBag.productId = new SelectList(db.Product, "productId", "name", invoice.productId);
            ViewBag.espc = invoice.InvoiceProjectDetails;
            ViewBag.ammount = invoice.amount;
            ViewBag.fecha = invoice.date;
            ViewBag.estado = db.InvoiceStateSet.Where(a => a.invoiceId == invoice.invoiceId).ToList()[0].description;
            ViewBag.producto = invoice.Product.name;
            ProjectDetails pd = db.ProjectDetails.Find(iddetalle);
            ViewBag.posible = invoice.amount+pd.toInvoice;
            ViewBag.qued = pd.ProjectDetailsSpecialist;
            List<ProjectDetailsSpecialist> quedan = new List<ProjectDetailsSpecialist>();
            foreach (var v in pd.ProjectDetailsSpecialist)
            {
                bool band = false;
                foreach (var i in invoice.InvoiceProjectDetails)
                {
                    if (v.ProjSpecialist.Specialist.specialistId ==
                        i.ProjectDetailsSpecialist.ProjSpecialist.Specialist.specialistId)
                    {
                        band = true;
                        break;
                    }
                   
                }
                if (band == false)
                {
                    quedan.Add(db.ProjectDetailsSpecialist.Find(v.projDetailsSpecialistId));
                }
            }
            ViewBag.quedan = quedan;
            ViewBag.iddet = iddetalle;
            ViewBag.idf = id;
            return View(invoice);
        }

        public void Editar_Factura(int? idfact, int? iddet, string projDetailsSpecialistId, string montos, string oldm)
        {
            List<int> ids = Convert(projDetailsSpecialistId);
            List<double> montosv = ConvertMontos(montos);
            Invoice i = db.Invoice.Find(idfact);
            double montoant = i.amount;
          
                db.InvoiceProjectDetails.RemoveRange(i.InvoiceProjectDetails);
           
           
            for (int j = 0; j < ids.Count; j++)
            {
                InvoiceProjectDetails ipd = new InvoiceProjectDetails();
                ipd.projDetailsSpecialistId = ids[j];
                ipd.amount = montosv[j];
                ipd.Invoice = i;
                i.InvoiceProjectDetails.Add(ipd);

            }
            double montof = montosv.Sum();
            i.amount = montof;
            //Actualizando el Detalle
            ProjectDetails pd = db.ProjectDetails.Find(iddet);
            pd.totalInvoiced = pd.totalInvoiced + montof - montoant;
            pd.toInvoice = pd.totalContracted - pd.totalInvoiced;
            //Actualizar Proyecto
            pd.ProjSup.Project.totalnvoiced = decimal.Parse((pd.totalInvoiced + montof - montoant).ToString());
            pd.ProjSup.Project.toInvoiced = decimal.Parse((pd.totalContracted - pd.totalInvoiced).ToString());
            //Actualizar Estado del SUplemento

            Supplement s = pd.ProjSup.Supplement;
            double facturado = s.ProjSup.Sum(z => z.ProjectDetails.Sum(ss => ss.totalInvoiced));
            if (Math.Abs(facturado - decimal.ToDouble(s.amount)) < 0.1)
            {
                foreach (var state in s.StateCSupplement)
                {
                    state.state = false;
                }
                StateC terminado = db.StateC.Find(10);
                StateCSupplement stateCSupplement = new StateCSupplement
                {
                    stateCId = terminado.stateCId,
                    Supplement = s,
                    date = DateTime.Now,
                    description = "Maximo de facturacion alcanzada",
                    state = true
                };
                db.StateCSupplement.Add(stateCSupplement);
            }
            else
            {
                if (s.StateCSupplement.First(a => a.state).stateCId == 10)
                {
                    foreach (var state in s.StateCSupplement)
                    {
                        state.state = false;
                    }
                    StateC iniciado = db.StateC.Find(5);
                    StateCSupplement stateCSupplement = new StateCSupplement
                    {
                        stateCId = iniciado.stateCId,
                        Supplement = s,
                        date = DateTime.Now,
                        description = "Estado cambiado dinamicamente x Ediacion de Facturas",
                        state = true
                    };
                    db.StateCSupplement.Add(stateCSupplement);
                }
            }
        
            /////////////////////////
            db.SaveChanges();
        }

        

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "invoiceId,clientId,amount,productId,date,state,invoiceNum")] Invoice invoice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(invoice).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.clientId = new SelectList(db.Client, "clientId", "name", invoice.clientId);
        //    ViewBag.productId = new SelectList(db.Product, "productId", "name", invoice.productId);
        //    return View(invoice);
        //}

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            List<ProjectDetailsSpecialist> quedan = new List<ProjectDetailsSpecialist>();
            int iddetalle = invoice.InvoiceProjectDetails.First().ProjectDetailsSpecialist.projectDetailsId;
            ProjectDetails pd = db.ProjectDetails.Find(iddetalle);
            foreach (var v in pd.ProjectDetailsSpecialist)
            {
                bool band = false;
                foreach (var i in invoice.InvoiceProjectDetails)
                {
                    if (v.ProjSpecialist.Specialist.specialistId ==
                        i.ProjectDetailsSpecialist.ProjSpecialist.Specialist.specialistId)
                    {
                        band = true;
                        break;
                    }

                }
                if (band == false)
                {
                    quedan.Add(db.ProjectDetailsSpecialist.Find(v.projDetailsSpecialistId));
                }
            }
            ViewBag.quedan = quedan;
            ViewBag.iddet = iddetalle;
            ViewBag.espc = invoice.InvoiceProjectDetails;
            return View(invoice);
        }

        // POST: Invoices/Delete/5
       
        //Eliminar factura
       
        public void DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoice.Find(id);

            //Actualizando el Detalle
            ProjectDetails pd = db.ProjectDetails.Find(invoice.InvoiceProjectDetails.First().ProjectDetailsSpecialist.projectDetailsId);
            pd.totalInvoiced = pd.totalInvoiced -invoice.amount;
            pd.toInvoice = pd.totalContracted - pd.totalInvoiced;
            //Actualizar Proyecto
            pd.ProjSup.Project.totalnvoiced = decimal.Parse((pd.totalInvoiced -invoice.amount).ToString());
            pd.ProjSup.Project.toInvoiced = decimal.Parse((pd.totalContracted - pd.totalInvoiced).ToString());
            //Actualizar Estado del SUplemento

            Supplement s = pd.ProjSup.Supplement;
            double facturado = s.ProjSup.Sum(z => z.ProjectDetails.Sum(ss => ss.totalInvoiced));
            if (Math.Abs(facturado - decimal.ToDouble(s.amount)) < 0.1)
            {
                foreach (var state in s.StateCSupplement)
                {
                    state.state = false;
                }
                StateC terminado = db.StateC.Find(10);
                StateCSupplement stateCSupplement = new StateCSupplement
                {
                    stateCId = terminado.stateCId,
                    Supplement = s,
                    date = DateTime.Now,
                    description = "Maximo de facturacion alcanzada",
                    state = true
                };
                db.StateCSupplement.Add(stateCSupplement);
            }
            else
            {
                if (s.StateCSupplement.First(a => a.state).stateCId == 10)
                {
                    foreach (var state in s.StateCSupplement)
                    {
                        state.state = false;
                    }
                    StateC iniciado = db.StateC.Find(5);
                    StateCSupplement stateCSupplement = new StateCSupplement
                    {
                        stateCId = iniciado.stateCId,
                        Supplement = s,
                        date = DateTime.Now,
                        description = "Estado cambiado dinamicamente x Ediacion de Facturas",
                        state = true
                    };
                    db.StateCSupplement.Add(stateCSupplement);
                }
            }

            db.InvoiceProjectDetails.RemoveRange(invoice.InvoiceProjectDetails);
            db.InvoiceStateSet.RemoveRange(invoice.InvoiceStateSet);
            db.Invoice.Remove(invoice);
            db.SaveChanges();
          
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
