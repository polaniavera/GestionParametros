using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IFormatoServicioServices
    {
        bool ExistSectorFormato(int sectorId);
        bool InactivateServicio(int formatoServicioId);
        bool ActivateServicio(int formatoServicioId);
    }
}
