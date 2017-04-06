using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IFormatoPlantillaServices
    {
        bool InactivatePlantilla(int formatoPlantillaId);
        bool ActivatePlantilla(int formatoPlantillaId);
        bool ExistPlantilla(int formatoId);
    }
}
