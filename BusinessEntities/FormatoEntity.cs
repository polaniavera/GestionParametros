using DataModel;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class FormatoEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormatoEntity()
        {
            this.FORMATO_PLANTILLA = new HashSet<FORMATO_PLANTILLA>();
        }

        public int IdFormato { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdTipoFormato { get; set; }
        public int IdPeriodicidad { get; set; }
        public int IdContenido { get; set; }
        public int IdPlazo { get; set; }
        public int IdEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORMATO_PLANTILLA> FORMATO_PLANTILLA { get; set; }
    }
}
