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
    public class HOJA_ATENCIONController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: HOJA_ATENCION
        public ActionResult Index()
        {
            var hOJA_ATENCION = db.HOJA_ATENCION.Include(h => h.MEDICO).Include(h => h.PACIENTE);
            return View(hOJA_ATENCION.ToList());
        }

        // GET: HOJA_ATENCION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOJA_ATENCION hOJA_ATENCION = db.HOJA_ATENCION.Find(id);
            if (hOJA_ATENCION == null)
            {
                return HttpNotFound();
            }
            return View(hOJA_ATENCION);
        }

        // GET: HOJA_ATENCION/Create
        public ActionResult Create()
        {
            ViewBag.id_med = new SelectList(db.MEDICO, "id_med", "id_med");
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente"); 
            //ViewBag.receta = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica");
            ViewBag.receta = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            return View();
        }

        // POST: HOJA_ATENCION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_hoja,ingreso,observacion,id_paciente,id_med")] HOJA_ATENCION hOJA_ATENCION)
        {
            if (ModelState.IsValid)
            {
                db.HOJA_ATENCION.Add(hOJA_ATENCION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_med = new SelectList(db.MEDICO, "id_med", "cargo_med", hOJA_ATENCION.id_med);
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "enfermedad_cronica", hOJA_ATENCION.id_paciente);
            return View(hOJA_ATENCION);
        }

        // GET: HOJA_ATENCION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOJA_ATENCION hOJA_ATENCION = db.HOJA_ATENCION.Find(id);
            if (hOJA_ATENCION == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_med = new SelectList(db.MEDICO, "id_med", "id_med", hOJA_ATENCION.id_med);
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente", hOJA_ATENCION.id_paciente);
            ViewBag.receta = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            return View(hOJA_ATENCION);
        }

        // POST: HOJA_ATENCION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_hoja,ingreso,observacion,id_paciente,id_med")] HOJA_ATENCION hOJA_ATENCION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOJA_ATENCION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_med = new SelectList(db.MEDICO, "id_med", "cargo_med", hOJA_ATENCION.id_med);
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "enfermedad_cronica", hOJA_ATENCION.id_paciente);
            return View(hOJA_ATENCION);
        }

        // GET: HOJA_ATENCION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOJA_ATENCION hOJA_ATENCION = db.HOJA_ATENCION.Find(id);
            if (hOJA_ATENCION == null)
            {
                return HttpNotFound();
            }
            return View(hOJA_ATENCION);
        }

        // POST: HOJA_ATENCION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOJA_ATENCION hOJA_ATENCION = db.HOJA_ATENCION.Find(id);
            db.HOJA_ATENCION.Remove(hOJA_ATENCION);
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
