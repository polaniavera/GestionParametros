using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class PlantillaCampoServices : IPlantillaCampoServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PlantillaCampoServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inactivates a campo
        /// </summary>
        /// <param name="campoId"></param>
        /// <returns></returns>
        public bool InactivateCampo(int campoId)
        {
            var success = false;
            if (campoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var campo = _unitOfWork.PlantillaCampoRepository.GetByID(campoId);
                    if (campo != null)
                    {
                        campo.IdEstado = 0;
                        _unitOfWork.PlantillaCampoRepository.Update(campo);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Activates a campo
        /// </summary>
        /// <param name="campoId"></param>
        /// <returns></returns>
        public bool ActivateCampo(int campoId)
        {
            var success = false;
            if (campoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var campo = _unitOfWork.PlantillaCampoRepository.GetByID(campoId);
                    if (campo != null)
                    {
                        campo.IdEstado = 1;
                        _unitOfWork.PlantillaCampoRepository.Update(campo);
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
