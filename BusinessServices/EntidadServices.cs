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
                    Descripcion = entidadEntity.Descripcion,
                    Direccion = entidadEntity.Direccion,
                    Email = entidadEntity.Email,
                    FechaConstitucion = entidadEntity.FechaConstitucion,
                    IdEntidad = entidadEntity.IdEntidad,
                    IdEntidadPadre = entidadEntity.IdEntidadPadre,
                    IdEstado = entidadEntity.IdEstado,
                    IdTipoEntidad = entidadEntity.IdTipoEntidad,
                    IdTipoRelacionEntidadPadre = entidadEntity.IdTipoRelacionEntidadPadre,
                    NaturalezaJuridica = entidadEntity.NaturalezaJuridica,
                    Nombre = entidadEntity.Nombre,
                    Sigla = entidadEntity.Sigla,
                    NumeroDocumento = entidadEntity.NumeroDocumento,
                    IdTipoDocumento = entidadEntity.IdTipoDocumento,
                    Telefono = entidadEntity.Telefono,
                    IdTipoCodigoHomologado = entidadEntity.IdTipoCodigoHomologado,
                    IdTransmite = entidadEntity.IdTransmite,
                    NombreContacto = entidadEntity.NombreContacto
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
                        entidad.Descripcion = entidadEntity.Descripcion;
                        entidad.Direccion = entidadEntity.Direccion;
                        entidad.Email = entidadEntity.Email;
                        entidad.FechaConstitucion = entidadEntity.FechaConstitucion;
                        entidad.IdEntidad = entidadEntity.IdEntidad;
                        entidad.IdEntidadPadre = entidadEntity.IdEntidadPadre;
                        entidad.IdEstado = entidadEntity.IdEstado;
                        entidad.IdTipoEntidad = entidadEntity.IdTipoEntidad;
                        entidad.IdTipoRelacionEntidadPadre = entidadEntity.IdTipoRelacionEntidadPadre;
                        entidad.NaturalezaJuridica = entidadEntity.NaturalezaJuridica;
                        entidad.Nombre = entidadEntity.Nombre;
                        entidad.Sigla = entidadEntity.Sigla;
                        entidad.NumeroDocumento = entidadEntity.NumeroDocumento;
                        entidad.IdTipoDocumento = entidadEntity.IdTipoDocumento;
                        entidad.Telefono = entidadEntity.Telefono;
                        entidad.IdTipoCodigoHomologado = entidadEntity.IdTipoCodigoHomologado;
                        entidad.IdTransmite = entidadEntity.IdTransmite;
                        entidad.NombreContacto = entidadEntity.NombreContacto;

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

        public IEnumerable<NormaEntity> setDescripcionList(IEnumerable<NormaEntity> normas)
        {
            foreach (NormaEntity norma in normas)
            {
                var valor = GetEntidadById(norma.IdEntidadEmite);
                if (valor!=null)
                    norma.DescripcionEntidadEmite = valor.Nombre;

            }

            return normas;
        }
    }
}
