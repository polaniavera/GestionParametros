using DataModel;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class NormaEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NormaEntity()
        {
            this.NORMA_SECTOR = new HashSet<NORMA_SECTOR>();
        }

        public int IdNorma { get; set; }
        public string CodigoNorma { get; set; }
        public string NombreNorma { get; set; }
        public System.DateTime FechaNorma { get; set; }
        public int IdTipoNorma { get; set; }
        public int IdEntidadEmite { get; set; }
        public string UrlLink { get; set; }
        public string Descripcion { get; set; }
        public int IdNormaPadre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public int IdEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NORMA_SECTOR> NORMA_SECTOR { get; set; }
    }
}
