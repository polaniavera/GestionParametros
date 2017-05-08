using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessEntities.CustomEntities;

namespace GestionParametros.Controllers
{
    [RoutePrefix("api/formato")]
    public class FormatoController : ApiController
    {
        private readonly IFormatoServices _formatoServices;
        private readonly INormaServices _normaServices;
        private readonly ITablaServices _tablaServices;
        private readonly IPeriodicidadServices _periodicidadServices;
        private readonly IPlazoServices _plazoServices;
        private readonly IFormatoPlantillaServices _formatoPlantillaServices;

        public static string TIPOFORMATO = "TIPOFORMATO";
        public static string SECCION = "SECCION";
        public static string ESTADO = "ESTADO";

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize SectorServicio service instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public FormatoController(IFormatoServices formatoServices,
            INormaServices normaServices,
            ITablaServices tablaServices,
            IPeriodicidadServices periodicidadServices,
            IPlazoServices plazoServices,
            IFormatoPlantillaServices formatoPlantillaServices)
        {
            _formatoServices = formatoServices;
            _normaServices = normaServices;
            _tablaServices = tablaServices;
            _periodicidadServices = periodicidadServices;
            _plazoServices = plazoServices;
            _formatoPlantillaServices = formatoPlantillaServices;
        }

        #endregion

        // GET api/formato/get
        [Route("get")]
        public HttpResponseMessage get()
        {
            var formatos = _formatoServices.GetAllFormatos();
            if (formatos != null && formatos[0].Equals("0000"))
            {
                List<FormatoEntity> formatosList = (List<FormatoEntity>)formatos.ElementAt(1);
                var formatoEntities = formatosList as List<FormatoEntity> ?? formatosList.ToList();
                if (formatoEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, formatoEntities);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, formatos);
        }


        // GET api/formato/getActive
        [Route("getActive")]
        public HttpResponseMessage getActive()
        {
            var formatos = _formatoServices.GetAllFormatosActive();
            if (formatos != null && formatos[0].Equals("0000"))
            {
                List<FormatoEntity> formatosList = (List<FormatoEntity>)formatos.ElementAt(1);
                var formatoEntities = formatosList as List<FormatoEntity> ?? formatosList.ToList();
                if (formatoEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, formatoEntities);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, formatos);
        }

        // GET api/formato/get/5
        [Route("byId")]
        public HttpResponseMessage byId([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            var formato = _formatoServices.GetFormatoById(id);
            if (formato != null && formato[0].Equals("0000"))
            {
                FormatoEntity formatoEnt = (FormatoEntity)formato.ElementAt(1);
                formato = _formatoServices.setDescripcion(formatoEnt);

                return Request.CreateResponse(HttpStatusCode.OK, formato);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, formato);
        }

        // POST api/formato/create
        [Route("create")]
        public HttpResponseMessage create([FromBody] FormatoEntity formatoEntity)
        {
            //if (formatoEntity != null)
            //{
                var create = _formatoServices.CreateFormato(formatoEntity);
                return Request.CreateResponse(HttpStatusCode.OK, create);
            //}
            //return 0;
        }

        // DELETE api/formato/delete/5
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
                var flagObject = _formatoPlantillaServices.ExistPlantilla(id);
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
                        return Request.CreateResponse(HttpStatusCode.OK, _formatoServices.DeleteFormato(id));
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

        // POST api/formato/inactivate/5
        [Route("inactivate")]
        public HttpResponseMessage inactivate([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            bool success = false;
            if (id > 0)
            {
                var flagObject = _formatoServices.InactivateFormatoRelations(id);
                if (flagObject[0].Equals("0000"))
                {
                    bool flag = (bool)flagObject.ElementAt(1);
                    if (flag)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _formatoServices.InactivateFormato(id));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, flagObject);
                }
            }
            object[] resultado = { "0000", success };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
        }

        // POST api/formato/activate/5
        [Route("activate")]
        public HttpResponseMessage activate([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            bool success = false;
            if (id > 0)
            {
                var flagObject = _formatoServices.ActivateFormatoRelations(id);
                if (flagObject[0].Equals("0000"))
                {
                    bool flag = (bool)flagObject.ElementAt(1);
                    if (flag)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, _formatoServices.ActivateFormato(id));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, flagObject);
                }

            }
            object[] resultado = { "0000", success };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
        }

        // GET api/formato/nueva
        [Route("nueva")]
        public HttpResponseMessage getNueva()
        {
            //Normatividad Padre
            var normas = _normaServices.GetNormasPadre();
            //Tipo Formato
            var tipoFormato = _tablaServices.GetParametrosVert(TIPOFORMATO); 
             //Tipo periodicidad
             var tipoPeriodicidad = _periodicidadServices.GetAllPeriodicidades();
            //Tipo plazo
            var tipoPlazo = _plazoServices.GetAllPlazos();
            //Lista Sección
            var seccionList = _tablaServices.GetParametrosVert(SECCION);
            //Lista Sección
            var estadoList = _tablaServices.GetParametrosVert(ESTADO);

            if (normas != null && tipoFormato != null && tipoPeriodicidad != null && tipoPlazo != null && seccionList != null && estadoList != null)
            {
                //var normaEntities = normas as List<NormaPadreEntity> ?? normas.ToList();               
                //var sectorServicioEntities = sectorList as List<SectorServicioEntity> ?? sectorList.ToList();
                //var entidadEntities = entidadList as List<EntidadEntity> ?? entidadList.ToList();

                object[] jsonArray = {normas, tipoFormato, tipoPeriodicidad, tipoPlazo, seccionList, estadoList};

                return Request.CreateResponse(HttpStatusCode.OK, jsonArray);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }

        // POST     
        [Route("editar")]
        public HttpResponseMessage editarFormato([FromBody] IdEntity entity)
        {
            int id = 0;

            if (entity != null)
            {
                id = entity.Id;
            }

            //Formato a editar
            var formatoEditar = _formatoServices.GetFormatoById(id);
            //Normatividad Padre
            var normas = _normaServices.GetNormasPadre();
            //Tipo Formato
            var tipoFormato = _tablaServices.GetParametrosVert(TIPOFORMATO);
            //Tipo periodicidad
            var tipoPeriodicidad = _periodicidadServices.GetAllPeriodicidades();
            //Tipo plazo
            var tipoPlazo = _plazoServices.GetAllPlazos();
            //Lista Sección
            var seccionList = _tablaServices.GetParametrosVert(SECCION);
            //Lista estado
            var estadoList = _tablaServices.GetParametrosVert(ESTADO);

            if (formatoEditar != null && normas != null && tipoFormato != null && tipoPeriodicidad != null && tipoPlazo != null && seccionList != null && estadoList != null)
            {
                //var normaEntities = normas as List<NormaPadreEntity> ?? normas.ToList();               
                //var sectorServicioEntities = sectorList as List<SectorServicioEntity> ?? sectorList.ToList();
                //var entidadEntities = entidadList as List<EntidadEntity> ?? entidadList.ToList();

                object[] jsonArray = { formatoEditar, normas, tipoFormato, tipoPeriodicidad, tipoPlazo, seccionList, estadoList };

                return Request.CreateResponse(HttpStatusCode.OK, jsonArray);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }

        // PUT api/formato/update
        [Route("update")]
        public HttpResponseMessage update([FromBody]FormatoEntity formatoEntity)
        {
            bool success = false;
            if (formatoEntity != null)
            {
                //Reconoce cuando cambia el estado del formato
                //y actualiza todas sus relaciones al nuevo estado
                var flagObject = _formatoServices.changeFormatoState(formatoEntity);
                if (flagObject[0].Equals("0000"))
                {
                    bool flag = (bool)flagObject.ElementAt(1);
                    if (flag)
                    {
                        //Actualiza el formato como tal
                        return Request.CreateResponse(HttpStatusCode.OK, _formatoServices.UpdateFormato(formatoEntity.IdFormato, formatoEntity));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, flagObject);
                }
            }
            object[] resultado = { "0000", success };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, resultado);
        }

    }
}