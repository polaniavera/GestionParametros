using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ServicioEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServicioEntity()
        {
            this.FORMATO_SERVICIO = new HashSet<FORMATO_SERVICIO>();
            this.ENTIDAD_SERVICIO = new HashSet<ENTIDAD_SERVICIO>();
            this.SERVICIO1 = new HashSet<SERVICIO>();
        }

        public int IdServicio { get; set; }
        public int IdSector { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
        public Nullable<int> IdServicioPadre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORMATO_SERVICIO> FORMATO_SERVICIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENTIDAD_SERVICIO> ENTIDAD_SERVICIO { get; set; }
        public virtual SECTOR SECTOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICIO> SERVICIO1 { get; set; }
        public virtual SERVICIO SERVICIO2 { get; set; }
    }
}
