using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class TablaCampoEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TablaCampoEntity()
        {
            this.TABLA_VALOR = new HashSet<TABLA_VALOR>();
        }

        public int IdCampo { get; set; }
        public int IdTabla { get; set; }
        public string Codigo { get; set; }

        public virtual TABLA TABLA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TABLA_VALOR> TABLA_VALOR { get; set; }
    }
}
