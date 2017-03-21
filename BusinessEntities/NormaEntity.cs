using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public partial class NormaEntity
    {
        public NormaEntity()
        {
            this.NORMA_SECTOR = new HashSet<NORMA_SECTOR>();
        }

        public int IdNorma { get; set; }
        public string CodigoNorma { get; set; }
        public string NombreNorma { get; set; }
        public System.DateTime FechaNorma { get; set; }
        public int IdTipoNorma { get; set; }
        public string DescripcionTipoNorma { get; set; }
        public int IdEntidadEmite { get; set; }
        public string DescripcionEntidadEmite { get; set; }
        public string NombreArchivo { get; set; }
        public string Descripcion { get; set; }
        public int IdNormaPadre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public int IdEstado { get; set; }
        public string DescripcionEstado { get; set; }
        public Nullable<int> IdUrlLink { get; set; }
        public Nullable<int> IdSeccion { get; set; }

        public virtual ICollection<NORMA_SECTOR> NORMA_SECTOR { get; set; }
    }
}
