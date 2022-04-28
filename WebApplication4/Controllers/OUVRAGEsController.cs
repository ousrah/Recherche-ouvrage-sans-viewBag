using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4;
using WebApplication4.Models;


namespace WebApplication4.Controllers
{
    public class OUVRAGEsController : Controller
    {
        private librairieEntities db = new librairieEntities();

        // GET: OUVRAGEs
        [HttpPost]
        public ActionResult Index(string chercher, string classification)
        {
             var oUVRAGE = db.OUVRAGE.Include(o => o.CLASSIFICATION).Include(o => o.EDITEUR);

            if (!string.IsNullOrEmpty(chercher))
                oUVRAGE = oUVRAGE.Where(o => o.NOMOUVR.Contains(chercher));
           
            if (!string.IsNullOrEmpty(classification))
            {           
                int cl = Convert.ToInt32(classification);
                oUVRAGE = oUVRAGE.Where(o => o.CLASSIFICATION.NUMRUB.Equals(cl));
  
            }
            var a = new ClaOuv
            {
                Ouvrages = oUVRAGE.ToList(),
                Classifications = db.CLASSIFICATION.ToList(),
                selectedClassification=classification,
                chercher=chercher
            };


            return View(a);
        }

        // GET: OUVRAGEs
        public  ActionResult Index()
        {


            var oUVRAGE = db.OUVRAGE.Include(o => o.CLASSIFICATION).Include(o => o.EDITEUR);

            var a = new ClaOuv
            {
                Ouvrages = oUVRAGE.ToList(),
                Classifications = db.CLASSIFICATION.ToList(),
                selectedClassification = "",
                chercher=""
            };

            return View(a);
        }

        // GET: OUVRAGEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OUVRAGE oUVRAGE = db.OUVRAGE.Find(id);
            if (oUVRAGE == null)
            {
                return HttpNotFound();
            }
            return View(oUVRAGE);
        }

        // GET: OUVRAGEs/Create
        public ActionResult Create()
        {
            ViewBag.NUMRUB = new SelectList(db.CLASSIFICATION, "NUMRUB", "LIBRUB");
            ViewBag.NOMED = new SelectList(db.EDITEUR, "NOMED", "ADRED");
            return View();
        }

        // POST: OUVRAGEs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NUMOUVR,NOMOUVR,ANNEEPARU,NUMRUB,NOMED")] OUVRAGE oUVRAGE)
        {
            if (ModelState.IsValid)
            {
                db.OUVRAGE.Add(oUVRAGE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NUMRUB = new SelectList(db.CLASSIFICATION, "NUMRUB", "LIBRUB", oUVRAGE.NUMRUB);
            ViewBag.NOMED = new SelectList(db.EDITEUR, "NOMED", "ADRED", oUVRAGE.NOMED);
            return View(oUVRAGE);
        }

        // GET: OUVRAGEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OUVRAGE oUVRAGE = db.OUVRAGE.Find(id);
            if (oUVRAGE == null)
            {
                return HttpNotFound();
            }
            ViewBag.NUMRUB = new SelectList(db.CLASSIFICATION, "NUMRUB", "LIBRUB", oUVRAGE.NUMRUB);
            ViewBag.NOMED = new SelectList(db.EDITEUR, "NOMED", "ADRED", oUVRAGE.NOMED);
            return View(oUVRAGE);
        }

        // POST: OUVRAGEs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NUMOUVR,NOMOUVR,ANNEEPARU,NUMRUB,NOMED")] OUVRAGE oUVRAGE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oUVRAGE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NUMRUB = new SelectList(db.CLASSIFICATION, "NUMRUB", "LIBRUB", oUVRAGE.NUMRUB);
            ViewBag.NOMED = new SelectList(db.EDITEUR, "NOMED", "ADRED", oUVRAGE.NOMED);
            return View(oUVRAGE);
        }

        // GET: OUVRAGEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OUVRAGE oUVRAGE = db.OUVRAGE.Find(id);
            if (oUVRAGE == null)
            {
                return HttpNotFound();
            }
            return View(oUVRAGE);
        }

        // POST: OUVRAGEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OUVRAGE oUVRAGE = db.OUVRAGE.Find(id);
            db.OUVRAGE.Remove(oUVRAGE);
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
