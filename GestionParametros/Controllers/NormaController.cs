using BusinessEntities;
using BusinessEntities.CustomEntities;
using BusinessServices;
using System;
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
            IFormatoServices formatoServices)
        {
            _normaServices = normaServices;
            _normaSectorServices = normaSectorServices;
            _sectorServicioServices = sectorServicioServices;
            _entidadServices = entidadServices;
            _tablaServices = tablaServices;
            _tablaValorServices = tablaValorServices;
            _formatoServices = formatoServices;
        }

        #endregion

        // GET api/norma/get
        [Route("get")]
        public HttpResponseMessage get()
        {
            var normas = _normaServices.GetAllNormas();
            if (normas != null && normas[0].Equals("0000"))
            {
                List<NormaEntity> normasList = (List<NormaEntity>) normas.ElementAt(1);
                normas = _tablaValorServices.setDescripcionList(normasList);

                if (normas != null && normas[0].Equals("0000"))
                {
                    //var normaEntities = normas as List<NormaEntity> ?? normas.ToList();
                    //if (normaEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, normas);
                }

            }
            return Request.CreateResponse(HttpStatusCode.NotFound, normas);
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

        // GET api/norma/byId
        [Route("byId")]
        public HttpResponseMessage byId([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

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

        // DELETE api/norma/delete
        [Route("delete")]
        public bool delete([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

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

        // POST api/norma/inactivate
        [Route("inactivate")]
        public bool inactivate([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            if (id > 0)
            {
                return _normaServices.InactivateNormaRelations(id);
            }
            return false;
        }

        // POST api/norma/activate
        [Route("activate")]
        public bool activate([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            if (id > 0)
            {
                return _normaServices.ActivateNormaRelations(id);
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

        // POST api/norma/editar
        [Route("editar")]
        public HttpResponseMessage editarNorma([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            //Normatividad Padre
            var normas = _normaServices.GetNormasPadre();
            //Lista Sectores
            var sectorList = _sectorServicioServices.GetAllSectorServicios();
            //Lista Entidades
            var entidadList = _entidadServices.GetAllEntidades();
            //Lista Tipo Norma
            var tipoNormaList = _tablaServices.GetParametrosVert(TIPONORMA);
            //Norma a editar
            var normaEditar = _normaServices.GetNormaById(id);
            //Lista de sectores de la norma a editar
            var normaSector = _normaSectorServices.GetNormaSectorById(id);

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
