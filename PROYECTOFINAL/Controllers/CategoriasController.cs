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
    public class CategoriasController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Agreg()
        {
            return View();

        }
        public ActionResult Editar(int id)
        {
            TempData.Remove("idEditar");
            TempData.Add("idEditar", id);
            categoriamodel categ = new categoriamodel();
            categ = categoria.obtenerCategoria(id);
            TempData.Remove("idEditar");
            return View(categ);
        }

        public ActionResult Agregar(FormCollection formulario)
        {
            var nombre = Request.Form["nombre"];
            int idLocal = Convert.ToInt16(Session["idLocal"]);

            //agrega los productos a la base de datos y despues los lista y retorna a index
            using (MySqlConnection con = producto.AbrirConexion()) { 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "crearCategoria";
            cmd.Parameters.AddWithValue("Nomb", nombre);
            cmd.Parameters.AddWithValue("idLocal",idLocal);    

            int registros = cmd.ExecuteNonQuery();
            con.Close();
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Editar(FormCollection formulario)
        {
            var id = TempData["idEditar"];
            var nombre = Request.Form["nombre"];


            using (MySqlConnection con = producto.AbrirConexion())
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EditarCategoria";
                cmd.Parameters.AddWithValue("idCate", id);
                cmd.Parameters.AddWithValue("Nomb", nombre);


                int registros = cmd.ExecuteNonQuery();
                con.Close();
            }
            TempData.Remove("idEditar");
            return View("Index");
        }

        public ActionResult Eliminar(int id)
        {
            //obtener articulos por categoria si es 0 ejecuto 
            int registros = -1;
            bool tiene = false;
            int idLocal = Convert.ToInt16(Session["idLocal"]);
          using(MySqlConnection con = producto.AbrirConexion()){ 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "listarProductosxCate";
            cmd.Parameters.AddWithValue("cate", id);
                cmd.Parameters.AddWithValue("loc", idLocal);



                MySqlDataReader lector = cmd.ExecuteReader();


            while (lector.Read())
            {
                tiene = true;                
            }
            if (tiene == false)
            {
                    using (MySqlConnection con1 = producto.AbrirConexion()) { 
                        MySqlCommand cmd1 = con1.CreateCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "EliminarCategoria";
                cmd1.Parameters.AddWithValue("idCateg", id);
                registros = cmd1.ExecuteNonQuery();
                con1.Close();
                    }
                    return View("Index");
            }
            else
            {
                con.Close();
                return View("Error");
            }

            }
        }

    }
}
