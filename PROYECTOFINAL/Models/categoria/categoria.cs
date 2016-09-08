using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace PROYECTOFINAL.Models
{
    public class categoria
    {
        public static List<categoriamodel> ListarCategorias()
        {
            List<categoriamodel> lCat = new List<categoriamodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarCategorias";
            MySqlDataReader lector = cmd.ExecuteReader();
           

            while (lector.Read())
            {
                if(lector.FieldCount>0)
                { 
                categoriamodel cat = new categoriamodel();
                cat.nombre = (string)lector["Nombre"];
                cat.id = (int)lector["idCategorias"];
                lCat.Add(cat);
                }
            }
            con.Close();
            return lCat;
        }
        public static categoriamodel obtenerCategoria(int id)
        {
            categoriamodel categ = new categoriamodel();

            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerCategoria";
            cmd.Parameters.AddWithValue("idCategoria", id);
            MySqlDataReader lector = cmd.ExecuteReader();
            

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    categ.nombre = (string)lector["nombre"];
                }
            }
            con.Close();
            return (categ);
        }
    }
}