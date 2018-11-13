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
        private proyectoRequeEntities db = new proyectoRequeEntities();

        public ActionResult MainAdministrador(string cliente, string viaje)
        {

            var res = from m in db.reservacion
                        select m;
            List<reservacion> reservaciones = res.ToList();
            List<reservacion> nuevasReservaciones;
            if (!String.IsNullOrEmpty(cliente))
            {
                int clienteInt = Int32.Parse(cliente);
                nuevasReservaciones = new List<reservacion>();
                foreach (reservacion v in reservaciones)
                {
                    if (v.idcliente == clienteInt)
                    {
                        nuevasReservaciones.Add(v);
                    }
                }
                reservaciones = nuevasReservaciones;
            }
            if (!String.IsNullOrEmpty(viaje))
            {
                int viajeInt = Int32.Parse(viaje);
                nuevasReservaciones = new List<reservacion>();
                foreach (reservacion v in reservaciones)
                {
                    if (v.idviaje == viajeInt)
                    {
                        nuevasReservaciones.Add(v);
                    }
                }
                    reservaciones = nuevasReservaciones;
            }
            var resv = reservaciones.OrderByDescending(s => s.idreservacion);
            return View(resv);
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