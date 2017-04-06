using DataModel;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class EntidadEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntidadEntity()
        {
            //this.ENTIDAD_CANAL = new HashSet<ENTIDAD_CANAL>();
            this.ENTIDAD_SERVICIO = new HashSet<ENTIDAD_SERVICIO>();
        }

        public int IdEntidad { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdTipoEntidad { get; set; }
        public Nullable<int> NIT { get; set; }
        public string Sigla { get; set; }
        public string Email { get; set; }
        public Nullable<int> NaturalezaJuridica { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> Ciudad { get; set; }
        public Nullable<int> Departamento { get; set; }
        public Nullable<System.DateTime> FechaConstitucion { get; set; }
        public Nullable<int> Telefono { get; set; }
        public Nullable<int> IdEntidadPadre { get; set; }
        public Nullable<int> IdTipoRelacionEntidadPadre { get; set; }
        public Nullable<int> IdCodigoHomologado { get; set; }
        public string CodigoHomologado { get; set; }
        public int IdEstado { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ENTIDAD_CANAL> ENTIDAD_CANAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENTIDAD_SERVICIO> ENTIDAD_SERVICIO { get; set; }
    }
}
