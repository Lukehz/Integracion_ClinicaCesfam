using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;

namespace transbankk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            var amount = 2000;
            var buyOrder = new Random().Next(100000, 999999999).ToString();
            var sessionId = "sessionId";

            var urlReturn = "https://localhost:44320/Home/Return";
            var urlFinal = "https://localhost:44320/Home/Final";

            var initResult = transaction.initTransaction(amount, buyOrder, sessionId, urlReturn, urlFinal);

            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Amount = amount;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.TokenWs = tokenWs;
            ViewBag.FormAction = formAction;
            return View();
        }

        public ActionResult Final()
        {
            return View();
        }

        public ActionResult Return()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];

            var result = transaction.getTransactionResult(tokenWs);
            var output = result.detailOutput[0];
            if (output.responseCode == 0)
            {
                ViewBag.UrlRedirection = result.urlRedirection;
                ViewBag.TokenWs = tokenWs;
                ViewBag.ResponseCode = output.responseCode;
                ViewBag.Amount = output.amount;
                ViewBag.AuthorizationCode = output.authorizationCode;
            }

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