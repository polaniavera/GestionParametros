using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataModel.GenericRepository
{
    public class NormaRepositoryCustom
    {
        #region Private member variables...
        internal WebApiDbEntities Context;
        internal DbSet<NORMA> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public NormaRepositoryCustom(WebApiDbEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<NORMA>();
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// generic method to get many record on the idEstado is Activate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<NORMA> GetMany()
        {
            var normasAct = Context.NORMA.Where(c=>c.IdEstado==1);
            return normasAct;
        }

        /// <summary>
        /// Retrieve if exist a servicio in formato_servicio entity by IdSector
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool ExistServicio(int sectorId)
        {
            var sectoresByNorma =
               (from ns in Context.NORMA_SECTOR
                join ss in Context.SECTOR on ns.IdSector equals ss.IdSector
                join s in Context.SERVICIO on ss.IdSector equals s.IdSector
                join fs in Context.FORMATO_SERVICIO on s.IdServicio equals fs.IdServicio
                where ns.IdNormaSector == sectorId
                select new
                {
                    sector = ns
                }).ToList();

            if (sectoresByNorma.Count > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
