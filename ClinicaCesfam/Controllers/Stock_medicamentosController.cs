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
    public class Stock_medicamentosController : Controller
    {
        private clinicaEntities db = new clinicaEntities();

        // GET: Stock_medicamentos
        public ActionResult Index()
        {
            return View(db.Stock_medicamentos.ToList());
        }

        // GET: Stock_medicamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_medicamentos stock_medicamentos = db.Stock_medicamentos.Find(id);
            if (stock_medicamentos == null)
            {
                return HttpNotFound();
            }
            return View(stock_medicamentos);
        }

        // GET: Stock_medicamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock_medicamentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_medica,Stock_medicamento,Reservado,Restante")] Stock_medicamentos stock_medicamentos)
        {
            if (ModelState.IsValid)
            {
                db.Stock_medicamentos.Add(stock_medicamentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock_medicamentos);
        }

        // GET: Stock_medicamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_medicamentos stock_medicamentos = db.Stock_medicamentos.Find(id);
            if (stock_medicamentos == null)
            {
                return HttpNotFound();
            }
            return View(stock_medicamentos);
        }

        // POST: Stock_medicamentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_medica,Stock_medicamento,Reservado,Restante")] Stock_medicamentos stock_medicamentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock_medicamentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock_medicamentos);
        }

        // GET: Stock_medicamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_medicamentos stock_medicamentos = db.Stock_medicamentos.Find(id);
            if (stock_medicamentos == null)
            {
                return HttpNotFound();
            }
            return View(stock_medicamentos);
        }

        // POST: Stock_medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock_medicamentos stock_medicamentos = db.Stock_medicamentos.Find(id);
            db.Stock_medicamentos.Remove(stock_medicamentos);
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
