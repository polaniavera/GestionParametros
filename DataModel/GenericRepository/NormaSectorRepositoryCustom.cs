using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataModel.GenericRepository
{
    public class NormaSectorRepositoryCustom
    {
        #region Private member variables...
        internal WebApiDbEntities Context;
        internal DbSet<NORMA_SECTOR> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public NormaSectorRepositoryCustom(WebApiDbEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<NORMA_SECTOR>();
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// generic method to get many record link with the idNorma and is Activate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<NORMA_SECTOR> GetManyByIdNorma(int idNorma)
        {
            var normaSectorAct = Context.NORMA_SECTOR.Where(c => c.IdEstado == 1 && c.IdNorma==idNorma).ToList();
            return normaSectorAct;
        }



        #endregion
    }
}
