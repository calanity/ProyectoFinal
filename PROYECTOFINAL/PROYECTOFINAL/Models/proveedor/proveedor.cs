﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL.Models.proveedor
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

        public static proveedormodel ObtenerProveedor()
        {
            proveedormodel prov = new proveedormodel();
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
            cmd.Parameters.AddWithValue("idProve", idProve);
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


        public static int Editar(int idProve, int saldo)
        {

            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarSaldoProveedor";
            cmd.Parameters.AddWithValue("idProve", idProve);
            int registros = cmd.ExecuteNonQuery();

            con.Close();
            return registros;
        }

    }
}