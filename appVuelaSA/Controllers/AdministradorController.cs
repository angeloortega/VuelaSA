using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appVuelaSA.Models;

namespace appVuelaSA.Controllers
{
    public class AdministradorController : Controller
    {
        private proyecto2RequeEntities db = new proyecto2RequeEntities();

        public ActionResult MainAdministrador()
        {
            //prueba
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RegistrarPase()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarPase(FormCollection collection)
        {
            try
            {
                decimal idpasaje = Convert.ToDecimal(collection["idpasajeabordar"].ToString());

                pasajeabordar pasaje = db.pasajeabordar.Find(idpasaje);

                pasaje.estado = "Registrado";

                db.Entry(pasaje).State = EntityState.Modified;
                db.SaveChanges();
                return View("MainAdministrador");

            }
            catch(Exception e)
            {
                return View("RegistrarPase");
            }

            


        }

    }
}