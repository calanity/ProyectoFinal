﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



    namespace PROYECTOFINAL.Models
{
    public class venta
    {
        public static int CrearVenta(DateTime fecha, double montot, string mediop, int idlocal)
        {

            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "crearVenta";
            cmd.Parameters.AddWithValue("fech", fecha);
            cmd.Parameters.AddWithValue("montot", montot);
            cmd.Parameters.AddWithValue("mediop", mediop);
            cmd.Parameters.AddWithValue("idLocal", idlocal);
            int registros = cmd.ExecuteNonQuery();
            con.Close();
            return registros;
        }


        public static int ObtenerIdVenta()
        {
            int id = 0;
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerIdVentaActual";
            MySqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                id = Convert.ToInt16(lector["id"]);
            }

            con.Close();

            return id;
        }

        public static void CrearDetalleVenta(int idArti, int prec, int canti, int subt, int idVen)
        {
            int hola = 0;
            //si esa venta ya tiene el articulo creado le suma 1 y multiplica el subtotal
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleExistente";
            cmd.Parameters.AddWithValue("idDetalleV", idArti);
            cmd.Parameters.AddWithValue("idventaactual2", idVen);
            MySqlDataReader lector = cmd.ExecuteReader();
            int registros = 0;

            while (lector.Read())
            {
                if ((lector["count(*)"]) == null)
                {
                    registros = 0;
                }
                else
                {
                    registros = Convert.ToInt16(lector["count(*)"]);
                }
            }
            con.Close();


            if (registros == 0)
            {

                hola = venta.CrearDetVenta(idArti, prec, canti, subt, idVen);
            }
            else
            {
                hola = venta.ActualizarDetalle(subt, canti, prec, idVen, idArti);
            }
            con.Close();


        }



        public static int ActualizarDetalle(int subt, int canti, int prec, int idven, int idArtic)
        {
            //actualizar detVenta
            canti = canti + 1;
            subt = (prec * canti);
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarProductoDV";
            cmd.Parameters.AddWithValue("canti", canti);
            cmd.Parameters.AddWithValue("subti", subt);
            cmd.Parameters.AddWithValue("idVen", idven);
            cmd.Parameters.AddWithValue("idArtic", idArtic);


            int registros2 = cmd.ExecuteNonQuery();
            con.Close();
            return (registros2);
        }

        public static int CrearDetVenta(int idArti, int prec, int canti, int subt, int idVen)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "crearDetalleVenta";
            cmd.Parameters.AddWithValue("idArti", idArti);
            cmd.Parameters.AddWithValue("prec", prec);
            cmd.Parameters.AddWithValue("canti", canti);
            cmd.Parameters.AddWithValue("subt", subt);
            cmd.Parameters.AddWithValue("idVen", idVen);

            int registros2 = cmd.ExecuteNonQuery();
            con.Close();
            return (registros2);
        }


        public static void ActualizarStockProducto(int idArti, int cantidad)
        {

            int cantidad2 = 0;
            cantidad2 = (producto.ObtenerStockActual(idArti)) - cantidad;
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RestoStockVenta";
            //obtengo stock actual y mando el nuevo            
            cmd.Parameters.AddWithValue("stockActu", cantidad2);
            cmd.Parameters.AddWithValue("articId", idArti);
            int registros2 = cmd.ExecuteNonQuery();
            con.Close();


        }

        public static List<ventamodel> ListarVentasxDia(DateTime fecha, int idLocal)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarVentasxDia";
            cmd.Parameters.AddWithValue("fech", fecha);
            cmd.Parameters.AddWithValue("idLocal", idLocal);
            MySqlDataReader lector = cmd.ExecuteReader();
            List<ventamodel> l3 = new List<ventamodel>();

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    ventamodel oventa = new ventamodel();

                    if (lector["MedioPago"] != null && lector["MontoTotal"] != null)
                    {
                        if (Convert.ToInt16(lector["idLocal"]) == idLocal)
                        {
                            oventa.id = Convert.ToInt16(lector["idventas"]);
                            oventa.MedioPago = (string)(lector["MedioPago"]);
                            oventa.MontoTotal = (int)(lector["MontoTotal"]);
                            l3.Add(oventa);
                        }
                    }
                }


            }
            con.Close();
            return l3;


        }
               

        public static List<ventamodel> ListarVentasxMes(int mes, int año, int idLocal)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListarVentasMensuales";
            cmd.Parameters.AddWithValue("mes", mes);
            cmd.Parameters.AddWithValue("año", año);
            cmd.Parameters.AddWithValue("idLocal", idLocal);

            MySqlDataReader lector = cmd.ExecuteReader();
            List<ventamodel> l3 = new List<ventamodel>();

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    ventamodel oventa = new ventamodel();

                    if (lector["MedioPago"] != null && lector["MontoTotal"] != null)
                    {
                        if(Convert.ToInt16(lector["idLocal"]) ==idLocal)
                        { 
                            oventa.id = Convert.ToInt16(lector["idventas"]);
                            oventa.MedioPago = (string)(lector["MedioPago"]);
                            oventa.MontoTotal = (int)(lector["MontoTotal"]);
                            oventa.Fecha = (DateTime)(lector["Fecha"]);
                            l3.Add(oventa);
                        }
                    }
                }


            }
            con.Close();
            return l3;

        }

        public static int ObtenerTotalTarjeta(DateTime fecha, int idLocal)
        {
            //obtiene de la base de datos el total vendido con tarjeta
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerVentaTarjetaXDia";
            cmd.Parameters.AddWithValue("fech", fecha);
            cmd.Parameters.AddWithValue("idLocal", idLocal);

            MySqlDataReader lector = cmd.ExecuteReader();

            int totTarjeta = 0;

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    if (lector["MontoTotal"] != DBNull.Value)
                    {
                        totTarjeta = Convert.ToInt16(lector["MontoTotal"]);
                    }
                }
            }
            con.Close();
            return (totTarjeta);
        }
        public static int ObtenerTotalEfectivo(DateTime fecha, int idLocal)
        {
            //obtiene de la cbase de datos el total vendido con efectivo
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerVentaEfectivoxDia";
            cmd.Parameters.AddWithValue("fech", fecha);
            cmd.Parameters.AddWithValue("idLocal", idLocal);

            MySqlDataReader lector = cmd.ExecuteReader();

            int totEfectivo = 0;

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    if (lector["MontoTotal"] != DBNull.Value)
                    {
                        totEfectivo = Convert.ToInt16(lector["MontoTotal"]);
                    }
                }

            }
            con.Close();
            return (totEfectivo);
        }

        public static int ObtenerTotalVendidoXDia(DateTime fecha, int idLocal)
        {
            //obtienen el total vendido en un dia
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerVentaTotalXDia";
            cmd.Parameters.AddWithValue("fech", fecha);
            cmd.Parameters.AddWithValue("idLocal", idLocal);

            MySqlDataReader lector = cmd.ExecuteReader();

            int TotalDia = 0;

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    if (lector["Total"] != DBNull.Value)
                    {
                        TotalDia = Convert.ToInt16((lector["Total"]));
                    }
                }
            }
            con.Close();
            return (TotalDia);
        }


        public static int InsertarMovimientoCaja()
        {
            movimientosmodel mov = new movimientosmodel();
            mov = movimientos.ObtenerUltimoMovimiento();
            //inserto el movimiento en los movimientos de la caja

            MySqlConnection con1 = producto.AbrirConexion();
            MySqlCommand cmd1 = con1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "InsertarMovCaja";
            mov.Fecha = mov.Fecha.Date;
            cmd1.Parameters.AddWithValue("idMovimiento", mov.IdMovimientos);
            cmd1.Parameters.AddWithValue("fecha", mov.Fecha);
            cmd1.Parameters.AddWithValue("monto", mov.monto);
            cmd1.Parameters.AddWithValue("idConcepto", mov.idConcepto);

            int registros2 = cmd1.ExecuteNonQuery();
            con1.Close();

            return registros2;
        }


        public static int EliminarVenta(int id, int idLocal)
        {
            /*pregunto si es efectivo o tarjeta
            sumo el stock de los productos
            saco el movimiento, resto la plata de la venta          
             */
            
            //obtengo la venta
            ventamodel venta = new ventamodel();
            venta = ObtenerDetalleVenta(id);

            //eliminar detalle venta
            //obtengo el stock actual de los productos y le sumo los de la compra

            //recorro la lista de los productos de la venta y en cada uno obtenfo el stock actual
            //y le sumo la cantidad de la venta
            foreach (productomodel item in venta.ListaArticulos)
            {
                
                int stockActual = producto.ObtenerStockActual(item.id);
                stockActual += item.cantidad;
                producto.AltaProductos(item.id, stockActual, Convert.ToInt16(idLocal));
            }
            int mediopag;
            if (venta.MedioPago == "Efectivo")
            { mediopag = 1; }
            else { mediopag = 2; }
            MySqlConnection con1 = producto.AbrirConexion();
            MySqlCommand cmd1 = con1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "EliminarVenta";            
            cmd1.Parameters.AddWithValue("idDetalle",id);
            cmd1.Parameters.AddWithValue("medioPago", mediopag);


            int registros2 = cmd1.ExecuteNonQuery();
            con1.Close();
            return 1;
        }

        public static ventamodel ObtenerDetalleVenta(int id)
        {
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleVenta";
            cmd.Parameters.AddWithValue("id", id);
            MySqlDataReader lector = cmd.ExecuteReader();
            ventamodel vent = new ventamodel();
            List<productomodel> lista = new List<productomodel>();

            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    vent.id = Convert.ToInt16(lector["idventas"]);
                    vent.Fecha = Convert.ToDateTime(lector["Fecha"]);
                    vent.MontoTotal = Convert.ToInt16(lector["MontoTotal"]);
                    vent.MedioPago = Convert.ToString(lector["MedioPago"]);
                    vent.IdMovimientos = Convert.ToInt16(lector["IdMovimientos"]);
                    vent.IdConcepto= Convert.ToInt16(lector["IdConcepto"]);
                    productomodel prod = new productomodel();
                    prod.id= Convert.ToInt16(lector["idArticulo"]);
                    prod.precio= Convert.ToInt16(lector["precio"]);
                    prod.cantidad= Convert.ToInt16(lector["cantidad"]);
                    prod.subtotal= Convert.ToInt16(lector["subtotal"]);
                    lista.Add(prod);

                }
            }
            vent.ListaArticulos = lista;
            con.Close();
            return vent;

        }



    }
}
