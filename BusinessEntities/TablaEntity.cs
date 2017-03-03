using DataModel;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class TablaEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TablaEntity()
        {
            this.TABLA_CAMPO = new HashSet<TABLA_CAMPO>();
            this.TABLA_REGISTRO = new HashSet<TABLA_REGISTRO>();
        }

        public int IdTabla { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdTipoTabla { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLA_CAMPO> TABLA_CAMPO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLA_REGISTRO> TABLA_REGISTRO { get; set; }
    }
}
