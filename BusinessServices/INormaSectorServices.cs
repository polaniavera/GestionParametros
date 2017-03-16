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
        IEnumerable<NormaSectorEntity> GetNormaSectorById(int normaId);
        IEnumerable<NormaSectorEntity> GetAllNormaSectores();
        bool CreateNormaSector(int idSectorServicio, int idNorma);
        bool UpdateNormaSector(int normaSectorId, NormaSectorEntity normaSectorEntity);
        bool DeleteNormaSector(int normaSectorId);
        bool InactivateNormaSector(int normaSectorId);
        NormaEntity EditNormaSector(NormaEntity normaEntity, IEnumerable <NormaSectorEntity> normaSectorById);
    }
}
