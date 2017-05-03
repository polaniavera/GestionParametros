using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
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
        private readonly FormatoServices _formatoServices;
        private readonly NormaSectorServices _normaSectorServices;
        private readonly PlantillaCampoServices _plantillaCampoServices;

        /// <summary>
        /// Public constructor to initialize UnitOfWork instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public NormaServices(UnitOfWork unitOfWork,
            FormatoServices formatoServices,
            NormaSectorServices normaSectorServices,
            PlantillaCampoServices plantillaCampoServices)
        {
            _unitOfWork = unitOfWork;
            _formatoServices = formatoServices;
            _normaSectorServices = normaSectorServices;
            _plantillaCampoServices = plantillaCampoServices;
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
        public Object[] GetAllNormas()
        {
            try
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

                    object[] resultado = { "0000", normasModel };
                    return resultado;
                }
                var cod = new CodigoError();
                var codigoError = cod.Error("null");
                object[] resultado2 = { codigoError };
                return resultado2;
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
                    IdUrlLink = normaEntity.IdUrlLink,
                    NombreArchivo = normaEntity.NombreArchivo,
                    IdSeccion = normaEntity.IdSeccion
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
                        normaServicio.IdUrlLink = normaEntity.IdUrlLink;
                        normaServicio.NombreArchivo = normaEntity.NombreArchivo;
                        normaServicio.IdSeccion = normaEntity.IdSeccion;

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
        public bool InactivateNormaRelations(int normaId)
        {
            var success = false;
            if (normaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var norma = _unitOfWork.NormaRepository.GetByID(normaId);
                    if (norma != null)
                    {
                        //Obtener los formatos relacionados con Norma para cambiarles el estado
                        var formatos = _unitOfWork.FormatoRepositoryCustom.GetByIdNorma(normaId);
                        if (formatos != null)
                        {
                            foreach (FORMATO formato in formatos)
                            {
                                //Obtener los registros de campos asociados a estos formatos
                                var campos = _unitOfWork.FormatoRepositoryCustom.GetCamposByFormato(formato.IdFormato);
                                if (campos != null)
                                {
                                    foreach (PLANTILLA_CAMPO campo in campos)
                                    {
                                        //Inactivar los campos
                                        bool inactivateCampo = _plantillaCampoServices.InactivateCampo(campo.IdPlantillaCampo);
                                        if (!inactivateCampo)
                                            return false;
                                    }
                            }
                                //Inactivar todos los formatos devueltos segun idNorma
                                bool inactivateFormato = _formatoServices.InactivateFormato(formato.IdFormato);
                                if (!inactivateFormato)
                                    return false;
                            }
                        }
                        //Obtiene los sectores segun idNorma
                        var sectores = _normaSectorServices.GetNormaSectorByIdNorma(normaId);
                        if (sectores != null)
                        {
                            foreach (NormaSectorEntity sector in sectores)
                            {
                                //Inactivar los sectores
                                bool inactivateSector = _normaSectorServices.InactivateNormaSector(sector.IdNormaSector);
                                if (!inactivateSector)
                                    return false;
                            }
                            success = true;
                        }
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
        /// Activates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool ActivateNormaRelations(int normaId)
        {
            var success = false;
            if (normaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var norma = _unitOfWork.NormaRepository.GetByID(normaId);
                    if (norma != null)
                    {
                        //Obtener los formatos relacionados con Norma para cambiarles el estado
                        var formatos = _unitOfWork.FormatoRepositoryCustom.GetByIdNorma(normaId);
                        if (formatos != null)
                        {
                            foreach (FORMATO formato in formatos)
                            {
                                //Obtener los registros de campos asociados a estos formatos
                                var campos = _unitOfWork.FormatoRepositoryCustom.GetCamposByFormato(formato.IdFormato);
                                if (campos != null)
                                {
                                    foreach (PLANTILLA_CAMPO campo in campos)
                                    {
                                        //Activar los campos
                                        bool activateCampo = _plantillaCampoServices.ActivateCampo(campo.IdPlantillaCampo);
                                        if (!activateCampo)
                                            return false;
                                    }
                                }
                                //Activar todos los formatos devueltos segun idNorma
                                bool activateFormato = _formatoServices.ActivateFormato(formato.IdFormato);
                                if (!activateFormato)
                                    return false;
                            }
                        }
                        //Obtiene los sectores segun idNorma
                        var sectores = _normaSectorServices.GetNormaSectorByIdNorma(normaId);
                        if (sectores != null)
                        {
                            foreach (NormaSectorEntity sector in sectores)
                            {
                                //Activar los sectores
                                bool activateSector = _normaSectorServices.ActivateNormaSector(sector.IdNormaSector);
                                if (!activateSector)
                                    return false;
                            }
                            success = true;
                        }
                        norma.IdEstado = 1;
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
        /// Activates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool ActivateNorma(int normaId)
        {
            var success = false;
            if (normaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var norma = _unitOfWork.NormaRepository.GetByID(normaId);
                    if (norma != null)
                    {
                        norma.IdEstado = 1;
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

        /// <summary>
        /// return true if exist sector formato by sectorid
        /// </summary>
        /// <returns></returns>
        public bool ExistSectorFormato(int sectorId)
        {
            return _unitOfWork.NormaRepositoryCustom.ExistServicio(sectorId);

        }

        /// <summary>
        /// Validate the normaSector asociated with Norma
        /// </summary>
        /// <returns></returns>
        public bool ValidateSector(NormaEntity normaEntity, IEnumerable<NormaSectorEntity> normaSectorById)
        {
            int match = 0;

            if (normaSectorById != null)
            {
                foreach (NormaSectorEntity sectorExistente in normaSectorById)
                {
                    foreach (DataModel.NORMA_SECTOR sectorNuevo in normaEntity.NORMA_SECTOR)
                    {
                        if (sectorNuevo.IdSectorServicio.Equals(sectorExistente.IdSectorServicio))
                            match++;
                    }
                    //Si el sector existente no se encuentra en los seleccionados
                    //valida que dicho sector no tenga relaciones para poderlo borrar
                    if (match == 0)
                    {
                        //Valido relaciones
                        if (ExistSectorFormato(sectorExistente.IdSectorServicio))
                            return false;
                    }
                    else
                        match = 0;
                }
            }
            return true;
        }

        /// <summary>
        /// Find when the norma has changed the state
        /// </summary>
        /// <returns></returns>
        public NormaEntity changeNormaState(NormaEntity normaEntity)
        {
            if (normaEntity.IdNorma > 0)
            {
                //Obtiene Norma almacenada
                var norma = _unitOfWork.NormaRepository.GetByID(normaEntity.IdNorma);

                if (norma != null)
                {
                    //Encuentra si la Norma cambio de estado con respecto a la norma a actualizar
                    if (!(norma.IdEstado == normaEntity.IdEstado))
                    {
                        //Si la Norma a actualizar tiene estado activo, activa todas sus relaciones
                        if (normaEntity.IdEstado == 1)
                        {
                            if (ActivateNormaRelations(normaEntity.IdNorma))
                            {
                                //Y activa los sectores nuevos a almacenar
                                foreach (var item in normaEntity.NORMA_SECTOR)
                                {
                                    item.IdEstado = 1;
                                }
                            }
                        }
                        else
                        {
                            if (InactivateNormaRelations(normaEntity.IdNorma))
                            {
                                foreach (var item in normaEntity.NORMA_SECTOR)
                                {
                                    item.IdEstado = 0;
                                }
                            }
                        }
                    }
                }

            }
            return normaEntity;
        }
    }
}