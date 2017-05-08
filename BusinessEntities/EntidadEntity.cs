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
            this.ACUERDO = new HashSet<ACUERDO>();
            this.ACUERDO1 = new HashSet<ACUERDO>();
            this.ENTIDAD_SECTOR = new HashSet<ENTIDAD_SECTOR>();
            this.ENTIDAD_SERVICIO = new HashSet<ENTIDAD_SERVICIO>();
        }

        public int IdEntidad { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdTipoEntidad { get; set; }
        public string NumeroDocumento { get; set; }
        public Nullable<int> IdTipoDocumento { get; set; }
        public string Sigla { get; set; }
        public string Email { get; set; }
        public Nullable<int> NaturalezaJuridica { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> Ciudad { get; set; }
        public Nullable<System.DateTime> FechaConstitucion { get; set; }
        public string Telefono { get; set; }
        public Nullable<int> IdEntidadPadre { get; set; }
        public Nullable<int> IdTipoRelacionEntidadPadre { get; set; }
        public Nullable<int> IdTipoCodigoHomologado { get; set; }
        public string CodigoHomologado { get; set; }
        public int IdEstado { get; set; }
        public Nullable<bool> IdTransmite { get; set; }
        public string NombreContacto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACUERDO> ACUERDO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACUERDO> ACUERDO1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENTIDAD_SECTOR> ENTIDAD_SECTOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENTIDAD_SERVICIO> ENTIDAD_SERVICIO { get; set; }
    }
}
