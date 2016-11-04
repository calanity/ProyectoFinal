using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROYECTOFINAL.Models;

namespace PROYECTOFINAL.Controllers
{
    public class MovimientosController : Controller
    {
        // GET: Movimientos
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

        // GET: Movimientos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            return View();
        }

       
        // GET: Movimientos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movimientos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movimientos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movimientos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult mostrar(FormCollection hola, int monto)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                //cargo en la tabla la salida
                DateTime fecha = DateTime.Now;
                string idConc = Request.Form["concepto"];
                int func = movimientos.AgregarMovimiento(monto, idConc, fecha, "Movimiento", Convert.ToInt16(idLocal));
                return View("Index");
            }
        }

        public ActionResult MostrarMovMensuales(FormCollection formulario)
        {

            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
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
                    List<movimientosmodel> lista = movimientos.ListarMovxMes(Convert.ToInt16(mes), Convert.ToInt16(año));
                    TempData.Keep();
                    return View(lista);
                }
            }
        }

        public ActionResult ElegirMes()
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View();

            }
        }

        public ActionResult MostrarMovDia(DateTime fecha)
        {
            /* var dia =(Request.Form["dia"]);
             var mes = (Request.Form["mes"]);*/

            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                if (fecha == null)
                {
                    return View("Index");
                }
                else
                {
                    List<movimientosmodel> lista = movimientos.ListarMovxDia(fecha);
                    TempData.Keep();
                    return View(lista);
                }
            }

        }

        public ActionResult ElegirDia()
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View();
            }
        }

    }
}
