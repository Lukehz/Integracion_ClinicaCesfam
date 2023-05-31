using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaCesfam.Models;

namespace ClinicaCesfam.Controllers
{
    public class MedicosController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: Medicos
        public ActionResult Index()
        {
            var mEDICO = db.MEDICO.Include(m => m.PERSONA);
            return View(mEDICO.ToList());
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO mEDICO = db.MEDICO.Find(id);
            if (mEDICO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICO);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "id_persona");
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_med,cargo_med,id_persona")] MEDICO mEDICO)
        {
            if (ModelState.IsValid)
            {
                db.MEDICO.Add(mEDICO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "dv_run", mEDICO.id_persona);
            return View(mEDICO);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO mEDICO = db.MEDICO.Find(id);
            if (mEDICO == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "id_persona", mEDICO.id_persona);
            return View(mEDICO);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_med,cargo_med,id_persona")] MEDICO mEDICO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEDICO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "dv_run", mEDICO.id_persona);
            return View(mEDICO);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO mEDICO = db.MEDICO.Find(id);
            if (mEDICO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICO);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MEDICO mEDICO = db.MEDICO.Find(id);
            db.MEDICO.Remove(mEDICO);
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
