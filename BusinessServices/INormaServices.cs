using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Norma Service Contract
    /// </summary>
    public interface INormaServices
    {
        object[] GetNormaById(int normaId);
        object[] GetAllNormas();
        object[] GetAllNormasActive();
        object[] CreateNorma(NormaEntity normaEntity);
        object[] UpdateNorma(int normaId, NormaEntity normaEntity);
        object[] DeleteNorma(int normaId);
        object[] InactivateNormaRelations(int normaId);
        object[] ActivateNormaRelations(int normaId);
        object[] InactivateNorma(int normaId);
        object[] ActivateNorma(int normaId);
        object[] GetNormasPadre();
        //bool ExistSectorFormato(int normaId);
        object[] ValidateSector(NormaEntity normaEntity, IEnumerable<NormaSectorEntity> normaSectorById);
        object[] changeNormaState(NormaEntity normaEntity);
    }
}
