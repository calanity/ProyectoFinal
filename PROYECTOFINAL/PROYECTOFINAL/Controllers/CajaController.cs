using MySql.Data.MySqlClient;
using PROYECTOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            int cajaI =Convert.ToInt16(Request.Form["cajaInicial"]);
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerCajaExistente";
            cmd.Parameters.AddWithValue("fech", DateTime.Today);
            MySqlDataReader lector = cmd.ExecuteReader();

                       

            while (lector.Read())
            {
                existe = true;               

            }
            con.Close();

            if(existe == false)
            {
                MySqlConnection con2 = producto.AbrirConexion();
                MySqlCommand cmd2 = con2.CreateCommand();
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "InsertarCajaInicial";
                cmd2.Parameters.AddWithValue("fecha", DateTime.Today);
                cmd2.Parameters.AddWithValue("montoCaja", cajaI);
                registros = cmd2.ExecuteNonQuery();
                con.Close();
               
            }



            return View(registros);
        }
               
        public ActionResult CerrarCaja()
        {
            cajamodel caja = new cajamodel();
            //listo ventas efectivos
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerVentaEfectivoxDia";
            cmd.Parameters.AddWithValue("fech", DateTime.Today);
            MySqlDataReader lector = cmd.ExecuteReader();

            

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    if (lector["MontoTotal"] != DBNull.Value)
                    {
                        caja.CajaFinal+= Convert.ToInt16(lector["MontoTotal"]);
                    }
                }

            }
            con.Close();

            MySqlConnection con2 = producto.AbrirConexion();
            MySqlCommand cmd2 = con2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "ObtenerMovimientosEntrada";
            cmd2.Parameters.AddWithValue("fechaa", DateTime.Today);
            MySqlDataReader lector2 = cmd2.ExecuteReader();
            while (lector2.Read())
            {
                if (lector.FieldCount > 0)
                {
                    if (lector2["Monto"] != DBNull.Value)
                    {
                        caja.CajaFinal += Convert.ToInt16(lector["Monto"]);
                    }
                }

            }
            con2.Close();

            MySqlConnection con3 = producto.AbrirConexion();
            MySqlCommand cmd3 = con3.CreateCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "ObtenerMovimientosSalida";
            cmd3.Parameters.AddWithValue("fechaaa", DateTime.Today);
            MySqlDataReader lector3 = cmd3.ExecuteReader();
            while (lector3.Read())
            {
                if (lector.FieldCount > 0)
                {
                    if (lector["Monto"] != DBNull.Value)
                    {
                        caja.CajaFinal -= Convert.ToInt16(lector["Monto"]);
                    }
                }

            }


            con3.Close();
            
            
            //si tiene algo cargado en el fin, no lo puede actualizar
            return View(caja);
        }

        
    }
}