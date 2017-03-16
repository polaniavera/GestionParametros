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
        /// Method to get TipoNorma record on vertical tables from join resources.
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
                select new {cv.IdTablaValor, cv.ValorAlfanumerico}).ToList();

            for (int i=0; i< results.Count; i++)
            {
                if (i%2==0)
                    arrCodigo.Add(results[i].ValorAlfanumerico);
                else
                {
                    tablaValor = new TABLA_VALOR();
                    arrValor.Add(results[i].ValorAlfanumerico);
                    tablaValor.IdTablaValor = results[i].IdTablaValor;
                    tablaValor.ValorAlfanumerico = results[i].ValorAlfanumerico;
                    tablaValorList.Add(tablaValor);
                }    
            }

            return tablaValorList;
        }

        /// <summary>
        /// Method to get TipoFormato record on vertical tables from join resources.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TABLA_VALOR> GetTipoFormato()
        {
            List<String> arrCodigo = new List<String>();
            List<String> arrValor = new List<String>();
            List<TABLA_VALOR> tablaValorList = new List<TABLA_VALOR>();
            TABLA_VALOR tablaValor = new TABLA_VALOR();

            var results =
                (from c in Context.TABLA
                 join cc in Context.TABLA_CAMPO on c.IdTabla equals cc.IdTabla
                 join cv in Context.TABLA_VALOR on cc.IdCampo equals cv.IdCampo
                 where c.Codigo == "TIPOFORMATO"
                 select new { cv.IdTablaValor, cv.ValorAlfanumerico }).ToList();

            for (int i = 0; i < results.Count; i++)
            {
                if (i % 2 == 0)
                    arrCodigo.Add(results[i].ValorAlfanumerico);
                else
                {
                    tablaValor = new TABLA_VALOR();
                    arrValor.Add(results[i].ValorAlfanumerico);
                    tablaValor.IdTablaValor = results[i].IdTablaValor;
                    tablaValor.ValorAlfanumerico = results[i].ValorAlfanumerico;
                    tablaValorList.Add(tablaValor);
                }
            }

            return tablaValorList;
        }

        /// <summary>
        /// Method to get parametros record on vertical tables from parametro string.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TABLA_VALOR> GetParametros(string parametro)
        {
            List<String> arrCodigo = new List<String>();
            List<String> arrValor = new List<String>();
            List<TABLA_VALOR> tablaValorList = new List<TABLA_VALOR>();
            TABLA_VALOR tablaValor = new TABLA_VALOR();

            var results =
                (from c in Context.TABLA
                 join cc in Context.TABLA_CAMPO on c.IdTabla equals cc.IdTabla
                 join cv in Context.TABLA_VALOR on cc.IdCampo equals cv.IdCampo
                 where c.Codigo == parametro
                 select new { cv.IdTablaValor, cv.ValorAlfanumerico }).ToList();

            for (int i = 0; i < results.Count; i++)
            {
                if (i % 2 == 0)
                    arrCodigo.Add(results[i].ValorAlfanumerico);
                else
                {
                    tablaValor = new TABLA_VALOR();
                    arrValor.Add(results[i].ValorAlfanumerico);
                    tablaValor.IdTablaValor = results[i].IdTablaValor;
                    tablaValor.ValorAlfanumerico = results[i].ValorAlfanumerico;
                    tablaValorList.Add(tablaValor);
                }
            }

            return tablaValorList;
        }

        #endregion
    }
}
