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
        object[] GetAllNormas();
        IEnumerable<BusinessEntities.NormaEntity> GetAllNormasActive();
        int CreateNorma(NormaEntity normaEntity);
        bool UpdateNorma(int normaId, NormaEntity normaEntity);
        bool DeleteNorma(int normaId);
        bool InactivateNormaRelations(int normaId);
        bool ActivateNormaRelations(int normaId);
        bool InactivateNorma(int normaId);
        bool ActivateNorma(int normaId);
        IEnumerable<NormaPadreEntity> GetNormasPadre();
        //bool ExistSectorFormato(int normaId);
        bool ValidateSector(NormaEntity normaEntity, IEnumerable<NormaSectorEntity> normaSectorById);
        NormaEntity changeNormaState(NormaEntity normaEntity);
    }
}
