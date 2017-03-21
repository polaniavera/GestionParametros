using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class PlazoEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlazoEntity()
        {
            this.FORMATO = new HashSet<FORMATO>();
        }

        public int IdPlazo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool CalcularFecha { get; set; }
        public bool PeriodoVencido { get; set; }
        public bool UltimoDia { get; set; }
        public int TipoDia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<FORMATO> FORMATO { get; set; }
    }
}
