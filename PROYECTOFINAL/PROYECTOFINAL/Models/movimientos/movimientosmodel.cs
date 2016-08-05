using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class movimientosmodel
    {      
        public DateTime Fecha{get; set;}
        public int monto { get; set; }
        public string concepto { get; set; }
        public int idConcepto { get; set; }
        public string entradaSalida { get; set; }
    }
}