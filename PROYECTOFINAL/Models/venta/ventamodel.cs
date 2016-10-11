using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class ventamodel
    {
        public int id { get; set; }
        public DateTime Fecha { get; set; }

        public List<productomodel> ListaArticulos;
        public string MedioPago { get; set; }
        public double MontoTotal { get; set; }

    }
}