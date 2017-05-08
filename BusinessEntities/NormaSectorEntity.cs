using DataModel;

namespace BusinessEntities
{
    public class NormaSectorEntity
    {
        public int IdNormaSector { get; set; }
        public int IdNorma { get; set; }
        public int IdSector { get; set; }
        public int IdEstado { get; set; }

        public virtual NORMA NORMA { get; set; }
        public virtual SECTOR SECTOR { get; set; }
    }
}
