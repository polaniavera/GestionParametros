using DataModel;
using System;

namespace BusinessEntities
{
    public class TablaValorEntity
    {
        public int IdTablaValor { get; set; }
        public int IdCampo { get; set; }
        public string Valor { get; set; }

        public virtual TABLA_CAMPO TABLA_CAMPO { get; set; }
    }
}
