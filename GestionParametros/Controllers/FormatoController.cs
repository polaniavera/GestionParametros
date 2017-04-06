using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace GestionParametros.Controllers
{
    [RoutePrefix("api/formato")]
    public class FormatoController : ApiController
    {
        private readonly IFormatoServices _formatoServices;
        private readonly INormaServices _normaServices;
        private readonly INormaSectorServices _normaSectorServices;
        private readonly ISectorServicioServices _sectorServicioServices;
        private readonly IEntidadServices _entidadServices;
        private readonly ITablaServices _tablaServices;
        private readonly ITablaValorServices _tablaValorServices;
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
            INormaSectorServices normaSectorServices,
            ISectorServicioServices sectorServicioServices,
            IEntidadServices entidadServices,
            ITablaServices tablaServices,
            ITablaValorServices tablaValorServices,
            IPeriodicidadServices periodicidadServices,
            IPlazoServices plazoServices,
            IFormatoPlantillaServices formatoPlantillaServices)
        {
            _formatoServices = formatoServices;
            _normaServices = normaServices;
            _normaSectorServices = normaSectorServices;
            _sectorServicioServices = sectorServicioServices;
            _entidadServices = entidadServices;
            _tablaServices = tablaServices;
            _tablaValorServices = tablaValorServices;
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
            if (formatos != null)
            {
                //formatos = _tablaValorServices.setDescripcionList(formatos);
                //normas = _entidadServices.setDescripcionList(normas);

                var formatoEntities = formatos as List<FormatoEntity> ?? formatos.ToList();
                if (formatoEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, formatoEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Formatos not found");
        }


        // GET api/formato/getActive
        [Route("getActive")]
        public HttpResponseMessage getActive()
        {
            var formatos = _formatoServices.GetAllFormatosActive();
            if (formatos != null)
            {
                var formatoEntities = formatos as List<FormatoEntity> ?? formatos.ToList();
                if (formatoEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, formatoEntities);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "formatos not found");
        }

        // GET api/formato/get/5
        [Route("get/{id:int}")]
        public HttpResponseMessage get(int id)
        {
            var formato = _formatoServices.GetFormatoById(id);
            if (formato != null)
            {
                formato = _formatoServices.setDescripcion(formato);

                return Request.CreateResponse(HttpStatusCode.OK, formato);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No formato found for this id");
        }

        // POST api/formato/create
        [Route("create")]
        public int create([FromBody] FormatoEntity formatoEntity)
        {
            if (formatoEntity != null)
            {
                return _formatoServices.CreateFormato(formatoEntity);
            }
            return 0;
        }

        // DELETE api/formato/delete/5
        [Route("delete/{id:int}")]
        public bool delete(int id)
        {
            if (id > 0)
                if (_formatoPlantillaServices.ExistPlantilla(id))
                    return false;
                else
                    return _formatoServices.DeleteFormato(id);
            return false;
        }

        // POST api/formato/inactivate/5
        [Route("inactivate/{id:int}")]
        public bool inactivate(int id)
        {
            bool success = false;
            if (id > 0)
            {
                success = _formatoServices.InactivateFormatoRelations(id);
                if (success)
                {
                    success = _formatoServices.InactivateFormato(id);
                }
            }
            return success;
        }

        // POST api/formato/activate/5
        [Route("activate/{id:int}")]
        public bool activate(int id)
        {
            bool success = false;
            if (id > 0)
            {
                success = _formatoServices.ActivateFormatoRelations(id);
                if (success)
                {
                    success = _formatoServices.ActivateFormato(id);
                }
            }
            return success;
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

        // POST api/formato/editar/2
        [Route("editar/{idFormato:int}")]
        public HttpResponseMessage editarFormato(int idFormato)
        {
            //Formato a editar
            var formatoEditar = _formatoServices.GetFormatoById(idFormato);
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
        public bool update([FromBody]FormatoEntity formatoEntity)
        {
            bool success = false;
            if (formatoEntity != null)
            {
                //Reconoce cuando cambia el estado del formato
                //y actualiza todas sus relaciones al nuevo estado
                success = _formatoServices.changeFormatoState(formatoEntity);

                if (success)
                {
                    //Actualiza el formato como tal
                    success = _formatoServices.UpdateFormato(formatoEntity.IdFormato, formatoEntity);
                }
            }
            return success;
        }

    }
}