using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaCesfam.Models;
using ClinicaCesfam.ViewModels;
using System.Data.Entity;
using Transbank;
using Transbank.Common;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace ClinicaCesfam.Controllers
{
    public class ReservaPagoController : Controller
    {

        private clinicaEntities db = new clinicaEntities();
        // GET: ReservaPago
        
        //index
        public ActionResult Index()
        {
            return View(db.MEDICAMENTO.ToList());
        }

        //detalles
        public ActionResult PagoReserva(int? id)
        {
            //el id_paciente inicializa en 1
            RESERVA rESERVA = db.RESERVA.FirstOrDefault(r => r.id_paciente == 1);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //inner join para PERSONA.id_persona==PACIENTE.id_paciente para obtener el rut 
            var pacientes = db.PERSONA
            .Join(db.PACIENTE, p => p.id_persona, pa => pa.id_persona, (p, pa) => new { p.numrun, pa.id_paciente })
            .ToList();
            //Crea la lista desplegable
            ViewBag.id_paciente = new SelectList(pacientes, "id_paciente", "numrun");
            //Busca el medicamento por su id
            MEDICAMENTO mEDICAMENTO = db.MEDICAMENTO.Find(id);
            //Crea el viewModel que contiene Models.MEDICAMENTO Y Models.RESERVA
            var viewModel = new MedicamentoReservaViewModel
            {
                Medicamento = mEDICAMENTO,
                Reserva = rESERVA
            };
            if (mEDICAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Create, mas la transaccion Transbank, recibe el total a pagar y sessionid
        public ActionResult PagoReserva([Bind(Include = "id_reserva,cantidad,id_paciente,id_medica")] RESERVA rESERVA, int? id, int? totalHidden, string usuarioReserva)
        {
            //TRANSBANK
            //Constructor con parametros obtenidos del transbankSdk codigo de comercio, api keys,y integracion.test ".test" especifica que es de testeo.
            //Todo esto es integreacion por eso "Integration...WEBPAY" y "WebpayIntegrationType.Test" de testeo
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));


            var buyOrder = new Random().Next(100000, 999999999).ToString();
            //condiciones para que los valores a enviar a transbank no sean nulos.
            if (totalHidden != null)
            {
                var amount = (int)totalHidden;
                if (usuarioReserva != null)
                {
                    if (usuarioReserva != "")
                    { 

                        usuarioReserva = usuarioReserva.Replace(" ", "_");
                        var sessionId = usuarioReserva;
                        string returnUrl = "https://localhost:44321/ReservaPago/Comprobante";
                        //CREA LA TRANSACCION 
                        var response = tx.Create(buyOrder, sessionId, amount, returnUrl);
                        //AL CREAR LA TRANSACCION, TRANSBANK ENVIE URL DE LA PAGINA DE TRANSACCION Y EL TOKEN QUE IDENTIFICA LA TRANSACCION
                        //RECIBO URL Y TOKEN Y LAS ALMACENO
                        var formAction = response.Url;
                        var tokenWs = response.Token;
                        //CREO UNA SOLA URL Y AL ACCIONAR EL BOTON PAGAR EN EL INDEX.CSHTML EN CARRO ME ENVIA A LA URL
                        var url = formAction + "?token_ws=" + tokenWs;

                        ViewBag.buyOrder = buyOrder;
                        ViewBag.url = url;
                        {
                            if (ModelState.IsValid)
                            {
                                db.RESERVA.Add(rESERVA);
                                db.SaveChanges();
                                return Redirect(url);
                            }
                        }
                    }
                }
            }
            MEDICAMENTO mEDICAMENTO = db.MEDICAMENTO.Find(id);
            var viewModel = new MedicamentoReservaViewModel
            {
                Medicamento = mEDICAMENTO,
                Reserva = rESERVA
            };
            //inner join para PERSONA.id_persona==PACIENTE.id_paciente para obtener el rut 
            var pacientes = db.PERSONA
            .Join(db.PACIENTE, p => p.id_persona, pa => pa.id_persona, (p, pa) => new { p.numrun, pa.id_paciente })
            .ToList();
            //Crea la lista desplegable
            ViewBag.id_paciente = new SelectList(pacientes, "id_paciente", "numrun");
            return View(viewModel);
        }

        public ActionResult Comprobante(string token_ws)
        {
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
            var response = tx.Commit(token_ws);


            //este es del commit
            ViewBag.Vci = response.Vci;
            ViewBag.Amount = response.Amount;
            ViewBag.Status = response.Status;
            ViewBag.BuyOrder = response.BuyOrder;
            ViewBag.SessionId = response.SessionId;
            ViewBag.CardDetail = response.CardDetail;
            ViewBag.AccountingDate = response.AccountingDate;
            ViewBag.TransactionDate = response.TransactionDate;
            ViewBag.AuthorizationCode = response.AuthorizationCode;
            ViewBag.PaymentTypeCode = response.PaymentTypeCode;
            ViewBag.ResponseCode = response.ResponseCode;
            ViewBag.InstallmentsAmount = response.InstallmentsAmount;
            ViewBag.InstallmentsNumber = response.InstallmentsNumber;
            ViewBag.Balance = response.Balance;

            var responsee = tx.Status(token_ws);

            //este es el status
            ViewBag.ci = responsee.Vci;
            ViewBag.mount = responsee.Amount;
            ViewBag.tatus = responsee.Status;
            ViewBag.uyOrder = responsee.BuyOrder;
            ViewBag.essionId = responsee.SessionId;
            ViewBag.ardDetail = responsee.CardDetail;
            ViewBag.ccountingDate = responsee.AccountingDate;
            ViewBag.ransactionDate = responsee.TransactionDate;
            ViewBag.uthorizationCode = responsee.AuthorizationCode;
            ViewBag.aymentTypeCode = responsee.PaymentTypeCode;
            ViewBag.esponseCode = responsee.ResponseCode;
            ViewBag.nstallmentsAmount = responsee.InstallmentsAmount;
            ViewBag.nstallmentsNumber = responsee.InstallmentsNumber;
            ViewBag.alance = responsee.Balance;
            return View();
        }
        //Obtiene el nombre y apellido a traves del id_paciente en la vista se ve mediante el rut
        //recibe el parametro id_paciente por que literal recibe el id_paciente de @Html.DropDownList
        public JsonResult ObtenerNombreApellido(int id_paciente)
        {   //define al paciente por su id_paciente(PACIENTE.id_paciente == id_paciente(Parametro obtenido))
            var paciente = db.PACIENTE.FirstOrDefault(p => p.id_paciente == id_paciente);

            if (paciente != null)
            {   
                //define la persona por el id_persona(PERSONA.id_persona == PACIENTE.id_persona)
                var persona = db.PERSONA.FirstOrDefault(p => p.id_persona == paciente.id_persona);
                //si la persona tiene valor
                if (persona != null)
                {
                    //Concatena nombre y apellido
                    string nombreApellido = persona.pnombre + " " + persona.appaterno;
                    //retorna el nombre mediante json
                    return Json(nombreApellido, JsonRequestBehavior.AllowGet);
                }
            }
            //no devuelve nada
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
