//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace appVuelaSA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class asiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public asiento()
        {
            this.reservacion = new HashSet<reservacion>();
        }
    
        public decimal idasiento { get; set; }
        public string estado { get; set; }
        public string categoria { get; set; }
        public decimal precio { get; set; }
        public decimal idavion { get; set; }
    
        public virtual avion avion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservacion> reservacion { get; set; }
    }
}
