using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class FormatoServicioServices : IFormatoServicioServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor to initialize UnitOfWork instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public FormatoServicioServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ExistSectorFormato(int sectorId)
        {
            bool exist = false;
            exist = _unitOfWork.FormatoServicioRepositoryCustom.ExistServicio(sectorId);

            if (exist)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Inactivates a Servicio
        /// </summary>
        /// <param name="formatoServicioId"></param>
        /// <returns></returns>
        public bool InactivateServicio(int formatoServicioId)
        {
            var success = false;
            if (formatoServicioId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var servicio = _unitOfWork.FormatoServicioRepository.GetByID(formatoServicioId);
                    if (servicio != null)
                    {
                        servicio.IdEstado = 0;
                        _unitOfWork.FormatoServicioRepository.Update(servicio);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Activates a Servicio
        /// </summary>
        /// <param name="formatoServicioId"></param>
        /// <returns></returns>
        public bool ActivateServicio(int formatoServicioId)
        {
            var success = false;
            if (formatoServicioId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var servicio = _unitOfWork.FormatoServicioRepository.GetByID(formatoServicioId);
                    if (servicio != null)
                    {
                        servicio.IdEstado = 1;
                        _unitOfWork.FormatoServicioRepository.Update(servicio);
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
