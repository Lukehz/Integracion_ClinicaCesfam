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
    public class Datos_medicoController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: Datos_medico
        public ActionResult Index()
        {
            return View(db.Datos_medico.ToList());
        }
        /*
        // GET: Datos_medico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos_medico datos_medico = db.Datos_medico.Find(id);
            if (datos_medico == null)
            {
                return HttpNotFound();
            }
            return View(datos_medico);
        }

        // GET: Datos_medico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Datos_medico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_persona,id_medico,Nombre_Completo,Rut,Cargo_Medico,Telefono,Correo,Direccion")] Datos_medico datos_medico)
        {
            if (ModelState.IsValid)
            {
                db.Datos_medico.Add(datos_medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datos_medico);
        }

        // GET: Datos_medico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos_medico datos_medico = db.Datos_medico.Find(id);
            if (datos_medico == null)
            {
                return HttpNotFound();
            }
            return View(datos_medico);
        }

        // POST: Datos_medico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_persona,id_medico,Nombre_Completo,Rut,Cargo_Medico,Telefono,Correo,Direccion")] Datos_medico datos_medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datos_medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datos_medico);
        }

        // GET: Datos_medico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos_medico datos_medico = db.Datos_medico.Find(id);
            if (datos_medico == null)
            {
                return HttpNotFound();
            }
            return View(datos_medico);
        }

        // POST: Datos_medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Datos_medico datos_medico = db.Datos_medico.Find(id);
            db.Datos_medico.Remove(datos_medico);
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
