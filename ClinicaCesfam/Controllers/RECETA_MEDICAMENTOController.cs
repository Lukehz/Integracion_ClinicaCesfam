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
    public class RECETA_MEDICAMENTOController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: RECETA_MEDICAMENTO
        public ActionResult Index()
        {
            var rECETA_MEDICAMENTO = db.RECETA_MEDICAMENTO.Include(r => r.HOJA_ATENCION).Include(r => r.MEDICAMENTO);
            return View(rECETA_MEDICAMENTO.ToList());
        }

        // GET: RECETA_MEDICAMENTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECETA_MEDICAMENTO rECETA_MEDICAMENTO = db.RECETA_MEDICAMENTO.Find(id);
            if (rECETA_MEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(rECETA_MEDICAMENTO);
        }

        // GET: RECETA_MEDICAMENTO/Create
        public ActionResult Create()
        {
            ViewBag.id_hoja = new SelectList(db.HOJA_ATENCION, "id_hoja", "id_hoja");
            //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica");
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            return View();
        }

        // POST: RECETA_MEDICAMENTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_hoja,id_medica")] RECETA_MEDICAMENTO rECETA_MEDICAMENTO)
        {
            if (ModelState.IsValid)
            {
                db.RECETA_MEDICAMENTO.Add(rECETA_MEDICAMENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_hoja = new SelectList(db.HOJA_ATENCION, "id_hoja", "id_hoja", rECETA_MEDICAMENTO.id_hoja);
            //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "id_medica", rECETA_MEDICAMENTO.id_medica);
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            return View(rECETA_MEDICAMENTO);
        }

        // GET: RECETA_MEDICAMENTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECETA_MEDICAMENTO rECETA_MEDICAMENTO = db.RECETA_MEDICAMENTO.Find(id);
            if (rECETA_MEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_hoja = new SelectList(db.HOJA_ATENCION, "id_hoja", "id_hoja", rECETA_MEDICAMENTO.id_hoja);
            //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica", rECETA_MEDICAMENTO.id_medica);
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            return View(rECETA_MEDICAMENTO);
        }

        // POST: RECETA_MEDICAMENTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_receta_medica,id_hoja,id_medica")] RECETA_MEDICAMENTO rECETA_MEDICAMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECETA_MEDICAMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_hoja = new SelectList(db.HOJA_ATENCION, "id_hoja", "id_hoja", rECETA_MEDICAMENTO.id_hoja);
            //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica", rECETA_MEDICAMENTO.id_medica);
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            return View(rECETA_MEDICAMENTO);
        }

        // GET: RECETA_MEDICAMENTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECETA_MEDICAMENTO rECETA_MEDICAMENTO = db.RECETA_MEDICAMENTO.Find(id);
            if (rECETA_MEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(rECETA_MEDICAMENTO);
        }

        // POST: RECETA_MEDICAMENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RECETA_MEDICAMENTO rECETA_MEDICAMENTO = db.RECETA_MEDICAMENTO.Find(id);
            db.RECETA_MEDICAMENTO.Remove(rECETA_MEDICAMENTO);
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
