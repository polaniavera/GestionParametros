using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace GestionParametros.Controllers
{
    public class TablaCampoController : ApiController
    {
        private readonly ITablaCampoServices _tablaCampoServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize TablaCampo service instance
        /// </summary>
        public TablaCampoController()
        {
            _tablaCampoServices = new TablaCampoServices();
        }

        #endregion

        // GET api/tablaCampo
        public HttpResponseMessage Get()
        {
            var tablaCampos = _tablaCampoServices.GetAllTablaCampos();
            if (tablaCampos != null)
            {
                var tablaCampoEntities = tablaCampos as List<TablaCampoEntity> ?? tablaCampos.ToList();
                if (tablaCampoEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, tablaCampoEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TablaCampos not found");
        }

        // GET api/tablaCampo/5
        public HttpResponseMessage Get(int id)
        {
            var tablaCampo = _tablaCampoServices.GetTablaCampoById(id);
            if (tablaCampo != null)
                return Request.CreateResponse(HttpStatusCode.OK, tablaCampo);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No tablaCampo found for this id");
        }

        // POST api/tablaCampo
        public int Post([FromBody] TablaCampoEntity tablaCampoEntity)
        {
            return _tablaCampoServices.CreateTablaCampo(tablaCampoEntity);
        }

        // PUT api/tablaCampo/5
        public bool Put(int id, [FromBody]TablaCampoEntity tablaCampoEntity)
        {
            if (id > 0)
            {
                return _tablaCampoServices.UpdateTablaCampo(id, tablaCampoEntity);
            }
            return false;
        }

        // DELETE api/tablaCampo/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _tablaCampoServices.DeleteTablaCampo(id);
            return false;
        }
    }
}
