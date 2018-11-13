using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private proyectoRequeEntities bd = new proyectoRequeEntities();
        public ActionResult MainCliente(string origen , string destino)
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
                    if (v.origen.ciudad.Contains(origen)) {
                        nuevosViajes.Add(v);
                    }
                }
                vuelos = nuevosViajes;
            }
            if (!String.IsNullOrEmpty(destino))
            {
                nuevosViajes = new List<ViajeCustom>();
                foreach (ViajeCustom v in vuelos)
                {
                    if (v.destino.ciudad.Contains(destino))
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
            TempData["idVuelo"] = identificador;
            return RedirectToAction("Boletos");
        }

        public ActionResult Perfil()
        {
            decimal idCliente = Convert.ToDecimal(Session["IDUsuario"].ToString());

            Perfil perfil = new Perfil();
            perfil.idCliente = idCliente;

            var historial = bd.Historial_Vuelos(idCliente);
            var proximos = bd.Proximos_Vuelos(idCliente);


            var historiales = new ObservableCollection<Historial>();
            foreach (var linea in historial)
            {
                Historial temp = new Historial();
                temp.destino = linea.destino;
                temp.origen = linea.origen;
                temp.horapartida = linea.horadepartida;
                temp.horallegada = linea.horadellegada;
                temp.id = linea.idreservacion;
                historiales.Add(temp);

            }

            var promioss = new ObservableCollection<Historial>();
            foreach (var linea in proximos)
            {
                Historial temp = new Historial();
                temp.destino = linea.destino;
                temp.origen = linea.origen;
                temp.horapartida = linea.horadepartida;
                temp.horallegada = linea.horadellegada;
                temp.id = linea.idreservacion;
                promioss.Add(temp);

            }

            perfil.historial = historiales;
            perfil.proximos = promioss;

            return View(perfil);
        }

        public ActionResult CancelarVuelo(int id)
        {
            reservacion res = bd.reservacion.Find(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        [HttpPost, ActionName("CancelarVuelo")]

        public ActionResult CancelarVueloConfirmed(int id)
        {
            reservacion res = bd.reservacion.Find(id);
            if ((res.viajevuelo.viaje.horadepartida - DateTime.Now).TotalHours < 24) {
                return RedirectToAction("MainCliente");
            }
            bd.reservacion.Remove(res);
            bd.SaveChanges();
            return RedirectToAction("MainCliente");
        }

        public ActionResult CambiarVuelo(int id)
        {
            reservacion res = bd.reservacion.Find(id);
            if ((res.viajevuelo.viaje.horadepartida - DateTime.Now).TotalHours < 24)
            {
                return RedirectToAction("MainCliente");
            }
            ViewBag.idvuelo = new SelectList(bd.reservacion, "idvuelo", "idvuelo");
            ViewBag.idviaje = new SelectList(bd.reservacion, "idviaje", "idviaje");
            return View();
        }

        [HttpPost]
        public ActionResult CambiarVuelo(FormCollection collection)
        {
            reservacion res = new reservacion();
            res.idcliente = Convert.ToDecimal(collection["idcliente"].ToString());
            res.idviaje = Convert.ToDecimal(collection["idviaje"].ToString());
            res.idvuelo = Convert.ToDecimal(collection["idvuelo"].ToString());

            bd.reservacion.Add(res);

            return View("EscogerAsientos");
        }
    }
}