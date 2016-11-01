using MySql.Data.MySqlClient;
using PROYECTOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria;

namespace PROYECTOFINAL.Controllers
{
    public class CajaController : Controller
    {
        // GET: Caja
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inicio(FormCollection formulario)
        {
            bool existe= false;            
            int registros = -1;
            var idLocal = Session["idLocal"];
            int cajaI =Convert.ToInt16(Request.Form["cajaInicial"]);
            using (MySqlConnection con = producto.AbrirConexion())
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerCajaExistente";
                cmd.Parameters.AddWithValue("fech", DateTime.Today);
                cmd.Parameters.AddWithValue("idLocal", Convert.ToInt16(idLocal));
                MySqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    existe = true;

                }
                con.Close();

            }



            if (existe == false)
            {
                using (MySqlConnection con2 = producto.AbrirConexion())
                { 
                MySqlCommand cmd2 = con2.CreateCommand();
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "InsertarCajaInicial";
                cmd2.Parameters.AddWithValue("fecha", DateTime.Today);
                cmd2.Parameters.AddWithValue("montoCaja", cajaI);
                cmd2.Parameters.AddWithValue("idLocal",idLocal);    
                registros = cmd2.ExecuteNonQuery();
                con2.Close();
                }
            }



            return View(registros);
        }

        public ActionResult CerrarCaja()
        {
            //averigua si la caja del dia de la fecha esta cerrada y sino la cierra
            int idLocal = Convert.ToInt16(Session["idLocal"]);
            int cajafinal = caja.ObtenerCajaFinal(idLocal);
            if (cajafinal ==-1)
            {
                using (MySqlConnection con2 = producto.AbrirConexion())
                {

                    MySqlCommand cmd2 = con2.CreateCommand();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "CerrarCaja";
                    cmd2.Parameters.AddWithValue("idLocal", idLocal);
                    int registros = cmd2.ExecuteNonQuery();
                    con2.Close();
                }
                cajafinal = caja.ObtenerCajaFinal(idLocal);
            }
            
            return View(cajafinal);
        }

        public ActionResult ListarMovimientosCaja()
        {
            return View();
        }

        public ActionResult MostrarMovDia(DateTime fecha)
        {
            //seleccionar los movimientos de la caja de un dia           
            ViewBag.Fecha = fecha;
            List<movimientosmodel>lmov = caja.ListarMovimientosCajaXDia(fecha);
            
            return View(lmov);
        }
        
    }
}

