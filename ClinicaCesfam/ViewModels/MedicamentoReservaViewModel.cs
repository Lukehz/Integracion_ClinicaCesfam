using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaCesfam.Models;
using System.Data.Entity;

namespace ClinicaCesfam.ViewModels
{
    public class MedicamentoReservaViewModel
    {
            public MEDICAMENTO Medicamento { get; set; }
            public RESERVA Reserva { get; set; }
        }
    }
