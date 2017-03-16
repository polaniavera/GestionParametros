using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Tabla Service Contract
    /// </summary>
    public interface ITablaServices
    {
        TablaEntity GetTablaById(int tablaId);
        IEnumerable<TablaEntity> GetAllTablas();
        IEnumerable<TablaValorListEntity> GetTipoNorma();
        IEnumerable<TablaValorListEntity> GetTipoFormato();
        IEnumerable<TablaValorListEntity> GetParametrosVert(string parametro);
        int CreateTabla(TablaEntity tablaEntity);
        bool UpdateTabla(int tablaId, TablaEntity tablaEntity);
        bool DeleteTabla(int tablaId);
    }
}
