using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class productomodel
    {
        
        public int id { get; set; }
      
        [Display(Name = "producto")]
        [Required(ErrorMessage = "Ingrese un producto")]
        public string nombre { get; set; }
       
                
        public int precio { get; set; }      
        public int cantidad { get; set; }

        [Required]
        [Display(Name = "cate")]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "Ingrese una categoria")]
        public string Categoria { get; set; }
        public string MedioPago { get; set; }

        public int idLocal { get; set; }
        public int subtotal { get; set; }
        public int totalProductos { get; set; }

        public int stockminimo { get; set; }
        public int stockactual { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }
    }
}