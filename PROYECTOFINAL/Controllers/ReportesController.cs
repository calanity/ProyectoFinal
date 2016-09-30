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
            int idProd = Convert.ToInt16(Request.Form["productos"]);
            if (efectivo == "efectivo" && tarjeta == "tarjeta" && idProd==0)
            {
                lista = producto.ListarProductosVendidosEntreFechas(fecha1, fecha2);
            }
                        
          
           lista = producto.ListarProductoVendido(idProd, fecha1, fecha2);
                       //segun los parametros que entran, segun que lista obtiene           
           
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
    }
}