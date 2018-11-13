using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appVuelaSA.Models
{
    public class Historial
    {

        public Historial() { }

        public decimal id { get; set; }

        public DateTime horapartida { get; set; }

        public DateTime horallegada { get; set; }

        public string origen { get; set; }

        public string destino { get; set; }
    }
}