using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

                pasaje.estado = "Sellado";

                db.Entry(pasaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MainAdministrador");

            }
            catch(Exception e)
            {
                return RedirectToAction("RegistrarPase");
            }

            


        }

        public ActionResult CheckinAdmin(int id)
        {
            reservacion res = db.reservacion.Find(id);
            if ((res.viajevuelo.viaje.horadepartida - DateTime.Now).TotalHours > 24)
            {
                return RedirectToAction("MainCliente");
            }
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }
        [HttpPost, ActionName("CheckinAdmin")]
        public ActionResult CheckinConfirmed(int id)
        {
            reservacion res = db.reservacion.Find(id);
            SqlParameter parametro1 = new SqlParameter("@idViaje", res.idviaje);
            SqlParameter parametro2 = new SqlParameter("@idCliente", res.idcliente);
            db.Database.ExecuteSqlCommand("exec dbo.Check_In_Viaje @idViaje, @idCliente", parametro1, parametro2);
            return RedirectToAction("MainAdministrador");
        }

        public ActionResult AgregarEquipaje(int idReservacion)
        {
            reservacion res = db.reservacion.Find(idReservacion);
            if ((res.viajevuelo.viaje.horadepartida - DateTime.Now).TotalHours > 24 || res.estado != "check-in")
            {
                return RedirectToAction("MainCliente");
            }
            if (res == null)
            {
                return HttpNotFound();
            }
            TempData["idcliente"] = res.idcliente;
            TempData["idViaje"] = res.idviaje;
            return View(new equipaje());
        }
        [HttpPost]
        public ActionResult AgregarEquipaje(equipaje Luggage)
        {
            
            if (ModelState.IsValid)
            {
                var equipajes = from m in db.equipaje
                                select m;
                decimal primary = (decimal) equipajes.Count() + 1;
                Luggage.idequipaje = primary;
                Luggage.idcliente = (decimal)TempData["idcliente"];
                Luggage.idViaje = (decimal)TempData["idviaje"];
                db.equipaje.Add(Luggage);
                db.SaveChanges();
                return RedirectToAction("MainAdministrador");
            }
            return RedirectToAction("MainAdministrador");
        }
    }
}