using DataModel;
using System;

namespace BusinessEntities
{
    public class TablaValorEntity
    {
        public int IdTablaValor { get; set; }
        public int IdTablaRegistro { get; set; }
        public int IdCampo { get; set; }
        public string ValorAlfanumerico { get; set; }
        public Nullable<int> ValorEntero { get; set; }
        public Nullable<System.DateTime> ValorFecha { get; set; }
        public Nullable<bool> ValorBoleano { get; set; }
        public Nullable<decimal> ValorDinero { get; set; }
        public Nullable<decimal> ValorTasa { get; set; }

        public TABLA_CAMPO TABLA_CAMPO { get; set; }
        public TABLA_REGISTRO TABLA_REGISTRO { get; set; }
    }
}
