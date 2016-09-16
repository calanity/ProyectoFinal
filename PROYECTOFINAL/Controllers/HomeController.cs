﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROYECTOFINAL.Models;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace PROYECTOFINAL.Controllers
{
    public class HomeController : Controller
    {
        int subtotal = 0;

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Index()
        {
            TempData.Keep();
            return View();
        }
        public ActionResult FinT()
        {
            return View();
        }
        public ActionResult Cancelar()
        {
            TempData.Clear();
            return RedirectToAction("Inicio");
        }

        public ActionResult Inicio()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(FormCollection alario)
        {
            bool distinto = true;
            List<productomodel> actual = new List<productomodel>();
            //carga los articulos en el temp data, si es != null lo baja, lo vacia y lo vuelve a cargar
            var lala = (int)Convert.ToInt16(Request.Form["cantidad"]);
            if ((TempData["catego"]) == null || TempData["producto"] == null || (TempData["preci"] == null) || lala <= 0)
            {
                TempData.Keep();
                return View("ErrorValidacion");
            }
            else
            {

                productomodel opro = new productomodel();
                int id = Convert.ToInt16(TempData["producto"]);
                opro.nombre = producto.obtenerNombre(id);
                opro.cantidad = Convert.ToInt16(Request.Form["cantidad"]);
                opro.precio = Convert.ToInt16(TempData["preci"]);
                opro.IdCategoria = (int)(TempData["catego"]);
                opro.subtotal = (opro.precio * opro.cantidad);
                opro.id = id;
                int stock = producto.ObtenerStockActual(opro.id);

                //preguntar si ya se resto el stock del producto que lo reste del obtenido


                var list = (List<productomodel>)TempData["listaActual"];
                if (list != null)
                {
                    foreach (productomodel item in list)
                    {
                        if (item.id == opro.id)
                        {
                            stock -= item.cantidad;
                        }
                    }
                }
                if ((stock - (opro.cantidad)) >= 0)
                {
                    actual.Add(opro);

                    if (TempData["listaActual"] == null)
                    {
                        TempData.Add("listaActual", actual);
                    }
                    else
                    {

                        //pregunta si el producto ya existe y lo actualiza
                        foreach (productomodel item in list)
                        {
                            if (item.id == opro.id)
                            {
                                distinto = false;
                                item.cantidad += opro.cantidad;
                                item.subtotal += (opro.cantidad * opro.precio);
                            }
                        }

                        if (distinto == true)
                        {
                            list.Add(opro);
                        }
                        TempData.Remove("listaActual");
                        TempData.Add("listaActual", list);
                    }

                    TempData.Remove("producto");
                    TempData.Remove("cantidad");
                    TempData.Remove("preci");
                    TempData.Remove("catego");

                    TempData.Keep();
                    return View("Index");

                }
                else
                {
                    TempData.Keep();
                    return View("FaltaStock");
                }




            }
        }

        public ActionResult FaltaStock()
        {
            var lista = (productomodel)TempData["listaActual"];
            TempData.Remove("listaActual");
            TempData.Add("listaActual", lista);
            TempData.Keep();
            return View(lista);
        }

        public ActionResult ErrorValidacion()
        {
            var lista = (productomodel)TempData["listaActual"];
            TempData.Remove("listaActual");
            TempData.Add("listaActual", lista);
            TempData.Keep();
            return View(lista);
        }

        public ActionResult Venta(FormCollection barovero)
        {
            int subtotal = 0;
            if (TempData["listaActual"] != null)
            {
                var lista = (List<productomodel>)TempData["listaActual"];
                   
                foreach (productomodel item in lista)
                {
                    subtotal += item.subtotal;
                }

                TempData.Remove("listaActual");
                TempData.Add("listaActual", lista);
                TempData.Keep("listaActual");
            }
            else
            {
                subtotal = 0;
            }


            return View(subtotal);


        }

        

        public ActionResult Fin(FormCollection cavenaghi)   
        {
            
            //carga la venta, obtiene el id y su detalle en la base de datos y lista la venta
            string medioP;
            ventamodel l2 = new ventamodel();
            l2.Fecha = DateTime.Now;
            int mediopago = Convert.ToInt16(cavenaghi["mediopago"]);
            if (mediopago == 1)
            {
                medioP = "Efectivo";
            }
            else
            {
                medioP = "Tarjeta";
            }
            if (medioP == "Tarjeta")
            {

                //cargo los datos de la tarjeta que levanto del formulario
            }
            else
            {
                var lista = (List<productomodel>)TempData["listaActual"];
                if (TempData["listaActual"] == null)
                {
                    return View("Index");
                }
                else
                {

                    foreach (productomodel item in lista)
                    {
                        subtotal += item.subtotal;
                    }

                    l2.MontoTotal = subtotal;

                    //pregunto si la caja no esta cerrada, si esta cerrada, va para el dia siguiente

                    int cajaFinal = caja.ObtenerCajaFinal();
                    if (cajaFinal < 0)
                    {

                        int hola = venta.CrearVenta(l2.Fecha, subtotal, medioP);
                        int idVentaActual = venta.ObtenerIdVenta();

                        //var listaProd = (List<productomodel>)TempData["listaActual"];
                        foreach (productomodel item in lista)
                        {
                            venta.CrearDetalleVenta(item.id, item.precio, item.cantidad, item.subtotal, idVentaActual);
                            venta.ActualizarStockProducto(item.id, item.cantidad);
                        }

                        //cargo el detalle venta
                        l2.ListaArticulos = lista;
                        l2.MedioPago = medioP;

                        //insertar en movimientos la venta

                        movimientos.AgregarMovimiento(l2.MontoTotal, "7", l2.Fecha, l2.MedioPago);

                    }
                    else
                    {
                        DateTime fech = (l2.Fecha.AddDays(1));
                        int hola = venta.CrearVenta(fech, subtotal, medioP);
                        int idVentaActual = venta.ObtenerIdVenta();

                        //var listaProd = (List<productomodel>)TempData["listaActual"];
                        foreach (productomodel item in lista)
                        {
                            venta.CrearDetalleVenta(item.id, item.precio, item.cantidad, item.subtotal, idVentaActual);
                            venta.ActualizarStockProducto(item.id, item.cantidad);
                        }

                        //cargo el detalle venta
                        l2.ListaArticulos = lista;
                        l2.MedioPago = medioP;

                        //insertar en movimientos la venta

                        movimientos.AgregarMovimiento(l2.MontoTotal, "7", fech, l2.MedioPago);
                    }

                    //pregunta si el stock actual es igual o menoor a la minima y mando el mail
                    List<productomodel> listaEnviar = producto.ObtenerStockMinimoYActual(l2.ListaArticulos);


                    if (listaEnviar.Count > 0)
                    {

                        producto.EnviarMailFaltaStock(listaEnviar);
                    }
                }
            }
                return View(l2);
                
        }

        public ActionResult SelectProductos(int idCategoria)
        {
            var productos = producto.ListarProductosXCategoria(idCategoria);
            //TempData.Remove("");
            var validador = TempData["catego"];
            if (validador == null)
            {
                TempData.Add("catego", idCategoria);
            }
            else
            {
                if (validador != null)
                {
                    TempData.Remove("catego");
                    TempData.Remove("producto");
                    TempData.Remove("precio");
                    TempData.Remove("cantidad");
                    TempData.Add("catego", idCategoria);
                }
            }

            return PartialView("_selectProductos", productos);
        }

        public ActionResult SelectPrecio(int idProducto)
        {
            var prec = 0;
            // if (idProducto > 0)            {

            prec = producto.ObtenerPrecio(idProducto);
            var validador = TempData["preci"];

            if (validador == null)
            {
                TempData.Add("preci", prec);
            }
            else
            {
                if (validador != null)
                {
                    TempData.Remove("preci");
                    TempData.Add("preci", prec);
                }
            }

            string nomProd = producto.obtenerNombre(idProducto);
            var validador2 = TempData["producto"];
            if (validador2 == null)
            {
                TempData.Add("producto", idProducto);
            }
            else
            {
                if (validador2 == null)
                {
                    TempData.Remove("producto");
                    TempData.Add("producto", idProducto);
                }
            }

            /*else
            {
                prec = 0;
            }*/
            return PartialView("_selectPrecio", prec);
        }

        public ActionResult SelectCantidad(int cantidad)
        {
            //pregunta si la cantidad es valida y retorna la cantidad y el bool
            TempData.Remove("valido");
            cantidadmodel canti = new cantidadmodel();
            canti.cantidad = cantidad;
            var producto2 = (TempData["producto"]);
            if (producto2 != null)
            {
                int prodi = (int)(TempData["producto"]);
                prodi = Convert.ToInt16(prodi);
                var stock = producto.ObtenerStockActual(prodi);

                var list = (List<productomodel>)TempData["listaActual"];
                if (list != null)
                {
                    foreach (productomodel item in list)
                    {
                        if (item.id == prodi)
                        {
                            stock -= item.cantidad;
                        }
                    }
                }
                if ((stock - (cantidad)) >= 0)
                {
                    canti.resultado = true;
                    TempData.Add("valido", true);
                }
                else
                {
                    canti.resultado = false;
                    TempData.Add("valido", false);

                }
            }
            TempData.Keep();
                return PartialView("_selectCantidad", canti);
        }

        public ActionResult SelectMedioPago()
        {
             if (TempData["listaActual"] != null)
            {
                var lista = (List<productomodel>)TempData["listaActual"];
                   
                foreach (productomodel item in lista)
                {
                    subtotal += item.subtotal;
                }

                TempData.Remove("listaActual");
                TempData.Add("listaActual", lista);
                TempData.Keep("listaActual");
            }
            return PartialView("_selectMedioPago", subtotal);
        }
    }
}


