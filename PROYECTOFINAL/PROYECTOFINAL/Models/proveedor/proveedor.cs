using MySql.Data.MySqlClient;
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
                    prov.deuda = (int)lector["Deuda"];

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
    }
}