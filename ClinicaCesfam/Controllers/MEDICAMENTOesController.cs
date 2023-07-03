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
    public class MEDICAMENTOesController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: MEDICAMENTOes
        public ActionResult Index()
        {
            return View(db.MEDICAMENTO.ToList());
        }

        // GET: MEDICAMENTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICAMENTO mEDICAMENTO = db.MEDICAMENTO.Find(id);
            if (mEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICAMENTO);
        }

        // GET: MEDICAMENTOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MEDICAMENTOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre_medica,fabricante_medica,gramaje_medica,exp_medica,stock_medica,precio")] MEDICAMENTO mEDICAMENTO)
        {
            if (mEDICAMENTO.stock_medica > 0)
            {
                if (mEDICAMENTO.Precio >0)
                {
                    if (ModelState.IsValid)
                    {
                        db.MEDICAMENTO.Add(mEDICAMENTO);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            if (string.IsNullOrEmpty(mEDICAMENTO.nombre_medica))
            {
                ModelState.AddModelError("nombre_medica", "El campo nombre_medica es obligatorio.");
            }

            if (string.IsNullOrEmpty(mEDICAMENTO.fabricante_medica))
            {
                ModelState.AddModelError("fabricante_medica", "El campo fabricante_medica es obligatorio.");
            }

            if (string.IsNullOrEmpty(mEDICAMENTO.gramaje_medica))
            {
                ModelState.AddModelError("gramaje_medica", "El campo gramaje_medica es obligatorio.");
            }
            if (mEDICAMENTO.stock_medica <= 0)
            {
                ModelState.AddModelError("stock_medica", "El stock_medica no puede ser 0 o negativo.");
            }
            if (mEDICAMENTO.Precio <= 0)
            {
                ModelState.AddModelError("Precio", "El Precio no puede ser 0 o negativo.");
            }


            return View(mEDICAMENTO);
        }

        // GET: MEDICAMENTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICAMENTO mEDICAMENTO = db.MEDICAMENTO.Find(id);
            if (mEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICAMENTO);
        }

        // POST: MEDICAMENTOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_medica,nombre_medica,fabricante_medica,gramaje_medica,exp_medica,stock_medica,precio")] MEDICAMENTO mEDICAMENTO)
        {
            if (string.IsNullOrEmpty(mEDICAMENTO.nombre_medica))
            {
                ModelState.AddModelError("nombre_medica", "El campo nombre_medica es obligatorio.");
            }

            if (string.IsNullOrEmpty(mEDICAMENTO.fabricante_medica))
            {
                ModelState.AddModelError("fabricante_medica", "El campo fabricante_medica es obligatorio.");
            }

            if (string.IsNullOrEmpty(mEDICAMENTO.gramaje_medica))
            {
                ModelState.AddModelError("gramaje_medica", "El campo gramaje_medica es obligatorio.");
            }

            if (mEDICAMENTO.stock_medica <= 0)
            {
                ModelState.AddModelError("stock_medica", "El stock_medica no puede ser 0 o negativo.");
            }
            if (mEDICAMENTO.Precio <= 0)
            {
                ModelState.AddModelError("Precio", "El Precio no puede ser 0 o negativo.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(mEDICAMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mEDICAMENTO);
        }

        // GET: MEDICAMENTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICAMENTO mEDICAMENTO = db.MEDICAMENTO.Find(id);
            if (mEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICAMENTO);
        }

        // POST: MEDICAMENTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MEDICAMENTO mEDICAMENTO = db.MEDICAMENTO.Find(id);
            db.MEDICAMENTO.Remove(mEDICAMENTO);
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
