using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.GenericRepository
{
    public class FormatoServicioRepositoryCustom
    {
        #region Private member variables...
        internal WebApiDbEntities Context;
        internal DbSet<FORMATO_SERVICIO> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public FormatoServicioRepositoryCustom(WebApiDbEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<FORMATO_SERVICIO>();
        }
        #endregion

        #region Public member methods..

        /// <summary>
        /// Retrieve if exist a servicio in formato_servicio entity by IdSector
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public bool ExistServicio(int sectorId)
        {
            int count = 0;

            var categorizedProducts =
                from ns in Context.NORMA_SECTOR
                join s in Context.SERVICIO on ns.IdSectorServicio equals s.IdSectorServicio
                join fs in Context.FORMATO_SERVICIO on s.IdServicio equals fs.IdServicio into servicioFormato
                select new
                {
                    // Select the number of Servicio within the Formato_Servicio     
                    count = servicioFormato.Count()
                };

            if (count > 0)
                return true;
            else
                return false;
        }



        #endregion
    }
}
