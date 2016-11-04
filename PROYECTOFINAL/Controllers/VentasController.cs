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
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View();
            }
        }

        public ActionResult MostrarVentas(DateTime? fecha= null)
        {
            var idLocal = Session["idlocal"];
            if (idLocal == null)
            {

                return RedirectToAction("Index", "Usuarios");

            }
            else
            {

                List<ventamodel> lista = new List<ventamodel>();
                //obtener fecha y ejecutar la consulta
                if (fecha == null)
                {
                    return View("Index");
                }                   
                                   
                else
                {
                    DateTime fecha2 = Convert.ToDateTime(fecha);
                    //pregunta si el dia tienen ventas y despues las lista
                    var resul = venta.ObtenerTotalVendidoXDia(fecha2, Convert.ToInt16(idLocal));
                    if (resul > 0)
                    {
                        lista = venta.ListarVentasxDia(fecha2, Convert.ToInt16(idLocal));
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
        }


        public ActionResult VentasMensual()
        {
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View();
            }
        }
        public ActionResult MostrarVentasMensuales(FormCollection formulario)
        {

            if (Session["idLocal"]== null)
            {
                return RedirectToAction("Index", "Usuarios");
            }

            else
            {

                var mes = (Request.Form["mes"]);
                var año = (Request.Form["año"]);
                if (mes == null && año == null)
                {
                    return View("Index");
                }
                else
                {
                    List<ventamodel> lista = venta.ListarVentasxMes(Convert.ToInt16(mes), Convert.ToInt16(año), Convert.ToInt16(Session["idLocal"]));
                    TempData.Keep();
                    return View(lista);
                }
            }
        }
        public ActionResult IngresosYSalidas()
        {
            return View();
        }
        public ActionResult Eliminar(int id=0)
        {

            var idLocal = Session["idLocal"];

            int ret = venta.EliminarVenta(id, Convert.ToInt16(idLocal));
                return RedirectToAction("Index");            

        }
    }
}