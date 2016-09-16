using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class tarjeta
    {
        public void ListarNombresTarjetas(){       }

        public int InsertarDatosTarjeta(int tipo, int cuotas, int marca, int cupon, int monto )
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertarDatosTarjetas";
            cmd.Parameters.AddWithValue("tipot", tipo);
            cmd.Parameters.AddWithValue("cuot", cuotas);
            cmd.Parameters.AddWithValue("marca", marca);
            cmd.Parameters.AddWithValue("cup", cupon);
            cmd.Parameters.AddWithValue("mon", monto);


            int registros2 = cmd.ExecuteNonQuery();
            con.Close();
            return (registros2);
        }
    }
}