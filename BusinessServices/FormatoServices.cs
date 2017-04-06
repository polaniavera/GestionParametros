using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for Formato specific CRUD operations
    /// </summary>
    public class FormatoServices: IFormatoServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TablaValorServices _tablaValorServices;
        private readonly PeriodicidadServices _periodicidadServices;
        private readonly PlazoServices _plazoServices;
        private readonly FormatoPlantillaServices _formatoPlantillaServices;
        private readonly FormatoServicioServices _formatoServicioServices;
        private readonly PlantillaCampoServices _plantillaCampoServices;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public FormatoServices(UnitOfWork unitOfWork,
            TablaValorServices tablaValorServices,
            PeriodicidadServices periodicidadServices,
            PlazoServices plazoServices,
            FormatoPlantillaServices formatoPlantillaServices,
            FormatoServicioServices formatoServicioServices,
            PlantillaCampoServices plantillaCampoServices)
        {
            _unitOfWork = unitOfWork;
            _tablaValorServices = tablaValorServices;
            _periodicidadServices = periodicidadServices;
            _plazoServices = plazoServices;
            _formatoPlantillaServices = formatoPlantillaServices;
            _formatoServicioServices = formatoServicioServices;
            _plantillaCampoServices = plantillaCampoServices;
        }

        /// <summary>
        /// Fetches formato details by id
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public BusinessEntities.FormatoEntity GetFormatoById(int formatoId)
        {
            var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
            if (formato != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<FORMATO, FormatoEntity>();
                });
                var formatoModel = Mapper.Map<FORMATO, FormatoEntity>(formato);
                return formatoModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the formatos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.FormatoEntity> GetAllFormatos()
        {
            var formatos = _unitOfWork.FormatoRepository.GetAll().ToList();
            if (formatos.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<FORMATO, FormatoEntity>();
                });
                var formatosModel = Mapper.Map<List<FORMATO>, List<FormatoEntity>>(formatos);
                return formatosModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a formato
        /// </summary>
        /// <param name="formatoEntity"></param>
        /// <returns></returns>
        public int CreateFormato(BusinessEntities.FormatoEntity formatoEntity)
        {
            using (var scope = new TransactionScope())
            {
                var formato = new FORMATO
                {
                    Codigo = formatoEntity.Codigo,
                    FORMATO_PLANTILLA = formatoEntity.FORMATO_PLANTILLA,
                    IdEstado = formatoEntity.IdEstado,
                    IdFormato = formatoEntity.IdFormato,
                    IdPeriodicidad = formatoEntity.IdPeriodicidad,
                    IdPlazo = formatoEntity.IdPlazo,
                    IdTipoFormato = formatoEntity.IdTipoFormato,
                    Nombre = formatoEntity.Nombre
                };
                _unitOfWork.FormatoRepository.Insert(formato);
                _unitOfWork.Save();
                scope.Complete();
                return formato.IdFormato;
            }
        }

        /// <summary>
        /// Updates a formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <param name="formatoEntity"></param>
        /// <returns></returns>
        public bool UpdateFormato(int formatoId, BusinessEntities.FormatoEntity formatoEntity)
        {
            var success = false;
            if (formatoEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
                    if (formato != null)
                    {
                        formato.Codigo = formatoEntity.Codigo;
                        formato.FORMATO_PLANTILLA = formatoEntity.FORMATO_PLANTILLA;
                        formato.IdEstado = formatoEntity.IdEstado;
                        formato.IdFormato = formatoEntity.IdFormato;
                        formato.IdPeriodicidad = formatoEntity.IdPeriodicidad;
                        formato.IdPlazo = formatoEntity.IdPlazo;
                        formato.IdTipoFormato = formatoEntity.IdTipoFormato;
                        formato.Nombre = formatoEntity.Nombre;

                        _unitOfWork.FormatoRepository.Update(formato);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public bool DeleteFormato(int formatoId)
        {
            var success = false;
            if (formatoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
                    if (formato != null)
                    {
                        _unitOfWork.FormatoRepository.Delete(formato);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }


        /// <summary>
        /// Fetches all the actives formatos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.FormatoEntity> GetAllFormatosActive()
        {
            var formatoServicios = _unitOfWork.FormatoRepositoryCustom.GetMany().ToList();
            if (formatoServicios.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<FORMATO, FormatoEntity>();
                });
                var formatos = Mapper.Map<List<FORMATO>, List<FormatoEntity>>(formatoServicios);
                return formatos;
            }
            return null;
        }


        /// <summary>
        /// Inactivates a formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public bool InactivateFormato(int formatoId)
        {
            var success = false;
            if (formatoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
                    if (formato != null)
                    {
                        formato.IdEstado = 0;
                        _unitOfWork.FormatoRepository.Update(formato);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Activates a formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public bool ActivateFormato(int formatoId)
        {
            var success = false;
            if (formatoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
                    if (formato != null)
                    {
                        formato.IdEstado = 1;
                        _unitOfWork.FormatoRepository.Update(formato);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Retrieve if exist a norma in formato entity by IdNorma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool ExistNormaFormato(int normaId)
        {
            bool exist = false;
            exist = _unitOfWork.FormatoRepositoryCustom.ExistNorma(normaId);

            if(exist)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Retrieve description from id
        /// </summary>
        /// <param name="formatoEntity"></param>
        /// <returns></returns>
        public FormatoEntity setDescripcion(FormatoEntity formato)
        {
            var norma = _unitOfWork.NormaRepository.GetByID(formato.IdNorma);
            if (norma != null)
                formato.nombreNorma = norma.NombreNorma;

            var valor = _tablaValorServices.GetTablaValorById(formato.IdTipoFormato);
            if (valor != null)
                formato.nombreTipoFormato = valor.ValorAlfanumerico;

            var tipoPeriodicidad = _periodicidadServices.GetPeriodicidadById(formato.IdPeriodicidad);
            if (tipoPeriodicidad != null)
                formato.nombrePeriodicidad = tipoPeriodicidad.Descripcion;

            var tipoPlazo = _plazoServices.GetPlazoById(formato.IdPlazo);
            if (tipoPlazo != null)
                formato.nombrePlazo = tipoPlazo.Descripcion;

            var seccion = _tablaValorServices.GetTablaValorById(formato.IdSeccion);
            if (seccion != null)
                formato.nombreSeccion = valor.ValorAlfanumerico;

            if (formato.IdEstado == 1)
            {
                formato.nombreEstado = "Activo";
            }
            else
            {
                formato.nombreEstado = "Inactivo";
            }

            return formato;
        }

        /// <summary>
        /// Find when the formato has changed the state
        /// </summary>
        /// <returns></returns>
        public bool changeFormatoState(FormatoEntity formatoEntity)
        {
            bool success = false;
            if (formatoEntity.IdFormato > 0)
            {
                //Obtiene Formato almacenado
                var formato = _unitOfWork.FormatoRepository.GetByID(formatoEntity.IdFormato);

                if (formato != null)
                {
                    //Encuentra si el formato cambio de estado con respecto al formato a actualizar
                    if (!(formato.IdEstado == formatoEntity.IdEstado))
                    {
                        //Si el formato a actualizar tiene estado activo, activa todas sus relaciones
                        if (formatoEntity.IdEstado == 1)
                        {
                            success = ActivateFormatoRelations(formatoEntity.IdFormato);   
                        }
                        else
                        {
                            success = InactivateFormatoRelations(formatoEntity.IdFormato);
                        }
                    }
                }

            }
            return success;
        }

        /// <summary>
        /// Activates a formato entity relations
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public bool ActivateFormatoRelations(int formatoId)
        {
            var success = false;
            if (formatoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var plantillas = _unitOfWork.FormatoPlantillaRepository.GetMany(c => c.IdFormato == formatoId);
                    if (plantillas != null)
                    {
                        foreach (FORMATO_PLANTILLA plantilla in plantillas)
                        {
                            //Activar las plantillas
                            bool activatePlantilla = _formatoPlantillaServices.ActivatePlantilla(plantilla.IdFormatoPlantilla);
                            if (!activatePlantilla)
                                return false;

                            //Obtener los registros de servicios asociados a estas plantillas
                            var servicios = _unitOfWork.FormatoServicioRepository.GetMany(c => c.IdFormatoPlantilla == plantilla.IdFormatoPlantilla);
                            //Obtener los registros de campos asociados a estas plantillas
                            var campos = _unitOfWork.PlantillaCampoRepository.GetMany(c => c.IdFormatoPlantilla == plantilla.IdFormatoPlantilla);

                            if (servicios != null)
                            {
                                foreach (FORMATO_SERVICIO servicio in servicios)
                                {
                                    //Activar los servicios asociados a la plantilla
                                    bool activateServicio = _formatoServicioServices.ActivateServicio(servicio.IdFormatoServicio);
                                    if (!activateServicio)
                                        return false;
                                }
                            }

                            if (campos != null)
                            {
                                foreach (PLANTILLA_CAMPO campo in campos)
                                {
                                    //Activar los campos asociados a la plantilla
                                    bool activateCampo = _plantillaCampoServices.ActivateCampo(campo.IdPlantillaCampo);
                                    if (!activateCampo)
                                        return false;
                                }
                            }
                        }
                    }
                }
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Inactivates a formato entity relations
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public bool InactivateFormatoRelations(int formatoId)
        {
            var success = false;
            if (formatoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var plantillas = _unitOfWork.FormatoPlantillaRepository.GetMany(c => c.IdFormato == formatoId);
                    if (plantillas != null)
                    {
                        foreach (FORMATO_PLANTILLA plantilla in plantillas)
                        {
                            //Activar las plantillas
                            bool inactivatePlantilla = _formatoPlantillaServices.InactivatePlantilla(plantilla.IdFormatoPlantilla);
                            if (!inactivatePlantilla)
                                return false;

                            //Obtener los registros de servicios asociados a estas plantillas
                            var servicios = _unitOfWork.FormatoServicioRepository.GetMany(c => c.IdFormatoPlantilla == plantilla.IdFormatoPlantilla);
                            //Obtener los registros de campos asociados a estas plantillas
                            var campos = _unitOfWork.PlantillaCampoRepository.GetMany(c => c.IdFormatoPlantilla == plantilla.IdFormatoPlantilla);

                            if (servicios != null)
                            {
                                foreach (FORMATO_SERVICIO servicio in servicios)
                                {
                                    //Activar los servicios asociados a la plantilla
                                    bool inactivateServicio = _formatoServicioServices.InactivateServicio(servicio.IdFormatoServicio);
                                    if (!inactivateServicio)
                                        return false;
                                }
                            }

                            if (campos != null)
                            {
                                foreach (PLANTILLA_CAMPO campo in campos)
                                {
                                    //Activar los campos asociados a la plantilla
                                    bool inactivateCampo = _plantillaCampoServices.InactivateCampo(campo.IdPlantillaCampo);
                                    if (!inactivateCampo)
                                        return false;
                                }
                            }
                        }
                    }
                }
                success = true;
            }
            return success;
        }

    }
}
