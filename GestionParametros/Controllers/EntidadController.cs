using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace GestionParametros.Controllers
{
    public class EntidadController : ApiController
    {
        private readonly IEntidadServices _entidadServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize Entidad service instance
        /// </summary>
        public EntidadController(IEntidadServices entidadServices)
        {
            _entidadServices = entidadServices;
        }

        #endregion

        // GET api/entidad
        public HttpResponseMessage Get(bool tipo)
        {
            var entidades = _entidadServices.GetAllEntidades();
            if (entidades != null)
            {
                var entidadEntities = entidades as List<EntidadEntity> ?? entidades.ToList();
                if (entidadEntities.Any())
                {
                    if (tipo)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entidadEntities);
                    } else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entidadEntities, Configuration.Formatters.XmlFormatter);
                    }
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Entidades not found");
        }

        // GET api/entidad/5
        public HttpResponseMessage Get(int id, bool tipo)
        {
            var entidad = _entidadServices.GetEntidadById(id);
            if (entidad != null)
            {
                if (tipo)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entidad);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entidad, Configuration.Formatters.XmlFormatter);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entidad found for this id");
        }

        // POST api/entidad
        public int Post([FromBody] EntidadEntity entidadEntity)
        {
            return _entidadServices.CreateEntidad(entidadEntity);
        }

        // PUT api/entidad/5
        public bool Put(int id, [FromBody]EntidadEntity entidadEntity)
        {
            if (id > 0)
            {
                return _entidadServices.UpdateEntidad(id, entidadEntity);
            }
            return false;
        }

        // DELETE api/entidad/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _entidadServices.DeleteEntidad(id);
            return false;
        }
    }
}
