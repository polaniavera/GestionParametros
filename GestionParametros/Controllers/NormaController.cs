using BusinessEntities;
using BusinessServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionParametros.Controllers
{
    [RoutePrefix("api/norma")]
    public class NormaController : ApiController
    {
        private readonly INormaServices _normaServices;
        private readonly INormaSectorServices _normaSectorServices;
        private readonly ISectorServicioServices _sectorServicioServices;
        private readonly IEntidadServices _entidadServices;
        private readonly ITablaServices _tablaServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize SectorServicio service instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public NormaController(INormaServices normaServices, 
            INormaSectorServices normaSectorServices, 
            ISectorServicioServices sectorServicioServices,
            IEntidadServices entidadServices,
            ITablaServices tablaServices)
        {
            _normaServices = normaServices;
            _normaSectorServices = normaSectorServices;
            _sectorServicioServices = sectorServicioServices;
            _entidadServices = entidadServices;
            _tablaServices = tablaServices;
        }

        #endregion

        // GET api/norma/get
        [Route("get")]
        public HttpResponseMessage get()
        {
            var normas = _normaServices.GetAllNormas();
            if (normas != null)
            {
                var normaEntities = normas as List<NormaEntity> ?? normas.ToList();
                if (normaEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, normaEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Formatos not found");
        }


        // GET api/norma/getActive
        [Route("getActive")]
        public HttpResponseMessage getActive()
        {
            var normas =_normaServices.GetAllNormasActive();
            if (normas != null)
            {
                var normaEntities = normas as List<NormaEntity> ?? normas.ToList();
                if (normaEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, normaEntities);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "normas not found");
        }

        // GET api/norma/get/5
        [Route("get/{id:int}")]
        public HttpResponseMessage get(int id)
        {
            var norma = _normaServices.GetNormaById(id);
            if (norma != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, norma);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }

        // POST api/norma/create
        [Route("create")]
        public int create([FromBody] NormaEntity normaEntity)
        {
            return _normaServices.CreateNorma(normaEntity);
        }

        // PUT api/norma/update/5
        [Route("update/{id:int}")]
        public bool update(int id, [FromBody]NormaEntity normaEntity)
        {
            if (id > 0)
            {
                return _normaServices.UpdateNorma(id, normaEntity);
            }
            return false;
        }

        // DELETE api/norma/delete/5
        public bool delete(int id)
        {
            if (id > 0)
                return _normaServices.DeleteNorma(id);
            return false;
        }

        // POST api/norma/inactivate/5
        [Route("inactivate/{id:int}")]
        public bool inactivate(int id)
        {
            if (id > 0)
            {
                return _normaServices.InactivateNorma(id);
            }
            return false;
        }

        // POST api/norma/createnorma
        [Route("createnorma")]
        public int createNorma([FromBody] NormaEntity normaEntity, [FromUri] int[] normaSectorArray)
        {
            var idNorma = _normaServices.CreateNorma(normaEntity);
            foreach (int idSectorServicio in normaSectorArray)
            {
                _normaSectorServices.CreateNormaSector(idSectorServicio, idNorma);
            }
            return 1;
        }

        // GET api/norma/nueva
        [Route("nueva")]
        public HttpResponseMessage getNueva()
        {
            //Normatividad Padre
            var normas = _normaServices.GetNormasPadre();
            //Lista Sectores
            var sectorList = _sectorServicioServices.GetAllSectorServicios();
            //Lista Entidades
            var entidadList = _entidadServices.GetAllEntidades();
            //Lista Tipo Norma
            var tipoNormaList = _tablaServices.GetTipoNorma();

            if (normas != null && sectorList != null && entidadList != null && tipoNormaList != null)
            {
                //var normaEntities = normas as List<NormaPadreEntity> ?? normas.ToList();               
                //var sectorServicioEntities = sectorList as List<SectorServicioEntity> ?? sectorList.ToList();
                //var entidadEntities = entidadList as List<EntidadEntity> ?? entidadList.ToList();
                
                object[] jsonArray = {normas, sectorList, entidadList, tipoNormaList};

                //if (normaEntities.Any())
                //{
                    return Request.CreateResponse(HttpStatusCode.OK, jsonArray);
                //}
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }
    }
}
