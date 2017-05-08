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
        public object[] GetNormaById(int normaId)
        {
            try
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

                    object[] resultado = { "0000", normaModel };
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
        /// Fetches all the normas
        /// </summary>
        /// <returns></returns>
        public object[] GetAllNormas()
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
        public object[] GetAllNormasActive()
        {
            try
            {
                var normaServicios = _unitOfWork.NormaRepositoryCustom.GetMany().ToList();
                if (normaServicios.Any())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<NORMA, NormaEntity>();
                    });
                    var normasModel = Mapper.Map<List<NORMA>, List<NormaEntity>>(normaServicios);
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
        /// Creates a norma
        /// </summary>
        /// <param name="normaEntity"></param>
        /// <returns></returns>
        public object[] CreateNorma(BusinessEntities.NormaEntity normaEntity)
        {
            try
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
    
                    object[] resultado = { "0000", normaServicio.IdNorma };
                    return resultado;
                }
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
        /// Updates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <param name="normaEntity"></param>
        /// <returns></returns>
        public object[] UpdateNorma(int normaId, BusinessEntities.NormaEntity normaEntity)
        {
            var success = false;
            try
            {
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
        /// Deletes a particular norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public object[] DeleteNorma(int normaId)
        {
            var success = false;
            try
            {
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
        /// Inactivates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public object[] InactivateNormaRelations(int normaId)
        {
            var success = false;
            try
            {
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
                                            {
                                                object[] resultado2 = { "0000", false };
                                                return resultado2;
                                            }
                                        }
                                    }
                                    //Inactivar todos los formatos devueltos segun idNorma
                                    var flagObject = _formatoServices.InactivateFormato(formato.IdFormato);
                                    if (flagObject[0].Equals("0000"))
                                    {
                                        bool flag = (bool)flagObject.ElementAt(1);
                                        if (!flag)
                                        {
                                            object[] resultado2 = { "0000", false };
                                            return resultado2;
                                        }
                                    }
                                    else
                                    {
                                        return flagObject;
                                    }
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
                                    {
                                        object[] resultado2 = { "0000", false };
                                        return resultado2;
                                    }
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
        /// Activates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public object[] ActivateNormaRelations(int normaId)
        {
            var success = false;
            try
            {
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
                                            {
                                                object[] resultado2 = { "0000", false };
                                                return resultado2;
                                            }
                                        }
                                    }
                                    //Activar todos los formatos devueltos segun idNorma
                                    var flagObject = _formatoServices.ActivateFormato(formato.IdFormato);
                                    if (flagObject[0].Equals("0000"))
                                    {
                                        bool flag = (bool)flagObject.ElementAt(1);
                                        if (!flag)
                                        {
                                            object[] resultado2 = { "0000", false };
                                            return resultado2;
                                        }
                                    }
                                    else
                                    {
                                        return flagObject;
                                    }                                    
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
                                    {
                                        object[] resultado2 = { "0000", false };
                                        return resultado2;
                                    }
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
        /// Inactivates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public object[] InactivateNorma(int normaId)
        {
            var success = false;
            try
            {
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
        /// Activates a norma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public object[] ActivateNorma(int normaId)
        {
            var success = false;
            try
            {
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
        /// Fetches all the normaPadre
        /// </summary>
        /// <returns></returns>
        public object[] GetNormasPadre()
        {
            try
            {
                var normas = _unitOfWork.NormaRepository.GetAll().ToList();
                if (normas.Any())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<NORMA, NormaPadreEntity>();
                    });
                    var normaPadreList = Mapper.Map<List<NORMA>, List<NormaPadreEntity>>(normas);

                    object[] resultado = { "0000", normaPadreList };
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
        /// return true if exist sector formato by sectorid
        /// </summary>
        /// <returns></returns>
        public object[] ExistSectorFormato(int sectorId)
        {
            try
            {
                var exist = _unitOfWork.NormaRepositoryCustom.ExistServicio(sectorId);
                object[] resultado = { "0000", exist };
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
        /// Validate the normaSector asociated with Norma
        /// </summary>
        /// <returns></returns>
        public object[] ValidateSector(NormaEntity normaEntity, IEnumerable<NormaSectorEntity> normaSectorById)
        {
            int match = 0;
            try
            {
                if (normaSectorById != null)
                {
                    foreach (NormaSectorEntity sectorExistente in normaSectorById)
                    {
                        foreach (DataModel.NORMA_SECTOR sectorNuevo in normaEntity.NORMA_SECTOR)
                        {
                            if (sectorNuevo.IdSector.Equals(sectorExistente.IdSector))
                                match++;
                        }
                        //Si el sector existente no se encuentra en los seleccionados
                        //valida que dicho sector no tenga relaciones para poderlo borrar
                        if (match == 0)
                        {
                            //Valido relaciones
                            var flagObject = ExistSectorFormato(sectorExistente.IdSector);
                            if (flagObject[0].Equals("0000"))
                            {
                                bool flag = (bool) flagObject.ElementAt(1);
                                if (flag)
                                {
                                    object[] resultado2 = { "0000", false };
                                    return resultado2;
                                }
                            }else
                            {
                                return flagObject;
                            }
                            
                        }
                        else
                            match = 0;
                    }
                }
                object[] resultado = { "0000", true };
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
        /// Find when the norma has changed the state
        /// </summary>
        /// <returns></returns>
        public object[] changeNormaState(NormaEntity normaEntity)
        {
            try
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
                                var flagObject = ActivateNormaRelations(normaEntity.IdNorma);
                                if (flagObject[0].Equals("0000"))
                                {
                                    bool flag = (bool)flagObject.ElementAt(1);
                                    if (flag)
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
                                    return flagObject;
                                }
                            }
                            else
                            {
                                var flagObject = InactivateNormaRelations(normaEntity.IdNorma);
                                if (flagObject[0].Equals("0000"))
                                {
                                    bool flag = (bool)flagObject.ElementAt(1);
                                    if (flag)
                                    {
                                        foreach (var item in normaEntity.NORMA_SECTOR)
                                        {
                                            item.IdEstado = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    return flagObject;
                                }
                            }
                        }
                    }

                }
                object[] resultado = { "0000", normaEntity };
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
    }
}