using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace GestionParametros.Controllers
{
    public class FormatoController : ApiController
    {
        private readonly IFormatoServices _formatoServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize Formato service instance
        /// </summary>
        public FormatoController(IFormatoServices formatoServices)
        {
            _formatoServices = formatoServices;
        }

        #endregion

        // GET api/formato
        public HttpResponseMessage Get()
        {
            var formatos = _formatoServices.GetAllFormatos();
            if (formatos != null)
            {
                var formatoEntities = formatos as List<FormatoEntity> ?? formatos.ToList();
                if (formatoEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, formatoEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Formatos not found");
        }

        // GET api/formato/5
        public HttpResponseMessage Get(int id)
        {
            var formato = _formatoServices.GetFormatoById(id);
            if (formato != null)
                return Request.CreateResponse(HttpStatusCode.OK, formato);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No formato found for this id");
        }

        // POST api/formato
        public int Post([FromBody] FormatoEntity formatoEntity)
        {
            return _formatoServices.CreateFormato(formatoEntity);
        }

        // PUT api/formato/5
        public bool Put(int id, [FromBody]FormatoEntity formatoEntity)
        {
            if (id > 0)
            {
                return _formatoServices.UpdateFormato(id, formatoEntity);
            }
            return false;
        }

        // DELETE api/formato/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _formatoServices.DeleteFormato(id);
            return false;
        }
    }
}