using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appVuelaSA.Models
{
    public class ViajeCustom
    {
        public virtual aeropuerto origen { get; set; }
        public virtual aeropuerto destino { get; set; }
        public Vista_Viaje viajeDetails { get; set; }
    }
}