using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for NormaSector specific CRUD operations
    /// </summary>
    public class NormaSectorServices: INormaSectorServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor to initialize UnitOfWork instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public NormaSectorServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches normaSector details by id
        /// </summary>
        /// <param name="normaSectorId"></param>
        /// <returns></returns>
        public BusinessEntities.NormaSectorEntity GetNormaSectorById(int normaSectorId)
        {
            var normaSector = _unitOfWork.NormaSectorRepository.GetByID(normaSectorId);
            if (normaSector != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<NORMA_SECTOR, NormaSectorEntity>();
                });
                var normaSectorModel = Mapper.Map<NORMA_SECTOR, NormaSectorEntity>(normaSector);
                return normaSectorModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the normaSectores
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.NormaSectorEntity> GetAllNormaSectores()
        {
            var normaSectores = _unitOfWork.NormaSectorRepository.GetAll().ToList();
            if (normaSectores.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<NORMA_SECTOR, NormaSectorEntity>();
                });
                var normaSectoresModel = Mapper.Map<List<NORMA_SECTOR>, List<NormaSectorEntity>>(normaSectores);
                return normaSectoresModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a normaSector
        /// </summary>
        /// <param name="normaSectorEntity"></param>
        /// <returns></returns>
        public int CreateNormaSector(int idSectorServicio, int idNorma)
        {
            using (var scope = new TransactionScope())
            {
                var normaSector = new NORMA_SECTOR
                {
                    IdEstado = 1,
                    IdNorma = idNorma,
                    //IdNormaSector = normaSectorEntity.IdNormaSector,
                    IdSectorServicio = idSectorServicio
                };
                _unitOfWork.NormaSectorRepository.Insert(normaSector);
                _unitOfWork.Save();
                scope.Complete();
                return normaSector.IdNormaSector;
            }
        }

        /// <summary>
        /// Updates a normaSector
        /// </summary>
        /// <param name="normaSectorId"></param>
        /// <param name="normaSectorEntity"></param>
        /// <returns></returns>
        public bool UpdateNormaSector(int normaSectorId, BusinessEntities.NormaSectorEntity normaSectorEntity)
        {
            var success = false;
            if (normaSectorEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var normaSector = _unitOfWork.NormaSectorRepository.GetByID(normaSectorId);
                    if (normaSector != null)
                    {
                        normaSector.IdEstado = normaSectorEntity.IdEstado;
                        normaSector.IdNorma = normaSectorEntity.IdNorma;
                        normaSector.IdNormaSector = normaSectorEntity.IdNormaSector;
                        normaSector.IdSectorServicio = normaSectorEntity.IdSectorServicio;
                        //normaSector.NORMA = normaSectorEntity.NORMA;
                        //normaSector.SECTOR_SERVICIO = normaSectorEntity.SECTOR_SERVICIO;

                        _unitOfWork.NormaSectorRepository.Update(normaSector);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular normaSector
        /// </summary>
        /// <param name="normaSectorId"></param>
        /// <returns></returns>
        public bool DeleteNormaSector(int normaSectorId)
        {
            var success = false;
            if (normaSectorId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var normaSector = _unitOfWork.NormaSectorRepository.GetByID(normaSectorId);
                    if (normaSector != null)
                    {
                        _unitOfWork.NormaSectorRepository.Delete(normaSector);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;

        }

        /// <summary>
        /// Inactivates a normaSector
        /// </summary>
        /// <param name="normaSectorId"></param>
        /// <returns></returns>
        public bool InactivateNormaSector(int normaSectorId)
        {
            var success = false;
            if (normaSectorId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var normaSector = _unitOfWork.NormaSectorRepository.GetByID(normaSectorId);
                    if (normaSector != null)
                    {
                        normaSector.IdEstado = 0;
                        _unitOfWork.NormaSectorRepository.Update(normaSector);
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
