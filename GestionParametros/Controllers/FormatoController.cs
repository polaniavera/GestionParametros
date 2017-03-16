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

        public static string TIPOFORMATO = "TIPOFORMATO";
        public static string SECCION = "SECCION";

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
            IPlazoServices plazoServices)
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
                //formato = _tablaValorServices.setDescripcion(formato);

                return Request.CreateResponse(HttpStatusCode.OK, formato);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No formato found for this id");
        }

        // POST api/formato/create
        [Route("create")]
        public int create([FromBody] FormatoEntity formatoEntity)
        {
            return _formatoServices.CreateFormato(formatoEntity);
        }

        // PUT api/formato/update/5
        [Route("update/{id:int}")]
        public bool update(int id, [FromBody]FormatoEntity formatoEntity)
        {
            if (id > 0)
            {
                return _formatoServices.UpdateFormato(id, formatoEntity);
            }
            return false;
        }

        // DELETE api/formato/delete/5
        public bool delete(int id)
        {
            if (id > 0)
                return _formatoServices.DeleteFormato(id);
            return false;
        }

        // POST api/formato/inactivate/5
        [Route("inactivate/{id:int}")]
        public bool inactivate(int id)
        {
            if (id > 0)
            {
                return _formatoServices.InactivateFormato(id);
            }
            return false;
        }

        // POST api/formato/createformato
        [Route("createformato")]
        public bool createFormato([FromBody] FormatoEntity formatoEntity, [FromUri] int[] formatoArray)
        {
            var idFormato = _formatoServices.CreateFormato(formatoEntity);
            bool sector = false;

            foreach (int idSectorServicio in formatoArray)
            {
                //sector = _normaSectorServices.CreateNormaSector(idSectorServicio, idNorma);
                //if (!sector)
                //    return false;
            }
            return true;
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

                object[] jsonArray = { normas, sectorList, entidadList, tipoNormaList };

                return Request.CreateResponse(HttpStatusCode.OK, jsonArray);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No norma found for this id");
        }

        // POST api/norma/editar/save
        //[Route("editar/save")]
        //public bool editarNorma([FromBody] NormaEntity normaEntity)
        //{
        //    //var normaFlag = _normaServices.UpdateNorma(normaEntity.IdNorma, normaEntity);

        //    var normaSectorById = _normaSectorServices.GetNormaSectorById(normaEntity.IdNorma);
        //    normaEntity = _normaSectorServices.EditNormaSector(normaEntity, normaSectorById);

        //    return _normaServices.UpdateNorma(normaEntity.IdNorma, normaEntity); ;
        //}































        //    private readonly IFormatoServices _formatoServices;

        //    #region Public Constructor

        //    /// <summary>
        //    /// Public constructor to initialize Formato service instance
        //    /// </summary>
        //    public FormatoController(IFormatoServices formatoServices)
        //    {
        //        _formatoServices = formatoServices;
        //    }

        //    #endregion

        //    // GET api/formato
        //    public HttpResponseMessage Get()
        //    {
        //        var formatos = _formatoServices.GetAllFormatos();
        //        if (formatos != null)
        //        {
        //            var formatoEntities = formatos as List<FormatoEntity> ?? formatos.ToList();
        //            if (formatoEntities.Any())
        //                return Request.CreateResponse(HttpStatusCode.OK, formatoEntities);
        //        }
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Formatos not found");
        //    }

        //    // GET api/formato/5
        //    public HttpResponseMessage Get(int id)
        //    {
        //        var formato = _formatoServices.GetFormatoById(id);
        //        if (formato != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, formato);
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No formato found for this id");
        //    }

        //    // POST api/formato
        //    public int Post([FromBody] FormatoEntity formatoEntity)
        //    {
        //        return _formatoServices.CreateFormato(formatoEntity);
        //    }

        //    // PUT api/formato/5
        //    public bool Put(int id, [FromBody]FormatoEntity formatoEntity)
        //    {
        //        if (id > 0)
        //        {
        //            return _formatoServices.UpdateFormato(id, formatoEntity);
        //        }
        //        return false;
        //    }

        //    // DELETE api/formato/5
        //    public bool Delete(int id)
        //    {
        //        if (id > 0)
        //            return _formatoServices.DeleteFormato(id);
        //        return false;
        //    }
        //}
    }
}