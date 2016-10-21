using System;
using System.Collections.Generic;
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
            return View();
        }
        public ActionResult Ingresar(FormCollection form)
        {
            var idLocal = Convert.ToInt16(Request.Form["nombre"]);
            var contraseña = Request.Form["contraseña"];
            Session["idLocal"] = idLocal;
            if (idLocal == 1)
            {
                Session["nombre"] = "Concepcion";
            }
            else
            {
                Session["nombre"] = "Gualeguaychu";
            }
            Session["contraseña"] = contraseña;
            var password = Session["contraseña"].ToString();
            var nom = Session["nombre"].ToString();
            var loc = Session["idLocal"];
            return View("../Home/Index");
        }
    }
}