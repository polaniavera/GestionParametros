using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataModel.GenericRepository
{
    public class FormatoRepositoryCustom
    {
        #region Private member variables...
        internal WebApiDbEntities Context;
        internal DbSet<FORMATO> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public FormatoRepositoryCustom(WebApiDbEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<FORMATO>();
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// generic method to get many record on the idEstado is Activate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<FORMATO> GetMany()
        {
            var formatosAct = Context.FORMATO.Where(c => c.IdEstado == 1);
            return formatosAct;
        }



        #endregion
    }
}
