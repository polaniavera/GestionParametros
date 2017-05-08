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
        private readonly ISectorServices _sectorServicioServices;
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
            ISectorServices sectorServicioServices,
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
            //if (normas != null)
            //{
            //    var normaEntities = normas as List<NormaEntity> ?? normas.ToList();
            //    if (normaEntities.Any())
            //    {
            //        return Request.CreateResponse(HttpStatusCode.OK, normaEntities);
            //    }
            //}
            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "normas not found");
            return Request.CreateResponse(HttpStatusCode.OK, normas);
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

            if (norma != null && norma[0].Equals("0000"))
            {
                NormaEntity normaEnt = (NormaEntity)norma.ElementAt(1);
                var normaSector = _normaSectorServices.GetNormaSectorById(normaEnt.IdNorma).ToList();

                if (normaSector != null && normaSector[0].Equals("0000"))
                {
                    List<NormaSectorEntity> normasSectorEnt = (List<NormaSectorEntity>)normaSector.ElementAt(1);
                    var normaFinal = _tablaValorServices.setDescripcion(normaEnt, normasSectorEnt);

                    //object[] jsonArray = { norma, normaSector };

                    return Request.CreateResponse(HttpStatusCode.OK, normaFinal);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, normaSector);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, norma);
        }

        // POST api/norma/create
        [Route("create")]
        public HttpResponseMessage create([FromBody] NormaEntity normaEntity)
        {
        //    if (normaEntity != null)
        //    {
                var create = _normaServices.CreateNorma(normaEntity);
                return Request.CreateResponse(HttpStatusCode.OK, create);
            //}
            //return 0;
        }

        // DELETE api/norma/delete
        [Route("delete")]
        public HttpResponseMessage delete([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            if (id > 0)
            {
                //si existe relacion de la Norma en la tabla formato no la borra
                var flagObject = _formatoServices.ExistNormaFormato(id);
                if (flagObject[0].Equals("0000"))
                {
                    bool flag = (bool)flagObject.ElementAt(1);

                    if (flag)
                    {
                        object[] resultado2 = { "0000", false };
                        return Request.CreateResponse(HttpStatusCode.NotModified, resultado2);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _normaServices.DeleteNorma(id));
                    }
                        
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, flagObject);
                }
            }
            object[] resultado = { "0000", false };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
        }

        // POST api/norma/inactivate
        [Route("inactivate")]
        public HttpResponseMessage inactivate([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            if (id > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, _normaServices.InactivateNormaRelations(id));
            }
            object[] resultado = { "0000", false };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
        }

        // POST api/norma/activate
        [Route("activate")]
        public HttpResponseMessage activate([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            if (id > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, _normaServices.ActivateNormaRelations(id));
            }
            object[] resultado = { "0000", false };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
        }

        // POST api/norma/createnorma
        [Route("createnorma")]
        public HttpResponseMessage createNorma([FromBody] NormaEntity normaEntity, [FromUri] int[] normaSectorArray)
        {
            var idNormaObject = _normaServices.CreateNorma(normaEntity);
            bool sector = false;

            if (idNormaObject != null && idNormaObject[0].Equals("0000"))
            {
                var idNorma = (int)idNormaObject.ElementAt(1);

                foreach (int idSectorServicio in normaSectorArray)
                {
                    var flagObject = _normaSectorServices.CreateNormaSector(idSectorServicio, idNorma);
                    if (flagObject[0].Equals("0000"))
                    {
                        bool flag = (bool)flagObject.ElementAt(1);

                        if (!flag)
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
                        }
                    }else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, flagObject);
                    }
                            
                }
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, idNormaObject);
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
        public HttpResponseMessage updateNorma([FromBody] NormaEntity normaEntity)
        {
            if (normaEntity == null)
            {
                object[] resultado = { "0000", false };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
            }

            bool deleteSector = false;

            //Busca las normas sectores relacionadas con la norma
            var normaSectorByIdObject = _normaSectorServices.GetNormaSectorById(normaEntity.IdNorma);

            if (normaSectorByIdObject != null && normaSectorByIdObject[0].Equals("0000"))
            {
                var normaSectorById = (List<NormaSectorEntity>) normaSectorByIdObject.ElementAt(1);

                var flagObject = _normaServices.ValidateSector(normaEntity, normaSectorById);
                if (flagObject[0].Equals("0000"))
                {
                    bool flag = (bool)flagObject.ElementAt(1);
                    //si existe relacion del sector en la tabla formato_servicio no la borra
                    if (flag)
                    {
                        foreach (NormaSectorEntity sector in normaSectorById)
                        {
                            //Si no existe relacion borra los sectores directamente de la tabla NormaSector
                            var deleteSectorObject = _normaSectorServices.DeleteNormaSector(sector.IdNormaSector);
                            if (deleteSectorObject[0].Equals("0000"))
                            {
                                bool flagDelete = (bool)deleteSectorObject.ElementAt(1);
                                if (!flagDelete)
                                {
                                    return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, deleteSectorObject);
                            }
                        }
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, flagObject);
                }

                //Reconoce cuando cambia el estado de la norma
                //y actualiza todas sus relaciones al nuevo estado
                var normaEntityObject = _normaServices.changeNormaState(normaEntity);

                if (normaEntityObject != null && normaEntityObject[0].Equals("0000"))
                {
                    normaEntity = (NormaEntity)normaEntityObject.ElementAt(1);
                    var updateObject = _normaServices.UpdateNorma(normaEntity.IdNorma, normaEntity);

                    return Request.CreateResponse(HttpStatusCode.OK, updateObject);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, normaEntityObject);
                }
            }
            else
            {
                object[] resultado = { "0000", false };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, normaSectorByIdObject);
            }
        }

    }
}
