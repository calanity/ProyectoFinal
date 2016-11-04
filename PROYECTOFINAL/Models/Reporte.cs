using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class Reporte
    {
        public static List<productomodel> GenerarReporte(DateTime fecha1, DateTime fecha2, int medioP, int proveedor = 0, int categoria = 0, int prod = 0)
        {
            List<productomodel> lista = new List<productomodel>();

            using (MySqlConnection con = producto.AbrirConexion())
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GenerarReporte";
                cmd.Parameters.AddWithValue("fecha1", fecha1);
                cmd.Parameters.AddWithValue("fecha2", fecha2);
                cmd.Parameters.AddWithValue("cate", categoria);
                cmd.Parameters.AddWithValue("produ", prod);
                cmd.Parameters.AddWithValue("medioP", medioP);
                cmd.Parameters.AddWithValue("prove", proveedor);


                MySqlDataReader lector = cmd.ExecuteReader();


                while (lector.Read())
                {
                    productomodel prodi = new productomodel();
                    prodi.id = Convert.ToInt16(lector["IdArticulos"]);
                    prodi.nombre = (string)lector["Nombre"];
                    prodi.Categoria = (string)lector["Categoria"];
                    prodi.precio = Convert.ToInt16(lector["Precio"]);
                    prodi.stockactual = Convert.ToInt16(lector["StockActual"]);
                    prodi.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                    prodi.Proveedor = (string)lector["Proveedor"];
                    prodi.Fecha = (DateTime)lector["Fecha"];


                    lista.Add(prodi);
                }


            }

            return lista;
        }

        public static List<ventamodel> DetalleVenta(int id)
        {
            List<ventamodel> detalle = new List<ventamodel>();
            List<productomodel> lista2 = new List<productomodel>();
            ventamodel venta = new ventamodel();
            
        
            using (MySqlConnection con = producto.AbrirConexion())
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerDetalleVenta";
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataReader lector = cmd.ExecuteReader();


                while (lector.Read())
                {
                    productomodel prodi = new productomodel();
                    prodi.id = Convert.ToInt16(lector["idArticulo"]);
                    prodi.nombre = (string)lector["Nombre"];
                    prodi.Categoria = (string)lector["Categoria"];
                    prodi.cantidad = (int)lector["Cantidad"];
                    prodi.precio = Convert.ToInt16(lector["Precio"]);
                    prodi.Fecha = (DateTime)lector["Fecha"];
                    prodi.MedioPago = (string)lector["MedioPago"];
                    prodi.MontoTotal = (int)lector["MontoTotal"];


                    //agregar monto total

                    lista2.Add(prodi);
                }
                
                venta.ListaArticulos=(lista2);               
                venta.MedioPago = lista2[0].MedioPago;
                venta.MontoTotal = lista2[0].MontoTotal;
                detalle.Add(venta);
                return detalle;
            }
        }

    }
}
