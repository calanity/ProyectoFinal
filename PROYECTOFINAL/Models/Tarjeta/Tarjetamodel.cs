using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class Tarjetamodel
    {
        //tipo(cred/deb) lote monto cuotas cupon
            public int cuotas { get; set; }
            public int cupon { get; set; }
            public int monto { get; set; }
            public string tipo { get; set; }
            public string marca { get; set; }
            public int IdVenta { get; set; }

    }
}