using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class usuario
    {
        public static List<usuariomodel>ListarUsuarios()
        {
            List<usuariomodel> lista = new List<usuariomodel>();

            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarUsuarios";
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    usuariomodel usuario = new usuariomodel();
                    usuario.IdUsuario = (int)lector["IdUsuario"];
                    usuario.Usuario = (string)lector["Nombre"];
                    usuario.constraseña = (string)lector["Contraseña"];
                    lista.Add(usuario);
                }
            }
            con.Close();            
            return (lista);
        }

       
    }
}