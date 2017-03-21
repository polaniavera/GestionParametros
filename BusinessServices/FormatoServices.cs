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

        /// <summary>
        /// Public constructor.
        /// </summary>
        public FormatoServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

    }
}
