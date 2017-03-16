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
    /// Offers services for Norma specific CRUD operations
    /// </summary>
    public class NormaServices: INormaServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor to initialize UnitOfWork instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public NormaServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches norma details by id
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public BusinessEntities.NormaEntity GetNormaById(int normaId)
        {
            var normaServicio = _unitOfWork.NormaRepository.GetByID(normaId);
            if (normaServicio != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<NORMA, NormaEntity>();
                });
                var normaModel = Mapper.Map<NORMA, NormaEntity>(normaServicio);

                if (normaModel.IdEstado == 1)
                    normaModel.DescripcionEstado = "Activo";
                else
                    normaModel.DescripcionEstado = "Inactivo";

                return normaModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the normas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.NormaEntity> GetAllNormas()
        {
            var normaServicios = _unitOfWork.NormaRepository.GetAll().ToList();
            if (normaServicios.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<NORMA, NormaEntity>();
                });
                var normasModel = Mapper.Map<List<NORMA>, List<NormaEntity>>(normaServicios);

                foreach (NormaEntity norma in normasModel)
                {
                    if (norma.IdEstado == 1)
                        norma.DescripcionEstado = "Activo";
                    else
                        norma.DescripcionEstado = "Inactivo";
                }
                
                return normasModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the actives normas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.NormaEntity> GetAllNormasActive()
        {
            var normaServicios = _unitOfWork.NormaRepositoryCustom.GetMany().ToList();
            if (normaServicios.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<NORMA, NormaEntity>();
                });
                var normasModel = Mapper.Map<List<NORMA>, List<NormaEntity>>(normaServicios);
                return normasModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a norma
        /// </summary>
        /// <param name="normaEntity"></param>
        /// <returns></returns>
        public int CreateNorma(BusinessEntities.NormaEntity normaEntity)
        {
            using (var scope = new TransactionScope())
            {
                var normaServicio = new NORMA
                {
                    CodigoNorma = normaEntity.CodigoNorma,
                    Descripcion = normaEntity.Descripcion,
                    FechaInicio = normaEntity.FechaInicio,
                    FechaNorma = normaEntity.FechaNorma,
                    IdEntidadEmite = normaEntity.IdEntidadEmite,
                    IdEstado = normaEntity.IdEstado,
                    IdNorma = normaEntity.IdNorma,
                    IdNormaPadre = normaEntity.IdNormaPadre,
                    IdTipoNorma = normaEntity.IdTipoNorma,
                    NombreNorma = normaEntity.NombreNorma,
                    NORMA_SECTOR = normaEntity.NORMA_SECTOR,
                    UrlLink = normaEntity.UrlLink
                };
                _unitOfWork.NormaRepository.Insert(normaServicio);
                _unitOfWork.Save();
                scope.Complete();
                return normaServicio.IdNorma;
            }
        }

        /// <summary>
        /// Updates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <param name="normaEntity"></param>
        /// <returns></returns>
        public bool UpdateNorma(int normaId, BusinessEntities.NormaEntity normaEntity)
        {
            var success = false;
            if (normaEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var normaServicio = _unitOfWork.NormaRepository.GetByID(normaId);
                    if (normaServicio != null)
                    {
                        normaServicio.CodigoNorma = normaEntity.CodigoNorma;
                        normaServicio.Descripcion = normaEntity.Descripcion;
                        normaServicio.FechaInicio = normaEntity.FechaInicio;
                        normaServicio.FechaNorma = normaEntity.FechaNorma;
                        normaServicio.IdEntidadEmite = normaEntity.IdEntidadEmite;
                        normaServicio.IdEstado = normaEntity.IdEstado;
                        normaServicio.IdNorma = normaEntity.IdNorma;
                        normaServicio.IdNormaPadre = normaEntity.IdNormaPadre;
                        normaServicio.IdTipoNorma = normaEntity.IdTipoNorma;
                        normaServicio.NombreNorma = normaEntity.NombreNorma;
                        normaServicio.NORMA_SECTOR = normaEntity.NORMA_SECTOR;
                        normaServicio.UrlLink = normaEntity.UrlLink;

                        _unitOfWork.NormaRepository.Update(normaServicio);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool DeleteNorma(int normaId)
        {
            var success = false;
            if (normaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var norma = _unitOfWork.NormaRepository.GetByID(normaId);
                    if (norma != null)
                    {
                        _unitOfWork.NormaRepository.Delete(norma);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;

        }

        /// <summary>
        /// Inactivates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool InactivateNorma(int normaId)
        {
            var success = false;
            if (normaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var norma = _unitOfWork.NormaRepository.GetByID(normaId);
                    if (norma != null)
                    {
                        norma.IdEstado = 0;
                        _unitOfWork.NormaRepository.Update(norma);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Fetches all the normaPadre
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.NormaPadreEntity> GetNormasPadre()
        {
            var normas = _unitOfWork.NormaRepository.GetAll().ToList();
            if (normas.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<NORMA, NormaPadreEntity>();
                });
                var normaPadreList = Mapper.Map<List<NORMA>, List<NormaPadreEntity>>(normas);

                return normaPadreList;
            }
            return null;
        }
    }
}