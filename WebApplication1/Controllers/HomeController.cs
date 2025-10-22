using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            string descripcionApp = "Página de descripción de la aplicación.";
            ViewBag.Message = descripcionApp;

            return View();
        }

        public ActionResult Contact()
        {
            string mensajeContacto = "Información de contacto.";
            ViewBag.Message = mensajeContacto;

            return View();
        }
    }
}