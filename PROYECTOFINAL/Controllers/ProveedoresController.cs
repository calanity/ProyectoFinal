﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROYECTOFINAL.Models;
using MySql.Data.MySqlClient;
using System.Data;
using Libreria;
using Microsoft.Reporting.WebForms;
namespace PROYECTOFINAL.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        public ActionResult Index()
        {
           return View();
        }
        public ActionResult Agreg()
        {
            return View();
        }

        public ActionResult Agregar(FormCollection form)
        {
            var nombre = Request.Form["nombre"];
            var telefono = Convert.ToInt32(Request.Form["telefono"]);
            proveedor.CrearProveedor(nombre, telefono);
            return View("Index");
        }
        // GET: Proveedores/Edit/5
        public ActionResult Editar(int id)
        {
            TempData.Remove("idEditar");
            TempData.Add("idEditar", id);
            proveedormodel prove = new proveedormodel();
            prove = proveedor.ObtenerProveedor(id);
           
            return View(prove);
            
        }
        [HttpPost]
        public ActionResult Editar(FormCollection formulario)
        {
            var nombre = Request.Form["nombre"];
            var telefono = Convert.ToInt32(Request.Form["telefono"]);
            var id = (int)TempData["idEditar"];
            proveedor.EditarProveedor(id, nombre, telefono);

            TempData.Remove("idEditar");

            return View("Index");
        }


        public ActionResult Eliminar(int id)
        {
            int registros = proveedor.EliminarProveedor(id);
                       
                return View("Index");
            
        }


        public ActionResult Compra()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Comprar(FormCollection form )
        {
            var id = Convert.ToInt16(Request.Form["prove"]);
            var monto = Convert.ToInt16(Request.Form["monto"]);
            int producto = Convert.ToInt16(Request.Form["prod"]);
            int cantidad = Convert.ToInt16(Request.Form["cantidad"]);
            int registros = proveedor.RegistrarCompra(id, monto, producto, cantidad);
            return View("Index");
        }

        public ActionResult Pago()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pagar(FormCollection form)
        {
            var id = Convert.ToInt16(Request.Form["prove"]);
            var monto = Convert.ToInt16(Request.Form["monto"]);
            int registros = proveedor.EditarSaldo(id, monto);
            return View("Index");
        }

        public ActionResult ProductosXProveedor()
        {
            List<proveedormodel> lista = proveedor.ListarProveedores();
            return View(lista);
        }

        public ActionResult SelectProveedor(int id)
        {
                    
            List<productomodel> lista = proveedor.ObtenerProductosPorProveedor(id);
            return PartialView("_selectProve" , lista);
        }
        public ActionResult SelectProducto(int id)
        {

            List<productomodel> lista = proveedor.ObtenerProductosPorProveedor(id);
            return PartialView("_selectProducto", lista);
        }

        public ActionResult ReporteProveedores()
        {
            LocalReport reporte = new LocalReport();
            List<proveedormodel> lista = new List<proveedormodel>();
            lista = proveedor.ListarProveedores();
            ReportDataSource dc = new ReportDataSource("Nombre", lista);
            reporte.DataSources.Add(dc);            
        }


    }
}
