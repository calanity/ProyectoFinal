using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROYECTOFINAL.Models;


namespace PROYECTOFINAL.Controllers
{
    public class VentasController : Controller
    {
        // GET: Caja
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarVentas(DateTime fecha)
        {
            List<ventamodel> lista = new List<ventamodel>();
            //obtener fecha y ejecutar la consulta
            if (fecha== null)
            {
                return View("Index");
            }
            else
            {
                //pregunta si el dia tienen ventas y despues las lista
                var resul = venta.ObtenerTotalVendidoXDia(fecha);
                if(resul>0)
                { 
                    lista = venta.ListarVentasxDia(fecha);
                }

                if (TempData["fecha"] == null)
                {
                    TempData.Add("fecha", fecha);
                }
                else
                {
                    TempData["fecha"] = fecha;
                }
                TempData.Keep();
                return View(lista);
            }
            
        }
        public ActionResult VentasMensual()
        {
            return View();
        }
        public ActionResult MostrarVentasMensuales(FormCollection formulario)
        {
            var mes = (Request.Form["mes"]);
            var año = (Request.Form["año"]);
            if(mes==null&&año==null)
            {
                return View("Index");
            }
            else
            { 
            List<ventamodel> lista = venta.ListarVentasxMes(Convert.ToInt16(mes), Convert.ToInt16(año));
                TempData.Keep();
                return View(lista);
            }
        }
        public ActionResult IngresosYSalidas()
        {
            return View();
        }
        public ActionResult Eliminar(int id)
        {
            /*eliminar la compra de la base de datos, devuelvo el stock, 
             * elimino el movimiento(caja/tarj/mov)resto plata caja/venta*/
            int ret = venta.EliminarVenta(id);
            return View();
        }
        
    }
}