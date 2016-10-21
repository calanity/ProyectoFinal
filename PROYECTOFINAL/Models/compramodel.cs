using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class compramodel
    {
        public int idcompra { get; set; }
        public int idProvedor { get; set; }
        public string Proveedor { get; set; }
        public int idProducto { get; set; }
        public string Product { get; set; }
        public DateTime fecha { get; set; }
        public int cantidad { get; set; }
        public int costo { get; set; }
        public int total { get; set; }
        public int idLocal { get; set; }
    }
}