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
    /// Offers services for Tabla specific CRUD operations
    /// </summary>
    public class TablaServices: ITablaServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TablaServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches Tabla details by id
        /// </summary>
        /// <param name="tablaId"></param>
        /// <returns></returns>
        public BusinessEntities.TablaEntity GetTablaById(int tablaId)
        {
            var tabla = _unitOfWork.TablaRepository.GetByID(tablaId);
            if (tabla != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TABLA, TablaEntity>();
                });
                var tablaModel = Mapper.Map<TABLA, TablaEntity>(tabla);
                return tablaModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the Tablas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.TablaEntity> GetAllTablas()
        {
            var tablas = _unitOfWork.TablaRepository.GetAll().ToList();
            if (tablas.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TABLA, TablaEntity>();
                });
                var tablasModel = Mapper.Map<List<TABLA>, List<TablaEntity>>(tablas);
                return tablasModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches Tipo Norma list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.TablaValorListEntity> GetTipoNorma()
        {
            TablaValorListEntity tablaValor = new TablaValorListEntity();
            List<TablaValorListEntity> tablaValorList = new List<TablaValorListEntity>();
            var tabla = _unitOfWork.TablaRepositoryCustom.GetTipoNorma().ToList();
            if (tabla != null)
            {
                foreach (TABLA_VALOR item in tabla)
                {
                    tablaValor = new TablaValorListEntity();
                    tablaValor.IdTablaValor = item.IdTablaValor;
                    tablaValor.Valor = item.ValorAlfanumerico;
                    tablaValorList.Add(tablaValor);
                }

                return tablaValorList;
            }
            return null;
        }

        /// <summary>
        /// Fetches Tipo Formato list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.TablaValorListEntity> GetTipoFormato()
        {
            TablaValorListEntity tablaValor = new TablaValorListEntity();
            List<TablaValorListEntity> tablaValorList = new List<TablaValorListEntity>();
            var tabla = _unitOfWork.TablaRepositoryCustom.GetTipoFormato().ToList();
            if (tabla != null)
            {
                foreach (TABLA_VALOR item in tabla)
                {
                    tablaValor = new TablaValorListEntity();
                    tablaValor.IdTablaValor = item.IdTablaValor;
                    tablaValor.Valor = item.ValorAlfanumerico;
                    tablaValorList.Add(tablaValor);
                }

                return tablaValorList;
            }
            return null;
        }

        /// <summary>
        /// Fetches Sección list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.TablaValorListEntity> GetParametrosVert(string _parametro)
        {
            TablaValorListEntity tablaValor = new TablaValorListEntity();
            List<TablaValorListEntity> tablaValorList = new List<TablaValorListEntity>();
            var tabla = _unitOfWork.TablaRepositoryCustom.GetParametros(_parametro).ToList();
            if (tabla != null)
            {
                foreach (TABLA_VALOR item in tabla)
                {
                    tablaValor = new TablaValorListEntity();
                    tablaValor.IdTablaValor = item.IdTablaValor;
                    tablaValor.Valor = item.ValorAlfanumerico;
                    tablaValorList.Add(tablaValor);
                }

                return tablaValorList;
            }
            return null;
        }


        /// <summary>
        /// Creates a Tabla
        /// </summary>
        /// <param name="tablaEntity"></param>
        /// <returns></returns>
        public int CreateTabla(BusinessEntities.TablaEntity tablaEntity)
        {
            using (var scope = new TransactionScope())
            {
                var tabla = new TABLA
                {
                    Descripcion = tablaEntity.Descripcion,
                    IdTabla = tablaEntity.IdTabla,
                    IdTipoTabla = tablaEntity.IdTipoTabla,
                    TABLA_CAMPO = tablaEntity.TABLA_CAMPO
                };

                _unitOfWork.TablaRepository.Insert(tabla);
                _unitOfWork.Save();
                scope.Complete();
                return tabla.IdTabla;
            }
        }

        /// <summary>
        /// Updates a Tabla
        /// </summary>
        /// <param name="tablaId"></param>
        /// <param name="tablaEntity"></param>
        /// <returns></returns>
        public bool UpdateTabla(int tablaId, BusinessEntities.TablaEntity tablaEntity)
        {
            var success = false;
            if (tablaEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var tabla = _unitOfWork.TablaRepository.GetByID(tablaId);
                    if (tabla != null)
                    {
                        tabla.Descripcion = tablaEntity.Descripcion;
                        tabla.IdTabla = tablaEntity.IdTabla;
                        tabla.IdTipoTabla = tablaEntity.IdTipoTabla;
                        tabla.TABLA_CAMPO = tablaEntity.TABLA_CAMPO;

                        _unitOfWork.TablaRepository.Update(tabla);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular Tabla
        /// </summary>
        /// <param name="tablaId"></param>
        /// <returns></returns>
        public bool DeleteTabla(int tablaId)
        {
            var success = false;
            if (tablaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var tabla = _unitOfWork.TablaRepository.GetByID(tablaId);
                    if (tabla != null)
                    {
                        _unitOfWork.TablaRepository.Delete(tabla);
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
