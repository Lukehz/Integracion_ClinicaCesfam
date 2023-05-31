using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank;
using Transbank.Common;
using Transbank.Webpay;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace VistaFarmaceutico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));

            var amount = 2000;
            var buyOrder = new Random().Next(100000, 999999999).ToString();
            var sessionId = "sessionId";
            string returnUrl = "http://localhost:50463/Home/About";

            var response = tx.Create(buyOrder, sessionId, amount, returnUrl);

            var formAction = response.Url;
            var tokenWs = response.Token;


            @ViewBag.Amount = amount;
            @ViewBag.BuyOrder = buyOrder;
            @ViewBag.TokenWs = tokenWs;
            @ViewBag.FormAction = formAction;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}