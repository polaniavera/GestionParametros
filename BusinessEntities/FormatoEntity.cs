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
        public int IdNorma { get; set; }
        public string nombreNorma { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdTipoFormato { get; set; }
        public string nombreTipoFormato { get; set; }
        public int IdPlazo { get; set; }
        public string nombrePlazo { get; set; }
        public int IdPeriodicidad { get; set; }
        public string nombrePeriodicidad { get; set; }
        public int IdEstado { get; set; }
        public string nombreEstado { get; set; }
        public Nullable<int> DiasPlazo { get; set; }
        public int IdSeccion { get; set; }
        public string nombreSeccion { get; set; }
        public Nullable<bool> InlcuyeFecha { get; set; }

        public virtual PERIODICIDAD PERIODICIDAD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORMATO_PLANTILLA> FORMATO_PLANTILLA { get; set; }
        public virtual PLAZO PLAZO { get; set; }
    }
}
