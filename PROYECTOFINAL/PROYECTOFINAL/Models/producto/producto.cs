using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Mvc;

namespace PROYECTOFINAL.Models
{
    public class producto
    {
        
        public static MySqlConnection AbrirConexion()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "us-cdbr-azure-east-c.cloudapp.net";
            builder.UserID = "bcaf7709a9bf09";
            builder.Password = "1d3406fc";
            builder.Database = "acsm_49aeb6a572de874";
            MySqlConnection conn = new MySqlConnection(builder.ToString());
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            return conn;

        }

        public static productomodel ObtenerProducto(int id)
        {
            productomodel oprod = new productomodel();
            //obtiene el producto a partir de un id "ObtenerProducto"
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerProducto";
            cmd.Parameters.AddWithValue("idArti", id);
            MySqlDataReader lector = cmd.ExecuteReader();
            oprod.id = id;

            while (lector.Read())
            {
                oprod.nombre = (string)lector["nombre"];
                oprod.categoria= (int)lector["IdCat"];
                oprod.precio= (int)lector["precio"];
                oprod.stockactual= (int)lector["StockActual"];
                oprod.stockminimo= (int)lector["StockMinimo"];
            }
            con.Close();
            return oprod;
        }
        public static List<productomodel> ListarProductos()
        {
            List<productomodel> lprod = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarArticulos";
            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.nombre = (string)lector["Nombre"];
                prod.id = (int)lector["idArticulos"];
                prod.precio = (int)lector["precio"];
                prod.categoria = (int)lector["idCat"];
                prod.stockactual= (int)lector["StockActual"];
                prod.stockminimo= (int)lector["StockMinimo"];


                lprod.Add(prod);
            }
            con.Close();

            return lprod;

        }
        public static List<productomodel> ListarProductosXCategoria(int cate)
        {
            List<productomodel> lProd = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarProductosxCate";
            cmd.Parameters.AddWithValue("cate", cate);
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.nombre = (string)lector["Nombre"];
                prod.id = (int)lector["idArticulos"];
                lProd.Add(prod);
            }
            con.Close();
            return lProd;
        }

        public static int ObtenerPrecio(int id)
        {
            int precio = 0;
            List<productomodel> lProd = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "precioProducto";
            cmd.Parameters.AddWithValue("idArti", id);
            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
               precio= (int)lector["Precio"];
            }
            con.Close();
            return precio;
        }

        public static string obtenerNombre(int id)
        {
            string nombre = "";
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "nombreProducto";
            cmd.Parameters.AddWithValue("idArti", id);
            MySqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                nombre = (string)lector["nombre"];
            }
            con.Close();

            return nombre;
        }

        public static int ObtenerStockActual(int idArti)
        {
            //obtengo el stock de un producto
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerStockActual";
            cmd.Parameters.AddWithValue("idArtii2", idArti);
            MySqlDataReader lector = cmd.ExecuteReader();

            int stockActual = 0;

            while (lector.Read())
            {
                stockActual = Convert.ToInt16(lector["StockActual"]);
            }
            con.Close();
            return (stockActual);
        }

        internal static object ObtenerStockActual(object prodi)
        {
            throw new NotImplementedException();
        }
    }
}
