using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    /// <summary>
    /// NormaSector Service Contract
    /// </summary>
    public interface INormaSectorServices
    {
        object[] GetNormaSectorById(int normaId);
        IEnumerable<NormaSectorEntity> GetAllNormaSectores();
        object[] CreateNormaSector(int idSectorServicio, int idNorma);
        bool UpdateNormaSector(int normaSectorId, NormaSectorEntity normaSectorEntity);
        object[] DeleteNormaSector(int normaSectorId);
        bool InactivateNormaSector(int normaSectorId);
        NormaEntity EditNormaSector(NormaEntity normaEntity, IEnumerable <NormaSectorEntity> normaSectorById);
    }
}
