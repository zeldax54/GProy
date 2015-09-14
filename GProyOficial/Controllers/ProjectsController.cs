using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using GProyOficial.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace GProyOficial.Controllers
{
    public class ProjectsController : Controller
    {
        private GProyEntities db = new GProyEntities();



        public string SCron(Int32 id, Int32 idf, string d, string m)
        {

            List<ProjectDetails> pdtDetails = db.ProjectDetails.Where(p => p.projSupId == id).ToList();

            if (pdtDetails.Count > 0)
            {
                foreach (var x in pdtDetails)
                {
                    List<ProjectDetailsSpecialist> pds =
                        db.ProjectDetailsSpecialist.Where(p => p.projectDetailsId == x.projectDetailsId).ToList();
                    if (pds.Count > 0)
                    {
                        return "-1";
                    }
                }
            }



            List<Schedule> sch = db.Schedule.Where(p => p.projSupId == id && p.stageId == idf).ToList();
            if (sch.Count > 0)
            {
                foreach (var c in sch)
                {
                    db.Schedule.Remove(c);
                }
            }
            Schedule s = new Schedule();
            s.projSupId = id;
            s.stageId = idf;
            s.nDays = float.Parse(d);
            s.amount = float.Parse(m);
            db.Schedule.Add(s);

            /*ASignando detalle al proyecto*/
            foreach (var projectdetail in db.ProjectDetails.Where(p => p.projSupId == id && p.stageId == idf))
            {
                db.ProjectDetails.Remove(projectdetail);
            }

            ProjectDetails pd = new ProjectDetails();
            pd.projSupId = id;
            pd.stageId = idf;
            pd.totalContracted = float.Parse(m);
            pd.totalInvoiced = 0;
            pd.toInvoice = float.Parse(m);
            pd.state = false;
            db.ProjectDetails.Add(pd);
            db.SaveChanges();

            return "1";


        }

        public JsonResult GetPhasesname(Int32 id)
        {
            ICollection<Stage> stages = db.Stage.Where(stage => stage.serviceId == id).ToList();
            string fin = "{" + '"' + "Fases" + '"' + ":[";

            fin = stages.Aggregate(fin, (current, stage) => current + ("{" + '"' + "id" + '"' + ':' + '"' + stage.stageId + '"' + "," + '"' + "fase" + '"' + ':' + '"' + stage.name + '"' + "},"));
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";

            return Json(fin);

        }

        public JsonResult Cronograma(Int32 id)
        {
            ICollection<Schedule> schedules = db.Schedule.Where(schedule => schedule.projSupId == id).ToList();
            if (schedules.Count == 0)
                return Json(-1);
            string fin = "{" + '"' + "Cron" + '"' + ":[";
            foreach (Schedule schedule in schedules)
            {
                fin += "{" + '"' + "id" + '"' + ':' + '"' + schedule.stageId + '"' + "," + '"' + "fase" + '"' + ':' +
                       '"' + schedule.Stage.name + '"' + "," + '"' + "dias" + '"' + ':' + '"' + schedule.nDays + '"' +
                       "," + '"' + "monto" + '"' + ':' + '"' + schedule.amount + '"' + "},";
            }
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";
            return Json(fin);

        }

        //especialistas posibles en una fase de un proyecto excepto asignados
        public JsonResult SpesProy(Int32 idp, Int32 idddet)
        {
            List<ProjSpecialist> pe =
                db.ProjSpecialist.Include(p => p.Specialist)
                    .Include(p => p.Specialist.Area)
                    .Where(p => p.projectId == idp)
                    .ToList();
            List<ProjectDetailsSpecialist> ed =
                db.ProjectDetailsSpecialist.Include(p => p.ProjSpecialist)
                    .Where(p => p.projectDetailsId == idddet)
                    .ToList();
            foreach (var a in ed)
            {
                for (int i = 0; i < pe.Count; i++)
                {
                    if (a.ProjSpecialist.specialistId == pe[i].specialistId)
                    {
                        pe.RemoveAt(i);
                        break;
                    }
                }
            }
            if (pe.Count == 0)
                return Json(-1);
            string fin = "{" + '"' + "Esp" + '"' + ":[";
            foreach (ProjSpecialist eProjSpecialist in pe)
            {
                fin += "{" + '"' + "area" + '"' + ':' + '"' + eProjSpecialist.Specialist.Area.name + '"'
                       + "," + '"' + "idspec" + '"' + ':' + '"' + eProjSpecialist.projSpecialistId + '"'
                       + "," + '"' + "nombre" + '"' + ':' + '"' + eProjSpecialist.Specialist.name +
                       '"' + "," + '"' + "jefe" + '"' + ':' + '"' + eProjSpecialist.boss + '"' + "},";
            }
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";
            return Json(fin);
        }

        //Especialistas asignados a  una fase en un proyecto 
        public JsonResult SpesProyAct(Int32 idddet)
        {
            List<ProjectDetailsSpecialist> ed =
                db.ProjectDetailsSpecialist.Include(p => p.ProjSpecialist.Specialist.Area)
                    .Where(p => p.projectDetailsId == idddet)
                    .ToList();

            if (ed.Count == 0)
            {
                return Json("");
            }

            string fin = "{" + '"' + "Asig" + '"' + ":[";
            foreach (var spec in ed)
            {

                fin += "{" + '"' + "area" + '"' + ':' + '"' + spec.ProjSpecialist.Specialist.Area.name + '"'
                       + "," + '"' + "idspec" + '"' + ':' + '"' + spec.projDetailsSpecialistId + '"'
                       + "," + '"' + "nombre" + '"' + ':' + '"' + spec.ProjSpecialist.Specialist.name +
                       '"' + "," + '"' + "part" + '"' + ':' + '"' + spec.participationPercent + '"' + "},";

            }
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";
            return Json(fin);
        }

        public JsonResult GetDetallesFase(Int32 proysupid, Int32 idfase)
        {
            ICollection<ProjectDetails> det =
                db.ProjectDetails.Include(p => p.Stage)
                    .Where(p => p.projSupId == proysupid && p.stageId == idfase)
                    .ToList();
            string fin = "{" + '"' + "Det" + '"' + ":[";
            if (det.Count > 0)
            {

                foreach (ProjectDetails details in det)
                {
                    fin += "{" + '"' + "iddet" + '"' + ':' + '"' + details.projectDetailsId + '"'
                           + "," + '"' + "nombfase" + '"' + ':' + '"' + details.Stage.name + '"'
                           + "," + '"' + "tc" + '"' + ':' + '"' + details.totalContracted + '"'
                           + "," + '"' + "tf" + '"' + ':' + '"' + details.totalInvoiced + '"'
                           + "," + '"' + "tpf" + '"' + ':' + '"' + details.toInvoice + '"'
                           + "," + '"' + "statesup" + '"' + ':' + '"' + IsSupDetoCanc(null, proysupid, null) + '"' +
                           "},";
                }
            }
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";
            return Json(fin);
        }

        public void DeleteEspProy(int idproy, int idesp)
        {
            foreach (var projspec in db.ProjSpecialist.Where(p => p.projectId == idproy && p.specialistId == idesp))
            {
                db.ProjSpecialist.Remove(projspec);

            }
            db.SaveChanges();
        }

        public void AddEspProy(int idproy, int idesp, bool jefe)
        {

            ProjSpecialist pe = new ProjSpecialist
            {
                projectId = idproy,
                specialistId = idesp,
                boss = jefe
            };
            db.ProjSpecialist.Add(pe);
            db.SaveChanges();

        }

        public void SetJefeP(int projectid, int idesp)
        {
            foreach (var projspec in db.ProjSpecialist.Where(p => p.projectId == projectid))
            {
                projspec.boss = projspec.specialistId == idesp;
            }
            db.SaveChanges();
        }

        public JsonResult Especialistafase(Int32 idprojspec, Int32 projdetid, string valor)
        {
            ProjectDetailsSpecialist pd = new ProjectDetailsSpecialist();
            pd.projSpecialistId = idprojspec;
            pd.projectDetailsId = projdetid;
            pd.participationPercent = double.Parse(valor);
            db.ProjectDetailsSpecialist.Add(pd);
            if (IsSupDetoCanc(null, null, pd.projectDetailsId) == 1)
            {
                throw new Exception("Accion Ilegal Traza Guardada send mail to admin user,localuser,time,localmail");
            }
            db.SaveChanges();
            return Json(pd.projDetailsSpecialistId);
        }

        public string EspecialistafaseDelete(Int32 id)
        {
            try
            {
                if (IsSupDetoCanc(null, null, db.ProjectDetailsSpecialist.Find(id).projectDetailsId) == 1)
                {
                    throw new Exception("Accion Ilegal Traza Guardada send mail to admin user,localuser,time,localmail");
                }
                db.ProjectDetailsSpecialist.Remove(db.ProjectDetailsSpecialist.Find(id));

                db.SaveChanges();
                return "1";
            }
            catch (Exception)
            {

                return "0";
            }

        }






















        // GET: Projects

        public ActionResult Index(int? idp, int? idcliente)
        {

            if (!User.Identity.IsAuthenticated)//Temporal soluction until a find more information
            {
                return RedirectToAction("Index", "Home");
              
            }

            if (idcliente != null)
            {
                if (!User.IsInRole("Administrador") && !User.IsInRole("Consultor"))
                {
                    EntitiesLogueo el = new EntitiesLogueo();
                    AspNetUsers u = el.AspNetUsers.Find(User.Identity.GetUserId());
                    var project = db.Project.Include(p => p.Client).Where( p => p.Area.name == u.PhoneNumber && p.clientId==idcliente);
                    return View(project.ToList());
                }
                else
                {
                    var project = db.Project.Include(p => p.Client).Where(c => c.clientId == idcliente);
                    return View(project.ToList());
                }
               
            }
            if (idp == null)
            {
                if (!User.IsInRole("Administrador") && !User.IsInRole("Consultor"))
                {
                    EntitiesLogueo el = new EntitiesLogueo();
                    AspNetUsers u = el.AspNetUsers.Find(User.Identity.GetUserId());
                    var project = db.Project.Include(p => p.Client).Where(p=> p.Area.name == u.PhoneNumber);
                    return View(project.ToList());
                }
                else
                {
                    var project = db.Project.Include(p => p.Client);
                    return View(project.ToList());
                }
              
            }
            else
            {
                var project = db.Project.Include(p => p.Client).Where(p => p.projectId == idp);
                return View(project.ToList());
               
            }
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.areaId = new SelectList(db.Area, "areaId", "name");
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name");
            ViewBag.productId = new SelectList(db.Product, "productId", "name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "projectId,name,state,areaId,clientId,advancePercent,startDate,endDate,endDateEstimate,totalContracted,totalnvoiced,toInvoiced,productId,modulequantity_,complexity,plannedhours,executedhours,chronogramDeviation,detentionCancellationDate,detentionCancellationCause,detentionCancellationDetails,observation,reprogrammed,reopeningDate,resumed_"
                )] Project project)
        {
            if (ModelState.IsValid)
            {



                db.Project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.areaId = new SelectList(db.Area, "areaId", "name", project.areaId);
            ViewBag.clientId = new SelectList(db.Client, "clientId", "name", project.clientId);
            ViewBag.productId = new SelectList(db.Product, "productId", "name", project.productId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Project project = db.Project.Find(id);
            if (!User.IsInRole("Administrador") && !User.IsInRole("Consultor"))
            {
                EntitiesLogueo el = new EntitiesLogueo();
                AspNetUsers u = el.AspNetUsers.Find(User.Identity.GetUserId());
                if (u.PhoneNumber != project.Area.name)
                {
                    return HttpNotFound();
                }
            }
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.areaId = new SelectList(db.Area, "areaId", "name", project.areaId);
            ViewBag.clientId = project.Client;
            ViewBag.productId = new SelectList(db.Product, "productId", "name", project.productId);
            ViewBag.idProduct = project.Product;
            ProjectState state = GetEstado(id);
            ViewBag.state = state;

            return View(project);
        }

        public ProjectState GetEstado(int? id)
        {
            return db.ProjectState.Where(p => p.projectId == id && p.state).ToList()[0];
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "projectId,name,state,areaId,clientId,advancePercent,startDate,endDate,endDateEstimate,totalContracted,totalnvoiced,toInvoiced,productId,modulequantity_,complexity,plannedhours,executedhours,chronogramDeviation,detentionCancellationDate,detentionCancellationCause,detentionCancellationDetails,observation,reprogrammed,reopeningDate,resumed_"
                )] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.areaId = new SelectList(db.Area, "areaId", "name", project.areaId);
            ViewBag.clientId = project.Client;
            ViewBag.productId = new SelectList(db.Product, "productId", "name", project.productId);

            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
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

        //Esatados
        public JsonResult EnProc(Int32 id)
        {
            ProjectState state = GetEstado(id);
            if (state.State1.name == "EnProceso")
            {
                return Json("0");
            }

            Project p = db.Project.Find(id);
            foreach (var e in p.ProjectState)
            {
                if (e.state)
                    e.state = false;
            }
            ProjectState ps = new ProjectState();
            ps.state = true;
            ps.State1 = db.State.Find(2);
            ps.date = DateTime.Now;
            p.ProjectState.Add(ps);
            db.SaveChanges();
            return Json("1");


        }

        public JsonResult DetP(int? id, string rason)
        {
            ProjectState state = GetEstado(id);
            if (state.State1.name == "Detenido")
            {
                return Json("0");
            }
            Project p = db.Project.Find(id);
            foreach (var e in p.ProjectState)
            {
                if (e.state)
                    e.state = false;
            }
            ProjectState ps = new ProjectState();
            ps.state = true;
            ps.State1 = db.State.Find(3);
            ps.date = DateTime.Now;
            ps.description = rason;
            p.ProjectState.Add(ps);
            db.SaveChanges();
            return Json("1");
        }

        public JsonResult CancProy(int? id, string rason)
        {
            ProjectState state = GetEstado(id);
            if (state.State1.name == "Cancelado")
            {
                return Json("0");
            }
            Project p = db.Project.Find(id);
            foreach (var e in p.ProjectState)
            {
                if (e.state)
                    e.state = false;
            }
            ProjectState ps = new ProjectState();
            ps.state = true;
            ps.State1 = db.State.Find(4);
            ps.date = DateTime.Now;
            ps.description = rason;
            p.ProjectState.Add(ps);
            db.SaveChanges();
            return Json("1");
        }

        public JsonResult GetAreas()
        {
            ICollection<Area> stages = db.Area.ToList();
            string fin = "{" + '"' + "Areas" + '"' + ":[";

            foreach (Area stage in stages)
            {
                fin += "{" + '"' + "nomb" + '"' + ':' + '"' + stage.name + '"' + "},";

            }
            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";

            return Json(fin);
        }

        public int IsSupDetoCanc(Int32? idsup, int? proysupid, int? iddet)
        {
            if (iddet != null)
            {
                ProjectDetails ed = db.ProjectDetails.Find(iddet);
                StateCSupplement stateCSupplement =
                    db.StateCSupplement.Where(st => st.supplementId == ed.ProjSup.supplementId && st.state)
                        .ToList()
                        .First();
                if (stateCSupplement.stateCId == 6 || stateCSupplement.stateCId == 7)
                {
                    return 1;
                }
                return 0;
            }
            if (proysupid != null)
            {
                ProjSup pSup = db.ProjSup.Find(proysupid);
                StateCSupplement stateCSupplement =
                    db.StateCSupplement.Where(st => st.supplementId == pSup.supplementId && st.state).ToList().First();
                if (stateCSupplement.stateCId == 6 || stateCSupplement.stateCId == 7)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                StateCSupplement stateCSupplement =
                    db.StateCSupplement.Where(st => st.supplementId == idsup && st.state).ToList().First();
                if (stateCSupplement.stateCId == 6 || stateCSupplement.stateCId == 7)
                {
                    return 1;
                }
                return 0;
            }


        }

        public JsonResult ProysState(int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            IEnumerable<Project> proys = db.Project.Where(x => x.startDate.Value.Year == year);

            int contterm = 0;
            int enproceso = 0;
            int detenido = 0;
            int cancelado = 0;
            foreach (var p in proys)
            {
                ProjectState ps = p.ProjectState.First(s => s.state && s.projectId == p.projectId);
                if (ps.stateId == 5)
                {
                    contterm++;
                }
                if (ps.stateId == 4)
                {
                    cancelado++;
                }
                if (ps.stateId == 3)
                {
                    detenido++;
                }
                if (ps.stateId == 2)
                {
                    enproceso++;
                }
            }
            string fin = "{" + '"' + "ArrayEstados" + '"' + ":[";
            fin += "{" + '"' + "Terminado" + '"' + ':' + '"' + contterm + '"'
                   + "," + '"' + "EnProceso" + '"' + ':' + '"' + enproceso + '"'
                   + "," + '"' + "Detenido" + '"' + ':' + '"' + detenido +
                   '"' + "," + '"' + "Cacelado" + '"' + ':' + '"' + cancelado + '"' + "},";


            fin = fin.Remove(fin.Count() - 1);
            fin += "]}";
            return Json(fin);

        }

       
    }


}
