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
    
    public partial class viajevuelo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public viajevuelo()
        {
            this.reservacion = new HashSet<reservacion>();
        }
    
        public decimal idviaje { get; set; }
        public decimal idvuelo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservacion> reservacion { get; set; }
        public virtual viaje viaje { get; set; }
        public virtual vuelo vuelo { get; set; }
    }
}