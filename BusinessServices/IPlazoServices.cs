using BusinessEntities;
using System.Collections.Generic;

namespace BusinessServices
{
    public interface IPlazoServices
    {
        IEnumerable<PlazoEntity> GetAllPlazos();
    }
}
