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
        public object[] GetNormaSectorById(int normaSectorId)
        {
            try
            {
                var normaSector = _unitOfWork.NormaSectorRepositoryCustom.GetManyByIdNorma(normaSectorId).ToList();
                if (normaSector != null)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<NORMA_SECTOR, NormaSectorEntity>();
                    });
                    var normaSectorModel = Mapper.Map<List<NORMA_SECTOR>, List<NormaSectorEntity>>(normaSector);
                    object[] resultado = { "0000", normaSectorModel };
                    return resultado;
                }
                return null;
            }
            catch (Exception e)
            {
                var cod = new CodigoError();
                var codigoError = cod.Error(e.ToString());
                object[] resultado = { codigoError, e.ToString() };
                return resultado;
            }
            
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
        public object[] CreateNormaSector(int idSectorServicio, int idNorma)
        {
            var success = false;

            try
            {
                using (var scope = new TransactionScope())
                {
                    var normaSector = new NORMA_SECTOR
                    {
                        IdEstado = 1,
                        IdNorma = idNorma,
                        IdSector = idSectorServicio
                    };
                    _unitOfWork.NormaSectorRepository.Insert(normaSector);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                    //return normaSector.IdNormaSector;
                }
                object[] resultado = { "0000", success };
                return resultado;
            }
            catch (Exception e)
            {
                var cod = new CodigoError();
                var codigoError = cod.Error(e.ToString());
                object[] resultado = { codigoError, e.ToString() };
                return resultado;
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
                        normaSector.IdSector = normaSectorEntity.IdSector;
                        normaSector.IdEstado = normaSectorEntity.IdEstado;
                        normaSector.IdNorma = normaSectorEntity.IdNorma;
                        normaSector.IdNormaSector = normaSectorEntity.IdNormaSector;
                        normaSector.NORMA = normaSectorEntity.NORMA;
                        normaSector.SECTOR = normaSectorEntity.SECTOR;

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
        public object[] DeleteNormaSector(int normaSectorId)
        {
            var success = false;
            try
            {
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
                object[] resultado = { "0000", success };
                return resultado;
            }
            catch (Exception e)
            {
                var cod = new CodigoError();
                var codigoError = cod.Error(e.ToString());
                object[] resultado = { codigoError, e.ToString() };
                return resultado;
            }
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



        /// <summary>
        /// Inactivates a normaSector
        /// </summary>
        /// <param name="normaSectorId"></param>
        /// <returns></returns>
        public bool ActivateNormaSector(int normaSectorId)
        {
            var success = false;
            if (normaSectorId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var normaSector = _unitOfWork.NormaSectorRepository.GetByID(normaSectorId);
                    if (normaSector != null)
                    {
                        normaSector.IdEstado = 1;
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
        /// Fetches all the normaSector asociated with IdNorma
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.NormaSectorEntity> GetNormaSectorByIdNorma(int idNorma)
        {
            var normaSectores = _unitOfWork.NormaSectorRepositoryCustom.GetManyByIdNorma(idNorma).ToList();
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
        /// Edit all the normaSector asociated with IdNorma
        /// </summary>
        /// <returns></returns>
       
        public NormaEntity EditNormaSector(NormaEntity normaEntity, IEnumerable<NormaSectorEntity> normaSectorById)
        {
            int match = 0;
            //var normaSectorById = GetNormaSectorById(normaEntity.IdNorma);

            if (normaSectorById != null)
            {
                foreach (DataModel.NORMA_SECTOR sectorNuevo in normaEntity.NORMA_SECTOR)
                {
                    foreach (NormaSectorEntity sectorExistente in normaSectorById)
                    {
                        if (sectorNuevo.IdSector.Equals(sectorExistente.IdSector))
                            ActivateNormaSector(sectorExistente.IdNormaSector);
                        match++;
                    }

                    if (match == 0)
                        //ATLA***********************cuando existe en 1 o 0 no se crea, se actualiza
                        CreateNormaSector(sectorNuevo.IdSector, normaEntity.IdNorma);
                    else
                        match = 0;
                }
                
                foreach (NormaSectorEntity sectorExistente in normaSectorById)
                {
                    foreach (DataModel.NORMA_SECTOR sectorNuevo in normaEntity.NORMA_SECTOR)
                    {
                        if (sectorNuevo.IdSector.Equals(sectorExistente.IdSector))
                            match++;
                    }
                    if (match == 0)
                        InactivateNormaSector(sectorExistente.IdNormaSector);
                    else
                        match = 0;
                }
            }
                       
            normaEntity.NORMA_SECTOR = null;

            return normaEntity;
        }

    }
}
