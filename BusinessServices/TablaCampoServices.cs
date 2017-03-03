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
    /// Offers services for TablaCampo specific CRUD operations
    /// </summary>
    public class TablaCampoServices: ITablaCampoServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TablaCampoServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches TablaCampo details by id
        /// </summary>
        /// <param name="tablaCampoId"></param>
        /// <returns></returns>
        public BusinessEntities.TablaCampoEntity GetTablaCampoById(int tablaCampoId)
        {
            var tablaCampo = _unitOfWork.TablaCampoRepository.GetByID(tablaCampoId);
            if (tablaCampo != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TABLA_CAMPO, TablaCampoEntity>();
                });
                var tablaCampoModel = Mapper.Map<TABLA_CAMPO, TablaCampoEntity>(tablaCampo);
                return tablaCampoModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the TablaCampos.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.TablaCampoEntity> GetAllTablaCampos()
        {
            var tablaCampos = _unitOfWork.TablaCampoRepository.GetAll().ToList();
            if (tablaCampos.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TABLA_CAMPO, TablaCampoEntity>();
                });
                var tablaCamposModel = Mapper.Map<List<TABLA_CAMPO>, List<TablaCampoEntity>>(tablaCampos);
                return tablaCamposModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a TablaCampo
        /// </summary>
        /// <param name="tablaCampoEntity"></param>
        /// <returns></returns>
        public int CreateTablaCampo(BusinessEntities.TablaCampoEntity tablaCampoEntity)
        {
            using (var scope = new TransactionScope())
            {
                var tablaCampo = new TABLA_CAMPO
                {
                    IdCampo = tablaCampoEntity.IdCampo,
                    IdTabla = tablaCampoEntity.IdTabla,
                    TABLA = tablaCampoEntity.TABLA,
                    TABLA_VALOR = tablaCampoEntity.TABLA_VALOR
                };
                _unitOfWork.TablaCampoRepository.Insert(tablaCampo);
                _unitOfWork.Save();
                scope.Complete();
                return tablaCampo.IdCampo;
            }
        }

        /// <summary>
        /// Updates a TablaCampo
        /// </summary>
        /// <param name="tablaCampoId"></param>
        /// <param name="tablaCampoEntity"></param>
        /// <returns></returns>
        public bool UpdateTablaCampo(int tablaCampoId, BusinessEntities.TablaCampoEntity tablaCampoEntity)
        {
            var success = false;
            if (tablaCampoEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var tablaCampo = _unitOfWork.TablaCampoRepository.GetByID(tablaCampoId);
                    if (tablaCampo != null)
                    {
                        tablaCampo.IdCampo = tablaCampoEntity.IdCampo;
                        tablaCampo.IdTabla = tablaCampoEntity.IdTabla;
                        tablaCampo.TABLA = tablaCampoEntity.TABLA;
                        tablaCampo.TABLA_VALOR = tablaCampoEntity.TABLA_VALOR;

                        _unitOfWork.TablaCampoRepository.Update(tablaCampo);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular TablaCampo
        /// </summary>
        /// <param name="tablaCampoId"></param>
        /// <returns></returns>
        public bool DeleteTablaCampo(int tablaCampoId)
        {
            var success = false;
            if (tablaCampoId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var tablaCampo = _unitOfWork.TablaCampoRepository.GetByID(tablaCampoId);
                    if (tablaCampo != null)
                    {

                        _unitOfWork.TablaCampoRepository.Delete(tablaCampo);
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