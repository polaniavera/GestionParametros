using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.GenericRepository
{
    public class TablaRepositoryCustom
    {
        #region Private member variables...
        internal WebApiDbEntities Context;
        internal DbSet<TABLA> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public TablaRepositoryCustom(WebApiDbEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TABLA>();
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// generic method to get many record on the idEstado is Activate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TABLA_VALOR> GetTipoNorma()
        {
            List<String> arrCodigo = new List<String>();
            List<String> arrValor = new List<String>();
            List<TABLA_VALOR> tablaValorList = new List<TABLA_VALOR>();
            TABLA_VALOR tablaValor = new TABLA_VALOR();

            var results = 
                (from c in Context.TABLA
                join cc in Context.TABLA_CAMPO on c.IdTabla equals cc.IdTabla
                join cv in Context.TABLA_VALOR on cc.IdCampo equals cv.IdCampo
                where c.Codigo == "TIPONORMA"
                select new {cv.IdTablaValor, cv.Valor}).ToList();

            for (int i=0; i< results.Count; i++)
            {
                if (i%2==0)
                    arrCodigo.Add(results[i].Valor);
                else
                {
                    tablaValor = new TABLA_VALOR();
                    arrValor.Add(results[i].Valor);
                    tablaValor.IdTablaValor = results[i].IdTablaValor;
                    tablaValor.Valor = results[i].Valor;
                    tablaValorList.Add(tablaValor);
                }    
            }

            return tablaValorList;
        }
        #endregion
    }
}
