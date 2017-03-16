using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace GestionParametros.Controllers
{
    public class TablaValorController : ApiController
    {
        private readonly ITablaValorServices _tablaValorServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize tablaValor service instance
        /// </summary>
        public TablaValorController(ITablaValorServices tablaValorServices)
        {
            _tablaValorServices = tablaValorServices;
        }

        #endregion

        // GET api/tablaValor
        public HttpResponseMessage Get()
        {
            var tablaValores = _tablaValorServices.GetAllTablaValores();
            if (tablaValores != null)
            {
                var tablaValorEntities = tablaValores as List<TablaValorEntity> ?? tablaValores.ToList();
                if (tablaValorEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, tablaValorEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TablaValores not found");
        }

        // GET api/tablaValor/5
        public HttpResponseMessage Get(int id)
        {
            var tablaValor = _tablaValorServices.GetTablaValorById(id);
            if (tablaValor != null)
                return Request.CreateResponse(HttpStatusCode.OK, tablaValor);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No tablaValor found for this id");
        }

        // POST api/tablaValor
        public int Post([FromBody] TablaValorEntity tablaValorEntity)
        {
            return _tablaValorServices.CreateTablaValor(tablaValorEntity);
        }

        // PUT api/tablaValor/5
        public bool Put(int id, [FromBody]TablaValorEntity tablaValorEntity)
        {
            if (id > 0)
            {
                return _tablaValorServices.UpdateTablaValor(id, tablaValorEntity);
            }
            return false;
        }

        // DELETE api/tablaValor/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _tablaValorServices.DeleteTablaValor(id);
            return false;
        }
    }
}
