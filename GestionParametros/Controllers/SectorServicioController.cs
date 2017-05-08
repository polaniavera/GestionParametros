using BusinessEntities;
using BusinessServices;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionParametros.Controllers
{
    [RoutePrefix("api/sectorservicio")]
    public class SectorServicioController : ApiController
    {
        private readonly ISectorServices _sectorServicioServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize SectorServicio service instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public SectorServicioController(ISectorServices sectorServicioServices)
        {
            _sectorServicioServices = sectorServicioServices;
        }

        #endregion

        // GET api/sectorservicio/get/true
        [Route("get/{tipo:bool}")]
        public HttpResponseMessage Get(bool tipo)
        {
            var sectorServicios = _sectorServicioServices.GetAllSectorServicios();
            if (sectorServicios != null)
            {
                var sectorServicioEntities = sectorServicios as List<SectorEntity> ?? sectorServicios.ToList();
                if (sectorServicioEntities.Any())
                {
                    if (tipo)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, sectorServicioEntities);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, sectorServicioEntities, Configuration.Formatters.XmlFormatter);
                    }
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "sectorServicios not found");
        }

        // GET api/sectorservicio/get/5/true
        [Route("get/{id:int}/{tipo:bool}")]
        public HttpResponseMessage Get(int id, bool tipo)
        {
            var sectorServicio = _sectorServicioServices.GetSectorServicioById(id);
            if (sectorServicio != null)
            {
                if (tipo)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, sectorServicio);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, sectorServicio, Configuration.Formatters.XmlFormatter);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No sectorServicio found for this id");
        }

        // POST api/sectorservicio/create
        [Route("create")]
        public int Post([FromBody] SectorEntity sectorServicioEntity)
        {
            return _sectorServicioServices.CreateSectorServicio(sectorServicioEntity);
        }

        // PUT api/sectorservicio/update/5
        [Route("update/{id:int}")]
        public bool Put(int id, [FromBody]SectorEntity sectorServicioEntity)
        {
            if (id > 0)
            {
                return _sectorServicioServices.UpdateSectorServicio(id, sectorServicioEntity);
            }
            return false;
        }

        // DELETE api/sectorservicio/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _sectorServicioServices.DeleteSectorServicio(id);
            return false;
        }

        // PUT api/sectorservicio/inactivate/5
        [Route("inactivate/{id:int}")]
        public bool Put(int id)
        {
            if (id > 0)
            {
                return _sectorServicioServices.InactivateSectorServicio(id);
            }
            return false;
        }

    }
}
