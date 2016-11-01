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
        public static int AgregarMovimiento(double monto, string motivo, DateTime fecha, string mediopago,int idLocal, int venta=0)
        {
            int hola = -1;
            conceptomodel conc = new conceptomodel();

            using (MySqlConnection con = producto.AbrirConexion()) { 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertarMovimiento";
            cmd.Parameters.AddWithValue("mont", monto);
            cmd.Parameters.AddWithValue("concepto", motivo);
            cmd.Parameters.AddWithValue("fech", fecha);
            cmd.Parameters.AddWithValue("vent", venta);
            cmd.Parameters.AddWithValue("idlocal", idLocal);


            int registros2 = cmd.ExecuteNonQuery();
            con.Close();
            }
            if (mediopago== "Efectivo" || mediopago =="Movimiento")
            { 
                movimientosmodel movi2 = movimientos.ObtenerUltimoMovimiento();

                using (MySqlConnection con1 = producto.AbrirConexion()) { 
                    MySqlCommand cmd1 = con1.CreateCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "InsertarMovCaja";
                cmd1.Parameters.AddWithValue("idMovimiento", movi2.IdMovimientos);
                cmd1.Parameters.AddWithValue("monto", movi2.monto);
                cmd1.Parameters.AddWithValue("fecha", fecha);
                cmd1.Parameters.AddWithValue("idConcepto", motivo);
                cmd1.Parameters.AddWithValue("venta", venta);


                int registros4 = cmd1.ExecuteNonQuery();
                con1.Close();
                }
            }
            
            //pregunto si el mov que se esta creando es entrada o salida y lo paso a la caja.
            //insertar en movimientoscaja


            return hola;
        }
    


        public static List<conceptomodel> listarConceptos()
        {
            List<conceptomodel> l2 = new List<conceptomodel>();
            using (MySqlConnection con = producto.AbrirConexion()) { 
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
            }
            return l2;
        }
          
        public static List<movimientosmodel>ListarMovimientos()
        {
            List<movimientosmodel> l2 = new List<movimientosmodel>();
            using (MySqlConnection con = producto.AbrirConexion())
            { 
                MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarMovimientos";
            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                movimientosmodel mov = new movimientosmodel();
                mov.IdMovimientos = (int)(lector["IdMovimientos"]);
                mov.concepto = (string)(lector["Nombre"]);
                mov.Fecha = (DateTime)(lector["Fecha"]);
                mov.monto= (int)(lector["Monto"]);
                mov.entradaSalida= (string)(lector["SalidaEntrada"]);
                l2.Add(mov);
            }

            con.Close();
            }
            return l2;

        }
        public static List<movimientosmodel> ListarMovxMes(int mes, int año)
        {
            List<movimientosmodel> l2 = new List<movimientosmodel>();
            using (MySqlConnection con = producto.AbrirConexion())
            { 
                MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarMovimentosPorMes";
            cmd.Parameters.AddWithValue("mes", mes);
            cmd.Parameters.AddWithValue("año", año);

            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                movimientosmodel mov = new movimientosmodel();
                mov.IdMovimientos = (int)(lector["IdMovimientos"]);
                mov.concepto = (string)(lector["Nombre"]);
                mov.entradaSalida = (string)(lector["SalidaEntrada"]);                
                mov.monto = (int)(lector["Monto"]);
                mov.Fecha = (DateTime)(lector["Fecha"]);

                l2.Add(mov);
            }

            con.Close();
            }
            return l2;
        }

        public static int TotalEntradasPorMes(DateTime fecha)
        {           
           int totalEntrada = 0;


            using (MySqlConnection con = producto.AbrirConexion()) { 
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
            }
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

        public static List<movimientosmodel> ListarMovxDia(DateTime fecha)
        {
            fecha = fecha.Date;
            List<movimientosmodel> l2 = new List<movimientosmodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarMovimentosPorDia";                      
            cmd.Parameters.AddWithValue("fech", fecha);

            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                movimientosmodel mov = new movimientosmodel();
                mov.IdMovimientos = (int)(lector["IdMovimientos"]);
                mov.concepto = (string)(lector["Nombre"]);
                mov.entradaSalida = (string)(lector["SalidaEntrada"]);
                mov.monto = (int)(lector["Monto"]);
                mov.Fecha = (DateTime)(lector["Fecha"]);

                l2.Add(mov);
            }

            con.Close();
            return l2;
        }


        public static movimientosmodel ObtenerUltimoMovimiento()
        {
            movimientosmodel mov = new movimientosmodel();

            //obtengo el movimiento ingresado en la venta y lo cargo en cajamov
            //pregunto por el ultimo movimiento ingresado
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerUltimoMovimiento";
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    mov.IdMovimientos = (int)lector["IdMovimientos"];
                    mov.Fecha = (DateTime)lector["Fecha"];
                    mov.monto = (int)lector["Monto"];
                    mov.idConcepto = (int)lector["IdConcepto"];
                }
            }
            con.Close();

            return mov;
        }
    }
}
