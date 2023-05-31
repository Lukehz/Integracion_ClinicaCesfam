using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank;
using Transbank.Common;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace VistaFarmaceutico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Constructor con parametros obtenidos del transbankSdk codigo de comercio, api keys,y integracion.test ".test" especifica que es de testeo.
            //Todo esto es integreacion por eso "Integration...WEBPAY" y "WebpayIntegrationType.Test" de testeo
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));

            var amount = 2000;
            var buyOrder = new Random().Next(100000, 999999999).ToString();
            var sessionId = "sessionId";
            string returnUrl = "http://localhost:50463/Home/About";

            var response = tx.Create(buyOrder, sessionId, amount, returnUrl);

            var formAction = response.Url;
            var tokenWs = response.Token;

            
            ViewBag.Amount = amount;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.TokenWs = tokenWs;
            ViewBag.FormAction = formAction;
            return View();
        }

        public ActionResult About(string token_ws)
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