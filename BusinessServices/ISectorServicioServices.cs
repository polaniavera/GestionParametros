using BusinessEntities;
using System.Collections.Generic;

namespace BusinessServices
{
    /// <summary>
    /// sectorServicio Service Contract
    /// </summary>
    public interface ISectorServicioServices
    {
        SectorServicioEntity GetSectorServicioById(int sectorServicioId);
        IEnumerable<SectorServicioEntity> GetAllSectorServicios();
        int CreateSectorServicio(SectorServicioEntity sectorServicioEntity);
        bool UpdateSectorServicio(int sectorServicioId, SectorServicioEntity sectorServicioEntity);
        bool DeleteSectorServicio(int sectorServicioId);
        bool InactivateSectorServicio(int sectorServicioId);
    }
}
