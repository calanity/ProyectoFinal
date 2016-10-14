using Libreria;
using Microsoft.Reporting.WebForms;
using PROYECTOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTOFINAL.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarProdVendidos()
        {
            return View();
        }

        public ActionResult ReporteProductos(FormCollection form)
        {
            
            List<productomodel> lista = new List<productomodel>();
            var path = Server.MapPath(@"~/Reporte/Report2.rdlc");
            LocalReport reporte = new LocalReport();
            reporte.ReportPath = path;
            DateTime fecha1 = Convert.ToDateTime(Request.Form["fecha1"]);
            DateTime fecha2 = Convert.ToDateTime(Request.Form["fecha2"]);
            var efectivo = Request.Form["efectivo"];
            var tarjeta = Request.Form["tarjeta"];
            var produ = Request.Form["produc"];
            var prove = Request.Form["prove"];
            var provedor = Request.Form["proveedor"];
            int idProd = Convert.ToInt16(Request.Form["productos"]);
            var categoria = Request.Form["categoria"];
            int cate = Convert.ToInt16(Request.Form["cate"]);

            if (efectivo == null && tarjeta== null && produ!="produc" && prove!= "prove")
            {
             //todos los productos entre 2 fechas
                lista = Reporte.ListarProductosVendidosEntreFechas(fecha1, fecha2);
            }
            
            if (efectivo == null && tarjeta == null && produ == "produc" && prove != "prove")
            {
                lista = Reporte.ListarProductoVendido(idProd,fecha1, fecha2);
                //todas las ventas de 1 producto
            }
            if (efectivo == "efectivo" && tarjeta == "tarjeta"&& produ == "produc" && prove != "prove")
            {
                lista = Reporte.ListarProductoVendido(idProd, fecha1, fecha2);
                //todas las ventas de 1 producto
            }

            if (efectivo == "efectivo" && tarjeta == null && produ == null && provedor == null)
            {
                // efectivo de todos los productos
                lista = Reporte.ProductosEfectivo(fecha1, fecha2);
            }
           if (efectivo == "efectivo" && tarjeta == null && produ == "produc" && provedor != "prove")
            {
                //efectivo de 1 producto
                lista = Reporte.ProductoEfectivo(fecha1, fecha2, idProd);
            }
            if(efectivo==null && tarjeta=="tarjeta" &&produ ==null && provedor== null)
            {
                //tarjeta de todos los productos
                lista = Reporte.ProductosTarjetas(fecha1, fecha2);
            }
            if (efectivo == null && tarjeta == "tarjeta" && produ == "produc" && provedor != "prove")
            {
                //tarjeta de 1 solo producto
               lista = Reporte.ProductoTarjeta(fecha1,fecha2, idProd);
            }
           
            if (efectivo == "efectivo" && tarjeta != "tarjeta" && produ != "produc" && prove == "prove")
            {
                //efectivo de 1 provedor
                lista = Reporte.ProductoProvedorEfectivo(fecha1, fecha2, Convert.ToInt16(provedor));
            }

            if (efectivo != "efectivo" && tarjeta == "tarjeta" && produ != "produc" && prove == "prove")
            {
                //tarjeta de 1 proveedor
                lista = Reporte.ProductoProvedorTarjeta(fecha1, fecha2, Convert.ToInt16(provedor));
            }

            if (efectivo == "efectivo" && tarjeta == "tarjeta" && produ != "produc" && prove == "prove")
            {
                //todo de un proveedor
                lista = Reporte.ProductosProveedor(fecha1, fecha2, Convert.ToInt16(provedor));
            }

            if (efectivo == null && tarjeta == null && produ != "produc" && prove == "prove")
            {
                //todo de un proveedor
                lista = Reporte.ProductosProveedor(fecha1, fecha2, Convert.ToInt16(provedor));
            }


            //segun los parametros que entran, segun que lista obtiene           
            if (lista.Count > 0)
            {
                
                ReportDataSource dc = new ReportDataSource("DataSet2", lista);
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
            else
            {
                return View();
            }
        }
    }
}