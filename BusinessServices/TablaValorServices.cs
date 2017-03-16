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
    /// Offers services for TablaValor specific CRUD operations
    /// </summary>
    public class TablaValorServices: ITablaValorServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEntidadServices _entidadServices;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TablaValorServices(UnitOfWork unitOfWork, IEntidadServices entidadServices)
        {
            _unitOfWork = unitOfWork;
            _entidadServices = entidadServices;
        }

        /// <summary>
        /// Fetches TablaValor details by id
        /// </summary>
        /// <param name="tablaValorId"></param>
        /// <returns></returns>
        public BusinessEntities.TablaValorEntity GetTablaValorById(int tablaValorId)
        {
            var tablaValor = _unitOfWork.TablaValorRepository.GetByID(tablaValorId);
            if (tablaValor != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TABLA_VALOR, TablaValorEntity>();
                });
                var tablaValorModel = Mapper.Map<TABLA_VALOR, TablaValorEntity>(tablaValor);
                return tablaValorModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the TablaValores
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.TablaValorEntity> GetAllTablaValores()
        {
            var tablaValores = _unitOfWork.TablaValorRepository.GetAll().ToList();
            if (tablaValores.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TABLA_VALOR, TablaValorEntity>();
                });
                var tablaValoresModel = Mapper.Map<List<TABLA_VALOR>, List<TablaValorEntity>>(tablaValores);
                return tablaValoresModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a TablaValor
        /// </summary>
        /// <param name="tablaValorEntity"></param>
        /// <returns></returns>
        public int CreateTablaValor(BusinessEntities.TablaValorEntity tablaValorEntity)
        {
            using (var scope = new TransactionScope())
            {
                var tablaValor = new TABLA_VALOR
                {
                    IdCampo = tablaValorEntity.IdCampo,
                    IdTablaValor = tablaValorEntity.IdTablaValor,
                    TABLA_CAMPO = tablaValorEntity.TABLA_CAMPO
                };

                _unitOfWork.TablaValorRepository.Insert(tablaValor);
                _unitOfWork.Save();
                scope.Complete();
                return tablaValor.IdTablaValor;
            }
        }

        /// <summary>
        /// Updates a TablaValor
        /// </summary>
        /// <param name="tablaValorId"></param>
        /// <param name="tablaValorEntity"></param>
        /// <returns></returns>
        public bool UpdateTablaValor(int tablaValorId, BusinessEntities.TablaValorEntity tablaValorEntity)
        {
            var success = false;
            if (tablaValorEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var tablaValor = _unitOfWork.TablaValorRepository.GetByID(tablaValorId);
                    if (tablaValor != null)
                    {
                        tablaValor.IdCampo = tablaValorEntity.IdCampo;
                        tablaValor.IdTablaValor = tablaValorEntity.IdTablaValor;
                        tablaValor.TABLA_CAMPO = tablaValorEntity.TABLA_CAMPO;

                        _unitOfWork.TablaValorRepository.Update(tablaValor);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular TablaValor
        /// </summary>
        /// <param name="tablaValorId"></param>
        /// <returns></returns>
        public bool DeleteTablaValor(int tablaValorId)
        {
            var success = false;
            if (tablaValorId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var tablaValor = _unitOfWork.TablaValorRepository.GetByID(tablaValorId);
                    if (tablaValor != null)
                    {
                        _unitOfWork.TablaValorRepository.Delete(tablaValor);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public NormaEntity setDescripcion(NormaEntity norma)
        {
            var valor = GetTablaValorById(norma.IdTipoNorma);
            if (valor != null)
                norma.DescripcionTipoNorma = valor.ValorAlfanumerico;

            var entidad = _entidadServices.GetEntidadById(norma.IdEntidadEmite);
            if (entidad != null)
                norma.DescripcionEntidadEmite = entidad.Nombre;

            return norma;
        }

        public IEnumerable<NormaEntity> setDescripcionList(IEnumerable<NormaEntity> normas)
        {
            foreach (NormaEntity norma in normas)
            {
                var valor = GetTablaValorById(norma.IdTipoNorma);
                if(valor!=null)
                    norma.DescripcionTipoNorma = valor.ValorAlfanumerico;

                var entidad = _entidadServices.GetEntidadById(norma.IdEntidadEmite);
                if (entidad != null)
                    norma.DescripcionEntidadEmite = entidad.Nombre;
            }

            return normas;
        }

    }
}
