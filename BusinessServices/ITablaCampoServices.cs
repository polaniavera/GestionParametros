using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// TablaCampo Service Contract
    /// </summary>
    public interface ITablaCampoServices
    {
        TablaCampoEntity GetTablaCampoById(int tablaCampoId);
        IEnumerable<TablaCampoEntity> GetAllTablaCampos();
        int CreateTablaCampo(TablaCampoEntity tablaCampoEntity);
        bool UpdateTablaCampo(int tablaCampoId, TablaCampoEntity tablaCampoEntity);
        bool DeleteTablaCampo(int tablaCampoId);
    }
}
