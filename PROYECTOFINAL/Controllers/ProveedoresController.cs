﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PROYECTOFINAL.Models;
using Libreria;
using Microsoft.Reporting.WebForms;
namespace PROYECTOFINAL.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        public ActionResult Index()
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
        public ActionResult Agreg()
        {
            TempData.Remove("idEditar");
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

        public ActionResult Agregar(FormCollection form)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var nombre = Request.Form["nombre"];
                var telefono = Convert.ToInt32(Request.Form["telefono"]);
                proveedor.CrearProveedor(nombre, telefono);
                return View("Index");
            }
        }
        // GET: Proveedores/Edit/5
        public ActionResult Editar(int id)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                TempData.Remove("idEditar");
                TempData.Add("idEditar", id);
                proveedormodel prove = new proveedormodel();
                prove = proveedor.ObtenerProveedor(id);

                return View(prove);
            }
        }
        [HttpPost]
        public ActionResult Editar(FormCollection formulario)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var nombre = Request.Form["nombre"];
                var telefono = Convert.ToInt32(Request.Form["telefono"]);
                var id = (int)TempData["idEditar"];
                proveedor.EditarProveedor(id, nombre, telefono);

                TempData.Remove("idEditar");

                return View("Index");
            }
        }


        public ActionResult Eliminar(int id)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                int registros = proveedor.EliminarProveedor(id);

                return View("Index");
            }
        }


        public ActionResult Compra()
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


        [HttpPost]
        public ActionResult Comprar(FormCollection form)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var id = Convert.ToInt16(Request.Form["prove"]);
                var monto = Convert.ToInt16(Request.Form["monto"]);
                int producto = Convert.ToInt16(Request.Form["prod"]);
                int cantidad = Convert.ToInt16(Request.Form["cantidad"]);
                int unitario = Convert.ToInt16(Request.Form["unitario"]);

                int registros = proveedor.RegistrarCompra(id, monto, producto, cantidad, unitario, Convert.ToInt16(idLocal));
                return View("Index");
            }
        }

        public ActionResult Pago()
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

        [HttpPost]
        public ActionResult Pagar(FormCollection form)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var id = Convert.ToInt16(Request.Form["prove"]);
                var monto = Convert.ToInt16(Request.Form["monto"]);
                int registros = proveedor.EditarSaldo(id, monto);
                return View("Index");
            }
        }

        public ActionResult ProductosXProveedor()
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                List<proveedormodel> lista = proveedor.ListarProveedores();
                return View(lista);
            }
        }

        public ActionResult SelectProveedor(int id)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                List<productomodel> lista = proveedor.ObtenerProductosPorProveedor(id,Convert.ToInt16(idLocal));
                return PartialView("_selectProve", lista);
            } }
        public ActionResult SelectProducto(int id)
        {
            var idLocal = Session["idLocal"];
            List<productomodel> lista = proveedor.ObtenerProductosPorProveedor(id, Convert.ToInt16(idLocal));
            return PartialView("_selectProducto", lista);
        }

        public ActionResult ReporteProveedores()
        {
            //var path = Server.MapPath(@"~/Reporte/Report1.rdlc");
            
            var path = Server.MapPath(@"~/Reporte/Report1.rdlc");

            LocalReport reporte = new LocalReport();
            reporte.ReportPath = path;
            List<proveedormodel> lista = new List<proveedormodel>();
            lista = proveedor.ListarProveedores();
            ReportDataSource dc = new ReportDataSource("DataSet1", lista);
            reporte.DataSources.Add(dc);
            ReportViewer reportV = new ReportViewer();
            reportV.LocalReport.DataSources.Clear();
            reportV.LocalReport.ReportPath = path;
            reportV.LocalReport.DataSources.Add(dc);

            string reportType = "PDF";
            /*string mimetype;
            string encoding;
            string FileNameExtension;
            Warning[] warnings;
            string[] streams;
            */
            byte[] renderBytes;

            string deviceInfo =
                  "<DeviceInfo>" +
                  "  <OutputFormat>EMF</OutputFormat>" +
                  "  <PageWidth>8.5in</PageWidth>" +
                  "  <PageHeight>11in</PageHeight>" +
                  "  <MarginTop>0.25in</MarginTop>" +
                  "  <MarginLeft>0.25in</MarginLeft>" +
                  "  <MarginRight>0.25in</MarginRight>" +
                  "  <MarginBottom>0.25in</MarginBottom>" +
                  "</DeviceInfo>";

            renderBytes = reportV.LocalReport.Render(reportType, deviceInfo);
            return File(renderBytes, "application/pdf");
        }


    }
}
