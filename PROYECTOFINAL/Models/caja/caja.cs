using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class caja
    {
        public static List<movimientosmodel> ListarMovimientosCajaXDia(DateTime fecha)
        {

            List<movimientosmodel> lmov = new List<movimientosmodel>();
            fecha = fecha.Date;            
            
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarMovimientosCajaxDia";
            cmd.Parameters.AddWithValue("Fecha", fecha);
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    movimientosmodel mov = new movimientosmodel();                   
                    mov.IdMovimientos = (int)lector["Movimiento"];
                    mov.monto = (int)lector["Monto"];
                    mov.Fecha = (DateTime)lector["Fecha"];
                    mov.concepto = (string)lector["Nombre"];
                    mov.entradaSalida = (string)lector["SalidaEntrada"];
                    mov.idConcepto = (int)lector["IdConcep"];

                    lmov.Add(mov);
                }
            }
            con.Close();
            return lmov;
        }

        public static int TotalCajaActual(DateTime fecha)
        {
           //agregar la caja inicial
            int montoTotal = 0;
            List<movimientosmodel> lmov = caja.ListarMovimientosCajaXDia(fecha);
            foreach (movimientosmodel item in lmov)
            {
                if (item.entradaSalida == "Salida")
                {
                    montoTotal -= item.monto;
                }

                else
                {
                    if (item.entradaSalida == "Ingreso")
                    {
                        montoTotal += item.monto;
                    }
                }
            }
            montoTotal += ObtenerCajaInicial(fecha);
            return montoTotal;
        }

        public static int ObtenerCajaInicial(DateTime fecha)
        {
            int caja = 0;
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerCajaInicial";
            cmd.Parameters.AddWithValue("Fecha", fecha);
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    caja = (int)lector["montoInicial"];                    
                }
            }
            con.Close();
            return caja;
        }
    }
}