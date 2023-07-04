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
    public class Datos_pacienteController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: Datos_paciente
        public ActionResult Index()
        {
            return View(db.Datos_paciente.ToList());
        }
        /*
        // GET: Datos_paciente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos_paciente datos_paciente = db.Datos_paciente.Find(id);
            if (datos_paciente == null)
            {
                return HttpNotFound();
            }
            return View(datos_paciente);
        }

        // GET: Datos_paciente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Datos_paciente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_persona,id_paciente,Nombre_Completo,Rut,Enfermedad_Cronica,Enfermedad,Telefono,Correo,Direccion")] Datos_paciente datos_paciente)
        {
            if (ModelState.IsValid)
            {
                db.Datos_paciente.Add(datos_paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datos_paciente);
        }

        // GET: Datos_paciente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos_paciente datos_paciente = db.Datos_paciente.Find(id);
            if (datos_paciente == null)
            {
                return HttpNotFound();
            }
            return View(datos_paciente);
        }

        // POST: Datos_paciente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_persona,id_paciente,Nombre_Completo,Rut,Enfermedad_Cronica,Enfermedad,Telefono,Correo,Direccion")] Datos_paciente datos_paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datos_paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datos_paciente);
        }

        // GET: Datos_paciente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos_paciente datos_paciente = db.Datos_paciente.Find(id);
            if (datos_paciente == null)
            {
                return HttpNotFound();
            }
            return View(datos_paciente);
        }

        // POST: Datos_paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Datos_paciente datos_paciente = db.Datos_paciente.Find(id);
            db.Datos_paciente.Remove(datos_paciente);
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
        }*/
    }
}
