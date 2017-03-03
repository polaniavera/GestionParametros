using DataModel;

namespace BusinessEntities
{
    public class NormaSectorEntity
    {
        public int IdNormaSector { get; set; }
        public int IdNorma { get; set; }
        public int IdSectorServicio { get; set; }
        public int IdEstado { get; set; }

        public NORMA NORMA { get; set; }
        public virtual SECTOR_SERVICIO SECTOR_SERVICIO { get; set; }
    }
}
