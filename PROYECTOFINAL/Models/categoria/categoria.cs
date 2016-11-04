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
        public static List<categoriamodel> ListarCategorias(int idLocal)
        {
            
            List<categoriamodel> lCat = new List<categoriamodel>();

            using (MySqlConnection con = producto.AbrirConexion()) { 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarCategorias";
            cmd.Parameters.AddWithValue("idLocal",idLocal);
            MySqlDataReader lector = cmd.ExecuteReader();
           

            while (lector.Read())
            {
                if(lector.FieldCount>0 && Convert.ToInt16(lector["IdLocal"])==idLocal)
                { 
                categoriamodel cat = new categoriamodel();
                cat.nombre = (string)lector["Nombre"];
                cat.id = (int)lector["idCategorias"];
                lCat.Add(cat);
                }
            }
            con.Close();
            }
            return lCat;
        }
        public static categoriamodel obtenerCategoria(int id)
        {
            categoriamodel categ = new categoriamodel();

            using (MySqlConnection con = producto.AbrirConexion()) { 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerCategoria";
            cmd.Parameters.AddWithValue("idCategoria", id);
            MySqlDataReader lector = cmd.ExecuteReader();
            

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    categ.id = (int)lector["idCategorias"];
                    categ.nombre = (string)lector["nombre"];
                }
            }
            con.Close();
            }
            return (categ);
        }
    }
}