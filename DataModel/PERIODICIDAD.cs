//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class PERIODICIDAD
    {
        public PERIODICIDAD()
        {
            this.FORMATO = new HashSet<FORMATO>();
            this.PERIODO = new HashSet<PERIODO>();
        }
    
        public int IdPeriodicidad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<FORMATO> FORMATO { get; set; }
        public virtual ICollection<PERIODO> PERIODO { get; set; }
    }
}
