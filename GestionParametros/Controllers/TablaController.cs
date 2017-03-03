using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace GestionParametros.Controllers
{
    public class TablaController : ApiController
    {
        private readonly ITablaServices _tablaServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize tabla service instance
        /// </summary>
        public TablaController()
        {
            _tablaServices = new TablaServices();
        }

        #endregion

        // GET api/tabla
        public HttpResponseMessage Get()
        {
            var tablas = _tablaServices.GetAllTablas();
            if (tablas != null)
            {
                var tablaEntities = tablas as List<TablaEntity> ?? tablas.ToList();
                if (tablaEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, tablaEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tablas not found");
        }

        // GET api/tabla/5
        public HttpResponseMessage Get(int id)
        {
            var tabla = _tablaServices.GetTablaById(id);
            if (tabla != null)
                return Request.CreateResponse(HttpStatusCode.OK, tabla);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No tabla found for this id");
        }

        // POST api/tabla
        public int Post([FromBody] TablaEntity tablaEntity)
        {
            return _tablaServices.CreateTabla(tablaEntity);
        }

        // PUT api/tabla/5
        public bool Put(int id, [FromBody]TablaEntity tablaEntity)
        {
            if (id > 0)
            {
                return _tablaServices.UpdateTabla(id, tablaEntity);
            }
            return false;
        }

        // DELETE api/tabla/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _tablaServices.DeleteTabla(id);
            return false;
        }
    }
}