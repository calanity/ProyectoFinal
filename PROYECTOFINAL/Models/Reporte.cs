﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models
{
    public class Reporte
    {
        public static List<productomodel> ListarProductosVendidosEntreFechas(DateTime fecha1, DateTime fecha2)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductosEntreFechas";
            cmd.Parameters.AddWithValue("fecha1", fecha1.Date);
            cmd.Parameters.AddWithValue("fecha2", fecha2.Date);

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                //prod.MedioPago = (string)lector["MedioPago"];

                prod.Fecha = (DateTime)lector["Fecha"];
                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ListarProductoVendido(int produc, DateTime fecha1, DateTime fecha2)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductoFecha";
            cmd.Parameters.AddWithValue("producto", produc);
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);


            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
               // prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductosEfectivo(DateTime fecha1, DateTime fecha2)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductosVenta";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("medioP", "Efectivo");

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
              // prod.MedioPago = (string)lector["MedioPago"];


                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductoEfectivo(DateTime fecha1, DateTime fecha2, int produc)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductoVentas";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prod", produc);
            cmd.Parameters.AddWithValue("medioP", "Efectivo");


            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
            //    prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductoTarjeta(DateTime fecha1, DateTime fecha2, int prodi)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductoVentas";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prod", prodi);
            cmd.Parameters.AddWithValue("medioP", "Tarjeta");


            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
                //prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductosTarjetas(DateTime fecha1, DateTime fecha2)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductosVenta";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("medioP", "Tarjeta");

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
                //prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductoProvedorTarjeta(DateTime fecha1, DateTime fecha2, int proveedor)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductosVentaProveedor";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prov", proveedor);
            cmd.Parameters.AddWithValue("medioP", "Tarjeta");

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
                //prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductoProvedorEfectivo(DateTime fecha1, DateTime fecha2, int proveedor)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProductosVentaProveedor";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prov", proveedor);
            cmd.Parameters.AddWithValue("medioP", "Efectivo");

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
               // prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> ProductosProveedor(DateTime fecha1, DateTime fecha2, int proveedor)
        {
            List<productomodel> lista = new List<productomodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VentasProveedor";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prove", proveedor);

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                productomodel prod = new productomodel();
                prod.id = Convert.ToInt16(lector["IdArticulos"]);
                prod.nombre = (string)lector["Nombre"];
                prod.Categoria = (string)lector["Categoria"];
                prod.precio = Convert.ToInt16(lector["Precio"]);
                prod.stockactual = Convert.ToInt16(lector["StockActual"]);
                prod.stockminimo = Convert.ToInt16(lector["StockMinimo"]);
                prod.Proveedor = (string)lector["Proveedor"];
                prod.Fecha = (DateTime)lector["Fecha"];
              ///  prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prod);
            }

            con.Close();
            return lista;
        }

        public static List<productomodel> GenerarReporte(DateTime fecha1, DateTime fecha2, int proveedor = 0, int categoria = 0, int prod = 0, int efectivo = 0, int tarjeta = 0)
        {
            List<productomodel> lista = new List<productomodel>();
            //si es todo 1, si es efectivo 2, si es tarjeta 3
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GenerarReporte";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prove", proveedor);
            cmd.Parameters.AddWithValue("cate", categoria);
            cmd.Parameters.AddWithValue("produ", fecha1);
            cmd.Parameters.AddWithValue("medioP", fecha1);

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
                // prod.MedioPago = (string)lector["MedioPago"];

                lista.Add(prodi);
            }




            return lista;
        }
    }
}  