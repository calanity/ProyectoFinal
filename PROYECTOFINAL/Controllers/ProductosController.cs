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
            var idLocal =(Session["idLocal"]);

            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var lista = producto.ListarProductos(Convert.ToInt16(idLocal));
                return View(lista);
            }
        }

        public ActionResult Agreg()
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                TempData.Remove("idEditar");

                TempData.Add("idEditar", id);
                productomodel prod = new productomodel();
                prod = producto.ObtenerProducto(id);
                return View(prod);
            }
        }

        public ActionResult Agregar(FormCollection formulario)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var nombre = Request.Form["nombre"];
                var categoria = Request.Form["categoria"];
                var proveedor = Request.Form["proveedor"];
                var precio = Request.Form["precio"];
                var stockminimo = Request.Form["stockMinimo"];
                var accion = Request.Form["accion"];
                idLocal = Convert.ToInt16(Session["idLocal"]);



                //agrega los productos a la base de datos y despues los lista y retorna a index
                using (MySqlConnection con = producto.AbrirConexion())
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "crearArticulo";
                    cmd.Parameters.AddWithValue("Nomb", nombre);
                    cmd.Parameters.AddWithValue("cate", categoria);
                    cmd.Parameters.AddWithValue("prec", precio);
                    cmd.Parameters.AddWithValue("prove", proveedor);
                    cmd.Parameters.AddWithValue("stkMinimo", stockminimo);
                    cmd.Parameters.AddWithValue("idLocal", idLocal);


                    int registros = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (accion == null)
                { return View("Index"); }
                else
                { return View("../Proveedores/Compra"); }

            }
        }
        [HttpPost]
        public ActionResult Editar(FormCollection formulario)
        {
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                var id = TempData["idEditar"];
                var nombre = Request.Form["nombre"];
                var categoria = Request.Form["categoria"];
                var precio = Request.Form["precio"];
                var proveedor = Request.Form["proveedor"];
                var stockActual = Request.Form["stockActual"];
                var stockminimo = Request.Form["stockMinimo"];

                using (MySqlConnection con = producto.AbrirConexion())
                {
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
                }
                TempData.Remove("idEditar");
                return View("Index");
            }
        }
        public ActionResult Eliminar(int id)
        {
            //elimina el producto de la base de datos
            //pregunta si es cero, sino no lo elimina
            var idLocal = Session["idLocal"];
            if (Session["idLocal"] == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
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
}