using DataModel;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class SectorServicioEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SectorServicioEntity()
        {
            this.NORMA_SECTOR = new HashSet<NORMA_SECTOR>();
            this.SERVICIO = new HashSet<SERVICIO>();
        }

        public int IdSectorServicio { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NORMA_SECTOR> NORMA_SECTOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICIO> SERVICIO { get; set; }
    }
}
