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
    
    public partial class reservacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public reservacion()
        {
            this.pasajeabordar = new HashSet<pasajeabordar>();
            this.asiento = new HashSet<asiento>();
        }
    
        public decimal idreservacion { get; set; }
        public System.DateTime fchreservacion { get; set; }
        public string estado { get; set; }
        public decimal idcliente { get; set; }
        public decimal idvuelo { get; set; }
        public decimal idviaje { get; set; }
    
        public virtual cliente cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pasajeabordar> pasajeabordar { get; set; }
        public virtual viajevuelo viajevuelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<asiento> asiento { get; set; }
    }
}
