//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace appVuelaSA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class viaje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public viaje()
        {
            this.equipaje = new HashSet<equipaje>();
            this.viajevuelo = new HashSet<viajevuelo>();
        }
    
        public decimal idviaje { get; set; }
        public System.DateTime horadepartida { get; set; }
        public System.DateTime horadellegada { get; set; }
        public decimal precio { get; set; }
        public string descripcion { get; set; }
        public decimal idaeropuertoorigen { get; set; }
        public decimal idaeropuertodestino { get; set; }
    
        public virtual aeropuerto aeropuerto { get; set; }
        public virtual aeropuerto aeropuerto1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipaje> equipaje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<viajevuelo> viajevuelo { get; set; }
    }
}
