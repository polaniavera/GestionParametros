using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Entidad Service Contract
    /// </summary>
    public interface IEntidadServices
    {
        EntidadEntity GetEntidadById(int entidadId);
        IEnumerable<EntidadEntity> GetAllEntidades();
        int CreateEntidad(EntidadEntity entidadEntity);
        bool UpdateEntidad(int entidadId, EntidadEntity entidadEntity);
        bool DeleteEntidad(int entidadId);
    }
}
