using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appVuelaSA.Models;

namespace appVuelaSA.Controllers
{
    public class ClienteController : Controller
    {
        private proyecto2RequeEntities db = new proyecto2RequeEntities();

        public ActionResult MainCliente()
        {
            //prueba
            return View();
        }

        public ActionResult Perfil()
        {
            decimal idCliente = Convert.ToDecimal(Session["IDUsuario"].ToString());

            Perfil perfil = new Perfil();
            perfil.idCliente = idCliente;

            var historial = db.Historial_Vuelos(idCliente);
            var proximos = db.Proximos_Vuelos(idCliente);


            var historiales = new ObservableCollection<Historial>();
            foreach (var linea in historial)
            {
                Historial temp = new Historial();
                temp.destino = linea.destino;
                temp.origen = linea.origen;
                temp.horapartida = linea.horadepartida;
                temp.horallegada = linea.horadellegada;
                temp.id = 1;//temp.id = linea.idreservacion;
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
                temp.id = 1;//temp.id = linea.idreservacion;
                promioss.Add(temp);

            }

            perfil.historial = historiales;
            perfil.proximos = promioss;

            return View(perfil);
        }

        public ActionResult CambiarVuelo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarVuelo(FormCollection collection)
        {
            reservacion res = new reservacion();
            res.idcliente = Convert.ToDecimal(collection["idcliente"].ToString());
            res.idviaje = Convert.ToDecimal(collection["idviaje"].ToString());
            res.idvuelo = Convert.ToDecimal(collection["idvuelo"].ToString());

            db.reservacion.Add(res);

            return View("EscogerAsientos");
        }


    }
}