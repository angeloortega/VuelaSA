using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appVuelaSA.Models
{
    public class registro
    {
        public registro() { }

        public decimal id { get; set; }

        public string nombre { get; set; }

        public string correo { get; set; }

        public string usuario { get; set; }

        public string paisResidencia { get; set; }

        public string pasaporte { get; set; }

        public string contrasenna { get; set; }
    }
}