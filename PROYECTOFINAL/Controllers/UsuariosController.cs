using MySql.Data.MySqlClient;
using PROYECTOFINAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTOFINAL.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session == null)
            {
                Session.Clear();
            }
            return View(0);
        }
        public ActionResult Ingresar(FormCollection form)
        {
            
            var idLocal = Convert.ToInt16(Request.Form["nombre"]);
            var contraseña = Request.Form["contraseña"];
            usuariomodel us = new usuariomodel();

            using (MySqlConnection con = producto.AbrirConexion()) { 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerUsuario";
            cmd.Parameters.AddWithValue("id", idLocal);
            MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                if (lector.FieldCount > 0)
                {
                    us.IdUsuario = Convert.ToInt16(lector["IdUsuario"]);
                    us.Usuario = (string)lector["Nombre"];
                    us.constraseña = (string)lector["Contraseña"];
                }
            }
            con.Close();
            }

            if (us != null &&us.constraseña==contraseña)
            {
                Session["idLocal"] = idLocal;
                Session["nombre"] = us.Usuario;
                Session["contraseña"] = contraseña;
            }
            else
            {
                return View("../Usuarios/Index",1);
            }
          
            
            return View("../Home/Index");
        }
    }
}