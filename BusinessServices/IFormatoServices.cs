using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Formato Service Contract
    /// </summary>
    public interface IFormatoServices
    {
        object[] GetFormatoById(int formatoId);
        object[] GetAllFormatos();
        object[] CreateFormato(FormatoEntity formatoEntity);
        object[] UpdateFormato(int formatoId, FormatoEntity formatoEntity);
        object[] DeleteFormato(int formatoId);
        object[] GetAllFormatosActive();
        object[] InactivateFormato(int formatoId);
        object[] ActivateFormato(int formatoId);
        object[] ExistNormaFormato(int normaId);
        object[] setDescripcion(FormatoEntity formato);
        object[] changeFormatoState(FormatoEntity formatoEntity);
        object[] InactivateFormatoRelations(int formatoId);
        object[] ActivateFormatoRelations(int formatoId);
    }
}
