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
        /// method to get many record on the idEstado is Activate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<FORMATO> GetMany()
        {
            var formatosAct = Context.FORMATO.Where(c => c.IdEstado == 1);
            return formatosAct;
        }

        /// <summary>
        /// Retrieve if exist a norma in formato entity by IdNorma
        /// </summary>
        /// <param name="normaId"></param>
        /// <returns></returns>
        public bool ExistNorma(int normaId)
        {
            int count = 0;
            count = Context.FORMATO.Count(c => c.IdNorma == normaId);
            
            if(count>0)
                return true;
            else
                return false;
        }



        #endregion
    }
}
