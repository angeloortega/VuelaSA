using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appVuelaSA.Models
{
    public class Perfil
    {
        public Perfil() { }

        public decimal idCliente { get; set; }

        public IEnumerable<Historial> historial { get; set; }

        public IEnumerable<Historial> proximos { get; set; }
    }
}