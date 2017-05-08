using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for sectorServicio specific CRUD operations
    /// </summary>
    public class SectorServices: ISectorServices
    {
        private readonly UnitOfWork _unitOfWork;
        
        /// <summary>
        /// Public constructor to initialize UnitOfWork instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public SectorServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches sectorServicio details by id
        /// </summary>
        /// <param name="sectorServicioId"></param>
        /// <returns></returns>
        public BusinessEntities.SectorEntity GetSectorServicioById(int sectorServicioId)
        {
            var sectorServicio = _unitOfWork.SectorServicioRepository.GetByID(sectorServicioId);
            if (sectorServicio != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SECTOR, SectorEntity>();
                });
                var sectorServicioModel = Mapper.Map<SECTOR, SectorEntity>(sectorServicio);
                return sectorServicioModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the sectorServicios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.SectorEntity> GetAllSectorServicios()
        {
            var sectorServicios = _unitOfWork.SectorServicioRepository.GetAll().ToList();
            if (sectorServicios.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SECTOR, SectorEntity>();
                });
                var sectorServiciosModel = Mapper.Map<List<SECTOR>, List<SectorEntity>>(sectorServicios);
                return sectorServiciosModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a sectorServicio
        /// </summary>
        /// <param name="sectorServicioEntity"></param>
        /// <returns></returns>
        public int CreateSectorServicio(SectorEntity sectorServicioEntity)
        {
            using (var scope = new TransactionScope())
            {
                var sectorServicio = new SECTOR
                {
                    Codigo = sectorServicioEntity.Codigo,
                    Descripcion = sectorServicioEntity.Descripcion,
                    IdEstado = sectorServicioEntity.IdEstado,
                    IdSector = sectorServicioEntity.IdSector,
                    Nombre = sectorServicioEntity.Nombre,
                    NORMA_SECTOR = sectorServicioEntity.NORMA_SECTOR,
                    SERVICIO = sectorServicioEntity.SERVICIO
                };
                _unitOfWork.SectorServicioRepository.Insert(sectorServicio);
                _unitOfWork.Save();
                scope.Complete();
                return sectorServicio.IdSector;
            }
        }

        /// <summary>
        /// Updates a sectorServicio
        /// </summary>
        /// <param name="sectorServicioId"></param>
        /// <param name="sectorServicioEntity"></param>
        /// <returns></returns>
        public bool UpdateSectorServicio(int sectorServicioId, SectorEntity sectorServicioEntity)
        {
            var success = false;
            if (sectorServicioEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var sectorServicio = _unitOfWork.SectorServicioRepository.GetByID(sectorServicioId);
                    if (sectorServicio != null)
                    {
                        sectorServicio.Codigo = sectorServicioEntity.Codigo;
                        sectorServicio.Descripcion = sectorServicioEntity.Descripcion;
                        sectorServicio.IdEstado = sectorServicioEntity.IdEstado;
                        sectorServicio.IdSector = sectorServicioEntity.IdSector;
                        sectorServicio.Nombre = sectorServicioEntity.Nombre;
                        sectorServicio.NORMA_SECTOR = sectorServicioEntity.NORMA_SECTOR;
                        sectorServicio.SERVICIO = sectorServicioEntity.SERVICIO;

                        _unitOfWork.SectorServicioRepository.Update(sectorServicio);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular sectorServicio
        /// </summary>
        /// <param name="sectorServicioId"></param>
        /// <returns></returns>
        public bool DeleteSectorServicio(int sectorServicioId)
        {
            var success = false;
            if (sectorServicioId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var sectorServicio = _unitOfWork.SectorServicioRepository.GetByID(sectorServicioId);
                    if (sectorServicio != null)
                    {
                        _unitOfWork.SectorServicioRepository.Delete(sectorServicio);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;

        }

        /// <summary>
        /// Inactivates a sectorServicio
        /// </summary>
        /// <param name="sectorServicioId"></param>
        /// <returns></returns>
        public bool InactivateSectorServicio(int sectorServicioId)
        {
            var success = false;
            if (sectorServicioId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var sectorServicio = _unitOfWork.SectorServicioRepository.GetByID(sectorServicioId);
                    if (sectorServicio != null)
                    {
                        sectorServicio.IdEstado = 0;
                        _unitOfWork.SectorServicioRepository.Update(sectorServicio);
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
