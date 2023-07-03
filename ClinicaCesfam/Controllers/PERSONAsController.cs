using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaCesfam.Models;
//para el verificar el correo
using System.Text.RegularExpressions;

namespace ClinicaCesfam.Controllers
{
    public class PERSONAsController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: PERSONAs
        public ActionResult Index()
        {
            return View(db.PERSONA.ToList());
        }

        // GET: PERSONAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // GET: PERSONAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PERSONAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numrun,dv_run,pnombre,snombre,appaterno,apmaterno,direccion,correo,telefono")] PERSONA pERSONA)
        {
            //ALERTAS SI SE LLENAN MAL LOS CAMPOS O ESTAN VACIOS.
            //obtiene el largo de numrun para ver si es valio o no.
            string numrunString = pERSONA.numrun.ToString();
            if (7 > numrunString.Length || numrunString.Length > 8)
            {
                if (pERSONA.numrun <= 0)
                {
                    ModelState.AddModelError("numrun", "El numrun no puede ser 0 o negativo.");
                }
                ModelState.AddModelError("numrun", "El numrun no es valido");
            }

            if (string.IsNullOrEmpty(pERSONA.dv_run))
            {
                ModelState.AddModelError("dv_run", "El campo dv_run es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.pnombre))
            {
                ModelState.AddModelError("pnombre", "El campo pnombre es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.snombre))
            {
                ModelState.AddModelError("snombre", "El campo snombre es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.appaterno))
            {
                ModelState.AddModelError("appaterno", "El campo appaterno es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.apmaterno))
            {
                ModelState.AddModelError("apmaterno", "El campo apmaterno es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.direccion))
            {
                ModelState.AddModelError("direccion", "El campo direccion es obligatorio.");
            }

            //Correo Electronico
            if (string.IsNullOrEmpty(pERSONA.correo))
            {
                ModelState.AddModelError("correo", "El campo correo es obligatorio.");
            }
            //validar formato de correo a travez de la clase "using System.Text.RegularExpressions;"
            if (!Regex.IsMatch(pERSONA.correo ?? string.Empty, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                ModelState.AddModelError("correo", "El campo correo debe tener un formato válido.");
            }

            //obtiene el largo de telefono para ver si es valio o no.
            string telefonoString = pERSONA.telefono.ToString();
            if (telefonoString.Length != 9)
            {
                if (pERSONA.telefono <= 0)
                {
                    ModelState.AddModelError("telefono", "El telefono no puede ser 0 o negativo.");
                }
                ModelState.AddModelError("telefono", "El telefono no es valido formato: '912345678'");
            }

            if (ModelState.IsValid)
            {
                db.PERSONA.Add(pERSONA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pERSONA);
        }

        // GET: PERSONAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // POST: PERSONAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_persona,numrun,dv_run,pnombre,snombre,appaterno,apmaterno,direccion,correo,telefono")] PERSONA pERSONA)
        {
            //ALERTAS SI SE LLENAN MAL LOS CAMPOS O ESTAN VACIOS.
            //obtiene el largo de numrun para ver si es valio o no.
            string numrunString = pERSONA.numrun.ToString();
            if (7 > numrunString.Length || numrunString.Length > 8)
            {
                if (pERSONA.numrun <= 0)
                {
                    ModelState.AddModelError("numrun", "El numrun no puede ser 0 o negativo.");
                }
                ModelState.AddModelError("numrun", "El numrun no es valido");
            }

            if (string.IsNullOrEmpty(pERSONA.dv_run))
            {
                ModelState.AddModelError("dv_run", "El campo dv_run es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.pnombre))
            {
                ModelState.AddModelError("pnombre", "El campo pnombre es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.snombre))
            {
                ModelState.AddModelError("snombre", "El campo snombre es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.appaterno))
            {
                ModelState.AddModelError("appaterno", "El campo appaterno es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.apmaterno))
            {
                ModelState.AddModelError("apmaterno", "El campo apmaterno es obligatorio.");
            }

            if (string.IsNullOrEmpty(pERSONA.direccion))
            {
                ModelState.AddModelError("direccion", "El campo direccion es obligatorio.");
            }

            //Correo Electronico
            if (string.IsNullOrEmpty(pERSONA.correo))
            {
                ModelState.AddModelError("correo", "El campo correo es obligatorio.");
            }
            //validar formato de correo a travez de la clase "using System.Text.RegularExpressions;"
            if (!Regex.IsMatch(pERSONA.correo ?? string.Empty, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                ModelState.AddModelError("correo", "El campo correo debe tener un formato válido.");
            }

            //obtiene el largo de telefono para ver si es valio o no.
            string telefonoString = pERSONA.telefono.ToString();
            if (telefonoString.Length != 9)
            {
                if (pERSONA.telefono <= 0)
                {
                    ModelState.AddModelError("telefono", "El telefono no puede ser 0 o negativo.");
                }
                ModelState.AddModelError("telefono", "El telefono no es valido formato: '912345678'");
            }

            if (ModelState.IsValid)
            {
                db.Entry(pERSONA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERSONA);
        }

        // GET: PERSONAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // POST: PERSONAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PERSONA pERSONA = db.PERSONA.Find(id);
            db.PERSONA.Remove(pERSONA);
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
