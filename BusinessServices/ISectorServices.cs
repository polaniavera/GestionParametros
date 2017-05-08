using BusinessEntities;
using System.Collections.Generic;

namespace BusinessServices
{
    /// <summary>
    /// sectorServicio Service Contract
    /// </summary>
    public interface ISectorServices
    {
        SectorEntity GetSectorServicioById(int sectorServicioId);
        IEnumerable<SectorEntity> GetAllSectorServicios();
        int CreateSectorServicio(SectorEntity sectorServicioEntity);
        bool UpdateSectorServicio(int sectorServicioId, SectorEntity sectorServicioEntity);
        bool DeleteSectorServicio(int sectorServicioId);
        bool InactivateSectorServicio(int sectorServicioId);
    }
}
