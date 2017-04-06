using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class FormatoPlantillaServices : IFormatoPlantillaServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public FormatoPlantillaServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inactivates a plantilla
        /// </summary>
        /// <param name="formatoPlantillaId"></param>
        /// <returns></returns>
        public bool InactivatePlantilla(int formatoPlantillaId)
        {
            var success = false;
            if (formatoPlantillaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var plantilla = _unitOfWork.FormatoPlantillaRepository.GetByID(formatoPlantillaId);
                    if (plantilla != null)
                    {
                        plantilla.IdEstado = 0;
                        _unitOfWork.FormatoPlantillaRepository.Update(plantilla);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Activates a plantilla
        /// </summary>
        /// <param name="formatoPlantillaId"></param>
        /// <returns></returns>
        public bool ActivatePlantilla(int formatoPlantillaId)
        {
            var success = false;
            if (formatoPlantillaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var plantilla = _unitOfWork.FormatoPlantillaRepository.GetByID(formatoPlantillaId);
                    if (plantilla != null)
                    {
                        plantilla.IdEstado = 1;
                        _unitOfWork.FormatoPlantillaRepository.Update(plantilla);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
