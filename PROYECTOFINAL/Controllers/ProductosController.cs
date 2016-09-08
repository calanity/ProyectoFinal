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
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            var lista = producto.ListarProductos();
            return View(lista);
        }
        
        public ActionResult Agreg()
        {
            return View();
        }

        public ActionResult Editar(int id)
        {
            TempData.Add("idEditar", id);
            productomodel prod = new productomodel();
            prod=  producto.ObtenerProducto(id);
            return View(prod);
        }
        public ActionResult Agregar(FormCollection formulario)
        {
            var nombre = Request.Form["nombre"];
            var categoria = Request.Form["categoria"];
            var proveedor = Request.Form["proveedor"];
            var precio = Request.Form["precio"];
            var stockActual = Request.Form["stockActual"];
            var stockminimo = Request.Form["stockMinimo"];

            //agrega los productos a la base de datos y despues los lista y retorna a index
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "crearArticulo";
            cmd.Parameters.AddWithValue("Nomb", nombre);
            cmd.Parameters.AddWithValue("cate", categoria);
            cmd.Parameters.AddWithValue("prec", precio);
            cmd.Parameters.AddWithValue("prove", proveedor);
            cmd.Parameters.AddWithValue("stkActual", stockActual);
            cmd.Parameters.AddWithValue("stkMinimo", stockminimo);
            

            int registros = cmd.ExecuteNonQuery();
            con.Close();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Editar(FormCollection formulario)
        {
            var id = TempData["idEditar"];
            var nombre = Request.Form["nombre"];
            var categoria = Request.Form["categoria"];
            var precio = Request.Form["precio"];
            var proveedor = Request.Form["proveedor"];
            var stockActual = Request.Form["stockActual"];
            var stockminimo = Request.Form["stockMinimo"];

            
            MySqlConnection con = producto.AbrirConexion();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.CommandText = "ActualizarArticulo";
            cmd.Parameters.AddWithValue("idArti", id);
            cmd.Parameters.AddWithValue("Nomb", nombre);
            cmd.Parameters.AddWithValue("cate", categoria);
            cmd.Parameters.AddWithValue("prec", precio);
            cmd.Parameters.AddWithValue("stkActual", stockActual);
            cmd.Parameters.AddWithValue("stkMinimo", stockminimo);
            cmd.Parameters.AddWithValue("prove", proveedor);

            int registros = cmd.ExecuteNonQuery();
            con.Close();
            TempData.Remove("idEditar");
            return View("Index");
        }
       
        public ActionResult Eliminar(int id)
        {
            //elimina el producto de la base de datos
            //pregunta si es cero, sino no lo elimina
            int registros = producto.EliminarProducto(id);
            if (registros > 0)
            {
                return View("Index");
            }
            else
            {
                return View("");
            }
            
        }

    }
}