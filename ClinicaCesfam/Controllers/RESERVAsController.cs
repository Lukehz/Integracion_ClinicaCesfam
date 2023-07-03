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
    public class RESERVAsController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: RESERVAs
        public ActionResult Index()
        {
            var rESERVA = db.RESERVA.Include(r => r.MEDICAMENTO).Include(r => r.PACIENTE);
            return View(rESERVA.ToList());
        }

        // GET: RESERVAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESERVA rESERVA = db.RESERVA.Find(id);
            if (rESERVA == null)
            {
                return HttpNotFound();
            }
            return View(rESERVA);
        }

        // GET: RESERVAs/Create
        public ActionResult Create()
        {
            //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica");
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new {id_medica = m.id_medica,nombre_medica = m.nombre_medica + " - " + m.fabricante_medica+ " - "+ m.gramaje_medica }), "id_medica", "nombre_medica");
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente");
            return View();
        }

        // POST: RESERVAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cantidad,id_paciente,id_medica")] RESERVA rESERVA)
        {
            if (rESERVA.cantidad > 0)
            {
                if (ModelState.IsValid)
                {
                    db.RESERVA.Add(rESERVA);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica");
                ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
                ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente");
            }
            if (rESERVA.cantidad <= 0)
            {
                ModelState.AddModelError("cantidad", "La cantidad no puede ser 0 o negativo.");
            }

            //ViewBag.id_medica = new SelectList(db.MEDICAMENTO, "id_medica", "nombre_medica");
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente", rESERVA.id_paciente);
            return View(rESERVA);
        }

        // GET: RESERVAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESERVA rESERVA = db.RESERVA.Find(id);
            if (rESERVA == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente", rESERVA.id_paciente);
            return View(rESERVA);
        }

        // POST: RESERVAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reserva,cantidad,id_paciente,id_medica")] RESERVA rESERVA)
        {
            if (rESERVA.cantidad > 0)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(rESERVA).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            if (rESERVA.cantidad <= 0)
            {
                ModelState.AddModelError("cantidad", "La cantidad no puede ser 0 o negativo.");
            }

            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente", rESERVA.id_paciente);
            return View(rESERVA);
        }

        // GET: RESERVAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESERVA rESERVA = db.RESERVA.Find(id);
            if (rESERVA == null)
            {
                return HttpNotFound();
            }
            return View(rESERVA);
        }

        // POST: RESERVAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RESERVA rESERVA = db.RESERVA.Find(id);
            db.RESERVA.Remove(rESERVA);
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
