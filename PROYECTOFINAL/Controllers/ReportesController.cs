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
        public ActionResult ReporteProve()
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
            int provedor = Convert.ToInt16(Request.Form["proveedor"]);
            int idProd = Convert.ToInt16(Request.Form["productos"]);
            int categoria =Convert.ToInt16(Request.Form["categoria"]);
            var cate = (Request.Form["cate"]);
            var MedioP = 0;
            if (tarjeta == null && efectivo == null)
            { MedioP = 1; }
            else
            if (tarjeta != null && efectivo == null)
            { MedioP = 3; }
            else
            if (tarjeta == null && efectivo != null)
            { MedioP = 2; }

            if (produ == "produc")
            {
                lista = Reporte.GenerarReporte(fecha1, fecha2, MedioP, 0, 0, idProd);
            }
            else if (cate == "cate")
            {
                lista = Reporte.GenerarReporte(fecha1, fecha2, MedioP, 0, categoria, 0);
            }
            else if (prove == "prove")
            {
                lista = Reporte.GenerarReporte(fecha1, fecha2, MedioP, provedor, 0, 0);
            }
            else { lista = Reporte.GenerarReporte(fecha1, fecha2, 1, 0, 0, 0); }
            

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

        public ActionResult ReporteProveedores(FormCollection form)
        {
            List<compramodel> lista = new List<compramodel>();
            DateTime fecha1 = Convert.ToDateTime(Request.Form["fecha1"]);
            DateTime fecha2 = Convert.ToDateTime(Request.Form["fecha2"]);
            int prove = Convert.ToInt16(Request.Form["proveedor"]);
            lista=proveedor.ListarComprasProve(fecha1, fecha2, prove);
            var path = Server.MapPath(@"~/Reporte/Report3.rdlc");
            LocalReport reporte = new LocalReport();
            reporte.ReportPath = path;

            if (lista.Count > 0)
            {

                ReportDataSource dc = new ReportDataSource("DataSet3", lista);
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