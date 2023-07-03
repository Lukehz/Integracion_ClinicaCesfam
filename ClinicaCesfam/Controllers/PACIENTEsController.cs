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
    public class PACIENTEsController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: PACIENTEs
        public ActionResult Index()
        {
            var pACIENTE = db.PACIENTE.Include(p => p.PERSONA);
            return View(pACIENTE.ToList());
        }

        // GET: PACIENTEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            return View(pACIENTE);
        }

        // GET: PACIENTEs/Create
        public ActionResult Create()
        {
            List<SelectListItem> opciones = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "0" },
                new SelectListItem { Value = "1", Text = "1" }
            };

            ViewBag.enfermedad_cronica = new SelectList(opciones, "Value", "Text");
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "id_persona");
            return View();
        }

        // POST: PACIENTEs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "enfermedad_cronica,nombre_enfermedad,id_persona")] PACIENTE pACIENTE)
        {
            if (pACIENTE.enfermedad_cronica == "1")
            {
                if (string.IsNullOrEmpty(pACIENTE.nombre_enfermedad))
                {
                    ModelState.AddModelError("nombre_enfermedad", "El campo nombre_enfermedad es obligatorio.");
                }
            }

            if (db.PACIENTE.Any(m => m.id_persona == pACIENTE.id_persona))
            {
                ModelState.AddModelError("id_persona", "El id_persona ya existe.");
            }

            if (ModelState.IsValid)
            {
                db.PACIENTE.Add(pACIENTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> opciones = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "0" },
                new SelectListItem { Value = "1", Text = "1" }
            };

            ViewBag.enfermedad_cronica = new SelectList(opciones, "Value", "Text", pACIENTE.enfermedad_cronica);
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "id_persona", pACIENTE.id_persona);
            return View(pACIENTE);
        }

        // GET: PACIENTEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> opciones = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "0" },
                new SelectListItem { Value = "1", Text = "1" }
            };

            ViewBag.enfermedad_cronica = new SelectList(opciones, "Value", "Text", pACIENTE.enfermedad_cronica);
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "id_persona", pACIENTE.id_persona);
            return View(pACIENTE);
        }

        // POST: PACIENTEs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_paciente,enfermedad_cronica,nombre_enfermedad,id_persona")] PACIENTE pACIENTE)
        {
            if(pACIENTE.enfermedad_cronica == "1")
            {
                if (string.IsNullOrEmpty(pACIENTE.nombre_enfermedad))
                {
                    ModelState.AddModelError("nombre_enfermedad", "El campo nombre_enfermedad es obligatorio.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(pACIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> opciones = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "0" },
                new SelectListItem { Value = "1", Text = "1" }
            };

            ViewBag.enfermedad_cronica = new SelectList(opciones, "Value", "Text", pACIENTE.enfermedad_cronica);
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "id_persona", pACIENTE.id_persona);
            return View(pACIENTE);
        }

        // GET: PACIENTEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            return View(pACIENTE);
        }

        // POST: PACIENTEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            db.PACIENTE.Remove(pACIENTE);
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
