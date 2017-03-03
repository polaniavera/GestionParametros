using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        #endregion
    }
}
