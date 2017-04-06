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
        private readonly ITablaValorServices _tablaValorServices;
        private readonly IFormatoServices _formatoServices;
        private readonly IFormatoServicioServices _formatoServicioServices;

        public static string TIPONORMA = "TIPONORMA";
        public static string SECCION = "SECCION";
        public static string ESTADO = "ESTADO";

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize SectorServicio service instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public NormaController(INormaServices normaServices, 
            INormaSectorServices normaSectorServices, 
            ISectorServicioServices sectorServicioServices,
            IEntidadServices entidadServices,
            ITablaServices tablaServices,
            ITablaValorServices tablaValorServices,
            IFormatoServices formatoServices,
            IFormatoServicioServices formatoServicioServices)
        {
            _normaServices = normaServices;
            _normaSectorServices = normaSectorServices;
            _sectorServicioServices = sectorServicioServices;
            _entidadServices = entidadServices;
            _tablaServices = tablaServices;
            _tablaValorServices = tablaValorServices;
            _formatoServices = formatoServices;
            _formatoServicioServices = formatoServicioServices;
        }

        #endregion

        // GET api/norma/get
        [Route("get")]
        public HttpResponseMessage get()
        {
            var normas = _normaServices.GetAllNormas();
            if (normas != null)
            {
                normas = _tablaValorServices.setDescripcionList(normas);

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

                var normaSector = _normaSectorServices.GetNormaSectorById(norma.IdNorma).ToList();
                norma = _tablaValorServices.setDescripcion(norma, normaSector);

                //object[] jsonArray = { norma, normaSector };

                return Request.CreateResponse(HttpStatusCode.OK, norma);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }

        // POST api/norma/create
        [Route("create")]
        public int create([FromBody] NormaEntity normaEntity)
        {
            if (normaEntity != null)
            {
                return _normaServices.CreateNorma(normaEntity);
            }
            return 0;
        }

        // PUT api/norma/update/5
        //[Route("update/{id:int}")]
        //public bool update(int id, [FromBody]NormaEntity normaEntity)
        //{
        //    if (id > 0)
        //    {
        //        return _normaServices.UpdateNorma(id, normaEntity);
        //    }
        //    return false;
        //}

        // DELETE api/norma/delete/5
        public bool delete(int id)
        {
            if (id > 0)
            { 
                //si existe relacion de la Norma en la tabla formato no la borra
                if (_formatoServices.ExistNormaFormato(id))
                    return false;
                else
                    return _normaServices.DeleteNorma(id);
            }              
            return false;
        }

        // POST api/norma/inactivate/5
        [Route("inactivate/{id:int}")]
        public bool inactivate(int id)
        {
            if (id > 0)
            {
                return _normaServices.InactivateNormaRelations(id);
                //if (relations)
                //{
                //    return _normaServices.InactivateNorma(id);
                //}
            }
            return false;
        }

        // POST api/norma/activate/5
        [Route("activate/{id:int}")]
        public bool activate(int id)
        {
            if (id > 0)
            {
                return _normaServices.ActivateNormaRelations(id);
                //if (relations)
                //{
                //    return _normaServices.ActivateNorma(id);
                //}
            }
            return false;
        }

        // POST api/norma/createnorma
        [Route("createnorma")]
        public bool createNorma([FromBody] NormaEntity normaEntity, [FromUri] int[] normaSectorArray)
        {
            var idNorma = _normaServices.CreateNorma(normaEntity);
            bool sector = false;

            foreach (int idSectorServicio in normaSectorArray)
            {
                sector=_normaSectorServices.CreateNormaSector(idSectorServicio, idNorma);
                if (!sector)
                    return false;
            }
            return true;
        }

        // GET api/norma/nueva
        [Route("nueva")]
        public HttpResponseMessage getNueva()
        {
            //Lista Tipo Norma
            var tipoNormaList = _tablaServices.GetParametrosVert(TIPONORMA);
            //Normatividad Padre
            var normas = _normaServices.GetNormasPadre();
            //Lista Entidades
            var entidadList = _entidadServices.GetAllEntidades();
            //Lista Sectores
            var sectorList = _sectorServicioServices.GetAllSectorServicios();
            //Lista Sección
            var estadoList = _tablaServices.GetParametrosVert(ESTADO);

            if (tipoNormaList != null && normas != null && entidadList != null && sectorList != null && estadoList != null)
            {
                //var normaEntities = normas as List<NormaPadreEntity> ?? normas.ToList();               
                //var sectorServicioEntities = sectorList as List<SectorServicioEntity> ?? sectorList.ToList();
                //var entidadEntities = entidadList as List<EntidadEntity> ?? entidadList.ToList();
                
                object[] jsonArray = {tipoNormaList, normas, entidadList, sectorList, estadoList};

                return Request.CreateResponse(HttpStatusCode.OK, jsonArray);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }

        // POST api/norma/editar/2
        [Route("editar/{idNorma:int}")]
        public HttpResponseMessage editarNorma(int idNorma)
        {
            //Normatividad Padre
            var normas = _normaServices.GetNormasPadre();
            //Lista Sectores
            var sectorList = _sectorServicioServices.GetAllSectorServicios();
            //Lista Entidades
            var entidadList = _entidadServices.GetAllEntidades();
            //Lista Tipo Norma
            var tipoNormaList = _tablaServices.GetParametrosVert(TIPONORMA);
            //Norma a editar
            var normaEditar = _normaServices.GetNormaById(idNorma);
            //Lista de sectores de la norma a editar
            var normaSector = _normaSectorServices.GetNormaSectorById(idNorma);

            if (normas != null && sectorList != null && entidadList != null && tipoNormaList != null && normaEditar != null && normaSector != null)
            {
                //var normaEntities = normas as List<NormaPadreEntity> ?? normas.ToList();               
                //var sectorServicioEntities = sectorList as List<SectorServicioEntity> ?? sectorList.ToList();
                //var entidadEntities = entidadList as List<EntidadEntity> ?? entidadList.ToList();

                object[] jsonArray = { normas, sectorList, entidadList, tipoNormaList, normaEditar, normaSector };

                return Request.CreateResponse(HttpStatusCode.OK, jsonArray);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }


        // POST  api/norma/update
        [Route("update")]
        public bool updateNorma([FromBody] NormaEntity normaEntity)
        {
            if (normaEntity == null)
            {
                return false;
            }

            bool deleteSector = false;

            //Busca las normas sectores relacionadas con la norma
            var normaSectorById = _normaSectorServices.GetNormaSectorById(normaEntity.IdNorma);

            //si existe relacion del sector en la tabla formato_servicio no la borra
            if (_normaServices.ValidateSector(normaEntity, normaSectorById))
            {
                foreach (NormaSectorEntity sector in normaSectorById)
                {
                    //Si no existe relacion borra los sectores directamente de la tabla NormaSector
                    deleteSector = _normaSectorServices.DeleteNormaSector(sector.IdNormaSector);
                    if (!deleteSector)
                        return false;
                }
            }

            //Reconoce cuando cambia el estado de la norma
            //y actualiza todas sus relaciones al nuevo estado
            normaEntity = _normaServices.changeNormaState(normaEntity);

            return _normaServices.UpdateNorma(normaEntity.IdNorma, normaEntity);
        }

    }
}
