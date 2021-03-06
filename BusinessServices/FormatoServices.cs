﻿using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;

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
        public object[] GetFormatoById(int formatoId)
        {
            try
            {
                var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
                if (formato != null)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<FORMATO, FormatoEntity>();
                    });
                    var formatoModel = Mapper.Map<FORMATO, FormatoEntity>(formato);
                    object[] resultado = { "0000", formatoModel };
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
        /// Fetches all the formatos
        /// </summary>
        /// <returns></returns>
        public object[] GetAllFormatos()
        {
            try
            {
                var formatos = _unitOfWork.FormatoRepository.GetAll().ToList();
                if (formatos.Any())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<FORMATO, FormatoEntity>();
                    });
                    var formatosModel = Mapper.Map<List<FORMATO>, List<FormatoEntity>>(formatos);
                    object[] resultado = { "0000", formatosModel };
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
        /// Creates a formato
        /// </summary>
        /// <param name="formatoEntity"></param>
        /// <returns></returns>
        public object[] CreateFormato(BusinessEntities.FormatoEntity formatoEntity)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var formato = new FORMATO
                    {
                        IdFormato = formatoEntity.IdFormato,
                        IdNorma = formatoEntity.IdNorma,
                        Codigo = formatoEntity.Codigo,
                        Nombre = formatoEntity.Nombre,
                        IdTipoFormato = formatoEntity.IdTipoFormato,
                        IdPlazo = formatoEntity.IdPlazo,
                        IdPeriodicidad = formatoEntity.IdPeriodicidad,
                        IdEstado = formatoEntity.IdEstado,
                        DiasPlazo = formatoEntity.DiasPlazo,
                        IdSeccion = formatoEntity.IdSeccion,
                        InlcuyeFecha = formatoEntity.InlcuyeFecha
                    };
                    _unitOfWork.FormatoRepository.Insert(formato);
                    _unitOfWork.Save();
                    scope.Complete();
                    object[] resultado = { "0000", formato.IdFormato };
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
        /// Updates a formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <param name="formatoEntity"></param>
        /// <returns></returns>
        public object[] UpdateFormato(int formatoId, BusinessEntities.FormatoEntity formatoEntity)
        {
            var success = false;
            try
            {
                if (formatoEntity != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        var formato = _unitOfWork.FormatoRepository.GetByID(formatoId);
                        if (formato != null)
                        {
                            formato.IdFormato = formatoEntity.IdFormato;
                            formato.IdNorma = formatoEntity.IdNorma;
                            formato.Codigo = formatoEntity.Codigo;
                            formato.Nombre = formatoEntity.Nombre;
                            formato.IdTipoFormato = formatoEntity.IdTipoFormato;
                            formato.IdPlazo = formatoEntity.IdPlazo;
                            formato.IdPeriodicidad = formatoEntity.IdPeriodicidad;
                            formato.IdEstado = formatoEntity.IdEstado;
                            formato.DiasPlazo = formatoEntity.DiasPlazo;
                            formato.IdSeccion = formatoEntity.IdSeccion;
                            formato.InlcuyeFecha = formatoEntity.InlcuyeFecha;

                            _unitOfWork.FormatoRepository.Update(formato);
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
        /// Deletes a particular formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public object[] DeleteFormato(int formatoId)
        {
            var success = false;
            try
            {
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
        /// Fetches all the actives formatos
        /// </summary>
        /// <returns></returns>
        public object[] GetAllFormatosActive()
        {
            try
            {
                var formatoServicios = _unitOfWork.FormatoRepositoryCustom.GetMany().ToList();
                if (formatoServicios.Any())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<FORMATO, FormatoEntity>();
                    });
                    var formatos = Mapper.Map<List<FORMATO>, List<FormatoEntity>>(formatoServicios);
                    object[] resultado = { "0000", formatos };
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
        /// Inactivates a formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public object[] InactivateFormato(int formatoId)
        {
            var success = false;
            try
            {
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
        /// Activates a formato
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public object[] ActivateFormato(int formatoId)
        {
            var success = false;
            try
            {
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
        /// Retrieve if exist a norma in formato entity by IdNorma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public object[] ExistNormaFormato(int normaId)
        {
            try
            {
                bool exist = false;
                exist = _unitOfWork.FormatoRepositoryCustom.ExistNorma(normaId);

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
        /// Retrieve description from id
        /// </summary>
        /// <param name="formatoEntity"></param>
        /// <returns></returns>
        public object[] setDescripcion(FormatoEntity formato)
        {
            try
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
                object[] resultado = { "0000", formato };
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
        /// Find when the formato has changed the state
        /// </summary>
        /// <returns></returns>
        public object[] changeFormatoState(FormatoEntity formatoEntity)
        {
            bool success = false;
            try
            {
                if (formatoEntity.IdFormato > 0)
                {
                    //Obtiene Formato almacenado
                    var formato = _unitOfWork.FormatoRepository.GetByID(formatoEntity.IdFormato);

                    if (formato != null)
                    {
                        success = true;
                        //Encuentra si el formato cambio de estado con respecto al formato a actualizar
                        if (!(formato.IdEstado == formatoEntity.IdEstado))
                        {
                            //Si el formato a actualizar tiene estado activo, activa todas sus relaciones
                            if (formatoEntity.IdEstado == 1)
                            {
                                var flagObject = ActivateFormatoRelations(formatoEntity.IdFormato);
                                if (flagObject[0].Equals("0000"))
                                {
                                    success = (bool)flagObject.ElementAt(1);
                                }
                                else
                                {
                                    return flagObject;
                                }
                            }
                            else
                            {
                                var flagObject = InactivateFormatoRelations(formatoEntity.IdFormato);
                                if (flagObject[0].Equals("0000"))
                                {
                                    success = (bool)flagObject.ElementAt(1);
                                }
                                else
                                {
                                    return flagObject;
                                }
                            }
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
        /// Activates a formato entity relations
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public object[] ActivateFormatoRelations(int formatoId)
        {
            var success = false;
            try
            {
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
                                {
                                    object[] resultado2 = { "0000", false };
                                    return resultado2;
                                }

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
                                        {
                                            object[] resultado2 = { "0000", false };
                                            return resultado2;
                                        }
                                    }
                                }

                                if (campos != null)
                                {
                                    foreach (PLANTILLA_CAMPO campo in campos)
                                    {
                                        //Activar los campos asociados a la plantilla
                                        bool activateCampo = _plantillaCampoServices.ActivateCampo(campo.IdPlantillaCampo);
                                        if (!activateCampo)
                                        {
                                            object[] resultado2 = { "0000", false };
                                            return resultado2;
                                        }
                                    }
                                }
                            }
                        }
                        scope.Complete();
                        success = true;
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
        /// Inactivates a formato entity relations
        /// </summary>
        /// <param name="formatoId"></param>
        /// <returns></returns>
        public object[] InactivateFormatoRelations(int formatoId)
        {
            var success = false;
            try
            {
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
                                {
                                    object[] resultado2 = { "0000", false };
                                    return resultado2;
                                }
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
                                        {
                                            object[] resultado2 = { "0000", false };
                                            return resultado2;
                                        }
                                    }
                                }

                                if (campos != null)
                                {
                                    foreach (PLANTILLA_CAMPO campo in campos)
                                    {
                                        //Activar los campos asociados a la plantilla
                                        bool inactivateCampo = _plantillaCampoServices.InactivateCampo(campo.IdPlantillaCampo);
                                        if (!inactivateCampo)
                                        {
                                            object[] resultado2 = { "0000", false };
                                            return resultado2;
                                        }
                                    }
                                }
                            }
                        }
                        scope.Complete();
                        success = true;
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

    }
}
