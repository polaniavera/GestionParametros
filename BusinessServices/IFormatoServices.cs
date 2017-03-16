using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Formato Service Contract
    /// </summary>
    public interface IFormatoServices
    {
        FormatoEntity GetFormatoById(int formatoId);
        IEnumerable<FormatoEntity> GetAllFormatos();
        int CreateFormato(FormatoEntity formatoEntity);
        bool UpdateFormato(int formatoId, FormatoEntity formatoEntity);
        bool DeleteFormato(int formatoId);
        IEnumerable<FormatoEntity> GetAllFormatosActive();
        bool InactivateFormato(int formatoId);
    }
}
