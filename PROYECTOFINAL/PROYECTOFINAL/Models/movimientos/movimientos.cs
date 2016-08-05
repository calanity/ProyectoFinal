using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PROYECTOFINAL.Models;
namespace PROYECTOFINAL.Models
{
    public class movimientos
    {
        public static int AgregarMovimiento(int monto, string motivo ,  DateTime fecha)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertarMovimiento";   
            cmd.Parameters.AddWithValue("mont", monto);
            cmd.Parameters.AddWithValue("concepto", motivo);            
            cmd.Parameters.AddWithValue("fech", fecha);



            int registros2 = cmd.ExecuteNonQuery();
            con.Close();
            return (registros2);
        }

        public static List<conceptomodel> listarConceptos()
        {
            List<conceptomodel> l2 = new List<conceptomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarTipoConceptos";
            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                conceptomodel concepto = new conceptomodel();
                concepto.nombre=(string)(lector["Nombre"]);
                concepto.idConceptos = (int)(lector["idConcepto"]);
                concepto.TipoConcepto = (string)(lector["SalidaEntrada"]);
                l2.Add(concepto);
            }

            con.Close();
            return l2;
        }
          
        public static List<movimientosmodel>ListarMovimientos()
        {
            List<movimientosmodel> l2 = new List<movimientosmodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarMovimientos";
            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                movimientosmodel mov = new movimientosmodel();
                mov.concepto = (string)(lector["Nombre"]);
                mov.Fecha = (DateTime)(lector["Fecha"]);
                mov.monto= (int)(lector["Monto"]);
                mov.entradaSalida= (string)(lector["SalidaEntrada"]);
                l2.Add(mov);
            }

            con.Close();
            return l2;

        }
        public static List<movimientosmodel> ListarMovxMes(int mes, int año)
        {
            List<movimientosmodel> l2 = new List<movimientosmodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarMovimentosPorMes";
            cmd.Parameters.AddWithValue("mes", mes);
            cmd.Parameters.AddWithValue("año", año);

            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                movimientosmodel mov = new movimientosmodel();
                mov.concepto = (string)(lector["Nombre"]);
                mov.entradaSalida = (string)(lector["SalidaEntrada"]);                
                mov.monto = (int)(lector["Monto"]);
                mov.Fecha = (DateTime)(lector["Fecha"]);

                l2.Add(mov);
            }

            con.Close();
            return l2;
        }

        public static int TotalEntradasPorMes(DateTime fecha)
        {           
           int totalEntrada = 0;


            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TotalEntradasPorMes";
            cmd.Parameters.AddWithValue("Anio", fecha.Year );
            cmd.Parameters.AddWithValue("Mes", fecha.Month);


            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                if (lector.FieldCount>0)
                { 
                    totalEntrada = Convert.ToInt16(lector["Monto"]);
                }
            }

            con.Close();

            return totalEntrada;
        }
        public static int TotalSalidasPorMes(DateTime fecha)
        {
            int totalSalida = 0;

            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TotalSalidasPorMes";
            cmd.Parameters.AddWithValue("Anio", fecha.Year);
            cmd.Parameters.AddWithValue("Mes", fecha.Month);


            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                if(lector.FieldCount>0)
                { 
                    totalSalida = Convert.ToInt16(lector["Monto"]);
                }
            }

            con.Close();

            return totalSalida; 
        }


    }
}
