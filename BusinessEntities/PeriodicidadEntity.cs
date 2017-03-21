using DataModel;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class PeriodicidadEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PeriodicidadEntity()
        {
            this.FORMATO = new HashSet<FORMATO>();
            this.PERIODO = new HashSet<PERIODO>();
        }

        public int IdPeriodicidad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<FORMATO> FORMATO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PERIODO> PERIODO { get; set; }
    }
}
