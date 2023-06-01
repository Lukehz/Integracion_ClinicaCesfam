using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Transbank;
using Transbank.Common;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;
using ClinicaCesfam.Models;

namespace ClinicaCesfam.Controllers
{
    public class CarroController : Controller
    {
        private clinicaEntities db = new clinicaEntities();


        public ActionResult Index()
        {


            ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica");
            ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente");
            //var buyOrderr = new Random().Next(100000, 999999999).ToString();
            //ViewBag.buyOrder = buyOrderr;
            /*
            //TRANSBANK
            //Constructor con parametros obtenidos del transbankSdk codigo de comercio, api keys,y integracion.test ".test" especifica que es de testeo.
            //Todo esto es integreacion por eso "Integration...WEBPAY" y "WebpayIntegrationType.Test" de testeo
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));

            var amount = 2000;
            var buyOrder = new Random().Next(100000, 999999999).ToString();
            var sessionId = "sessionId";
            string returnUrl = "http://localhost:51783/Carro/Comprobante";

            var response = tx.Create(buyOrder, sessionId, amount, returnUrl);

            var formAction = response.Url;
            var tokenWs = response.Token;

            var url = formAction + "?token_ws=" + tokenWs;

            ViewBag.url = url;

            ViewBag.Amount = amount;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.TokenWs = tokenWs;
            ViewBag.FormAction = formAction;
            */
            return View();
        }
        // POST: RESERVAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id_reserva,cantidad,id_paciente,id_medica")] RESERVA rESERVA, int inputValue)
        {
            //TRANSBANK
            //Constructor con parametros obtenidos del transbankSdk codigo de comercio, api keys,y integracion.test ".test" especifica que es de testeo.
            //Todo esto es integreacion por eso "Integration...WEBPAY" y "WebpayIntegrationType.Test" de testeo
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));

            int amount = 2000;
            amount = inputValue;
            var buyOrder = new Random().Next(100000, 999999999).ToString();
            var sessionId = "sessionId";
            string returnUrl = "http://localhost:51783/Carro/Comprobante";

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
                ViewBag.buyOrder = buyOrder;
                ViewBag.id_medica = new SelectList(db.MEDICAMENTO.Select(m => new { id_medica = m.id_medica, nombre_medica = m.nombre_medica + " - " + m.fabricante_medica + " - " + m.gramaje_medica }), "id_medica", "nombre_medica", rESERVA.id_medica);
                ViewBag.id_paciente = new SelectList(db.PACIENTE, "id_paciente", "id_paciente", rESERVA.id_paciente);
                return View();
            }
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
    }
}