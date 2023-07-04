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
            rESERVA.cantidad = 0;
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
        public ActionResult PagoReserva([Bind(Include = "cantidad,id_paciente,id_medica")] RESERVA rESERVA, int? id, int? totalHidden, string usuarioReserva)
        {
            if (rESERVA.cantidad <=0)
            {
                ModelState.AddModelError("Reserva.cantidad", "El cantidad no puede ser 0 o negativo.");
            }
            //TRANSBANK
            //Constructor con parametros obtenidos del transbankSdk codigo de comercio, api keys,y integracion.test ".test" especifica que es de testeo.
            //Todo esto es integreacion por eso "Integration...WEBPAY" y "WebpayIntegrationType.Test" de testeo
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));


            var buyOrder = new Random().Next(100000, 999999999).ToString();
            //condiciones para que los valores a enviar a transbank no sean nulos.
            if (totalHidden != null && totalHidden > 0)
            {
                var amount = (int)totalHidden;

                if (usuarioReserva != null && usuarioReserva != "")
                {

                    usuarioReserva = usuarioReserva.Replace(" ", "_");
                    var sessionId = usuarioReserva;
                    string returnUrl = "https://localhost:44321/ReservaPago/Resultado";

                    //Crea la transaccion
                    var response = tx.Create(buyOrder, sessionId, amount, returnUrl);

                    //Al crear la transacción, Transbank envíe URL de la página de transaccion y el token que identifica la transaccion
                    //Recibo URL y TOKEN y las almaceno
                    var formAction = response.Url;
                    var tokenWs = response.Token;

                    //Creo una sola URL y al accionar el botón "Pagar" en el archivo PagoReserva.cshtml en ReservaPago, me envía a la URL
                    var url = formAction + "?token_ws=" + tokenWs;

                    ViewBag.buyOrder = buyOrder;
                    ViewBag.url = url;
                    {
                        if (ModelState.IsValid)
                        {
                            //db.RESERVA.Add(rESERVA);
                            //db.SaveChanges();
                            TempData["reserva"] = rESERVA;
                            return Redirect(url);
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

        //Pagina que hace de intermediario para redirigir a la pagina Comprobante(Si la Transaccion es exitosa) y erroTransacccion(Si hay Error o a sido Rechazado)
        public ActionResult Resultado(string token_ws)
        {
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
            if (token_ws != null)
            {
                var response = tx.Commit(token_ws);

                // TRANSACCION EXITOSA
                if (response.ResponseCode == 0)
                {
                    var reserva = TempData["reserva"] as RESERVA;
                    db.RESERVA.Add(reserva);
                    db.SaveChanges();
                    return RedirectToAction("Comprobante", new { token_ws = token_ws });
                }
                // Manejar el caso en que reserva sea null si es necesario
                //TRANSACCION RECHZADA
                else if (response.ResponseCode != 0)
                {
                    // Ocurrió un problema durante la transacción, redirigir a la página de error
                    return RedirectToAction("TransaccionRechazada", new { token_ws = token_ws });
                }
            }
            //ERROR TRANSACCION
            if (token_ws == null)
            {
                //el token_ws lo modifique para que sea "1", para que en la pagina ErrorTransaccion responda solamente a "1"
                //lo que significa que si se hizo una transaccion pero fue anulada o se produjo un error
                //para asi cuando alguien busque mediante url ReservaPago/ErrorTransaccion no pueda ver la pagina al menos que 
                //haya anulado la transaccion o tuviera un problema en el transcurso de esta
                return RedirectToAction("ErrorTransaccion", new { token_ws = "1" });
            }

            // Error o Transaccion rechazada
            //antes
            //return View("TransaccionRechazada");
            //depues
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //se elimino "return View("TransaccionRechazada");" por que si buscaba por URL ReservaPago/Resultado me llevaba si o si a la pagina de "TransaccionRechazada" aunque no hubiera transaccion
            //esta pagina si o si sirve para deribar a la pagina con el resultado correcto dependiendo de si la transaccion fue realizada con exito o no.
            //ahora esta pagina mostraria el error de HTTP Error 400.0 - Bad Request
        }

        //Pagina si a sido exitosa
        public ActionResult Comprobante(string token_ws)
        {
            if (token_ws == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
            var response = tx.Commit(token_ws);

            if (response.Vci == "TSY")
            {
                ViewBag.Vci = ("Autenticación Exitosa");
            }

            if (response.Vci == "TSN")
            {
                ViewBag.Vci = ("Autenticación Rechazada");
            }





            //este es del commit
            //ViewBag.Vci = response.Vci;
            ViewBag.Amount = response.Amount;
            ViewBag.Status = response.Status;
            ViewBag.BuyOrder = response.BuyOrder;
            ViewBag.SessionId = response.SessionId;
            ViewBag.CardDetail = response.CardDetail;
            ViewBag.AccountingDate = response.AccountingDate;
            ViewBag.TransactionDate = response.TransactionDate;
            ViewBag.AuthorizationCode = response.AuthorizationCode;

            if (response.PaymentTypeCode == "VD")
            {
                ViewBag.PaymentTypeCode = ("Venta Débito");
            }

            if (response.PaymentTypeCode == "VN")
            {
                ViewBag.PaymentTypeCode = ("Venta Normal");
            }

            if (response.PaymentTypeCode == "VC")
            {
                ViewBag.PaymentTypeCode = ("Venta en cuotas");
            }

            if (response.PaymentTypeCode == "SI")
            {
                ViewBag.PaymentTypeCode = ("3 cuotas sin interés");
            }

            if (response.PaymentTypeCode == "S2")
            {
                ViewBag.PaymentTypeCode = ("2 cuotas sin interés");
            }

            if (response.PaymentTypeCode == "NC")
            {
                ViewBag.PaymentTypeCode = ("N Cuotas sin interés");
            }

            if (response.PaymentTypeCode == "VP")
            {
                ViewBag.PaymentTypeCode = ("Venta Prepago");
            }

            //ViewBag.PaymentTypeCode = response.PaymentTypeCode;

            if (response.ResponseCode == 0)
            {
                ViewBag.ResponseCode = ("Transacción aprobada");
            }

            if (response.ResponseCode == -1)
            {
                ViewBag.ResponseCode = ("Transacción rechazada");
            }

            //ViewBag.ResponseCode = response.ResponseCode;
            ViewBag.InstallmentsAmount = response.InstallmentsAmount;
            ViewBag.InstallmentsNumber = response.InstallmentsNumber;
            ViewBag.Balance = response.Balance;
            return View();
        }
        
        //Pagina si a sido rechazada
        public ActionResult TransaccionRechazada(string token_ws)
        {
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
            //si existe token_ws es por que se creo y termino la transaccion y el token en este caso si o si deberia ser -1
            //lo que significa que fue una transaccion rechazada
            if (token_ws != null)
            {
                var response = tx.Commit(token_ws);
                if (response.ResponseCode == -1)
                {
                    ViewBag.ResponseCode = ("Transacción rechazada");
                }
                return View();
            }
            //se elimino "return View();" por que si buscaba por URL ReservaPago/TransaccionRechazada me llevaba si o si a la pagina de "TransaccionRechazada" aunque no hubiera transaccion
            //esta pagina es solamente para cuando se hace un pago y es rechazado, si se busca ReservaPago/TransaccionRechazada mediante url 
            //mostrara el error de HTTP Error 400.0 - Bad Request
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //Pagina si a habido un error o anulacion
        public ActionResult ErrorTransaccion(string token_ws)
        {
            var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
            //si NO existe token_ws es por que se produjo un error o anulacion en medio de la transaccion.
            //el token deberia ser nulo
            //lo que significa que hubo un error o se anulo la transaccion
            if (token_ws == "1")
            {
                return View();
            }
            //se elimino "return View();" por que si se buscaba por URL ReservaPago/ErrorTransaccion me llevaba si o si a la pagina de "ErrorTransaccion" aunque no hubiera transaccion
            //esta pagina es solamente para cuando se hace un pago y es anulado o se produce un error en medio de la transaccion, si se busca ReservaPago/ErrorTransaccion mediante url 
            //mostrara el error de HTTP Error 400.0 - Bad Request
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }



        //Obtiene el nombre y apellido a traves del id_paciente en la vista se ve mediante el rut
        //recibe el parametro id_paciente por que literal recibe el id_paciente de @Html.DropDownList

        public ActionResult ObtenerNombreApellido(int id_paciente)

        {   //define al paciente por su id_paciente(PACIENTE.id_paciente == id_paciente(Parametro obtenido))
            var paciente = db.PACIENTE.FirstOrDefault(p => p.id_paciente == id_paciente);
            //define la persona por el id_persona(PERSONA.id_persona == PACIENTE.id_persona)
            var persona = db.PERSONA.FirstOrDefault(p => p.id_persona == paciente.id_persona);

            //Concatena nombre y apellido
            string nombreApellido = persona.pnombre + " " + persona.appaterno;
            //retorna el nombre concatenado
            return Content(nombreApellido);

        }
    }
}
