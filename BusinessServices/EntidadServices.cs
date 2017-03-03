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
    /// Offers services for Entidad specific CRUD operations
    /// </summary>
    public class EntidadServices: IEntidadServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public EntidadServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches Entidad details by id
        /// </summary>
        /// <param name="entidadId"></param>
        /// <returns></returns>
        public BusinessEntities.EntidadEntity GetEntidadById(int entidadId)
        {
            var entidad = _unitOfWork.EntidadRepository.GetByID(entidadId);
            if (entidad != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ENTIDAD, EntidadEntity>();
                });
                var entidadModel = Mapper.Map<ENTIDAD, EntidadEntity>(entidad);
                return entidadModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the Entidades
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.EntidadEntity> GetAllEntidades()
        {
            var entidades = _unitOfWork.EntidadRepository.GetAll().ToList();
            if (entidades.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ENTIDAD, EntidadEntity>();
                });
                var entidadesModel = Mapper.Map<List<ENTIDAD>, List<EntidadEntity>>(entidades);
                return entidadesModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a Entidad
        /// </summary>
        /// <param name="entidadEntity"></param>
        /// <returns></returns>
        public int CreateEntidad(BusinessEntities.EntidadEntity entidadEntity)
        {
            using (var scope = new TransactionScope())
            {
                var entidad = new ENTIDAD
                {
                    Ciudad = entidadEntity.Ciudad,
                    Codigo = entidadEntity.Codigo,
                    CodigoHomologado = entidadEntity.CodigoHomologado,
                    Departamento = entidadEntity.Departamento,
                    Descripcion = entidadEntity.Descripcion,
                    Direccion = entidadEntity.Direccion,
                    Email = entidadEntity.Email,
                    ENTIDAD_SERVICIO = entidadEntity.ENTIDAD_SERVICIO,
                    FechaConstitucion = entidadEntity.FechaConstitucion,
                    IdCodigoHomologado = entidadEntity.IdCodigoHomologado,
                    IdEntidad = entidadEntity.IdEntidad,
                    IdEntidadPadre = entidadEntity.IdEntidadPadre,
                    IdEstado = entidadEntity.IdEstado,
                    IdTipoEntidad = entidadEntity.IdTipoEntidad,
                    IdTipoRelacionEntidadPadre = entidadEntity.IdTipoRelacionEntidadPadre,
                    NaturalezaJuridica = entidadEntity.NaturalezaJuridica,
                    NIT = entidadEntity.NIT,
                    Nombre = entidadEntity.Nombre,
                    Sigla = entidadEntity.Sigla,
                    Telefono = entidadEntity.Telefono
                };

                _unitOfWork.EntidadRepository.Insert(entidad);
                _unitOfWork.Save();
                scope.Complete();
                return entidad.IdEntidad;
            }
        }

        /// <summary>
        /// Updates a entidad
        /// </summary>
        /// <param name="entidadId"></param>
        /// <param name="entidadEntity"></param>
        /// <returns></returns>
        public bool UpdateEntidad(int entidadId, BusinessEntities.EntidadEntity entidadEntity)
        {
            var success = false;
            if (entidadEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var entidad = _unitOfWork.EntidadRepository.GetByID(entidadId);
                    if (entidad != null)
                    {
                        entidad.Ciudad = entidadEntity.Ciudad;
                        entidad.Codigo = entidadEntity.Codigo;
                        entidad.CodigoHomologado = entidadEntity.CodigoHomologado;
                        entidad.Departamento = entidadEntity.Departamento;
                        entidad.Descripcion = entidadEntity.Descripcion;
                        entidad.Direccion = entidadEntity.Direccion;
                        entidad.Email = entidadEntity.Email;
                        entidad.ENTIDAD_SERVICIO = entidadEntity.ENTIDAD_SERVICIO;
                        entidad.FechaConstitucion = entidadEntity.FechaConstitucion;
                        entidad.IdCodigoHomologado = entidadEntity.IdCodigoHomologado;
                        entidad.IdEntidad = entidadEntity.IdEntidad;
                        entidad.IdEntidadPadre = entidadEntity.IdEntidadPadre;
                        entidad.IdEstado = entidadEntity.IdEstado;
                        entidad.IdTipoEntidad = entidadEntity.IdTipoEntidad;
                        entidad.IdTipoRelacionEntidadPadre = entidadEntity.IdTipoRelacionEntidadPadre;
                        entidad.NaturalezaJuridica = entidadEntity.NaturalezaJuridica;
                        entidad.NIT = entidadEntity.NIT;
                        entidad.Nombre = entidadEntity.Nombre;
                        entidad.Sigla = entidadEntity.Sigla;
                        entidad.Telefono = entidadEntity.Telefono;

                        _unitOfWork.EntidadRepository.Update(entidad);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular entidad
        /// </summary>
        /// <param name="entidadId"></param>
        /// <returns></returns>
        public bool DeleteEntidad(int entidadId)
        {
            var success = false;
            if (entidadId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var entidad = _unitOfWork.EntidadRepository.GetByID(entidadId);
                    if (entidad != null)
                    {

                        _unitOfWork.EntidadRepository.Delete(entidad);
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
