//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class NORMA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NORMA()
        {
            this.NORMA_SECTOR = new HashSet<NORMA_SECTOR>();
        }
    
        public int IdNorma { get; set; }
        public string CodigoNorma { get; set; }
        public string NombreNorma { get; set; }
        public System.DateTime FechaNorma { get; set; }
        public int IdTipoNorma { get; set; }
        public int IdEntidadEmite { get; set; }
        public string NombreArchivo { get; set; }
        public string Descripcion { get; set; }
        public int IdNormaPadre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public int IdEstado { get; set; }
        public Nullable<int> IdUrlLink { get; set; }
        public Nullable<int> IdSeccion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NORMA_SECTOR> NORMA_SECTOR { get; set; }
    }
}
