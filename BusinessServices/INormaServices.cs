using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Norma Service Contract
    /// </summary>
    public interface INormaServices
    {
        NormaEntity GetNormaById(int normaId);
        IEnumerable<NormaEntity> GetAllNormas();
        IEnumerable<BusinessEntities.NormaEntity> GetAllNormasActive();
        int CreateNorma(NormaEntity normaEntity);
        bool UpdateNorma(int normaId, NormaEntity normaEntity);
        bool DeleteNorma(int normaId);
        bool InactivateNorma(int normaId);

        IEnumerable<NormaPadreEntity> GetNormasPadre();
    }
}
