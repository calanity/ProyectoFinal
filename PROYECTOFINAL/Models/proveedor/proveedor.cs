﻿using Libreria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace PROYECTOFINAL.Models
{
    public class proveedor
    {
        public static List<proveedormodel> ListarProveedores()
        {
            List<proveedormodel> lProv = new List<proveedormodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarProveedores";
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    proveedormodel prov = new proveedormodel();
                    prov.nombre = (string)lector["Nombre"];
                    prov.idProveedores = (int)lector["idProveedores"];
                    prov.telefono = (int)lector["Telefono"];
                    prov.saldo = (int)lector["Saldo"];

                    lProv.Add(prov);
                }
            }
            con.Close();
            return lProv;
        }

        public static proveedormodel ObtenerProveedor(int id)
        {
            proveedormodel prov = new proveedormodel();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerProveedor";
            cmd.Parameters.AddWithValue("id", id);
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    prov.nombre = (string)lector["Nombre"];                   
                    prov.telefono = (int)lector["Telefono"];
                    prov.saldo = (int)lector["Saldo"];

                  }
            }
            con.Close();       

            return prov;
        }

        public static int CrearProveedor(String Nombre, int Telefono)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CrearProveedor";
            cmd.Parameters.AddWithValue("Nomb", Nombre);
            cmd.Parameters.AddWithValue("Telef", Telefono);
            int registros = cmd.ExecuteNonQuery();
            con.Close();
            return registros;
        }

        public static int EditarProveedor(int idProve, String Nombre, int Telefono)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarProveedor";
            cmd.Parameters.AddWithValue("idProve", idProve);
            cmd.Parameters.AddWithValue("nomb", Nombre);
            cmd.Parameters.AddWithValue("telef", Telefono);
            int registros = cmd.ExecuteNonQuery();
            con.Close();
            return registros;
        }

        public static int EliminarProveedor(int idProve)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;      
            cmd.CommandText = "EliminarProveedor";
            cmd.Parameters.AddWithValue("idProve", idProve);
            int registros = cmd.ExecuteNonQuery();
            con.Close();

            return registros;
         }

        public static proveedormodel ObtenerSaldo(int idProve)
        {
            
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerSaldoProveedor";
            cmd.Parameters.AddWithValue("idProv", idProve);
            MySqlDataReader lector = cmd.ExecuteReader();
            proveedormodel prov = new proveedormodel();

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {                    
                    prov.nombre = (string)lector["Nombre"];
                    prov.idProveedores = (int)lector["idProveedores"];
                    prov.saldo = (int)lector["Saldo"];

                }
            }
            con.Close();
            return prov;
        }

        //registra el pago a un proveedor
        public static int EditarSaldo(int idProve, int saldo)
        {
            proveedormodel saldoActual = ObtenerSaldo(idProve);
            saldo = saldoActual.saldo - saldo;
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarSaldoProveedor";
            cmd.Parameters.AddWithValue("idProve", idProve);
            cmd.Parameters.AddWithValue("sald", saldo);

            int registros = cmd.ExecuteNonQuery();

            con.Close();
            return registros;
        }

        public static int RegistrarCompra(int idProve, int total, int produc, int cantidad, int unitario, int idLocal)
        {
            proveedormodel saldoActual = ObtenerSaldo(idProve);
            int total2 = saldoActual.saldo + total;
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarSaldoProveedor";
            cmd.Parameters.AddWithValue("idProve", idProve);
            cmd.Parameters.AddWithValue("sald", total2);
            int registros = cmd.ExecuteNonQuery();
            con.Close();

            //dar de alta al stock del producto
            int stActual = producto.ObtenerStockActual(produc);
            stActual = stActual + cantidad;

            MySqlConnection con2 = producto.AbrirConexion();
            MySqlCommand cmd2 = con2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "AltaProductos";
            cmd2.Parameters.AddWithValue("idProd", produc);
            cmd2.Parameters.AddWithValue("stockAc", stActual);
            cmd2.Parameters.AddWithValue("idLocal", idLocal);



            int registros2 = cmd2.ExecuteNonQuery();
            con.Close();
            InsertarCompraProveedor(idProve, produc, unitario, total, cantidad);
            return registros;

           
        }

        public static void InsertarCompraProveedor(int prove, int prod, int costo, int tot, int cantidad)
        {
            MySqlConnection con3 = producto.AbrirConexion();
            MySqlCommand cmd3 = con3.CreateCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "InsertarCompraProveedor";
            cmd3.Parameters.AddWithValue("prov", prove);
            cmd3.Parameters.AddWithValue("prod", prod);
            cmd3.Parameters.AddWithValue("costo", costo);
            cmd3.Parameters.AddWithValue("tot", tot);
            cmd3.Parameters.AddWithValue("cant", cantidad);
            cmd3.Parameters.AddWithValue("fech", DateTime.Now);


            int registros2 = cmd3.ExecuteNonQuery();
            con3.Close();

        }
        public static List<productomodel> ObtenerProductosPorProveedor( int idProveedor, int idLocal)
        {
            List<productomodel> lista = new List<productomodel>();

            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarProductosxProveedor";
            cmd.Parameters.AddWithValue("id", idProveedor);
            cmd.Parameters.AddWithValue("idLocal", idLocal);

            MySqlDataReader lector = cmd.ExecuteReader();
            

            while (lector.Read())
            {
                productomodel oprod = new productomodel();
                oprod.id= (int)lector["IdArticulos"];
                oprod.nombre = (string)lector["nombre"];
                oprod.IdCategoria = (int)lector["IdCat"];
                oprod.Categoria = (string)lector["Categoria"];
                oprod.precio = (int)lector["precio"];
                oprod.stockactual = (int)lector["StockActual"];
                oprod.stockminimo = (int)lector["StockMinimo"];
                lista.Add(oprod);
            }
            con.Close();
            return lista;
        }

        public static List<compramodel> ListarComprasProve(DateTime fecha1, DateTime fecha2, int prov)
        {
            List<compramodel> lProv = new List<compramodel>();
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarComprasProv";
            cmd.Parameters.AddWithValue("fecha1", fecha1);
            cmd.Parameters.AddWithValue("fecha2", fecha2);
            cmd.Parameters.AddWithValue("prov", prov);

            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    compramodel com = new compramodel();
                    com.idcompra = Convert.ToInt16(lector["IdCompraP"]);
                    com.Proveedor = lector["Nombre"].ToString();
                    com.idProvedor = Convert.ToInt16(lector["Proveedor"]);
                    com.idProducto = Convert.ToInt16(lector["Producto"]);
                    com.Product = lector["ProductoN"].ToString();
                    com.costo = Convert.ToInt16(lector["CostoUnitario"]);
                    com.cantidad = Convert.ToInt16(lector["Cantidad"]);
                    com.total = Convert.ToInt16(lector["MontoTotal"]);
                    com.fecha = Convert.ToDateTime(lector["Fecha"]);
                    
                    lProv.Add(com);
                }
            }
            con.Close();
            return lProv;
        }

    }
}