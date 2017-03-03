using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// TablaValor Service Contract
    /// </summary>
    public interface ITablaValorServices
    {
        TablaValorEntity GetTablaValorById(int tablaValorId);
        IEnumerable<TablaValorEntity> GetAllTablaValores();
        int CreateTablaValor(TablaValorEntity tablaValorEntity);
        bool UpdateTablaValor(int tablaValorId, TablaValorEntity tablaValorEntity);
        bool DeleteTablaValor(int tablaValorId);
    }
}
