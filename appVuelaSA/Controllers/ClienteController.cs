using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using appVuelaSA.Models;

namespace appVuelaSA.Controllers
{
    public class ClienteController : Controller
    {
        private proyecto2RequeEntities bd = new proyecto2RequeEntities();
        public ActionResult MainCliente(string destino, string origen)
        {

            var vista = from m in bd.Vista_Viaje
                         select m;
            List<ViajeCustom> vuelos = new List<ViajeCustom>();
            ViajeCustom newVuelo;
            var aereopuertos = from m in bd.aeropuerto
                               select m;
            aeropuerto AeropuertoOrigen;
            aeropuerto AeropuertoDestino;
            foreach (Vista_Viaje trip in vista)
            {
                newVuelo = new ViajeCustom();
                newVuelo.viajeDetails = trip;
                AeropuertoOrigen = aereopuertos.Where(s => s.idaeropuerto == trip.idAeropuertoOrigen).First();
                AeropuertoDestino = aereopuertos.Where(s => s.idaeropuerto == trip.idAeropuertoDestino).First();
                newVuelo.origen = AeropuertoOrigen;
                newVuelo.destino = AeropuertoDestino;
                vuelos.Add(newVuelo);
            }
            List<ViajeCustom> nuevosViajes;
            if (!String.IsNullOrEmpty(origen))
            {
                nuevosViajes = new List<ViajeCustom>();
                foreach (ViajeCustom v in vuelos) {
                    if (v.origen.pais.Contains(origen)) {
                        nuevosViajes.Add(v);
                    }
                }
                vuelos = nuevosViajes;
            }
            if (!String.IsNullOrEmpty(origen))
            {
                nuevosViajes = new List<ViajeCustom>();
                foreach (ViajeCustom v in vuelos)
                {
                    if (v.destino.pais.Contains(origen))
                    {
                        nuevosViajes.Add(v);
                    }
                }
                vuelos = nuevosViajes;
            }
            var viajes = vuelos.OrderByDescending(s => s.viajeDetails.horaDePartida);
            //prueba
            return View(viajes);
        }

        public ActionResult Itinerario(decimal id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter parametro1 = new SqlParameter("@idViaje", id);
            var listaVuelos = bd.Database.SqlQuery<Vuelos_Por_Viaje_Result>("exec dbo.Vuelos_Por_Viaje @idViaje", parametro1);
            if (listaVuelos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idviaje = id.ToString();
            return View(listaVuelos.ToList());
        }

        public ActionResult Boletos() {
            List<vuelo> vuelos = (List<vuelo>) TempData["vuelos"];
            return View(vuelos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Itinerario(string idViaje)
        {
            int identificador = Int32.Parse(idViaje);
            var vuelos = from m in bd.vuelo
                         select m;
            var newVuelos = new List<vuelo>();
            foreach (vuelo v in vuelos) {
                if (v.viajevuelo.Count > 0)
                {
                    if (v.viajevuelo.First().idviaje == identificador)
                    {
                        newVuelos.Add(v);
                    }
                }
            }
            TempData["vuelos"] = newVuelos;
            return RedirectToAction("Boletos");
        }
    }
}