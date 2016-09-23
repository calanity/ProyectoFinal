using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Mvc;
using System.IO;
using System.Net.Mail;
using System.Net;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

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
                oprod.IdCategoria = (int)lector["IdCat"];
                oprod.precio = (int)lector["precio"];
                oprod.stockactual = (int)lector["StockActual"];
                oprod.stockminimo = (int)lector["StockMinimo"];
                oprod.IdProveedor = (int)lector["IdProve"];
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
                prod.IdCategoria = (int)lector["idCat"];
                prod.IdProveedor = (int)lector["IdProve"];
                prod.Proveedor = (string)lector["Proveedor"];
                prod.stockactual = (int)lector["StockActual"];
                prod.stockminimo = (int)lector["StockMinimo"];
                prod.Categoria = (string)lector["CategoriaNombre"];



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
                precio = (int)lector["Precio"];
            }
            con.Close();
            return precio;
        }

        public static int AltaProductos(int id, int stockActual)
        {
            int registros = 0;

            return registros;
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

        public static List<productomodel> ObtenerStockMinimoYActual(List<productomodel> list)
        {
            int stockminimo = 0;
            int stockactual = 0;
            List<productomodel> listaEnviar = new List<productomodel>();
            foreach (productomodel item in list)
            {
                MySqlConnection con = producto.AbrirConexion();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerStockMinimoYActual";
                cmd.Parameters.AddWithValue("idArti", item.id);
                MySqlDataReader lector = cmd.ExecuteReader();



                while (lector.Read())
                {
                    stockactual = Convert.ToInt16(lector["StockActual"]);
                    stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                }

                con.Close();

                if (stockactual <= stockminimo)
                {
                    //lo agrego a la lista para el mail
                    listaEnviar.Add(item);
                }

                con.Close();
            }

            return listaEnviar;

        }


        public static int EliminarProducto(int id)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarArticulo";
            cmd.Parameters.AddWithValue("idArti", id);
            int registros = cmd.ExecuteNonQuery();
            con.Close();
            return registros;
        }

        public static void EnviarMailFaltaStock(List<productomodel>listaEnviar)
        {
            string path = "D:/ProyectoFinal/archivo.pdf";
            StreamWriter MiObjetoArchivo = new StreamWriter(path);
            // string path2 = "D:/ProyectoFinal/";
            
          
           

            foreach (productomodel producto in listaEnviar)
            {
                MiObjetoArchivo.WriteLine("Nombre del producto: " + producto.nombre + "," + "Stock actual" + producto.stockactual + "," + "Proveedor" + producto.Proveedor);
            }

            MiObjetoArchivo.Close();


            //mando el mail

            var message = new MailMessage();
            message.To.Add(new MailAddress("calanity@gmail.com")); // reemplazar por un valor valido
            message.From = new MailAddress("silgralevi@hotmail.com"); // reemplazar por un valor valido
            message.Subject = "Falta de stock";
            message.Attachments.Add(new Attachment(path));
            //message.Attachments.Add(new Attachment("/" + filename+".pdf"));
            //message.Attachments.Add((document));
            message.Body = "Adjuntamos la falta de stock de un/os producto/s";
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            var credential = new NetworkCredential
            {
                UserName = "calanity@gmail.com", // reemplazar por un valor valido
                Password = "" // reemplazar por un valor valido
            };

            smtp.Credentials = credential;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}
