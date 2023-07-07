using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ClinicaCesfam.Models;

namespace ClinicaCesfam.Controllers
{
    public class HomeController : Controller
    {
        private clinicaEntities db = new clinicaEntities();
        public ActionResult Index()
        {
            //Recuento de pacientes
            var pACIENTE = db.PACIENTE.Include(p => p.PERSONA);
            ViewBag.pacientes = pACIENTE.Count();

            //Recuento de pacientes
            
            ViewBag.medicamentos = db.MEDICAMENTO.Count();

            //Recuento de pacientes
            var mEDICO = db.MEDICO.Include(p => p.PERSONA);
            ViewBag.medicos = mEDICO.Count();

            var stockMedicamentos = db.Stock_medicamentos.ToList();

            // Asignar los datos a ViewBag.StockMedicamentos
            ViewBag.StockMedicamentos = stockMedicamentos;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}